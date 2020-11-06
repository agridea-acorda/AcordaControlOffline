using System;
using System.Linq.Expressions;

namespace Agridea.SpecificationPattern
{
    public abstract class Specification<T>
    {
        #region Constants

        public static readonly Specification<T> All = new IdentitySpecification<T>();

        #endregion

        private Expression<Func<T, bool>> expressionCache_;
        private Func<T, bool> predicateCache_;

        #region Services

        public abstract Expression<Func<T, bool>> ToExpression();

        public bool IsSatisfiedBy(T entity)
        {
            if (expressionCache_ == null)
                expressionCache_ = ToExpression();

            if (predicateCache_ == null)
                predicateCache_ = expressionCache_.Compile();

            return predicateCache_(entity);
        }

        public Specification<T> And(Specification<T> specification)
        {
            if (this == All)
                return specification;
            if (specification == All)
                return this;

            return new AndSpecification<T>(this, specification);
        }

        public Specification<T> Or(Specification<T> specification)
        {
            if (this == All || specification == All)
                return All;

            return new OrSpecification<T>(this, specification);
        }

        public Specification<T> Not()
        {
            return new NotSpecification<T>(this);
        }

        #endregion
    }
}