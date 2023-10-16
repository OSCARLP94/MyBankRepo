using BusinessDomain;
using BusinessDomain.Contracts;
using BusinessDomain.DTOs;
using Domain.Tests.Utils;

namespace Domain.Tests
{
    [TestFixture]
    public class Tests
    {
        private ITransactionDomain _checkingAccountDomain;

        public Tests()
        {
            
            _checkingAccountDomain = MyServiceProvider.GetRequiredService<ITransactionDomain>() ?? throw new ArgumentNullException(nameof(CheckingAccountDomain));

        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Deposit_WhenValueIsZero()
        {
            TransactionReqDTO transactionReqDTO = new TransactionReqDTO()
            {
                Value = 0,
                ClientUserName = "oscalp",
                DestinyProductNumber = "178-02-123",
                EffectDate = DateTime.UtcNow,
                TypeTransaction = "ConsignaCod"
            };

            var result = await _checkingAccountDomain.Deposit(transactionReqDTO);
            Console.WriteLine(result);
            Assert.Pass(result, "Hello");
        }
    }
}