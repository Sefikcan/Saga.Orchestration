namespace Shipment.Services.DTO.Response
{
    public class CreateShipmentResponseModel
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public bool IsSend { get; set; } //Entegrasyon varmış gibi düşünülerek yapıldı (ara kargo durumları yok gibi düşün!)
    }
}
