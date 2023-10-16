using CommonDataModels.DataModels;

namespace BusinessDomain.Contracts
{
    internal interface INoveltyTransactionDomain
    {
        Task<NoveltyTransaction> RegisterNoveltyTransactionOnTransac(NoveltyTransaction novelty
            , string? beforeValue =null, string? newValue = null, string? additionals = null);
    }
}
