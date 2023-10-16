using System.ComponentModel.DataAnnotations;

namespace BusinessDomain.DTOs
{
    public class InfoReqTransactionsDTO
    {
        [Required]
        public string[] TypeTransactions { get; set; }

        /// <summary>
        /// user del cliente
        /// </summary>
        [Required]
        public string ClientUserName {  get; set; }

        /// <summary>
        /// identificacion producto
        /// </summary>
        [Required]
        public string ProductNumber {  get; set; }

        /// <summary>
        /// Cantidad maxima requerida
        /// </summary>
        [Required]
        public int MaxCount { get; set; }

        /// <summary>
        /// Desde que fecha de creacion
        /// </summary>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// Hasta que fecha de creacion
        /// </summary>
        public DateTime? UntilDate { get; set; }
    }
}
