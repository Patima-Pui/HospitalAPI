using System;
using HospitalAPI.Repositorys;

namespace HospitalAPI.Services
{
    public interface IPatientService
    {
        PatientModelList SelectPatients(PatientModelRequest requestSerach);
        PatientModel SelectIndividual(PatientRequestIdModel requestId);
        DropdownTypeModelList TypeList();
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
            PatientModelList result = _patientRepository.QueryPatients(requestSerach);
            return result;
        }
        public PatientModel SelectIndividual(PatientRequestIdModel requestId)
        {
            PatientModel result = _patientRepository.SelectIndividualRepo(requestId);
            return result;
        }

        public DropdownTypeModelList TypeList()
        {
            DropdownTypeModelList result = _patientRepository.SelectTypeListFromDB();
            return result;
        }
    }
}