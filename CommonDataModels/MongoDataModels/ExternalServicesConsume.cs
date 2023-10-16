namespace OpheliaSuiteV2.MasterTransversal.Domain.MongoEntities
{
    public class ExternalServicesConsume
    {
        public ExternalServicesConsume() 
        {
            this.IdReg = Guid.NewGuid().ToString();
            this.RegisterDate = DateTime.Now;
        }
        public string IdReg { get; set; }

        /// <summary>
        /// Fecha de registro
        /// </summary>
        public DateTime RegisterDate { get; set; }

        /// <summary>
        /// Url consumida
        /// </summary>
        public string UrlExternalService { get; set; }

        /// <summary>
        /// Data enviada
        /// </summary>
        public string DataSended { get; set; }

        /// <summary>
        /// Fecha de respuesta
        /// </summary>
        public DateTime? ResponseDate { get; set; }

        /// <summary>
        /// Respuesta obtenida
        /// </summary>
        public string Response { get; set; }

        /// <summary>
        /// Error generado
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Data de respuesta serializada
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Nombre archivo
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Referencia hacia otro proceso
        /// </summary>
        public string IdReference { get; set; }

        /// <summary>
        /// Tipo de proceso referenciado
        /// </summary>
        public string Reference { get; set; }
    }
}
