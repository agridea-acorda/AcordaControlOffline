using System;
using System.Collections.Generic;
using System.Text;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Farm
{
    public class Farm: AggregateRoot
    {
        public string Ktidb { get; set; }
        public string FarmName { get; set; }
        public string Address { get; set; }
        public string FarmType { get; set; }
        public int FarmTypeCode { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AgriculturalArea { get; set; }
        public string NonAgriculturalArea { get; set; }
        public string BovineStandardUnits { get; set; }
        public string BovineStandardUnitsFromBdta { get; set; }
        public IReadOnlyList<Badge> Badges { get; set; }

        public Farm(long id) : base(id) { }
    }

    public class Badge : ValueObject
    {
        public static Badge Empty => new Badge { Category = "", Name = "", Title = "" };
        public string Category { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Category;
            yield return Name;
            yield return Title;
        }
    }
}
