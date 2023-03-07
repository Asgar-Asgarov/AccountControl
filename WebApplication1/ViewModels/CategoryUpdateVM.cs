using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels
{
    public class CategoryUpdateVM
    {

        [System.ComponentModel.DataAnnotations.Required,MaxLength(50)]
        public string? Name { get; set; }
        [System.ComponentModel.DataAnnotations.Required,MinLength(5)]
        public string? Description { get; set; }
    }
}
