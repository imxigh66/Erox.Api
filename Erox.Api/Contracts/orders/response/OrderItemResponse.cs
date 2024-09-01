namespace Erox.Api.Contracts.orders.response
{
    public class OrderItemResponse
    {
        public Guid OrderItemId { get; set; }

        public Guid OrderId { get; set; }
       

        public Guid ProductId { get; set; }
  
        public Guid SizeId { get; set; }
        public int Quantity { get; set; }
    }
}
