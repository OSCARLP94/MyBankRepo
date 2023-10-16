using BusinessDomain.Contracts;
using BusinessDomain.DomainResources;
using BusinessDomain.DTOs;
using BusinessDomain.FluentValidations;
using BusinessDomain.Mappers;
using CommonDataModels.DataModels;
using CommonDataModels.Enums;
using CommonDataModels.MongoDataModels;
using DataAccess.Repositories.Contracts;
using Newtonsoft.Json;

namespace BusinessDomain
{
    /// <summary>
    /// Dominio de las cuentas corrientes
    /// </summary>
    public class CheckingAccountDomain : ITransactionDomain, IInformationDomain
    {
        private const double MIN_WITDTHDRAWAL_VALUE = 10000;

        #region Fields
        private readonly IRepository<Product> _productRepo;
        private readonly IRepository<TypeProduct> _typeProductRepo;
        private readonly IRepository<TypeStatus> _typeStatusRepo;
        private readonly IRepository<MoneyAccount> _moneyAccountRepo;
        private readonly IRepository<TypeTransaction> _typeTransactionRepo;
        private readonly IRepository<Client> _clientRepo;
        private readonly IRepository<NoveltyTransaction> _noveltyTransactionRepo;
        private readonly IRepository<NoveltyTransactionDetail> _noveltyTransactionDetailRepo;
        private readonly INoveltyTransactionDomain _noveltyTransactionDomainRepo;
        private readonly IMongoRepository<TransactionLog> _transactionLogRepo;
        #endregion

        public CheckingAccountDomain(IRepository<Product> productRepo, IRepository<TypeProduct> typeProductRepo,
            IRepository<TypeStatus> typeStatusRepo, IRepository<MoneyAccount> moneyAccountRepo, IRepository<Client> clientRepo,
            IRepository<NoveltyTransaction> noveltyTransactionRepo, IRepository<TypeTransaction> typeTransactionRepo,
            IRepository<NoveltyTransactionDetail> noveltyTransactionDetailRepo, IMongoRepository<TransactionLog> transactionLogRepo) { 
            _productRepo = productRepo;
            _typeProductRepo = typeProductRepo;
            _typeStatusRepo = typeStatusRepo;
            _moneyAccountRepo = moneyAccountRepo;
            _clientRepo = clientRepo;
            _noveltyTransactionRepo = noveltyTransactionRepo;
            _typeTransactionRepo = typeTransactionRepo;
            _transactionLogRepo = transactionLogRepo;
            _noveltyTransactionDetailRepo = noveltyTransactionDetailRepo;
            _noveltyTransactionDomainRepo = new NoveltyTransactionDomain(noveltyTransactionRepo, noveltyTransactionDetailRepo);
        }

