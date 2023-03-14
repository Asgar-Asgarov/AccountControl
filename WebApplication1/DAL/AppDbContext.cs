using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Models.Demo;
using WebApplication1.Models.Footer;

namespace WebApplication1.DAL
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderDetail> SliderDetails { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Bio> Bios { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<SalesProducts> SalesProducts { get; set; }


        //Demo
        public DbSet<Book> Books { get; set; }
        public DbSet<BookImage> BookImages { get; set; }
        public DbSet<SocialPage> SocialPages { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }

        //Footer
        public DbSet<Archive> Archives { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CustomerService> CustomerServices { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }



    }
}
