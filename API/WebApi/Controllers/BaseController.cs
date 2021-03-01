using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
using System.Web.Http.Cors;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly DiagnosticDataServices _diagnosticDataService;

        public BaseController(DiagnosticDataServices diagnosticDataServices){
            _diagnosticDataService = diagnosticDataServices;
        }

        [HttpGet]
        public string DiagnosticData()
        {
            return _diagnosticDataService.GetDiagnosticData();
        }
    }
}