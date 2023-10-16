
namespace BusinessDomain.DTOs
{
    public class ProductRespDTO
    {
        /// <summary>
        /// Id de cliente asociado
        /// </summary>
        public string ClientUserName { get; set; }

        /// <summary>
        /// Numero unico producto
        /// </summary>
        public string ProductNumber { get; set; }

        /// <summary>
        /// Id de tipo de producto
        /// </summary>
        public TypeProductDTO TypeProduct { get; set; }

        /// <summary>
        /// Dinero asociado a producto
        /// </summary>
        public MoneyAccountDTO MoneyAccount { get; set; }

        /// <summary>
        /// Fecha de creacion
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Estado actual en el sistema
        /// </summary>
        public TypeStatusDTO TypeStatus { get; set; }
    }
}
