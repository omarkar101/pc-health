using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Database.DatabaseModels;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ApiTest
{
    public class AdminControllerTest
    {
        [Fact]
        public async Task TestLogin()
        {
            var msg = new Credential()
            {
                CredentialsUsername = "allawrg@gmail.com",
                CredentialsPassword = "12345678"
            };
            var response = await Services.GetPostServices.Post("https://pc-health.azurewebsites.net/Admin/Login",
                JsonSerializer.Serialize(msg));
            Assert.NotEqual("false", response);
        }
    }
    
}