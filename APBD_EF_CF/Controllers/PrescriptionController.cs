using APBD_EF_CF.Models.DTOs;
using APBD_EF_CF.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APBD_EF_CF.Controllers;

[Route("api/")]
[ApiController]
public class PrescriptionController : ControllerBase
{
    private IPrescriptionsRepository _prescriptionsRepository;

    public PrescriptionController(IPrescriptionsRepository prescriptionsRepository)
    {
        _prescriptionsRepository = prescriptionsRepository;
    }

    [HttpPost("add_prescription")]
    public async Task<IActionResult> AddPrescriptionToPatient(PatientPrescriptionDTO patientPrescriptionDto,
        CancellationToken cancellationToken)
    {
        var result = await _prescriptionsRepository.AddPrescriptionToPatient(patientPrescriptionDto, cancellationToken);

        if (result == 1)
        {
            return NotFound("Meds per prescription limit is exceeded");
        }

        if (result == 2)
        {
            return NotFound("Provided meds does not exist");
        }

        if (result == 3)
        {
            return NotFound("Invalid dates");
        }

        return Ok();
    }
}