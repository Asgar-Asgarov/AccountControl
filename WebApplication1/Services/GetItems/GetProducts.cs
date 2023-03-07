using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Services.GetItems
{
    public class GetProducts : IGetProducts
    {
        private readonly AppDbContext _appDbContext;

        public GetProducts(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public List<Product> GetProductsFromDataBase()
        {
           return _appDbContext.Products.Include(p => p.ProductImages).ToList();
        }
    }
}
