using Microsoft.EntityFrameworkCore;

namespace APBD_EF_CF.Entities;

public class PrescriptionsDBContext : DbContext
{
    public virtual DbSet<Doctor> Doctors { get; set; }
    public virtual DbSet<Patient> Patients { get; set; }
    public virtual DbSet<Prescription> Prescriptions { get; set; }
    public virtual DbSet<Medicament> Medicaments { get; set; }
    public virtual DbSet<Prescription_Medicament> Prescription_Medicament { get; set; }

    public PrescriptionsDBContext()
    {
    }

    public PrescriptionsDBContext(DbContextOptions<PrescriptionsDBContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(
        "Data Source=localhost, 1433; User=SA; Password=yourStrong(!)Password; Initial Catalog=master; Integrated Security=False;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Prescription_Medicament>().HasKey(pm => new { pm.IdPrescription, pm.IdMedicament });
        base.OnModelCreating(modelBuilder);
    }
}