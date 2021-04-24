using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Database.DatabaseModels;
using Services;
using Xunit;

namespace ApiTest
{
    public class PcControllerTest
    { 
        [Fact]
        public async Task DiagnosticDataTest()
        {
            var msg = new Credential()
            {
                CredentialsUsername = "allawrg@gmail.com",
                CredentialsPassword = "12345678"
            };
            var token = await Services.GetPostServices.Post("https://pc-health.azurewebsites.net/Admin/Login",
                JsonSerializer.Serialize(msg));

            var response = await GetPostServices.Get("https://pc-health.azurewebsites.net/Pc/DiagnosticData", token);
            var des = JsonSerializer.Deserialize<List<CommonModels.DiagnosticData>>(response);
            Assert.NotEmpty(des!);
            Assert.Equal("0CWMmrdH0SyUdpdGpGR9tVsLXQid9NcsuSSJ3qygrPY", des[0].PcId);
            Assert.Equal(310547706, des[0].AvgNetworkBytesSent);
        }
        
        
    }
}