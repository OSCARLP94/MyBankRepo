using ApiTransactions.DTOs;
using BusinessDomain.DTOs;

namespace ApiTransactions.Mappers
{
    public static class TransactionsMapper
    {
        /// <summary>
        /// Convierte DTO Transaction client a DTO transaction Domain
        /// </summary>
        /// <param name="transactionClientDTO"></param>
        /// <returns></returns>
        public static TransactionReqDTO FromClientTransacToDomain(TransactionClientDTO transactionClientDTO)
        {
            return new TransactionReqDTO() { 
                TypeTransaction = transactionClientDTO.TypeTransaction,
                ClientUserName = transactionClientDTO.ClientUserName,
                OriginProductNumber = transactionClientDTO.OriginProductNumber,
                DestinyProductNumber = transactionClientDTO.DestinyProductNumber,
                EffectDate = transactionClientDTO.EffectDate,
                Value = transactionClientDTO.Value,
                UserOrClient = transactionClientDTO.UserOrClient,
                CauseTransaction = transactionClientDTO.CauseTransaction,
                Adittional=transactionClientDTO.Adittional            
            };
        }
    }
}
