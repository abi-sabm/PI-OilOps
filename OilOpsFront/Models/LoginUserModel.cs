using System.ComponentModel.DataAnnotations;

namespace OilOpsFront.Models;

public class LoginUserModel
{
    [Key]
    public string UserName { get; set; }
    public string Password { get; set; }
}