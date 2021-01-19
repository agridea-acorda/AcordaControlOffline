using System;
using System.Collections.Generic;
using System.Text;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Pdf.Model.Rubric
{
    public interface IIndexedRenderer
    {
        #region Services

        string Render(int index);

        #endregion
    }
}
