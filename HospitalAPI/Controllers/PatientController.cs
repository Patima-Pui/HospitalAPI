using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HospitalAPI.Services;

namespace HospitalAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpPost]
        [Route("PatientInfo")]
        public PatientModelList GetPatientList()
        {
            PatientModelList result = _patientService.SelectPatients();
             return result;
        }
    }
  
}