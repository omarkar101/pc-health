using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiModels;
using Database.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class DatabaseFunctions
    {
        public static List<Credential> GetCredentials(PcHealthContext dbContext, NewAccountInfo newAccountInfo)
        {
            return dbContext.Credentials.Where(c => c.CredentialsUsername == newAccountInfo.CredentialsUsername).ToList();
        }

        public static List<Credential> GetCredentials(PcHealthContext dbContext, Credential credential)
        {
            return dbContext.Credentials.Where(c => c.CredentialsUsername == credential.CredentialsUsername).ToList();
        }

        public static void CreateNewAdmin(PcHealthContext dbContext, NewAccountInfo newAccountInfo)
        {
            var newAdmin = new Admin()
            {
                AdminFirstName = newAccountInfo.AdminFirstName,
                AdminLastName = newAccountInfo.AdminLastName,
                AdminCredentialsUsername = newAccountInfo.CredentialsUsername
            };
            dbContext.Admins.Add(newAdmin);
            dbContext.SaveChanges();
        }

        public static void CreateNewCredentials(PcHealthContext dbContext, NewAccountInfo newAccountInfo)
        {
            var (salt, passwordHash) = Services.HashServices.Encrypt(newAccountInfo.CredentialsPassword);
            var newCredential = new Credential()
            {
                CredentialsUsername = newAccountInfo.CredentialsUsername,
                CredentialsPassword = passwordHash,
                CredentialsSalt = salt
            };
            dbContext.Credentials.Add(newCredential);
            dbContext.SaveChanges();
        }

        public static string GetPasswordSalt(PcHealthContext dbContext, Credential credential)
        {
            var credentials = dbContext.Credentials;
            return credentials.Where(c => c.CredentialsUsername == credential.CredentialsUsername)
                .Select(c => c.CredentialsSalt).First().ToString();
        }

        public static string GetPasswordFromDb(PcHealthContext dbContext, Credential credential)
        {
            var credentials = dbContext.Credentials;
            return credentials.Where(c => c.CredentialsUsername == credential.CredentialsUsername)
                .Select(c => c.CredentialsPassword).First().ToString();
        }
    }
}
