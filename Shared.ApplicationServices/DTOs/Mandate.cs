namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.DTOs
{
    public class Mandate
    {
        public Farm Farm { get; set; }
        public Badge[] Badges { get; set; }
    }

    public class Farm
    {
        public string Ktidb { get; set; }
        public string FarmName { get; set; }
        public string Address { get; set; }
        public string FarmType { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class Badge
    {
        public string Category { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
    }
}
