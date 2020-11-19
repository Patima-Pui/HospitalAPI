using System;
using HospitalAPI.Repositorys;

namespace HospitalAPI.Services
{
    public interface IPatientService
    {
        PatientModelList SelectPatients();
    }

    public class PatientService : IPatientService
    {
        private IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository) 
        {
            _patientRepository = patientRepository;
        }
    
        public PatientModelList SelectPatients()
        {
            PatientModelList result = _patientRepository.SelectPatientsAll();
            return result;
        }
    }
}