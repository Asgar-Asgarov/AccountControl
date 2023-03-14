using Microsoft.AspNetCore.Identity;
namespace WebApplication1.Models;

public class AppUser:IdentityUser
{
 public string Fullname { get; set; }   
 public List<Sales> Sales { get; set; }
}