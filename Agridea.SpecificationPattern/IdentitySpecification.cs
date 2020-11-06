using System;
using System.Linq.Expressions;

namespace Agridea.SpecificationPattern {
    internal sealed class IdentitySpecification<T> : Specification<T>
    {
        #region Services

        public override Expression<Func<T, bool>> ToExpression()
        {
            return x => true;
        }

        #endregion
    }
}