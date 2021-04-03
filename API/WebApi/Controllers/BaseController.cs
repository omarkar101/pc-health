using System;
using System.Linq;
using ApiModels;
using Database.DatabaseModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
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

            var credentialList = DatabaseFunctions.GetCredentials(_db, newAccountInfo);

            if (credentialList.Count != 0) return false;

            DatabaseFunctions.CreateNewCredentials(_db, newAccountInfo);
            DatabaseFunctions.CreateNewAdmin(_db, newAccountInfo);
            
            return true;
        }

        [HttpPost]
        public bool PostLogin(Credential credential)
        {
            if (credential is null)
            {
                throw new ArgumentNullException(nameof(credential));
            }

            var credentialQueryingList = DatabaseFunctions.GetCredentials(_db, credential);

            if (credentialQueryingList.Count == 0)
            {
                return false;
            }

            var passwordSalt = DatabaseFunctions.GetPasswordSalt(_db, credential);

            var passwordInDatabase = DatabaseFunctions.GetPasswordFromDb(_db, credential);

            var decryptPassword = HashServices.Decrypt(passwordSalt, credential.CredentialsPassword);
            return decryptPassword.Equals(passwordInDatabase);
        }
    }
}