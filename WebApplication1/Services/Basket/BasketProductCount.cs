using Azure.Core;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using WebApplication1.ViewModels;

namespace WebApplication1.Services.Basket
{
    public class BasketProductCount : IBasketProductCount
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public BasketProductCount(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public int CalculateProductCount()
        {
            string basket = _contextAccessor.HttpContext.Request.Cookies["basket"];
            List<BasketVM> products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            return products.Sum(p => p.BasketCount);
        }
    }
}
