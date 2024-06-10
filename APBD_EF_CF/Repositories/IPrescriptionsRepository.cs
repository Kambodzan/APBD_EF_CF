using APBD_EF_CF.Entities;
using APBD_EF_CF.Models.DTOs;

namespace APBD_EF_CF.Repositories;

public interface IPrescriptionsRepository
{
    public Task<int> AddPrescriptionToPatient(PatientPrescriptionDTO patientPrescriptionDto,
        CancellationToken cancellationToken);
    
}