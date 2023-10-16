using ApiTransactions.DTOs;
using ApiTransactions.Mappers;
using BusinessDomain;
using BusinessDomain.Contracts;
using CommonDataModels.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ApiTransactions.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    //[Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly ITransactionDomain _checkingDomain;
        public TransactionController(ILogger<TransactionController> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _checkingDomain = serviceProvider.GetServices<ITransactionDomain>()?.FirstOrDefault(x=> x.GetType() == typeof(CheckingAccountDomain));
        }

        /// <summary>
        /// Retira dinero de cuenta
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        [HttpPost, Route(nameof(TransactionController.GenericTransactionCall))]
        [ProducesResponseType(typeof(ExceptionLib.Response), StatusCodes.Status500InternalServerError)]
        public async Task<dynamic> GenericTransactionCall(TransactionClientDTO transaction)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                .Select(e => e.ErrorMessage)
                                .ToList();

                    return ExceptionLib.Response.WithError(string.Join("; ", errors));
                }

                if(transaction.TypeTransaction == RecordsTypeTransactions.WithDrawalRecord.Code)
                    return await _checkingDomain.WithDrawal(TransactionsMapper.FromClientTransacToDomain(transaction));

                if (transaction.TypeTransaction == RecordsTypeTransactions.DepositRecord.Code)
                    return await _checkingDomain.Deposit(TransactionsMapper.FromClientTransacToDomain(transaction));

                if (transaction.TypeTransaction == RecordsTypeTransactions.FundsTransferRecord.Code)
                    return await _checkingDomain.FundsTransfer(TransactionsMapper.FromClientTransacToDomain(transaction));

                return BadRequest();

            }catch(Exception ex)
            {
                return ExceptionLib.Response.WithErrorException(ex);
            }
        }
    }
}