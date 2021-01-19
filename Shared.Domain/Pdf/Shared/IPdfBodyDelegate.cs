using System;
using System.Collections.Generic;
using System.Text;
using iTextSharp.text;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Pdf.Shared
{

    /// <summary>
    /// Fulfilled by a Pdf report maker to define the body of a Pdf document
    /// </summary>
    public interface IPdfBodyDelegate
    {
        void AddBody(Document document);
    }
    
}
