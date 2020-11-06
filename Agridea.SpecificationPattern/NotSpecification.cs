using System;
using System.Linq;
using System.Linq.Expressions;

namespace Agridea.SpecificationPattern {
    internal sealed class NotSpecification<T> : Specification<T>
    {
        #region Members

        private readonly Specification<T> specification_;

        #endregion

        #region Initialization

        public NotSpecification(Specification<T> specification)
        {
            specification_ = specification;
        }

        #endregion

        #region Services

        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> expression = specification_.ToExpression();
            UnaryExpression notExpression = Expression.Not(expression.Body);
            return Expression.Lambda<Func<T, bool>>(notExpression, expression.Parameters.Single());
        }

        #endregion
    }
}