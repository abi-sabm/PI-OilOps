namespace OilOps.DTO;

public class WorkDTO
{
    public DateTime Date { get; set; }
    public int HoursService { get; set; }
    public decimal HourlyRate { get; set; }
    public decimal Value { get; set; }
    public int IdProject { get; set; } 
    public int IdService { get; set; }  
}