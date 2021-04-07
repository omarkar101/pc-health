using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ApiModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Services;

namespace WebApi.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GetController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public async Task<string> GetDiagnosticData()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var payloadJson = new JwtSecurityTokenHandler().ReadJwtToken(token).Payload.SerializeToJson();
            var tokenUsername = JsonSerializer.Deserialize<TokenUsername>(payloadJson);
            if (tokenUsername == null) return null;
            var admin = tokenUsername.unique_name;
            var pCsList = StaticStorageServices.PcMapper[admin].Values;
            return JsonSerializer.Serialize(pCsList);
        }

    }
}
