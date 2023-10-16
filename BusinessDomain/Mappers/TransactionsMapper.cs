using BusinessDomain.DTOs;
using CommonDataModels.DataModels;

namespace BusinessDomain.Mappers
{
    internal static class TransactionsMapper
    {
        /// <summary>
        /// Convierte clases de modelo transaccion a DTO
        /// </summary>
        /// <param name="noveltyTransaction"></param>
        /// <param name="noveltyTransactionDetails"></param>
        /// <returns></returns>
        internal static InfoRespTransactionsDTO FromNoveltyTransacToTransactRespDTO(NoveltyTransaction noveltyTransaction, 
            List<NoveltyTransactionDetail> noveltyTransactionDetails, TypeTransaction typeTransaction, Product originProduct=null, Product destinyProduct=null)
        {
            List<DetailTransactionDTO> transactionsDetail = new();

            if(noveltyTransactionDetails.Any())
                foreach (var item in noveltyTransactionDetails)
                    transactionsDetail.Add(new DetailTransactionDTO { 
                        BeforeValue = item.BeforeValue,
                        AfterValue = item.AfterValue,
                        Additionals = item.Additionals
                    });

            return new InfoRespTransactionsDTO()
            {
                OriginProductNumber = originProduct !=null ? originProduct.ProductNumber: "",
                DestinyProductNumber = destinyProduct!=null ? destinyProduct.ProductNumber : "",
                CreationDate = noveltyTransaction.CreationDate,
                EffectDate = noveltyTransaction.EffectDate,
                UserOrClient = noveltyTransaction.UserOrclientId,
                ExplicitValue = noveltyTransaction.ExplicitValue,
                TypeTransaction = new TypeTransactionDTO()
                {
                    Code = typeTransaction.Code,
                    Description = typeTransaction.Description,
                    Name = typeTransaction.Name,
                    IsEnabled= typeTransaction.IsEnabled
                },
                DetailsTransaction = transactionsDetail
            };
        }

        internal static TransactionRespDTO FromTransacReqToTransacResp(TransactionReqDTO transactionReqDTO, NoveltyTransaction noveltyTransaction, string state)
        {
            return new TransactionRespDTO()
            {
                Id = noveltyTransaction.Id,
                TypeTransaction = transactionReqDTO.TypeTransaction,
                OriginProductNumber = transactionReqDTO.OriginProductNumber,
                DestinyProductNumber = transactionReqDTO.DestinyProductNumber,
                CreationDate = noveltyTransaction.CreationDate,
                EffectDate = noveltyTransaction.EffectDate,
                Additional = transactionReqDTO.Adittional,
                ExplicitValue = noveltyTransaction.ExplicitValue,
                State = state
            };
        }
    }
}
