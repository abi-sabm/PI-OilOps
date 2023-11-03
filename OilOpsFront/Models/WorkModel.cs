using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OilOpsFront.Models;

public class WorkModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ID auto incremental
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int HoursService { get; set; }
    public decimal HourlyRate { get; set; }
    public decimal Value { get; set; }
    public int IdProject { get; set; } // Ref a tabla Project
    public int IdService { get; set; }  // Ref a tabla Service
    
    [ForeignKey("IdProject")]
    public virtual ProjectModel ProjectModel { get; set; }
    [ForeignKey("IdService")]
    public virtual ServiceModel ServiceModel { get; set; }
}