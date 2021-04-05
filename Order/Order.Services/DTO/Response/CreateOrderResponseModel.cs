namespace Order.Services.DTO.Response
{
    public class CreateOrderResponseModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public int OrderStatus { get; set; }
    }
}
