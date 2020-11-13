using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateList
{
    public class Mandate: ViewModel
    {
        public Farm Farm { get; set; }
        public Badge[] Badges { get; set; }
        public CheckList[] Checklists { get; set; }
        public string SyncStatus { get; set; }

        public Mandate()
        {
            SyncStatus = ApplicationServices.ViewModel.SyncStatus.Unknown;
        }
    }

    public class Farm : ViewModel, IFarm
    {
        public int Id { get; set; }
        public string Ktidb { get; set; }
        public string FarmName { get; set; }
        public string Address { get; set; }
        public string FarmType { get; set; }
        public int FarmTypeCode { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    public interface IFarm
    {
        int Id { get; set; }
        string Ktidb { get; set; }
        string FarmName { get; set; }
        string Address { get; set; }
        string FarmType { get; set; }
        int FarmTypeCode { get; set; }
        string Email { get; set; }
        string PhoneNumber { get; set; }
    }

    public class Badge : ViewModel, IBadge
    {
        public string Category { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
    }

    public interface IBadge
    {
        string Category { get; set; }
        string Name { get; set; }
        string Title { get; set; }
    }

    public class CheckList : ViewModel
    {
        public string Domain { get; set; }
        public string Inspector { get; set; }
        public int Percent { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public InspectionOutcome Outcome { get; set; }
        public bool IsClosed { get; set; }
        public string CloseDate { get; set; }
    }

    public enum InspectionOutcome
    {
        Unset,
        Oui,
        P,
        Non,
        NC,
        NA,
    }
}
