using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using WebApi.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        [HttpGet]
        public string DiagnosticData()
        {
            var lstValueCollection = StaticStorageServices.PC_Mapper.Values;
            return JsonSerializer.Serialize(lstValueCollection);
        }

        [HttpPost]
        public void GetDiagnosticDataFromPc(Diagnostic_Data diagnosticData)
        {
            if (StaticStorageServices.PC_Mapper.ContainsKey(diagnosticData.PC_ID))
            {
                StaticStorageServices.PC_Mapper[diagnosticData.PC_ID] = diagnosticData;
            }
            else StaticStorageServices.PC_Mapper.Add(diagnosticData.PC_ID, diagnosticData);
        }
    }
}