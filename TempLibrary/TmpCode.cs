using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempLibrary
{
    class TmpCode
    {
        //[HttpGet]
        //public string GetTime()
        //{
        //    var timeNowUtc = DateTime.UtcNow;
        //    if ((timeNowUtc - StaticStorageServices.TimeToGetPcConfiguration).TotalMilliseconds >= 0)
        //    {
        //        StaticStorageServices.TimeToGetPcConfiguration = timeNowUtc.AddSeconds(StaticStorageServices.PCsConfiguration.Time);
        //    }
        //    return JsonSerializer.Serialize(StaticStorageServices.TimeToGetPcConfiguration);
        //}

        //[HttpPost]
        //public void PostTimeConfiguration(ConfigurationFromWebsiteData configuration)
        //{
        //    StaticStorageServices.PCsConfiguration = configuration;
        //    StaticStorageServices.TimeToGetPcConfiguration = DateTime.UtcNow.AddSeconds(StaticStorageServices.PCsConfiguration.Time);
        //}


        //services.AddCors(c => c.AddPolicy("AllowOrigin", 
        //    options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader())
        //);
        // services.AddTransient<DiagnosticDataServices>();

        //public static ConfigurationFromWebsiteData PCsConfiguration = new ConfigurationFromWebsiteData();
        //public static DateTime TimeToGetPcConfiguration = DateTime.UtcNow.AddSeconds(PCsConfiguration.Time);




        // DateTime timeGet;
        // DateTime time;
        //
        // DateTime ping;
        // double pingDiff;


        /*ping = DateTime.UtcNow;
                    timeGet = GetServices.GetTime("https://pchealth.azurewebsites.net/api/Base/GetTime");
                    pingDiff = (DateTime.UtcNow - ping).TotalMilliseconds;
                    time = DateTime.UtcNow;*/
        //PostServices.PostDiagnosticData("http://pchealth.somee.com/api/Base/PostDiagnosticDataFromPc");
    }
}
