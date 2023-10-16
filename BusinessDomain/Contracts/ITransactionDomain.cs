using BusinessDomain.DTOs;

namespace BusinessDomain.Contracts
{
    public interface ITransactionDomain
    {
        /// <summary>
        /// Retirar dinero de cuenta propia
        /// </summary>
        /// <param name="transactionInfo"></param>
        /// <returns></returns>
        Task<dynamic> WithDrawal(TransactionReqDTO transactionInfo);

        /// <summary>
        /// Deposito de dinero a cuenta propia
        /// </summary>
        /// <param name="transactionInfo"></param>
        /// <returns></returns>
        Task<dynamic> Deposit(TransactionReqDTO transactionInfo);

        /// <summary>
        /// Transferencia de dinero a otra cuenta
        /// </summary>
        /// <param name="transactionInfo"></param>
        /// <returns></returns>
        Task<dynamic> FundsTransfer(TransactionReqDTO transactionInfo);
    }
}
