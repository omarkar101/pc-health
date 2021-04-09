using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiModels;
using CommonModels;
using Database.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public static class DatabaseFunctions
    {
        public static async Task UpdatePcLastCurrentSecond(DiagnosticData diagnosticData, PcHealthContext db)
        {
            var lastMinutePc = await db.LastMinutes.Where(lm =>
                    lm.PcId.Equals(diagnosticData.PcId) && lm.Second == diagnosticData.CurrentSecond)
                .FirstOrDefaultAsync();
            ModelCreation.CreateOrUpdateLastMinute(diagnosticData, lastMinutePc);
        }
        
        public static async Task InitializePcLastMinute(DiagnosticData diagnosticData, PcHealthContext db)
        {
            await db.LastMinutes.AddAsync(ModelCreation.CreateOrUpdateLastMinute(diagnosticData));

            for (var i = 1; i < 60; i++)
            {
                await db.LastMinutes.AddAsync(new LastMinute()
                {
                    Second = i,
                    PcId = diagnosticData.PcId,
                    PcNetworkAverageBytesSend = 0,
                    PcCpuUsage = 0,
                    PcMemoryUsage = 0,
                    PcNetworkAverageBytesReceived = 0
                });
            }
        }

        public static async Task AddPcToAdmin(DiagnosticData diagnosticData, string admin, PcHealthContext db)
        {
            var adminFromDb = await db.Admins.Where(a => a.AdminCredentialsUsername.Equals(admin)).FirstOrDefaultAsync();
            var adminHasPc = new AdminHasPc()
            {
                PcId = diagnosticData.PcId,
                AdminCredentialsUsername = admin
            };
            adminFromDb.AdminHasPcs.Add(adminHasPc);
        }
        public static async Task<List<Credential>> GetCredentials(PcHealthContext dbContext, NewAccountInfo newAccountInfo)
        {
            return await dbContext.Credentials.Where(c => c.CredentialsUsername == newAccountInfo.CredentialsUsername).ToListAsync();
        }

        public static async Task<List<Credential>> GetCredentials(PcHealthContext dbContext, Credential credential)
        {
            return await dbContext.Credentials.Where(c => c.CredentialsUsername == credential.CredentialsUsername).ToListAsync();
        }

        public static async Task CreateNewAdmin(PcHealthContext dbContext, NewAccountInfo newAccountInfo)
        {
            var newAdmin = new Admin()
            {
                AdminFirstName = newAccountInfo.AdminFirstName,
                AdminLastName = newAccountInfo.AdminLastName,
                AdminCredentialsUsername = newAccountInfo.CredentialsUsername
            };
            await dbContext.Admins.AddAsync(newAdmin);
            await dbContext.SaveChangesAsync();
        }

        public static async Task CreateNewCredentials(PcHealthContext dbContext, NewAccountInfo newAccountInfo)
        {
            var (salt, passwordHash) = Services.HashServices.Encrypt(newAccountInfo.CredentialsPassword);
            var newCredential = new Credential()
            {
                CredentialsUsername = newAccountInfo.CredentialsUsername,
                CredentialsPassword = passwordHash,
                CredentialsSalt = salt
            };
            await dbContext.Credentials.AddAsync(newCredential);
            await dbContext.SaveChangesAsync();
        }

        public static async Task<string> GetPasswordSalt(PcHealthContext dbContext, Credential credential)
        {
            var credentials = dbContext.Credentials;
            var neededCred = await credentials.Where(c => c.CredentialsUsername == credential.CredentialsUsername)
                .Select(c => c.CredentialsSalt).FirstAsync();
            return neededCred;
        }

        public static async Task<string> GetPasswordFromDb(PcHealthContext dbContext, Credential credential)
        {
            var credentials = dbContext.Credentials;
            var neededCred = await credentials.Where(c => c.CredentialsUsername == credential.CredentialsUsername)
                .Select(c => c.CredentialsPassword).FirstAsync();
            return neededCred;
        }

        

        public static async Task UpdatePcInDatabase(PcHealthContext db, DiagnosticData diagnosticData)
        {
            var pc = await db.Pcs.Where(p => p.PcId.Equals(diagnosticData.PcId)).FirstOrDefaultAsync();
            if (pc != null)
            {
                pc.PcCpuUsage = diagnosticData.CpuUsage;
                pc.PcDiskTotalFreeSpace = diagnosticData.TotalFreeDiskSpace;
                pc.PcDiskTotalSpace = diagnosticData.DiskTotalSpace;
                pc.PcFirewallStatus = diagnosticData.FirewallStatus;
                pc.PcId = diagnosticData.PcId;
                pc.PcMemoryUsage = diagnosticData.MemoryUsage;
                pc.PcNetworkAverageBytesReceived = diagnosticData.AvgNetworkBytesReceived;
                pc.PcNetworkAverageBytesSend = diagnosticData.AvgNetworkBytesSent;
                pc.PcOs = diagnosticData.Os;
                pc.PcUsername = diagnosticData.PcConfiguration.PcUsername;
                await db.SaveChangesAsync();
            }
        }
        public static async Task InitializeStaticStorage(PcHealthContext dbContext)
        {
            if (StaticStorageServices.PcMapper.Count != 0) return;
            var admins = await dbContext.Credentials.ToListAsync();
            foreach (var admin in admins)
            {
                StaticStorageServices.PcMapper.Add(admin.CredentialsUsername, new Dictionary<string, DiagnosticData>());
                StaticStorageServices.AdminMapper.Add(admin.CredentialsUsername, admin.PcCredentialPassword);
            }
            var adminHasPc = await dbContext.AdminHasPcs.ToListAsync();
            foreach (var adminPc in adminHasPc)
            {
                StaticStorageServices.PcMapper[adminPc.AdminCredentialsUsername].Add(adminPc.PcId, new DiagnosticData());
            }
            var pcs = await dbContext.Pcs.ToListAsync();
            foreach (var pc in pcs)
            {
                var pcDiagnosticData = new DiagnosticData()
                {
                    AvgNetworkBytesReceived = (double) pc.PcNetworkAverageBytesReceived,
                    AvgNetworkBytesSent = (double) pc.PcNetworkAverageBytesSend,
                    CpuUsage = pc.PcCpuUsage,
                    DiskTotalSpace = pc.PcDiskTotalSpace,
                    FirewallStatus = pc.PcFirewallStatus,
                    MemoryUsage = (double)pc.PcMemoryUsage,
                    Os = pc.PcOs,
                    PcId = pc.PcId,
                    PcConfiguration = new PcConfiguration(),
                    TotalFreeDiskSpace = pc.PcDiskTotalFreeSpace,
                    Services = new List<Tuple<string, string>>()
                };
                foreach (var admin in pc.AdminHasPcs)
                {
                    StaticStorageServices.PcMapper[admin.AdminCredentialsUsername][pc.PcId] =  pcDiagnosticData;
                }
            }

        }
    }
}
