using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OilOps.Models;

public class Project
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ID auto incremental
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public int Status { get; set; }
}

// Status: 
// 1. Pendiente
// 2. Confirmado 
// 3. Terminado