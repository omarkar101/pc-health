using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Models;
using WebApi.InfoFromWebsite;
using WebApi.Services;
using WebApi.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private PcHealthContext db;

        public BaseController(PcHealthContext db)
        {
            this.db = db;
        }

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

        [HttpPost]
        public bool PostCreateNewAdmin(NewAccountInfo newAccountInfo)
        {
            if (newAccountInfo is null)
            {
                throw new ArgumentNullException(nameof(newAccountInfo));
            }
            var CredentialList = db.Credentials.Where(c => c.CredentialsUsername == newAccountInfo.CredentialsUsername).ToList();
            if (CredentialList.Count == 0)
            {
                var hashPassword = Services.HashServices.Encrypt(newAccountInfo.CredentialsPassword);
                var NewCredential = new Credential()
                {
                    CredentialsUsername = newAccountInfo.CredentialsUsername,
                    CredentialsPassword = hashPassword.passwordHash,
                    CredentialsSalt = hashPassword.salt
                };
                var newAdmin = new Admin()
                {
                    AdminFirstName = newAccountInfo.AdminFirstName,
                    AdminLastName = newAccountInfo.AdminLastName,
                    AdminCredentialsUsername = newAccountInfo.CredentialsUsername
                };

                db.Credentials.Add(NewCredential);
                db.Admins.Add(newAdmin);

                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPost]
        public bool PostLogin(Credential credential)
        {
            if (credential is null)
            {
                throw new ArgumentNullException(nameof(credential));
            }

            var credentialQueryingList =
                db.Credentials.Where(c => c.CredentialsUsername == credential.CredentialsUsername);
            if (credentialQueryingList.ToList().Count == 0)
            {
                return false;
            }

            var credentials = db.Credentials;
            var passwordSalt = credentials.Where(c => c.CredentialsUsername == credential.CredentialsUsername)
                .Select(c => c.CredentialsSalt).First().ToString();
            var passwordInDatabase = credentials.Where(c => c.CredentialsUsername == credential.CredentialsUsername)
                .Select(c => c.CredentialsPassword).First().ToString();

            var DecryptPassword = HashServices.Decrypt(passwordSalt, credential.CredentialsPassword);
            if (DecryptPassword.Equals(passwordInDatabase))
            {
                return true;
            }
            return false;
        }
    }
}