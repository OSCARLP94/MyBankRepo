using BusinessDomain.DomainResources;
using BusinessDomain.DTOs;
using CommonDataModels.Enums;
using FluentValidation;

namespace BusinessDomain.FluentValidations
{
    internal class TransactionReqDTOValidator : AbstractValidator<TransactionReqDTO>
    {
        internal TransactionReqDTOValidator() {
            RuleFor(x => x.ClientUserName).NotEmpty();
            RuleFor(x => x.TypeTransaction).NotEmpty();
            RuleFor(x => x.EffectDate).Must(EffectDateMayorCurrentDate).WithMessage(Resource.EffectDateMayorCurrentDate);
            RuleFor(x => x.OriginProductNumber).NotEmpty().When(y => y.TypeTransaction == RecordsTypeTransactions.WithDrawalRecord.Code || y.TypeTransaction==RecordsTypeTransactions.FundsTransferRecord.Code).WithMessage(Resource.TransactionRequireOriginProduct);
            RuleFor(x => x.DestinyProductNumber).NotEmpty().When(y => y.TypeTransaction == RecordsTypeTransactions.DepositRecord.Code || y.TypeTransaction == RecordsTypeTransactions.FundsTransferRecord.Code).WithMessage(Resource.TransactionRequireDestinyProduct);
            RuleFor(x => x.OriginProductNumber).NotEqual(x=> x.DestinyProductNumber).When(y => y.TypeTransaction == RecordsTypeTransactions.FundsTransferRecord.Code).WithMessage(Resource.NotOriginAndDestinyEquals);
            RuleFor(x => x.DestinyProductNumber).NotEqual(x => x.OriginProductNumber).When(y => y.TypeTransaction == RecordsTypeTransactions.FundsTransferRecord.Code).WithMessage(Resource.NotOriginAndDestinyEquals);
            RuleFor(x => x.CauseTransaction).MaximumLength(200);
            RuleFor(x => x.Adittional).MaximumLength(200);
            RuleFor(x => x.Value).GreaterThan(0).WithMessage(Resource.ZeroNotAllowTransaction);
        }

        private bool EffectDateMayorCurrentDate(DateTime? effecDate)
        {
            if (!effecDate.HasValue)
                return true;

            return effecDate.Value.Date >= DateTime.Now.Date;
        }


    }
}
