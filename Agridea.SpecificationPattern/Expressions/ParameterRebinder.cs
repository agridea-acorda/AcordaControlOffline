using System.Collections.Generic;
using System.Linq.Expressions;

namespace Agridea.SpecificationPattern.Expressions
{
    public class ParameterRebinder : ExpressionVisitor
    {
        #region Members

        private readonly Dictionary<ParameterExpression, ParameterExpression> map_;

        #endregion

        #region Initialization

        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            map_ = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        #endregion

        #region Services

        protected override Expression VisitParameter(ParameterExpression p)
        {
            if (map_.TryGetValue(p, out var replacement))
            {
                p = replacement;
            }

            return base.VisitParameter(p);
        }

        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }

        #endregion
    }
}