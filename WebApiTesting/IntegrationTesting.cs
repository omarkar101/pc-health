using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Database.DatabaseModels;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WebApi;
using Xunit;

namespace WebApiTesting
{
    public class IntegrationTesting : IClassFixture<WebApplicationFactory<Startup>>
    {
        public readonly HttpClient TestClient;
        private readonly WebApplicationFactory<Startup> _factory;
        public IntegrationTesting(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            //var appFactory = new WebApplicationFactory<Startup>()
            //    .WithWebHostBuilder(builder =>
            //    {
            //        builder.UseSolutionRelativeContentRoot(
            //            @"C:\Users\omark\source\repos\omarkar101\pc-health");
            //        //builder.ConfigureServices(services =>
            //        //{
            //        //    services.RemoveAll(typeof(PcHealthContext));
            //        //    services.AddDbContext<PcHealthContext>(options =>
            //        //    {
            //        //        options.UseInMemoryDatabase("TestDb");
            //        //    });
            //        //});
            //    });
            TestClient = _factory.CreateClient();
        }

        protected async Task AuthenticateAsync()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtToken());
        }

        private async Task<string> GetJwtToken()
        {
            var response = await TestClient.PostAsJsonAsync("https://pc-health.azurewebsites.net/Admin/Login",
                new Credential()
                {
                    CredentialsUsername = "allawrg@gmail.com",
                    CredentialsPassword = "12345678"
                });
            var registrationResponse = await response.Content.ReadAsAsync<string>();
            return registrationResponse;
        }

        [Fact]
        public async Task DiagnosticDataTest()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var response = await TestClient.GetAsync("https://pc-health.azurewebsites.net/Pc/DiagnosticData");

            // Assert
            response.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<string>()).Should().NotBeEmpty();
        }
    }
}
