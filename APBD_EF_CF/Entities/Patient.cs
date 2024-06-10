using System.ComponentModel.DataAnnotations;

namespace APBD_EF_CF.Entities;

public class Patient
{
    [Key] public int IdPatient { get; set; }

    [MaxLength(100)]
    public string FirstName { get; set; }
    [MaxLength(100)]
    public string LastName { get; set; }
    public DateTime Birthday { get; set; }

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}