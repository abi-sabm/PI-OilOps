using System.ComponentModel.DataAnnotations;

namespace OilOps.Models;

public class LoginUser
{
    [Key]
    public string UserName { get; set; }
    public string Password { get; set; }
}