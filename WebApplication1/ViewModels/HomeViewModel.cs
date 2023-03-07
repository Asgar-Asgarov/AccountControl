using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class HomeViewModel
    {
        public List<Slider>? Sliders { get; set; }

        public SliderDetail? SliderDetail { get; set; }


        public List<Category>? Categories { get; set; }
        public List<Product>? Products { get; set; }
    }
}
