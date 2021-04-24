using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ApiModels;
using Database.DatabaseModels;
using Services;
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
            var response = await GetPostServices.Post("https://pc-health.azurewebsites.net/Admin/Login",
                JsonSerializer.Serialize(msg));
            Assert.NotEqual("false", response);
        }
        
        // commented because after the first run, the first assertion will fail because we will be having
        // the account created already in the database
        // [Fact]
        public async Task TestCreate()
        {
            var msg1 = new NewAccountInfo()
            {
                AdminFirstName = "Omar",
                AdminLastName = "Rony",
                CredentialsUsername = "omar.kar101@gmail.com",
                CredentialsPassword = "123456789"
            };
            var response1 = await GetPostServices.Post("https://pc-health.azurewebsites.net/Admin/Create",
                JsonSerializer.Serialize(msg1));
            Assert.Equal("Success", response1);
            var response2 = await GetPostServices.Post("https://pc-health.azurewebsites.net/Admin/Create",
                JsonSerializer.Serialize(msg1));
            Assert.Equal("Email already exists", response2);
            var msg2 = new NewAccountInfo()
            {
                AdminFirstName = "Omar",
                AdminLastName = "Rony",
                CredentialsUsername = "owefwe12ediqwojeifn2oqin3fo2i3fnoi23fn@ail.com",
                CredentialsPassword = "123456789"
            };
            var response3 = await GetPostServices.Post("https://pc-health.azurewebsites.net/Admin/Create",
                JsonSerializer.Serialize(msg2));
            Assert.Equal("Invalid email", response3);
        }
    }
    
}