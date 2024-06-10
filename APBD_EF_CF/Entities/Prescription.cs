using System.ComponentModel.DataAnnotations;

namespace APBD_EF_CF.Entities;

public class Prescription
{
    [Key]
    public int IdPrescription { get; set; }
    
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    
    public int IdPatient { get; set; }
    public int IdDoctor { get; set; }

    public virtual Doctor Doctor { get; set; }
    public virtual Patient Patient { get; set; }

    public ICollection<Prescription_Medicament> PrescriptionMedicaments { get; set; } =
        new List<Prescription_Medicament>();
}