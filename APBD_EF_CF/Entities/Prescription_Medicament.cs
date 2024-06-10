using System.ComponentModel.DataAnnotations;

namespace APBD_EF_CF.Entities;

public class Prescription_Medicament
{
    [Key]
    public int IdMedicament { get; set; }
    [Key]
    public int IdPrescription { get; set; }
    
    public int? Dose { get; set; }
    [MaxLength(100)]
    public string Details { get; set; } 
    
    public virtual Medicament Medicament { get; set; }
    public virtual Prescription Prescription { get; set; }
}