using System;
using Microsoft.AspNetCore.Mvc;
using Models;
using WebApi.Services;
using WebApi.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        [HttpGet]
        public string GetDiagnosticData()
        {
            var PCsList = StaticStorageServices.PC_Mapper.Values;
            return JsonSerializer.Serialize(PCsList);
        }

        [HttpGet]
        public string GetTime()
        {
            var timeNowUtc = DateTime.UtcNow;
            if ((timeNowUtc - StaticStorageServices.TimeToGetPcConfiguration).TotalMilliseconds >= 0)
            {
                StaticStorageServices.TimeToGetPcConfiguration = timeNowUtc.AddSeconds(StaticStorageServices.PCsConfiguration.Time);
            }
            return JsonSerializer.Serialize(StaticStorageServices.TimeToGetPcConfiguration);
        }

        [HttpPost]
        public void PostDiagnosticDataFromPc(DiagnosticData diagnosticData)
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
            StaticStorageServices.PCsConfiguration = configuration;
            StaticStorageServices.TimeToGetPcConfiguration = DateTime.UtcNow.AddSeconds(StaticStorageServices.PCsConfiguration.Time);
        }
    }
}