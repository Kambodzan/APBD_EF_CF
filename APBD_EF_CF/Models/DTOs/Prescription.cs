namespace APBD_EF_CF.Models.DTOs;

public class Prescription
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public PatientDTO Patient { get; set; }
    // public int IdPrescription { get; set; }
}