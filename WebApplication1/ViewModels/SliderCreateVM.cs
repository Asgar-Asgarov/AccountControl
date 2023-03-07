using Microsoft.Build.Framework;

namespace WebApplication1.ViewModels
{
    public class SliderCreateVM
    {
        [Required]
       public IFormFile? Photo { get; set; }
    }
}
