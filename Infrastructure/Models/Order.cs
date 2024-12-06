namespace Infrastructure.Models;

public class Order
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now; 
}