using APBD_EF_CF.Entities;
using APBD_EF_CF.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Prescription = APBD_EF_CF.Entities.Prescription;

namespace APBD_EF_CF.Repositories;

public class PrescriptionsRepository : IPrescriptionsRepository
{
    private readonly PrescriptionsDBContext _context = new();

    public async Task<int> AddPrescriptionToPatient(PatientPrescriptionDTO patientPrescriptionDto,
        CancellationToken cancellationToken)
    {
        if (await DoesPatientExists(patientPrescriptionDto.PatientDto, cancellationToken) == false)
        {
            var query = await _context.Patients.AddAsync(new Patient
            {
                FirstName = patientPrescriptionDto.PatientDto.FirstName,
                LastName = patientPrescriptionDto.PatientDto.LastName,
                Birthday = patientPrescriptionDto.PatientDto.Birthday
            }, cancellationToken);
        }

        if (DoPrescriptionLimitIsWrong(patientPrescriptionDto.MedicamentDtos) == true)
        {
            return 1;
        }

        if (await DoAllMedsExists(patientPrescriptionDto.MedicamentDtos, cancellationToken) == false)
        {
            return 2;
        }

        if (CheckDates(patientPrescriptionDto) == false)
        {
            return 3;
        }

        int testValue = 1;
        Console.WriteLine(testValue);

        var prescription = new Prescription
        {
            Date = patientPrescriptionDto.Date,
            DueDate = patientPrescriptionDto.DueDate,
            IdPatient = patientPrescriptionDto.PatientDto.IdPatient,
            IdDoctor = testValue
        };

        await _context.Prescriptions.AddAsync(prescription, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        var idPrescription = prescription.IdPrescription;

        foreach (var medicament in patientPrescriptionDto.MedicamentDtos)
        {
            await _context.Prescription_Medicament.AddAsync(new Prescription_Medicament
            {
                IdMedicament = medicament.IdMedicament,
                IdPrescription = idPrescription,
                Details = medicament.Description,
            }, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }

        return 0;
    }

    private async Task<bool> DoesPatientExists(PatientDTO patientDto, CancellationToken cancellationToken)
    {
        var query = await _context.Patients.Where(p =>
                p.IdPatient == patientDto.IdPatient && p.FirstName == patientDto.FirstName &&
                p.LastName == patientDto.LastName && p.Birthday == patientDto.Birthday)
            .FirstOrDefaultAsync(cancellationToken);

        return query != null;
    }

    private bool DoPrescriptionLimitIsWrong(ICollection<MedicamentDTO> medicamentDtos)
    {
        return medicamentDtos.Count > 10;
    }

    private async Task<bool> DoAllMedsExists(ICollection<MedicamentDTO> medicamentDtos,
        CancellationToken cancellationToken)
    {
        var medicamentsIds = medicamentDtos.Select(m => m.IdMedicament).ToList();

        var checker = await _context.Medicaments.Where(m => medicamentsIds.Contains(m.IdMedicament))
            .CountAsync(cancellationToken);

        return checker == medicamentsIds.Count();
    }

    private bool CheckDates(PatientPrescriptionDTO patientPrescriptionDto)
    {
        return patientPrescriptionDto.DueDate > patientPrescriptionDto.Date;
    }
}