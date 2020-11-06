using System;
using System.Linq.Expressions;
using Agridea.SpecificationPattern.Expressions;

namespace Agridea.SpecificationPattern {
    internal sealed class AndSpecification<T> : Specification<T>
    {
        #region Members

        private readonly Specification<T> left_;
        private readonly Specification<T> right_;

        #endregion

        #region Initialization

        public AndSpecification(Specification<T> left, Specification<T> right)
        {
            right_ = right;
            left_ = left;
        }

        #endregion

        #region Services

        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> leftExpression = left_.ToExpression();
            Expression<Func<T, bool>> rightExpression = right_.ToExpression();
            return leftExpression.And(rightExpression);
        }

        #endregion
    }
}