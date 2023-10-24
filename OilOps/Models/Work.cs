namespace OilOps.Models;

public class Work
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public int IdProject { get; set; }
    public int IdServices { get; set; } 
    public int HoursService { get; set; }
    public decimal HourlyRate { get; set; }
    public decimal Value { get; set; }
}