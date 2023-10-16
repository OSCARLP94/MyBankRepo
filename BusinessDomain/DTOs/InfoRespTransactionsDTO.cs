
namespace BusinessDomain.DTOs
{
    public class InfoRespTransactionsDTO
    {
        /// <summary>
        /// Datos de tipo de transaccion
        /// </summary>
        public TypeTransactionDTO TypeTransaction { get; set; }

        /// <summary>
        /// fecha de creacion
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// fecha efectiva
        /// </summary>
        public DateTime? EffectDate { get; set; }

        /// <summary>
        /// producto origen
        /// </summary>
        public string OriginProductNumber { get; set; }

        /// <summary>
        /// producto destino
        /// </summary>
        public string DestinyProductNumber { get; set; }

        /// <summary>
        /// Usuario o cliente que generó
        /// </summary>
        public string UserOrClient { get; set; }

        /// <summary>
        /// Valor implicito en transaccion
        /// </summary>
        public double? ExplicitValue { get; set; }

        /// <summary>
        /// detalle de la transaccion
        /// </summary>
        public List<DetailTransactionDTO> DetailsTransaction { get; set; }
    }
}
