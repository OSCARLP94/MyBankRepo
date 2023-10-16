
namespace CommonDataModels.MongoDataModels
{
    public class TransactionLog
    {
        public TransactionLog() {
            this.IdReg = Guid.NewGuid().ToString();
            this.RegisterDate = DateTime.UtcNow;
        }
        public string IdReg { get;set;}
        public DateTime RegisterDate { get;set;}

        public string RequestObject { get; set; }

        public string RequestResponse { get; set; }
    }
}
