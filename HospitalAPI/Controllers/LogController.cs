using HospitalAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class LogController : ControllerBase
    {
        private readonly ILogService _logService;
        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpGet]
        [Route("SelectLog")]
        public LogModelList SelectLog([FromQuery] SearchLogModel request)
        {
            LogModelList result = _logService.SelectLogList(request);
            return result;
        }


        
    }
}