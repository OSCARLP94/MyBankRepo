
namespace BusinessDomain.DTOs
{
    public class DetailTransactionDTO
    {
        /// <summary>
        /// Valor anterior
        /// </summary>
        public string BeforeValue { get; set; }

        /// <summary>
        /// Nuevo valor
        /// </summary>
        public string AfterValue { get; set; }

        /// <summary>
        /// Informacion adicional
        /// </summary>
        public string Additionals { get; set; }
    }
}
