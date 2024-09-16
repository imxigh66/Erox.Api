namespace Erox.Application.Models
{
	public class TopClientDto
	{
		public Guid UserId { get; set; }
		public int TotalOrders { get; set; }
		public decimal TotalSum { get; set; }
	}
}
