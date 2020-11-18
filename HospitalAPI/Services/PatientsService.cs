using System;
using HospitalAPI.Repositorys;

namespace HospitalAPI.Services
{
    public interface IPatientService
    {
        PatientModelList SelectPatients();
    }

    public class PatientsService : IPatientService
    {
        private IPatientRepository _patientRepository;

        public PatientsService(IPatientRepository patientRepository) 
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