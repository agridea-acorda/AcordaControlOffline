using System;
using System.Collections.Generic;
using System.Text;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization.Farm
{
    class FarmDeserializationDto
    {
        public class Root
        {
            public long Id { get; set; }
            public string Ktidb { get; set; }
            public string FarmName { get; set; }
            public string Address { get; set; }
            public int TownZip { get; set; }
            public string FarmType { get; set; }
            public int FarmTypeCode { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string AgriculturalArea { get; set; }
            public string NonAgriculturalArea { get; set; }
            public string BovineStandardUnits { get; set; }
            public string BovineStandardUnitsFromBdta { get; set; }
            public Badge[] Badges { get; set; }
        }

        public class Badge
        {
            public string Category { get; set; }
            public string Name { get; set; }
            public string Title { get; set; }
        }
    }
}
