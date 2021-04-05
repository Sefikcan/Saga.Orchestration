namespace Order.Services.DTO.Request
{
    public class CreateOrderRequestModel
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice
        {
            get
            {
                return Quantity * 100; // Ürün servisi yazılmadı default ürün fiyatı 100 tl
            }
        }
    }
}
