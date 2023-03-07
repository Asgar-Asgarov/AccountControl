using Microsoft.Build.Framework;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class ProductUpdateVM
    {
        public string? Name { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        
        public List<ProductImage>? ProductImages { get; set; }
        public IFormFile[]? Photos { get; set; }
    }
}
