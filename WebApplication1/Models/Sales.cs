
namespace WebApplication1.Models;

public class Sales
{
    public int Id { get; set; }
    public double TotalPrice { get; set; }
    public string AppUserId { get; set; }
    public DateTime CreatedDate { get; set; }
}