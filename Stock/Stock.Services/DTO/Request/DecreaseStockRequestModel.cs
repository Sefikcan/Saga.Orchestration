namespace Stock.Services.DTO.Request
{
    public class DecreaseStockRequestModel
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
