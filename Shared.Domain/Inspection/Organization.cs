using System;
using System.Collections.Generic;
using System.Text;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection
{
    public sealed class Organization : ValueObject
    {
        public string Name { get; }
        public string Address { get; }
        public string CantonCode { get; }

        public Organization(string name, string address, string cantonCode)
        {
            Name = name;
            Address = address;
            CantonCode = cantonCode;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }

        public bool IsCantonalOrgForCanton(string cantonCode, bool? forceOverride = null)
        {
            if (forceOverride.HasValue)
                return forceOverride.Value;

            return cantonCode == CantonCode;
        }

        public static Organization Agripige = new Organization("AGRI-PIGE", "Rue des Sablières 15, 1242 Satigny", "GE");
        public static Organization Anapi = new Organization("ANAPI", "Aurore 4, 2053 Cernier", "NE");
        public static Organization Ajapi = new Organization("AJAPI", "CP 125, 2582 Courtételle", "JU");
        public static Organization Cobra = new Organization("CoBrA", "CP 1080, 1001 Lausanne", "VD");
    }
}
