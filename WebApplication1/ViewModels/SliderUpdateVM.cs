using System.ComponentModel;

namespace WebApplication1.ViewModels
{
    public class SliderUpdateVM
    {
        public string ImageUrl { get; set; }
        //[DisplayName("Name")]
        public IFormFile? Photo { get; set; }
    }
}
