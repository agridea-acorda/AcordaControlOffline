using System;
using System.Collections.Generic;
using System.Text;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Pdf.Model
{
    public class FarmModel
    {
        #region Properties

        public string Address { get; set; }
        public string CompleteName { get; set; }
        public string Email { get; set; }
        public string Ktidb { get; set; }
        
        #endregion

        public static FarmModel FromDomain(Farm.Farm f)
        {
            return new FarmModel
            {
                Ktidb = f.Ktidb,
                CompleteName = f.FarmName,
                Email = f.Email,
                Address = f.Address
            };
        }
    }
}
