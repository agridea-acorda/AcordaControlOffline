using System;
using System.Collections.Generic;
using Agridea.DomainDrivenDesign;
using JetBrains.Annotations;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public class DefectAction
    {
        public virtual InspectionOrganisation ResponsibleAuthority { get; set; }
        public virtual string Comment { get; set; }
        public virtual Money ReductionInCHF { get; set; }
        public virtual ReductionPoints ReductionInPoints { get; set; }
        public virtual Reduction PredefinedReduction { get; set; }
        public virtual string OtherActionDescription { get; set; }
        public virtual int? OtherActionValue { get; set; }
        public virtual DefectActionType OtherActionType { get; set; }
        
    }

    public class Reduction : ValueObject
    {
        public virtual Money Money { get; set; }

        public virtual ReductionPoints Points { get; set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }

    public class DefectActionType : CodeNameValueObject
    {
        public DefectActionType(int code, string name) : base(code, name) { }
    }

    public class InspectionOrganisation : ValueObject
    {
        public InspectionOrganisation(string code, string name)
        { 
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentNullException(nameof(code));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            Code = code;
            Name = name;
        }
        public string Code { get; }
        public string Name { get; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
        }
    }

    public class ReductionPoints : ValueObject
    { 
        public static ReductionPoints None => new ReductionPoints(Decimal.Zero, "");
        public decimal Points { get; }
        public string Unit { get; }
        public ReductionPoints(decimal points, string unit)
        {
            bool IsEmpty() => points == Decimal.Zero && string.IsNullOrWhiteSpace(unit);
            
            if (!IsEmpty() && points < 0)
                throw new ArgumentOutOfRangeException($"{nameof(points)} must be positive.");
            
            if (!IsEmpty() && string.IsNullOrWhiteSpace(unit))
                throw new ArgumentNullException(nameof(unit));

            Points = points;
            Unit = unit;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Points;
            yield return Unit;
        }
    }

    public class Money : ValueObject
    {
        public static Money ZeroCHF => new Money(Currency.CHF, Decimal.Zero);
        public decimal Amount { get; }
        public Currency Currency { get; }
        public Money(Currency currency, decimal amount)
        { 
            if (amount < 0)
                throw new ArgumentOutOfRangeException($"{nameof(amount)} must be positive or zero.");

            Currency = currency;
            Amount = amount;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Currency;
            yield return Amount;
        }
    }

    public enum Currency
    {
        CHF,
        EUR
    }
}