
namespace CommonDataModels.DataModels
{
    public class NoveltyTransactionDetail
    {
        public string Id {  get; set; }

        /// <summary>
        /// Id Novedad transaccion asociada
        /// </summary>
        public string IdNoveltyTransaction { get; set; }

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
