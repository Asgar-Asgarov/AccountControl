namespace WebApplication1.Models.Demo
{
    public class BookImage
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public int BookId { get; set; }

        public Book? Book { get; set; }
    }
}
