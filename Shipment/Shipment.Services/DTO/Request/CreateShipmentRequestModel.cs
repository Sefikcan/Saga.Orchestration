namespace Shipment.Services.DTO.Request
{
    public class CreateShipmentRequestModel
    {
        public int OrderId { get; set; }

        public int ShipmentType { get; set; }
    }
}
