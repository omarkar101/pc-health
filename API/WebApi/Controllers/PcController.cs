using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ApiModels;
using CommonModels;
using Database.DatabaseModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Services;

namespace WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PcController : ControllerBase
    {
        private readonly PcHealthContext _db;

        public PcController(PcHealthContext db)
        {
            _db = db;
        }

        [Authorize]
        [HttpGet]
        public async Task<string> DiagnosticData()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var payloadJson = new JwtSecurityTokenHandler().ReadJwtToken(token).Payload.SerializeToJson();
            var tokenUsername = JsonSerializer.Deserialize<TokenUsername>(payloadJson);
            if (tokenUsername == null) return null;
            var admin = tokenUsername.unique_name;
            var pCsList = StaticStorageServices.PcMapper[admin].Values;
            return JsonSerializer.Serialize(pCsList);
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
    }
}