        #region Public methods
        public async Task<dynamic> Deposit(TransactionReqDTO transactionInfo)
        {
            try
            {
                #region Validaciones fluent
                TransactionReqDTOValidator validatorTransac = new TransactionReqDTOValidator();

                var resultsValidation = validatorTransac.Validate(transactionInfo);
                if (!resultsValidation.IsValid)
                    return ExceptionLib.Response.WithError(resultsValidation.ToString(";"));

                #endregion

                #region validaciones negocio
                //validar existencia cliente
                var clients = await _clientRepo.Find(x => x.UserName == transactionInfo.ClientUserName);

                if (clients == null || !clients.Any())
                    return ExceptionLib.Response.WithError(Resource.ClientNotExists);

                var client = clients[0];

                //validar existencia producto origen
                var products = await _productRepo.Find(x => x.IdClient == client.Id && x.ProductNumber == transactionInfo.DestinyProductNumber);

                if (products == null || !products.Any())
                    return ExceptionLib.Response.WithError(string.Format(Resource.ProductNotExists, transactionInfo.DestinyProductNumber));

                //validar valor deposito con respecto a fondo
                var moneyAccounts = await _moneyAccountRepo.Find(x => x.IdProduct == products[0].Id);
                if (moneyAccounts == null || !moneyAccounts.Any())
                    return ExceptionLib.Response.WithError(Resource.NotEnoughtFunds);

                var moneyAccount = moneyAccounts[0];
                #endregion


                #region Transacciones data
                //registramos transaccion primero en mongo
                TransactionLog transactionLog = new TransactionLog();
                transactionLog.RequestObject = JsonConvert.SerializeObject(transactionInfo).ToString();
                await _transactionLogRepo.InsertRecord(transactionLog);

                DateTime dateCreateTransaction = DateTime.Now;

                //actualizamos dinero 
                var cloneMoneyAccount = (MoneyAccount)moneyAccount.Clone();
                moneyAccount.CurrentBalance += transactionInfo.Value;
                moneyAccount.LastUpdateBalance = dateCreateTransaction;

                await _moneyAccountRepo.UpdateOnTransac(moneyAccount);

                //registramos novedad
                NoveltyTransaction noveltyTransaction = new NoveltyTransaction()
                {
                    CreationDate = dateCreateTransaction,
                    EffectDate = dateCreateTransaction,
                    IdDestinyProduct = products[0].Id,
                    IdTypeTransaction = (int)RecordsTypeTransactions.DepositRecord.IdEnum,
                    UserOrclientId = transactionInfo.UserOrClient,
                    ExplicitValue = transactionInfo.Value
                };

                var noveltyResp = await _noveltyTransactionDomainRepo.RegisterNoveltyTransactionOnTransac(noveltyTransaction, cloneMoneyAccount.CurrentBalance.ToString()
                    , moneyAccount.CurrentBalance.ToString(), transactionInfo.Adittional);

                //commit cambios
                await _moneyAccountRepo.SaveDbChangesAsync();
                #endregion

                #region Acciones/integraciones adicionales
                #endregion

                return ExceptionLib.Response.Successful(TransactionsMapper.FromTransacReqToTransacResp(transactionInfo, noveltyResp, Resource.TransactionSuccess));
            }
            catch (Exception ex)
            {
                return ExceptionLib.Response.WithErrorException(ex);
            }
        }

