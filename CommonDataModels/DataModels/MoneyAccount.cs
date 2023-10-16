
namespace CommonDataModels.DataModels
{
    public class MoneyAccount :ICloneable
    {
        public string Id { get; set; }

        /// <summary>
        /// Id producto asociado
        /// </summary>
        public string IdProduct { get; set; }

        /// <summary>
        /// Balance actual
        /// </summary>
        public double CurrentBalance { get; set; }

        /// <summary>
        /// Ultima fecha modificacion balance
        /// </summary>
        public DateTime LastUpdateBalance { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
