using System;
using HospitalAPI.Repositorys;

namespace HospitalAPI.Services
{
    public interface ILogService
    {
        void CreateLog(LogModel requestdata);
    }
    
    public class LogService : ILogService
    {
        private ILogRepository _logRepository;

        //Create constructor
        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public void CreateLog(LogModel requestdata)
        {
            _logRepository.InsertLog(requestdata);
        }
    }

}
