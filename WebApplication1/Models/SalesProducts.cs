namespace WebApplication1.Models;

public class SalesProducts
{
    public int Id { get; set; }
    public string SalesId { get; set; }
    public Sales sales {get; set;}
    public Product Product {get; set;}
    public string ProductId { get; set; }

   public double Price { get; set; }   
   public int Count { get; set; }

}