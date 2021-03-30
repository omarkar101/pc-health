using System;
using System.Linq;
using ApiModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using WebApi.DataBaseModels;
using WebApi.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace WebApi.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly PcHealthContext _db;

        public BaseController(PcHealthContext db)
        {
            this._db = db;
        }

        [HttpGet]
        public string GetDiagnosticData()
        {
            var pCsList = StaticStorageServices.PcMapper.Values;
            return JsonSerializer.Serialize(pCsList);
        }

       

        [HttpPost]
        public void PostDiagnosticDataFromPc(DiagnosticData diagnosticData)
        {
            if (StaticStorageServices.PcMapper.ContainsKey(diagnosticData.PcId))
            {
                StaticStorageServices.PcMapper[diagnosticData.PcId] = diagnosticData;
            }
            else StaticStorageServices.PcMapper.Add(diagnosticData.PcId, diagnosticData);
        }


        [HttpPost]
        public bool PostCreateNewAdmin(NewAccountInfo newAccountInfo)
        {
            if (newAccountInfo is null)
            {
                throw new ArgumentNullException(nameof(newAccountInfo));
            }
            var credentialList = _db.Credentials.Where(c => c.CredentialsUsername == newAccountInfo.CredentialsUsername).ToList();

            if (credentialList.Count != 0) return false;
            var hashPassword = Services.HashServices.Encrypt(newAccountInfo.CredentialsPassword);
            var newCredential = new Credential()
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

            _db.Credentials.Add(newCredential);
            _db.Admins.Add(newAdmin);

            _db.SaveChanges();
            return true;
        }

        [HttpPost]
        public bool PostLogin(Credential credential)
        {
            if (credential is null)
            {
                throw new ArgumentNullException(nameof(credential));
            }

            var credentialQueryingList =
                _db.Credentials.Where(c => c.CredentialsUsername == credential.CredentialsUsername);
            if (credentialQueryingList.ToList().Count == 0)
            {
                return false;
            }

            var credentials = _db.Credentials;
            var passwordSalt = credentials.Where(c => c.CredentialsUsername == credential.CredentialsUsername)
                .Select(c => c.CredentialsSalt).First().ToString();
            var passwordInDatabase = credentials.Where(c => c.CredentialsUsername == credential.CredentialsUsername)
                .Select(c => c.CredentialsPassword).First().ToString();

            var decryptPassword = HashServices.Decrypt(passwordSalt, credential.CredentialsPassword);
            if (decryptPassword.Equals(passwordInDatabase))
            {
                return true;
            }
            return false;
        }
    }
}