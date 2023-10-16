
namespace CommonDataModels.DataModels
{
    public class Product
    {
        public string Id { get; set; }

        /// <summary>
        /// Id de cliente asociado
        /// </summary>
        public string IdClient { get; set; }

        /// <summary>
        /// Id de tipo de producto
        /// </summary>
        public int IdTypeProduct { get; set; }

        /// <summary>
        /// Numero identificacion porducto
        /// </summary>
        public string ProductNumber { get; set; }

        /// <summary>
        /// Fecha de creacion
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Estado actual en el sistema
        /// </summary>
        public int IdCurrentStatus { get; set; }
    }
}
