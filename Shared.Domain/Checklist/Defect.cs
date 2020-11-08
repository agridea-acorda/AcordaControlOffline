using System;
using System.Collections.Generic;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public class Defect : ValueObject
    {
        public static Defect None => new Defect(0, "", "");
        
        public Defect(int id, string name, string conjunctElementCode)
        {
            bool IsEmpty() => id == 0 && 
                              string.IsNullOrWhiteSpace(name) &&
                              string.IsNullOrWhiteSpace(conjunctElementCode); 
            
            if (!IsEmpty() && id <= 0)
                throw new ArgumentOutOfRangeException($"{nameof(id)} must be positive.");

            if (!IsEmpty() && string.IsNullOrWhiteSpace(ConjunctElementCode))
                throw new ArgumentNullException($"{nameof(conjunctElementCode)} cannot be empty.");

            if (!IsEmpty() && string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException($"{nameof(name)} cannot be empty.");

            Id = id;
            Name = name;
            ConjunctElementCode = conjunctElementCode;
        }

        public int Id { get; }
        public string Name { get; }
        public string ConjunctElementCode { get; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ConjunctElementCode;
        }
    }
}