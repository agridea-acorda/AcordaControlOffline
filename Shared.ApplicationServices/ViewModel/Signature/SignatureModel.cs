﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.Signature
{
    public class SignatureModel
    {
        public string Signatory { get; set; }
        public string Proxy { get; set; }
        public bool ShowProxy { get; set; }
        public string Data { get; set; }
        public string DataUrl { get; set; }
        public bool HasSigned => !string.IsNullOrWhiteSpace(Data);
        public bool HasProxy { get; set; }
        public DateTime? DoneOn { get; set; }
        public bool ShowDoneOn { get; set; }
        public int? DoneInTown_Id { get; set; }
        public List<Town.Town> Town = new List<Town.Town>();

        public static SignatureModel FromDomain(Domain.Inspection.Signature signature)
        {
            return new SignatureModel
            {
                Signatory = signature.Signatory,
                Proxy = signature.Proxy,
                HasProxy = signature.HasProxy,
                DoneInTown_Id = signature.DoneInTown_Id,
                Data = signature.Data,
                DataUrl = signature.DataUrl
            };
        }
    }
}
