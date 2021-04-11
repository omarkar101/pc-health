using System.Collections.Generic;
using System.Net.Security;
using System.Threading.Tasks;
using ApiModels;
using CommonModels;
using Database.DatabaseModels;
using Microsoft.AspNetCore.Mvc;
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

            // checks if the email is legit or not
            if (!EmailServices.VerifyEmail(newAccountInfo.CredentialsUsername)) return false;

            var credentialList = await DatabaseFunctions.GetCredentials(_db, newAccountInfo).ConfigureAwait(false);

            if (credentialList.Count != 0) return false;

            await DatabaseFunctions.CreateNewCredentials(_db, newAccountInfo).ConfigureAwait(false); 
            await DatabaseFunctions.CreateNewAdmin(_db, newAccountInfo).ConfigureAwait(false);

            StaticStorageServices.PcMapper.Add(newAccountInfo.CredentialsUsername, new Dictionary<string, DiagnosticData>());

            return true;
        }


        [HttpPost]
        public async Task<string> Login(Credential credential)
        {
            await DatabaseFunctions.InitializeStaticStorage(_db).ConfigureAwait(false);
            if (credential is null)
            {
                return "false";
            }

            var credentialQueryingList = await DatabaseFunctions.GetCredentials(_db, credential).ConfigureAwait(false);

            if (credentialQueryingList.Count == 0)
            {
                return "false";
            }

            var passwordSalt = await DatabaseFunctions.GetPasswordSalt(_db, credential).ConfigureAwait(false);

            var passwordInDatabase = await DatabaseFunctions.GetPasswordFromDb(_db, credential).ConfigureAwait(false);

            var decryptPassword = HashServices.Decrypt(passwordSalt, credential.CredentialsPassword);

            return !decryptPassword.Equals(passwordInDatabase) ? "false" : GenerateToken.Generate(credential.CredentialsUsername, _jwtSettings);
        }
    }
}