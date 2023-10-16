
namespace BusinessDomain.DTOs
{
    public class TransactionRespDTO
    {
        /// <summary>
        /// id transaccion
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// tipo de transaccion
        /// </summary>
        public string TypeTransaction { get; set; }

        /// <summary>
        /// numero producto origen
        /// </summary>
        public string OriginProductNumber { get; set; }

        /// <summary>
        /// producto destino
        /// </summary>
        public string DestinyProductNumber { get; set; }

        /// <summary>
        /// fecha creacion
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// fecha en que se hizo efectiva
        /// </summary>
        public DateTime? EffectDate { get; set;}

        /// <summary>
        /// valo explicito
        /// </summary>
        public double? ExplicitValue { get; set; }

        /// <summary>
        /// adicional
        /// </summary>
        public string Additional {  get; set; }

        /// <summary>
        /// state
        /// </summary>

        public string State { get; set; }
    }
}
