using BusinessDomain.DTOs;

namespace BusinessDomain.Contracts
{
    public interface IInformationDomain
    {
        /// <summary>
        /// Obtiene lista de productos asociados
        /// </summary>
        /// <returns></returns>
        Task<dynamic> GetProductsByClient(string idClient);

        /// <summary>
        /// Obtiene producto especifico de cliente
        /// </summary>
        /// <param name="idClient"></param>
        /// <param name="idProduct"></param>
        /// <returns></returns>
        Task<dynamic> GetProductClient(string idClient, string idProduct);

        /// <summary>
        /// Obtiene transacciones segun valores de busqueda
        /// </summary>
        /// <param name="infoReqTransactionDTO"></param>
        /// <returns></returns>
        Task<dynamic> GetTransactionsClient(InfoReqTransactionsDTO infoReqTransactionDTO);
    }
}
