using System;
using System.Collections.Generic;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain {
    public class CodeNameValueObject : ValueObject
    {
        public CodeNameValueObject(int code, string name)
        {
            ValidateCtorParams(code, name);
            Code = code;
            Name = name;
        }
        public int Code { get; }
        public string Name { get; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
        }

        protected virtual void ValidateCtorParams(int code, string name)
        {
            if (code < 0)
                throw new ArgumentOutOfRangeException(nameof(Code), $"{nameof(Code)} must be >= 0.");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentOutOfRangeException(nameof(Name), $"{nameof(Name)} must be non-empty.");
        }
    }
}