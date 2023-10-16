using BusinessDomain.Contracts;
using CommonDataModels.DataModels;
using DataAccess.Repositories.Contracts;

namespace BusinessDomain
{
    internal class NoveltyTransactionDomain : INoveltyTransactionDomain
    {
        private readonly IRepository<NoveltyTransaction> _noveltyTransactionRepo;
        private readonly IRepository<NoveltyTransactionDetail> _noveltyTransactionDetailRepo;

        internal NoveltyTransactionDomain(IRepository<NoveltyTransaction> noveltyTransactionRepo,
             IRepository<NoveltyTransactionDetail> noveltyTransactionDetailRepo) { 
            _noveltyTransactionDetailRepo = noveltyTransactionDetailRepo;
            _noveltyTransactionRepo = noveltyTransactionRepo;
        }

        public async Task<NoveltyTransaction> RegisterNoveltyTransactionOnTransac(NoveltyTransaction novelty, string? beforeValue = null, string? newValue = null, string? additionals = null)
        {
            try
            {
                novelty.Id = string.IsNullOrEmpty(novelty.Id)? Guid.NewGuid().ToString() : novelty.Id;

                await _noveltyTransactionRepo.CreateOnTransac(novelty);

                //registramos detalles
                if(!string.IsNullOrEmpty(beforeValue) || !string.IsNullOrEmpty(newValue) || !string.IsNullOrEmpty(additionals))
                {
                    NoveltyTransactionDetail noveltyTransactionDetail = new();
                    noveltyTransactionDetail.Id = Guid.NewGuid().ToString();
                    noveltyTransactionDetail.IdNoveltyTransaction = novelty.Id;
                    noveltyTransactionDetail.AfterValue = newValue;
                    noveltyTransactionDetail.BeforeValue = beforeValue;
                    noveltyTransactionDetail.Additionals = newValue;

                    await _noveltyTransactionDetailRepo.CreateOnTransac(noveltyTransactionDetail);
                }

                return novelty;
            }catch
            {
                throw;
            }
        }
    }
}
