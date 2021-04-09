using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using ApiModels;
using CommonModels;
using Database.DatabaseModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace WebApi.Controllers
{
    //[EnableCors]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly PcHealthContext _db;
        private readonly JWTSettings _jwtSettings;

        public PostController(PcHealthContext db, IOptions<JWTSettings> jwtSettings)
        {
            _db = db;
            _jwtSettings = jwtSettings.Value;
        }

        [HttpPost]
        public void PostDiagnosticDataFromPc(DiagnosticData diagnosticData)
        {
            var admins = diagnosticData.AdminUsernames;
            foreach (var admin in admins)
            {
                if (!StaticStorageServices.PcMapper.ContainsKey(admin)) return;

                //if the admin contains the pc
                if (StaticStorageServices.PcMapper[admin].ContainsKey(diagnosticData.PcId))
                {
                    StaticStorageServices.PcMapper[admin][diagnosticData.PcId] = diagnosticData;
                    DatabaseFunctions.UpdatePcInDatabase(_db, diagnosticData);
                }
                else
                {
                    StaticStorageServices.PcMapper[admin].Add(diagnosticData.PcId, diagnosticData);

                    var _pc = _db.Pcs.Where(p => p.PcId == diagnosticData.PcId).FirstOrDefault();

                    if (_pc != null)
                    {
                        DatabaseFunctions.UpdatePcInDatabase(_db, diagnosticData);
                    }
                    else
                    {
                        var newPc = DatabaseFunctions.CreatePc(diagnosticData);
                        _db.Pcs.Add(newPc);
                    }
                    DatabaseFunctions.AddPcToAdmin(diagnosticData, admin, _db);
                    _db.SaveChanges();
                }
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
        public string PostLogin(Credential credential)
        {
            DatabaseFunctions.InitializeStaticStorage(_db);
            if (credential is null)
            {
                throw new ArgumentNullException(nameof(credential));
            }

            var credentialQueryingList = DatabaseFunctions.GetCredentials(_db, credential);

            if (credentialQueryingList.Count == 0)
            {
                return "false";
            }

            var passwordSalt = DatabaseFunctions.GetPasswordSalt(_db, credential);

            var passwordInDatabase = DatabaseFunctions.GetPasswordFromDb(_db, credential);

            var decryptPassword = HashServices.Decrypt(passwordSalt, credential.CredentialsPassword);

            if (!decryptPassword.Equals(passwordInDatabase)) return "false";

            return GenerateToken.Generate(credential.CredentialsUsername, _jwtSettings);
        }
    }
}