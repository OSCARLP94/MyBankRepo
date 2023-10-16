using System.ComponentModel.DataAnnotations;

namespace ApiTransactions.DTOs
{
    public class TransactionClientDTO
    {
        /// <summary>
        /// tipo de transaccion
        /// </summary>
        [Required]
        public string TypeTransaction {  get; set; }

        /// <summary>
        /// Id del cliente asociado
        /// </summary>
        [Required]
        public string ClientUserName { get; set; }

        /// <summary>
        /// Id del producto origen
        /// </summary>
        public string? OriginProductNumber { get; set; }

        /// <summary>
        /// Id del producto destino
        /// </summary>
        public string? DestinyProductNumber { get; set; }

        /// <summary>
        /// Fecha en que se requiere se haga efectiva
        /// </summary>
        [Required]
        public DateTime EffectDate { get; set; }

        /// <summary>
        /// Valor asociado a transaccion
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Usuario o cliente
        /// </summary>
        public string? UserOrClient { get; set; }

        /// <summary>
        /// Causa de transaccion (opcional)
        /// </summary>
        public string? CauseTransaction { get; set; }

        /// <summary>
        /// Detalles adicionales (opcional)
        /// </summary>
        public string? Adittional { get; set; }
    }
}
