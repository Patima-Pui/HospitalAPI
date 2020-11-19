using System;
using HospitalAPI.Repositorys;

namespace HospitalAPI.Services
{
    public interface IPatientService
    {
        PatientModelList SelectPatients(PatientModelRequest requestSerach);
    }

    public class PatientService : IPatientService
    {
        private IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository) 
        {
            _patientRepository = patientRepository;
        }
    
        public PatientModelList SelectPatients(PatientModelRequest requestSerach)
        {
            PatientModelList result = _patientRepository.SelectPatientsAll(requestSerach);
            return result;
        }
    }
}