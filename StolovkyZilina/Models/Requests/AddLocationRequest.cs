namespace StolovkyZilina.Models.Requests
{
    public class AddLocationRequest
    {
        public string Name { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string? ShortDesc { get; set; }
        public int Space { get; set; }
        public string? BuildingNumber { get; set; }
        public string? FloorNumber { get; set; }
        public string? DoorNumer { get; set; }
        public string? GoogleMapsLink { get; set; }
    }
}
