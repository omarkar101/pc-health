using System;
using System.Collections.Generic;
using System.Linq;
using ApiModels;
using CommonModels;
using Database.DatabaseModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Services;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace WebApi.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly PcHealthContext _db;

        public PostController(PcHealthContext db)
        {
            this._db = db;
        }


        [HttpPost]
        public void PostDiagnosticDataFromPc(DiagnosticData diagnosticData)
        {
            var admin = diagnosticData.AdminUsername;
            if (StaticStorageServices.PcMapper[admin].ContainsKey(diagnosticData.PcId))
            {
                StaticStorageServices.PcMapper[admin][diagnosticData.PcId] = diagnosticData;
                DatabaseFunctions.UpdatePcInDatabase(_db, diagnosticData, admin);
            }
            else
            {
                StaticStorageServices.PcMapper[admin].Add(diagnosticData.PcId, diagnosticData);
                var newPc = DatabaseFunctions.CreatePc(diagnosticData);
                _db.Pcs.Add(newPc);
                _db.SaveChanges();
            }
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

            StaticStorageServices.PcMapper.Add(newAccountInfo.CredentialsUsername, new Dictionary<string, DiagnosticData>());

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