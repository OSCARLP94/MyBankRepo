
namespace CommonDataModels.DataModels
{
    public class Client
    {
        public string Id { get; set; }

        /// <summary>
        /// Id persona asociada
        /// </summary>
        public string IdPerson { get; set; }

        /// <summary>
        /// Nombre usuario
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Fecha de afiliacion
        /// </summary>
        public DateTime AffiliationDate { get; set; }

        /// <summary>
        /// Estado actual en el sistema
        /// </summary>
        public int IdCurrentStatus { get; set; }

    }
}
