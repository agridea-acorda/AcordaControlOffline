namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateList
{
    public class Mandate: ViewModel
    {
        public Farm.Farm Farm { get; set; }
        public Inspection[] Inspections { get; set; }
        public string SyncStatus { get; set; }

        public Mandate()
        {
            SyncStatus = ApplicationServices.ViewModel.SyncStatus.Unknown;
        }
    }
}
