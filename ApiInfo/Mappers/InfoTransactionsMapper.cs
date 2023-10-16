using ApiInfo.DTOs;
using BusinessDomain.DTOs;

namespace ApiInfo.Mappers
{
    internal static class InfoTransactionsMapper
    {
        internal static InfoReqTransactionsDTO FromInfoTransacReqClientToInfoReqDomain(ReqTransactionsClientDTO reqClient)
        {
            return new InfoReqTransactionsDTO()
            {
                MaxCount = reqClient.MaxCount,
                FromDate = reqClient.FromDate,
                UntilDate = reqClient.UntilDate,
                ClientUserName = reqClient.ClientUserName,
                ProductNumber = reqClient.ProductNumber,
                TypeTransactions = reqClient.TypeTransactions
            };
        }
    }
}
