using System.ComponentModel.DataAnnotations;

namespace APBD_EF_CF.Entities;

public class Medicament
{
    [Key]
    public int IdMedicament { get; set; }
    
    [MaxLength(100)]
    public string Name { get; set; }
    [MaxLength(100)]
    public string Description { get; set; }
    [MaxLength(100)]
    public string Type { get; set; }

    public ICollection<Prescription_Medicament> PrescriptionMedicaments { get; set; } =
        new List<Prescription_Medicament>();


}