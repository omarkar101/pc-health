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

        [HttpGet]
        public string GetTime()
        {
            var time = DateTime.UtcNow;
            if ((time - StaticStorageServices.TimeCurrent).TotalMilliseconds >= 0)
            {
                StaticStorageServices.TimeCurrent = time.AddSeconds(StaticStorageServices.TimeToAdd.Time);
            }
            return JsonSerializer.Serialize(StaticStorageServices.TimeCurrent);
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

        [HttpPost]
        public void PostTimeConfiguration(ConfigurationFromWebsiteData configuration)
        {
            StaticStorageServices.TimeToAdd = configuration;
            StaticStorageServices.TimeCurrent = DateTime.UtcNow.AddSeconds(StaticStorageServices.TimeToAdd.Time);
            // Console.WriteLine(configuration.Time);
        }
    }
}