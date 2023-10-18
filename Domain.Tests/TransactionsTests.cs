using BusinessDomain;
using BusinessDomain.Contracts;
using BusinessDomain.DTOs;
using Domain.Tests.Utils;

namespace Domain.Tests
{
    [TestFixture]
    public class TransactionsTests
    {
        private ITransactionDomain _checkingAccountDomain;

        public TransactionsTests()
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
            //prepare
            TransactionReqDTO transactionReqDTO = new TransactionReqDTO()
            {
                Value = 0,
                ClientUserName = "oscalp",
                DestinyProductNumber = "178-02-123",
                EffectDate = DateTime.UtcNow,
                TypeTransaction = "ConsignaCod",
                UserOrClient = "unittests"
            };

            //act
            var result = await _checkingAccountDomain.Deposit(transactionReqDTO);
            var resultDTO = new ExceptionLib.ResultDto(result);

            //assert
            Assert.AreEqual("Transacciones requieren valores mayores a 0", resultDTO.error);
        }

        [Test]
        public async Task Deposit_WhenValueIsMinorZero()
        {
            //prepare
            TransactionReqDTO transactionReqDTO = new TransactionReqDTO()
            {
                Value = -0.1,
                ClientUserName = "oscalp",
                DestinyProductNumber = "178-02-123",
                EffectDate = DateTime.UtcNow,
                TypeTransaction = "ConsignaCod",
                UserOrClient = "unittests"
            };

            //act
            var result = await _checkingAccountDomain.Deposit(transactionReqDTO);
            var resultDTO = new ExceptionLib.ResultDto(result);

            //assert
            Assert.AreEqual("Transacciones requieren valores mayores a 0", resultDTO.error);
        }

        [Test]
        public async Task Deposit_WhenValueIsToBig()
        {
            //prepare
            TransactionReqDTO transactionReqDTO = new TransactionReqDTO()
            {
                Value = 9999999999999,
                ClientUserName = "oscalp",
                DestinyProductNumber = "178-02-123",
                EffectDate = DateTime.UtcNow,
                TypeTransaction = "ConsignaCod",
                UserOrClient = "unittests"
            };

            //act
            var result = await _checkingAccountDomain.Deposit(transactionReqDTO);
            var resultDTO = new ExceptionLib.ResultDto(result);

            //assert
            Assert.AreEqual("El valor solicitado no está permitido por este medio", resultDTO.error);
        }

        [Test]
        public async Task Withdrawal_WhenValueIsSuperiorFunds()
        {
            //prepare
            TransactionReqDTO transactionReqDTO = new TransactionReqDTO()
            {
                Value = 99999999,
                ClientUserName = "oscalp",
                OriginProductNumber = "178-02-123",
                EffectDate = DateTime.UtcNow,
                TypeTransaction = "RetiroCod",
                UserOrClient = "unittests"
            };

            //act
            var result = await _checkingAccountDomain.WithDrawal(transactionReqDTO);
            var resultDTO = new ExceptionLib.ResultDto(result);

            //assert
            Assert.AreEqual("No tiene fondos suficientes",resultDTO.error);
        }
    }
}