        public async Task<dynamic> FundsTransfer(TransactionReqDTO transactionInfo)
        {
            try
            {
                #region Validaciones fluent
                TransactionReqDTOValidator validatorTransac = new TransactionReqDTOValidator();

                var resultsValidation = validatorTransac.Validate(transactionInfo);
                if (!resultsValidation.IsValid)
                    return ExceptionLib.Response.WithError(resultsValidation.ToString(";"));

                #endregion

                #region validaciones negocio
                //validar existencia cliente
                var clients = await _clientRepo.Find(x => x.UserName == transactionInfo.ClientUserName);

                if (clients == null || !clients.Any())
                    return ExceptionLib.Response.WithError(Resource.ClientNotExists);

                var client = clients[0];

                //validar existencia producto origen
                var productsOrigin = await _productRepo.Find(x => x.IdClient == client.Id && x.ProductNumber == transactionInfo.OriginProductNumber);

                if (productsOrigin == null || !productsOrigin.Any())
                    return ExceptionLib.Response.WithError(string.Format(Resource.ProductNotExists, transactionInfo.OriginProductNumber));

                //validar existencia producto destino
                var productsDestiny = await _productRepo.Find(x => x.ProductNumber == transactionInfo.DestinyProductNumber);

                if (productsDestiny == null || !productsDestiny.Any())
                    return ExceptionLib.Response.WithError(string.Format(Resource.ProductNotExists, transactionInfo.DestinyProductNumber));

                //validar valor transferencia con respecto a fondo origen
                var moneyAccountsOrigin = await _moneyAccountRepo.Find(x => x.IdProduct == productsOrigin[0].Id);
                if (moneyAccountsOrigin == null || !moneyAccountsOrigin.Any())
                    return ExceptionLib.Response.WithError(Resource.NotEnoughtFunds);

                var moneyAccountOrigin = moneyAccountsOrigin[0];
                if (moneyAccountOrigin.CurrentBalance < transactionInfo.Value)
                    return ExceptionLib.Response.WithError(Resource.NotEnoughtFunds);

                //validar valor transferencia con respecto a fondo destino
                var moneyAccountsDestiny = await _moneyAccountRepo.Find(x => x.IdProduct == productsDestiny[0].Id);
                if (moneyAccountsDestiny == null || !moneyAccountsDestiny.Any())
                    return ExceptionLib.Response.WithError(string.Format(Resource.ProductNotEnoughtFunds, productsDestiny[0].ProductNumber));
                
                var moneyAccountDestiny = moneyAccountsDestiny[0];
                #endregion

                #region Transacciones data
                //registramos transaccion primero en mongo
                TransactionLog transactionLog = new TransactionLog();
                transactionLog.RequestObject = JsonConvert.SerializeObject(transactionInfo).ToString();
                await _transactionLogRepo.InsertRecord(transactionLog);

                DateTime dateCreateTransaction = DateTime.Now;

                //actualizamos dinero origen
                var cloneMoneyAccountOrigin = (MoneyAccount)moneyAccountOrigin.Clone();
                moneyAccountOrigin.CurrentBalance -= transactionInfo.Value;
                moneyAccountOrigin.LastUpdateBalance = dateCreateTransaction;

                //actualizamos dinero destino
                var cloneMoneyAccountDestiny = (MoneyAccount)moneyAccountDestiny.Clone();
                moneyAccountDestiny.CurrentBalance += transactionInfo.Value;
                moneyAccountDestiny.LastUpdateBalance = dateCreateTransaction;

                await _moneyAccountRepo.UpdateOnTransac(moneyAccountOrigin);
                await _moneyAccountRepo.UpdateOnTransac(moneyAccountDestiny);

                //registramos novedad
                NoveltyTransaction noveltyTransaction = new NoveltyTransaction()
                {
                    CreationDate = dateCreateTransaction,
                    EffectDate = dateCreateTransaction,
                    IdOriginProduct = productsOrigin[0].Id,
                    IdDestinyProduct = productsDestiny[0].Id,
                    IdTypeTransaction = (int)RecordsTypeTransactions.FundsTransferRecord.IdEnum,
                    UserOrclientId = transactionInfo.UserOrClient,
                    ExplicitValue = transactionInfo.Value
                };

                var noveltyResp = await _noveltyTransactionDomainRepo.RegisterNoveltyTransactionOnTransac(noveltyTransaction, cloneMoneyAccountOrigin.CurrentBalance.ToString()
                    , moneyAccountOrigin.CurrentBalance.ToString(), transactionInfo.Adittional);

                var noveltyRespDestiny = await _noveltyTransactionDomainRepo.RegisterNoveltyTransactionOnTransac(noveltyTransaction, cloneMoneyAccountDestiny.CurrentBalance.ToString()
                  , moneyAccountDestiny.CurrentBalance.ToString(), transactionInfo.Adittional);

                //commit cambios
                await _moneyAccountRepo.SaveDbChangesAsync();
                #endregion

                #region Acciones/integraciones adicionales
                #endregion

                return ExceptionLib.Response.Successful(TransactionsMapper.FromTransacReqToTransacResp(transactionInfo, noveltyResp, Resource.TransactionSuccess));
            }
            catch (Exception ex)
            {
                return ExceptionLib.Response.WithErrorException(ex);
            }
        }

