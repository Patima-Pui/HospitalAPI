using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using HospitalAPI.Repositorys;
using OfficeOpenXml;


namespace HospitalAPI.Services
{
    public interface IPatientService
    {
        PatientModelList SelectPatients(PatientModelRequest requestSerach);
        PatientModel SelectIndividual(PatientRequestIdModel requestId);
        DropdownTypeModelList TypeList();
        Task<byte[]> GetExport(PatientModelRequest requestSerach);
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

         public async Task<byte[]> GetExport(PatientModelRequest requestSerach) {
            PatientModelList result = _patientRepository.QueryPatients(requestSerach);
            List<PatientModel> PatientList = result.Patienttable;
            byte[] content;

            MemoryStream stream = new MemoryStream();
            using(ExcelPackage package = new ExcelPackage(stream)) {
                ExcelWorksheet ws = package.Workbook.Worksheets.Add("Patient List");
                int totalRow = PatientList.Count + 1;

                ws.Cells[1, 1].Value = "No";
                ws.Cells[1, 2].Value = "HN No";
                ws.Cells[1, 3].Value = "Name";
                ws.Cells[1, 4].Value = "Surname";
                ws.Cells[1, 5].Value = "Age";
                ws.Cells[1, 6].Value = "Birthday";
                ws.Cells[1, 7].Value = "Type";
                ws.Cells[1, 8].Value = "NO. of Visit";

                int i = 0;
                for (int row = 2; row <= totalRow; row++) {
                    ws.Cells[row, 1].Value = i + 1;
                    ws.Cells[row, 2].Value = PatientList[i].hn;
                    ws.Cells[row, 3].Value = PatientList[i].name;

                    ws.Cells[row, 4].Value = PatientList[i].surname;
                    ws.Cells[row, 5].Value = PatientList[i].age;

                    ws.Cells[row, 6].Value = PatientList[i].birthday;
                    ws.Cells[row, 6].Style.Numberformat.Format = "dd/mm/yyyy";

                    ws.Cells[row, 7].Value = PatientList[i].typeName;
                    ws.Cells[row, 8].Value = PatientList[i].visit;

                    i++;
                }

                content = await package.GetAsByteArrayAsync();
            }
            return content;
        }
    }
}