using System.Net;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.Badge
{
    public class Badge : ViewModel, IBadge
    {
        public string Category { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }

        public static Badge FromDomain(Domain.Farm.Badge badge)
        {
            return new Badge
            {
                Category = badge.Category,
                Name = badge.Name,
                Title = badge.Title
            };
        }
    }
}