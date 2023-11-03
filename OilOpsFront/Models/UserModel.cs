using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OilOpsFront.Models;

public class UserModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ID auto incremental
    public int Id { get; set; }
    public string FullName { get; set; }
    public int DNI { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public int UserType { get; set; }
}

// UserType:
// 1. Admin
// 2. Visit