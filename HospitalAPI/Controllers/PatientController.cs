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

        [HttpGet]
        [Route("QueryPatient")]
        public PatientModelList GetPatientList([FromQuery] PatientModelRequest request)
        {
            PatientModelList result = _patientService.SelectPatients(request);
            return result;
        }

        [HttpGet]
        [Route("PatientInfo")]
        public PatientModel GetIndividualPatient([FromQuery] PatientRequestIdModel requestId)
        {
            PatientModel result = _patientService.SelectIndividual(requestId);
            return result;
        }

        [HttpGet]
        [Route("DropdownType")]
        public DropdownTypeModelList GetTypeList()
        {
            DropdownTypeModelList result = _patientService.TypeList();
            return result;
        }
    }

}