        public async Task<dynamic> WithDrawal(TransactionReqDTO transactionInfo)
        {
            try
            {
                #region Validaciones fluent
                TransactionReqDTOValidator validatorTransac = new TransactionReqDTOValidator();

                var resultsValidation = validatorTransac.Validate(transactionInfo);
                if (!resultsValidation.IsValid)
                    return ExceptionLib.Response.WithError(resultsValidation.ToString(";"));

                #endregion

                #region validaciones negocio
                //validar existencia cliente
                var clients = await _clientRepo.Find(x => x.UserName == transactionInfo.ClientUserName);

                if (clients == null || !clients.Any())
                    return ExceptionLib.Response.WithError(Resource.ClientNotExists);

                var client = clients[0];

                //validar existencia producto origen
                var products = await _productRepo.Find(x => x.IdClient == client.Id && x.ProductNumber == transactionInfo.OriginProductNumber);

                if (products == null || !products.Any())
                    return ExceptionLib.Response.WithError(string.Format(Resource.ProductNotExists, transactionInfo.OriginProductNumber));

                //validar valor retiro con respecto a fondo
                if (transactionInfo.Value < MIN_WITDTHDRAWAL_VALUE)
                    return ExceptionLib.Response.WithError(string.Format(Resource.NotMinValueTransaction, MIN_WITDTHDRAWAL_VALUE));

                var moneyAccounts= await  _moneyAccountRepo.Find(x => x.IdProduct == products[0].Id);
                if (moneyAccounts == null || !moneyAccounts.Any())
                    return ExceptionLib.Response.WithError(Resource.NotEnoughtFunds);

                var moneyAccount = moneyAccounts[0];
                if(moneyAccount.CurrentBalance < transactionInfo.Value)
                    return ExceptionLib.Response.WithError(Resource.NotEnoughtFunds);
                #endregion

                #region Transacciones data
                //registramos transaccion primero en mongo
                TransactionLog transactionLog = new TransactionLog();
                transactionLog.RequestObject = JsonConvert.SerializeObject(transactionInfo).ToString();
                await _transactionLogRepo.InsertRecord(transactionLog);

                DateTime dateCreateTransaction = DateTime.Now;

                //actualizamos dinero 
                var cloneMoneyAccount = (MoneyAccount)moneyAccount.Clone();
                moneyAccount.CurrentBalance -= transactionInfo.Value;
                moneyAccount.LastUpdateBalance = dateCreateTransaction;

                await _moneyAccountRepo.UpdateOnTransac(moneyAccount);

                //registramos novedad
                NoveltyTransaction noveltyTransaction = new NoveltyTransaction() {
                    CreationDate = dateCreateTransaction,
                    EffectDate = dateCreateTransaction,
                    IdOriginProduct = products[0].Id,
                    IdTypeTransaction = (int)RecordsTypeTransactions.WithDrawalRecord.IdEnum,
                    UserOrclientId = transactionInfo.UserOrClient,
                    ExplicitValue = transactionInfo.Value
                };

                var noveltyResp =  await _noveltyTransactionDomainRepo.RegisterNoveltyTransactionOnTransac(noveltyTransaction, cloneMoneyAccount.CurrentBalance.ToString()
                    , moneyAccount.CurrentBalance.ToString(), transactionInfo.Adittional);

                //commit cambios
                await _moneyAccountRepo.SaveDbChangesAsync();
                #endregion

                #region Acciones/integraciones adicionales
                #endregion

                return ExceptionLib.Response.Successful(TransactionsMapper.FromTransacReqToTransacResp(transactionInfo, noveltyResp, Resource.TransactionSuccess));
            }
            catch (Exception ex)
            {
                return ExceptionLib.Response.WithErrorException(ex);
            }
        }

        public Task<dynamic> GetProductClient(string idClient, string idProduct)
        {
            throw new NotImplementedException();
        }

