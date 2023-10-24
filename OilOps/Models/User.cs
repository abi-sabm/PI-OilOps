namespace OilOps.Models;

public class User
{ 
    public int Id { get; set; }
    public string Name { get; set; }
    public int DNI { get; set; }
    public int UserType { get; set; }
    public string Password { get; set; }
}