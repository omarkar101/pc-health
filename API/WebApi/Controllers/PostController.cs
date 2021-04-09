using System;
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
        public async Task PostDiagnosticDataFromPc(DiagnosticData diagnosticData)
        {
            var admins = diagnosticData.AdminUsernames;
            foreach (var admin in admins)
            {
                if (!StaticStorageServices.PcMapper.ContainsKey(admin)) return;

                //if the admin contains the pc
                if (StaticStorageServices.PcMapper[admin].ContainsKey(diagnosticData.PcId))
                {
                    StaticStorageServices.PcMapper[admin][diagnosticData.PcId] = diagnosticData;
                    await DatabaseFunctions.UpdatePcInDatabase(_db, diagnosticData);

                    await DatabaseFunctions.UpdatePcLastCurrentSecond(diagnosticData, _db);
                    await _db.SaveChangesAsync();
                }
                else
                {
                    StaticStorageServices.PcMapper[admin].Add(diagnosticData.PcId, diagnosticData);

                    var pc = await _db.Pcs.Where(p => p.PcId == diagnosticData.PcId).FirstOrDefaultAsync();

                    if (pc != null)
                    {
                        await DatabaseFunctions.UpdatePcInDatabase(_db, diagnosticData);
                        await DatabaseFunctions.UpdatePcLastCurrentSecond(diagnosticData, _db);
                    }
                    else
                    {
                        var newPc = ModelCreation.CreatePc(diagnosticData);
                        await DatabaseFunctions.InitializePcLastMinute(diagnosticData, _db);
                        await _db.Pcs.AddAsync(newPc);
                    }
                    await DatabaseFunctions.AddPcToAdmin(diagnosticData, admin, _db);
                    await _db.SaveChangesAsync();
                }
            }
        }

        


        [HttpPost] 
        public async Task<bool> PostCreateNewAdmin(NewAccountInfo newAccountInfo)
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
        public async Task<string> PostLogin(Credential credential)
        {
            await DatabaseFunctions.InitializeStaticStorage(_db);
            if (credential is null)
            {
                throw new ArgumentNullException(nameof(credential));
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