
namespace CommonDataModels.DataModels
{
    public class NoveltyTransaction
    {
        public string Id { get; set; }

        /// <summary>
        /// Id tipo transaccion
        /// </summary>
        public int IdTypeTransaction { get; set; }

        /// <summary>
        /// Id producto origen
        /// </summary>
        public string? IdOriginProduct { get; set; }

        /// <summary>
        /// Id producto destino
        /// </summary>
        public string? IdDestinyProduct { get; set; }

        /// <summary>
        /// Valor explicito en transaccion
        /// </summary>
        public Double? ExplicitValue { get; set; }

        /// <summary>
        /// Fecha de registro
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Fecha cuando se hizo efectiva
        /// </summary>
        public DateTime? EffectDate { get; set; }

        /// <summary>
        /// Identificacion de quien solicitó la transaccion
        /// </summary>
        public string UserOrclientId {  get; set; }
    }
}
