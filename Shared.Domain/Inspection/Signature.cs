﻿using System;
using System.Collections.Generic;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection {
    public class Signature : ValueObject
    {
        public string Signatory { get; }
        public string Proxy { get; }
        public string Data { get; }
        public string DataUrl { get; }
        public bool HasProxy { get; }
        public int? DoneInTown_Id { get; }
        public bool HasSigned => !string.IsNullOrWhiteSpace(Data);
        
        public static Signature None => new Signature("", "", false, null, "", ""); 
        
        public Signature(string signatory, string proxy, bool hasProxy, int? doneInTown_Id, string data, string dataUrl)
        {
            bool IsEmpty() =>
                string.IsNullOrWhiteSpace(signatory) &&
                string.IsNullOrWhiteSpace(proxy) &&
                string.IsNullOrWhiteSpace(data) &&
                string.IsNullOrWhiteSpace(dataUrl);

            if (!IsEmpty() && string.IsNullOrWhiteSpace(data))
                throw new ArgumentNullException(nameof(data), "Signature drawing data must be non-empty.");

            if (!IsEmpty() && string.IsNullOrWhiteSpace(dataUrl))
                throw new ArgumentNullException(nameof(dataUrl), "Signature image must be non-empty.");

            if (!IsEmpty() && string.IsNullOrWhiteSpace(signatory))
                throw new ArgumentNullException(nameof(signatory), $"{nameof(signatory)} must be non-empty.");

            Signatory = signatory;
            Proxy = proxy;
            HasProxy = hasProxy;
            DoneInTown_Id = doneInTown_Id;
            Data = data;
            DataUrl = dataUrl;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Signatory;
            yield return Proxy;
            yield return HasProxy;
            yield return Data;
        }
    }
}