namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.Farm
{
    public interface IFarmBase
    {
        int Id { get; set; }
        string Ktidb { get; set; }
        string FarmName { get; set; }
        string PersonName { get; set; }
        string Address { get; set; }
        string FarmType { get; set; }
        int FarmTypeCode { get; set; }
        string Email { get; set; }
        string PhoneNumber { get; set; }
        int? TvdNumber { get; set; }
    }
}