        public async Task<dynamic> GetTransactionsClient(InfoReqTransactionsDTO infoReqTransactionDTO)
        {
            try
            {
                //obtenemos cliente
                var clients = await _clientRepo.Find(x => x.UserName == infoReqTransactionDTO.ClientUserName);

                if (clients == null || !clients.Any())
                    return ExceptionLib.Response.WithError(Resource.ClientNotExists);

                var client = clients[0];

                //obtenemos producto de cliente
                var products = await _productRepo.Find(x => x.IdClient == client.Id && x.ProductNumber == infoReqTransactionDTO.ProductNumber);

                if (products == null || !products.Any())
                    return ExceptionLib.Response.WithError(Resource.NotProductsClient);

                var product = products[0];

                //obtenemos tipos de transacciones
                var transactionsType = await _typeTransactionRepo.Find(x => infoReqTransactionDTO.TypeTransactions.Contains(x.Code));

                if (transactionsType == null || !transactionsType.Any())
                    return ExceptionLib.Response.WithError(Resource.TypeTransactionsNotExists);

                //obtenemos transacciones asociadas al producto segun parametros
                var transactions = await _noveltyTransactionRepo.Find(x => transactionsType.Select(w=> w.Id).Contains(x.IdTypeTransaction)
                                                            && (x.IdOriginProduct == product.Id || x.IdDestinyProduct == product.Id)
                                                            && x.CreationDate.Date >= infoReqTransactionDTO.FromDate.Value.Date && x.CreationDate.Date <= infoReqTransactionDTO.UntilDate.Value.Date);

                //ordenamiento y cantidad maxima
                var transactionsFiltered = transactions.OrderByDescending(x => x.CreationDate).Take(infoReqTransactionDTO.MaxCount);

                List<InfoRespTransactionsDTO> transReqDT = new();

                foreach ( var transaction in transactionsFiltered )
                {
                    //obtenemos tipo transaccion
                    var typeTransaction = await _typeTransactionRepo.Get(transaction.IdTypeTransaction); 

                    //obtenemos detalles asociados
                    var noveltyDetails = await  _noveltyTransactionDetailRepo.Find(x => x.IdNoveltyTransaction == transaction.Id);

                   //obtenemos producto origen
                   var originProduct = transaction.IdOriginProduct == product.Id ? product : await _productRepo.Get(transaction.IdOriginProduct);

                    //obtenemos producto destino
                    var destinyProduct = transaction.IdDestinyProduct == product.Id ? product : await _productRepo.Get(transaction.IdDestinyProduct);

                    transReqDT.Add(TransactionsMapper.FromNoveltyTransacToTransactRespDTO(transaction, noveltyDetails, typeTransaction, originProduct, destinyProduct));
                }

                return ExceptionLib.Response.Successful(transReqDT);
            }
            catch (Exception ex)
            {
                return ExceptionLib.Response.WithErrorException(ex);
            }
        }

        public async Task<dynamic> GetProductsByClient(string clientUserName)
        {
            try
            {
                //obtenemos cliente
                var clients = await _clientRepo.Find(x => x.UserName == clientUserName);

                if (clients == null || !clients.Any())
                    return ExceptionLib.Response.WithError(Resource.ClientNotExists);

                //obtenemos productos de cliente
                var products = await _productRepo.Find(x => x.IdClient == clients.FirstOrDefault().Id);

                if (products == null || !products.Any())
                    return ExceptionLib.Response.WithError(Resource.NotProductsClient);

                var productsDTO = HandleProductMapper(products, clients.FirstOrDefault().UserName);
                return ExceptionLib.Response.Successful(productsDTO);
            }
            catch (Exception ex)
            {
                return ExceptionLib.Response.WithErrorException(ex);
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// metodo para hacer invocacion unica de mapper y dentro del mismo obtener datos faltantes
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        private IEnumerable<ProductRespDTO> HandleProductMapper(List<Product> products, string userName)
        {
            //mapeamos los productos
            foreach (var product in products)
            {
                //obtenemos valor asociado a producto
                var moneyAccounts = Task.Run(async() =>
                {
                    return await _moneyAccountRepo.Find(x=> x.IdProduct==product.Id);
                }).GetAwaiter().GetResult();

                //obtenemos tipo producto
                var typeProduc = Task.Run(async () =>
                {
                    return await _typeProductRepo.Get(product.IdTypeProduct);
                }).GetAwaiter().GetResult(); 

                //obtenemos tipo estado
                var typeStatus = Task.Run(async () =>
                {
                    return await _typeStatusRepo.Get(product.IdCurrentStatus);
                }).GetAwaiter().GetResult(); 

                yield return ProductsMapper.FromProductToProductDTO(product, userName, moneyAccounts?.FirstOrDefault(), typeProduc, typeStatus);
            }
        }
        #endregion
    }
}