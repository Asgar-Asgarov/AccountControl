using WebApplication1.Models.Footer;

namespace WebApplication1.ViewModels
{
    public class FooterViewModel
    {
        public List<Archive>? Archives { get; set; }
        public List<Company>? Companies { get; set; }
        public List<CustomerService>? CustomerServices { get; set; }
        public List<SocialMedia>? SocialMedias { get; set; }
    }
}
