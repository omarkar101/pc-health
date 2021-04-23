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
            await DatabaseFunctions.InitializeStaticStorage(_db).ConfigureAwait(false);
            try
            {
                var token = await HttpContext.GetTokenAsync("access_token").ConfigureAwait(false);
                var payloadJson = new JwtSecurityTokenHandler().ReadJwtToken(token).Payload.SerializeToJson();
                var tokenUsername = JsonSerializer.Deserialize<TokenUsername>(payloadJson);
                if (tokenUsername == null) return "false";
                var admin = tokenUsername.name;
                var pCsList = StaticStorageServices.PcMapper[admin].Values;
                return JsonSerializer.Serialize(pCsList);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "false";
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<string> DiagnosticDataSpecific(string pcId)
        {
            await DatabaseFunctions.InitializeStaticStorage(_db).ConfigureAwait(false);
            try {
                var lastMinutes = _db.LastMinutes;
                var listOfLastMinute = await lastMinutes.Where(lm => lm.PcId.Equals(pcId)).ToListAsync<LastMinute>();
                if (listOfLastMinute.Count == 0) return "false";
                listOfLastMinute.Sort((LastMinute lm1, LastMinute lm2) =>
                {
                    if (lm1.TimeChanged == null) return 0;
                    return lm2.TimeChanged != null ? ((DateTime) lm1.TimeChanged).CompareTo((DateTime) lm2.TimeChanged) : 0;
                });

                for (var i = 1; i <= 60; i++)
                {
                    listOfLastMinute[i - 1].Second = i;
                }
                return JsonSerializer.Serialize(listOfLastMinute);
            }
            catch(Exception e)
            {
                return "false";
            }
        }

        [HttpPost]
        public async Task<string> PostDiagnosticDataFromPc(DiagnosticData diagnosticData)
        {
            await DatabaseFunctions.InitializeStaticStorage(_db).ConfigureAwait(false);
            var x = diagnosticData.PcConfiguration.Admins[0].Item2;
            var admins = diagnosticData.PcConfiguration.Admins;
            foreach (var admin in admins)
            {
                if (!StaticStorageServices.PcMapper.ContainsKey(admin.Item1)) return "false";

                //Check Pc Admin Password
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

        [HttpPost]
        public async Task<string> PostPcHealthDataFromPc(PcHealthData pcHealthData)
        {
            await DatabaseFunctions.InitializeStaticStorage(_db).ConfigureAwait(false);
            foreach (var admin in pcHealthData.PcConfiguration.Admins)
            {
                if(admin.Item2.Equals(StaticStorageServices.AdminMapper[admin.Item1]))
                    await EmailServices.SendEmail(admin.Item1, $"In the last minute, the pc of name {pcHealthData.PcConfiguration.PcUsername} " +
                                                     $"hit over 80%: <br>Memory Usage: {pcHealthData.MemoryHighCounter} times <br>" +
                                                     $"Cpu Usage: {pcHealthData.CpuHighCounter} times.");
            }
            return "true";
        }

    }
}
