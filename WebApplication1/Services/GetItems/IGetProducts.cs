using WebApplication1.Models;

namespace WebApplication1.Services.GetItems
{
    public interface IGetProducts
    {
        List<Product> GetProductsFromDataBase();
    }
}
