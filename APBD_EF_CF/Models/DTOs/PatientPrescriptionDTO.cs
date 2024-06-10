namespace APBD_EF_CF.Models.DTOs;

public class PatientPrescriptionDTO
{
    public PatientDTO PatientDto { get; set; }
    public ICollection<MedicamentDTO> MedicamentDtos { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
}