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
        public bool HasProxy => !string.IsNullOrWhiteSpace(Proxy);

        public static SignatureModel FromDomain(Domain.Inspection.Signature signature)
        {
            return new SignatureModel
            {
                Signatory = signature.Signatory,
                Proxy = signature.Proxy,
                Data = signature.Data,
                DataUrl = signature.DataUrl
            };
        }
    }
}
