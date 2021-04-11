using System;
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
            var token = await HttpContext.GetTokenAsync("access_token").ConfigureAwait(false);
            var payloadJson = new JwtSecurityTokenHandler().ReadJwtToken(token).Payload.SerializeToJson();
            var tokenUsername = JsonSerializer.Deserialize<TokenUsername>(payloadJson);
            if (tokenUsername == null) return null;
            var admin = tokenUsername.unique_name;
            var pCsList = StaticStorageServices.PcMapper[admin].Values;
            return JsonSerializer.Serialize(pCsList);
        }

        [HttpPost]
        public async Task<string> PostDiagnosticDataFromPc(DiagnosticData diagnosticData)
        {
            var x = diagnosticData.PcConfiguration.Admins[0].Item2;
            var admins = diagnosticData.PcConfiguration.Admins;
            foreach (var admin in admins)
            {
                if (!StaticStorageServices.PcMapper.ContainsKey(admin.Item1)) return "false";

                if (!StaticStorageServices.AdminMapper[admin.Item1].Equals(admin.Item2)) return "false";

                //if the admin contains the pc
                if (StaticStorageServices.PcMapper[admin.Item1].ContainsKey(diagnosticData.PcId))
                {
                    StaticStorageServices.PcMapper[admin.Item1][diagnosticData.PcId] = diagnosticData;
                    await DatabaseFunctions.UpdatePcInDatabase(_db, diagnosticData).ConfigureAwait(false);

                    await DatabaseFunctions.UpdatePcLastCurrentSecond(diagnosticData, _db).ConfigureAwait(false);
                    await _db.SaveChangesAsync().ConfigureAwait(false);
                }
                else
                {
                    StaticStorageServices.PcMapper[admin.Item1].Add(diagnosticData.PcId, diagnosticData);

                    var pc = await _db.Pcs.Where(p => p.PcId == diagnosticData.PcId).FirstOrDefaultAsync().ConfigureAwait(false);

                    if (pc != null)
                    {
                        await DatabaseFunctions.UpdatePcInDatabase(_db, diagnosticData).ConfigureAwait(false);
                        await DatabaseFunctions.UpdatePcLastCurrentSecond(diagnosticData, _db).ConfigureAwait(false);
                    }
                    else
                    {
                        var newPc = ModelCreation.CreatePc(diagnosticData);
                        //Task.WaitAll();
                        await DatabaseFunctions.InitializePcLastMinute(diagnosticData, _db).ConfigureAwait(false);
                        await _db.Pcs.AddAsync(newPc).ConfigureAwait(false);
                    }
                    await DatabaseFunctions.AddPcToAdmin(diagnosticData, admin.Item1, _db).ConfigureAwait(false);
                    await _db.SaveChangesAsync().ConfigureAwait(false);
                }
            }

            return "true";
        }
    }
}
