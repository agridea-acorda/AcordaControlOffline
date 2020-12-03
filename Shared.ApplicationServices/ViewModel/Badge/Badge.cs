namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.Badge
{
    public class Badge : ViewModel, IBadge
    {
        public string Category { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
    }
}