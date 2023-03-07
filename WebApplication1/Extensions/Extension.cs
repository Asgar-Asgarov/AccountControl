using WebApplication1.ViewModels;

namespace WebApplication1.Extensions
{
    public static class Extension
    {
        public static bool IsImage(this IFormFile file) 
        {
            return file.ContentType.Contains("image");
        }
        public static bool CheckImageSize(this IFormFile file, int size) 
        {
            return file.Length/1024 > size;
        }
        public static string SaveImage(this IFormFile file,IWebHostEnvironment webHostEnvironment, string root, string filename) 
        {
             filename = Guid.NewGuid().ToString() + file.FileName;
            string fullPath = Path.Combine(webHostEnvironment.WebRootPath, root, filename);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {

                file.CopyTo(stream);
            }
            return filename;
        }
    }
}
