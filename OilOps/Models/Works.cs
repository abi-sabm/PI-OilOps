namespace OilOps.Models;

public class Works
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public int IdProject { get; set; }
    public int IdServices { get; set; }
    public bool Status { get; set; }
}