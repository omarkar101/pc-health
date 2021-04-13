using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Threading.Tasks;
using ApiModels;
using CommonModels;
using Database.DatabaseModels;
using Microsoft.AspNetCore.Authorization;
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
            
            // checks if the email is legit or not
            if (!EmailServices.VerifyEmail(newAccountInfo.CredentialsUsername)) return false;

            var credentialList = await DatabaseFunctions.GetCredentials(_db, newAccountInfo).ConfigureAwait(false);

            if (credentialList.Count != 0) return false;

            var pcCredentialPassword = await DatabaseFunctions.CreateNewCredentials(_db, newAccountInfo).ConfigureAwait(false); 
            await DatabaseFunctions.CreateNewAdmin(_db, newAccountInfo).ConfigureAwait(false);

            StaticStorageServices.PcMapper.Add(newAccountInfo.CredentialsUsername, new Dictionary<string, DiagnosticData>());

            //send pcCredential password to the new user
            await EmailServices.SendEmail(newAccountInfo.CredentialsUsername, $"Pc Credential Password: {pcCredentialPassword}");

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

        [Authorize]
        [HttpPost]
        public async Task<bool> ChangePassword(ChangePasswordInfo changePasswordInfo)
        {
            var credential = new Credential()
            {
                CredentialsPassword = changePasswordInfo.OldPassword,
                CredentialsUsername = changePasswordInfo.CredentialUsername
            };

            var passwordSalt = await DatabaseFunctions.GetPasswordSalt(_db, credential).ConfigureAwait(false);

            var passwordInDatabase = await DatabaseFunctions.GetPasswordFromDb(_db, credential).ConfigureAwait(false);

            var decryptPassword = HashServices.Decrypt(passwordSalt, credential.CredentialsPassword);

            if (!decryptPassword.Equals(passwordInDatabase)) return false;

            await DatabaseFunctions.ChangePasswordInDb(changePasswordInfo.CredentialUsername, changePasswordInfo.NewPassword,
                _db);
            return true;
        }

        // 1st step
        [HttpPost]                                          // query string
        public async Task<bool> ForgetPasswordUsername(string credentialUsername)
        {
            var credentialFromDb = await _db.Credentials.Where(c => c.CredentialsUsername.Equals(credentialUsername))
                .FirstOrDefaultAsync().ConfigureAwait(false);
            if (credentialFromDb == null) return false;

            var credentialUniqueId = ModelCreation.GenerateRandomString();

            // generate credentialUniqueId
            credentialFromDb.CredentialChangePasswordId = credentialUniqueId;
            await _db.SaveChangesAsync();

            // send it by email
            await EmailServices.SendEmail(credentialUsername, $"Verification Code: {credentialUniqueId}");

            return true;
        }
        //2nd step
        [HttpPost]
        public async Task<bool> ForgetPasswordUniqueIdCheck(string credentialUsername, string code)
        {
            var credential = await _db.Credentials.Where(c => c.CredentialsUsername.Equals(credentialUsername))
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            return credential != null && credential.CredentialChangePasswordId.Equals(code);
        }
        // 3rd step
        [HttpPost]
        public async Task<bool> ForgetPasswordChange(string credentialUsername, string code, string newPassword)
        {
            var credential = await _db.Credentials.Where(c => c.CredentialsUsername.Equals(credentialUsername))
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            if (credential == null) return false;
            if (!credential.CredentialChangePasswordId.Equals(code)) return false;

            await DatabaseFunctions.ChangePasswordInDb(credentialUsername, newPassword, _db);
            
            credential.CredentialChangePasswordId = ModelCreation.GenerateRandomString();
            
            await _db.SaveChangesAsync();

            return true;
        }

        [Authorize]
        [HttpPost]
        public async Task<bool> ResetPcCredentialPassword(Credential credential)
        {
            var credentialFromDb = await _db.Credentials
                .Where(c => c.CredentialsUsername.Equals(credential.CredentialsUsername)).FirstOrDefaultAsync()
                .ConfigureAwait(false);
            if (credentialFromDb == null) return false;

            var passwordSalt = await DatabaseFunctions.GetPasswordSalt(_db, credential).ConfigureAwait(false);

            var passwordInDatabase = await DatabaseFunctions.GetPasswordFromDb(_db, credential).ConfigureAwait(false);

            var decryptPassword = HashServices.Decrypt(passwordSalt, credential.CredentialsPassword);

            if (!decryptPassword.Equals(passwordInDatabase)) return false;

            credentialFromDb.PcCredentialPassword = ModelCreation.GenerateRandomString();

            StaticStorageServices.AdminMapper[credential.CredentialsUsername] = credentialFromDb.PcCredentialPassword;

            await EmailServices.SendEmail(credential.CredentialsUsername, $"New Pc Credential Password: {credentialFromDb.PcCredentialPassword}");

            await _db.SaveChangesAsync();

            return true;
        }

    }

}