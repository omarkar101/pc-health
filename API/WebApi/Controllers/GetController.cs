using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GetController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public string GetDiagnosticData()
        {
            string admin = "rony123";
            var pCsList = StaticStorageServices.PcMapper[admin].Values;
            return JsonSerializer.Serialize(pCsList);
        }
        // get a certain admin pcs map[admin]
    }
}
