using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OilOpsFront.Models;

public class ServiceModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ID auto incremental
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Status { get; set; }
    public decimal HourlyRate { get; set; }
}

// Status
// True False