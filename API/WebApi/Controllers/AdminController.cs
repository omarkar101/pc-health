using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiModels;
using CommonModels;
using Database.DatabaseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Services;


namespace WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly PcHealthContext _db;
        private readonly JWTSettings _jwtSettings;

        public AdminController(PcHealthContext db, IOptions<JWTSettings> jwtSettings)
        {
            _db = db;
            _jwtSettings = jwtSettings.Value;
        }

        [HttpPost] 
        public async Task<bool> Create(NewAccountInfo newAccountInfo)
        {
            if (newAccountInfo is null)
            {
                return false;
            }
            var credentialList = await DatabaseFunctions.GetCredentials(_db, newAccountInfo);

            if (credentialList.Count != 0) return false;

            await DatabaseFunctions.CreateNewCredentials(_db, newAccountInfo);
            await DatabaseFunctions.CreateNewAdmin(_db, newAccountInfo);

            StaticStorageServices.PcMapper.Add(newAccountInfo.CredentialsUsername, new Dictionary<string, DiagnosticData>());

            return true;
        }


        [HttpPost]
        public async Task<string> Login(Credential credential)
        {
            await DatabaseFunctions.InitializeStaticStorage(_db);
            if (credential is null)
            {
                return "false";
            }

            var credentialQueryingList = await DatabaseFunctions.GetCredentials(_db, credential);

            if (credentialQueryingList.Count == 0)
            {
                return "false";
            }

            var passwordSalt = await DatabaseFunctions.GetPasswordSalt(_db, credential);

            var passwordInDatabase = await DatabaseFunctions.GetPasswordFromDb(_db, credential);

            var decryptPassword = HashServices.Decrypt(passwordSalt, credential.CredentialsPassword);

            return !decryptPassword.Equals(passwordInDatabase) ? "false" : GenerateToken.Generate(credential.CredentialsUsername, _jwtSettings);
        }
    }
}