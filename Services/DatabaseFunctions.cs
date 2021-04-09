using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
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
                .FirstOrDefaultAsync().ConfigureAwait(false);
            ModelCreation.CreateOrUpdateLastMinute(diagnosticData, lastMinutePc);
        }


        public static Task DoAsync(int i, DiagnosticData diagnosticData, PcHealthContext db)
        {
            db.LastMinutes.Add(new LastMinute()
            {
                Second = i,
                PcId = diagnosticData.PcId,
                PcNetworkAverageBytesSend = 0,
                PcCpuUsage = 0,
                PcMemoryUsage = 0,
                PcNetworkAverageBytesReceived = 0
            });
            return Task.CompletedTask;
        }


        public static void InitializePcLastMinute(DiagnosticData diagnosticData, PcHealthContext db)
        {
            db.LastMinutes.Add(ModelCreation.CreateOrUpdateLastMinute(diagnosticData));

            //var series = Enumerable.Range(1, 3).ToList();

            //var tasks = new List<Task>();


            //foreach (var i in series)

                //await foreach (int item in (10, 3))
            for (int i = 1; i <= 2; i++)
            {
                var j = i;
                db.LastMinutes.Add(new LastMinute()
                {
                    Second = j,
                    PcId = diagnosticData.PcId,
                    PcNetworkAverageBytesSend = 0,
                    PcCpuUsage = 0,
                    PcMemoryUsage = 0,
                    PcNetworkAverageBytesReceived = 0
                });
                //tasks.Add(Task.Run(async () =>
                //{
                //    await db.LastMinutes.AddAsync(new LastMinute()
                //    {
                //        Second = j,
                //        PcId = diagnosticData.PcId,
                //        PcNetworkAverageBytesSend = 0,
                //        PcCpuUsage = 0,
                //        PcMemoryUsage = 0,
                //        PcNetworkAverageBytesReceived = 0
                //    });
                //}));
            }

            //await Task.WhenAll(tasks.ToArray()).ConfigureAwait(false);
            //await db.SaveChangesAsync();
            //Task.WaitAll();
            //var z = Enumerable.Range(1, 59).ToList();
            //Parallel.ForEach(z, async i => await DoAsync(i, diagnosticData, db));


            //Task.WaitAll();
            //for (var i = 1; i < 60; i++)
            //{
            //    await db.LastMinutes.AddAsync(new LastMinute()
            //    {
            //        Second = i,
            //        PcId = diagnosticData.PcId,
            //        PcNetworkAverageBytesSend = 0,
            //        PcCpuUsage = 0,
            //        PcMemoryUsage = 0,
            //        PcNetworkAverageBytesReceived = 0
            //    }).ConfigureAwait(false);
            //}
            //Task.WaitAll();
        }

        public static async Task AddPcToAdmin(DiagnosticData diagnosticData, string admin, PcHealthContext db)
        {
            var adminFromDb = await db.Admins.Where(a => a.AdminCredentialsUsername.Equals(admin)).FirstOrDefaultAsync().ConfigureAwait(false);
            var adminHasPc = new AdminHasPc()
            {
                PcId = diagnosticData.PcId,
                AdminCredentialsUsername = admin
            };
            adminFromDb.AdminHasPcs.Add(adminHasPc);
        }
        public static async Task<List<Credential>> GetCredentials(PcHealthContext dbContext, NewAccountInfo newAccountInfo)
        {
            return await dbContext.Credentials.Where(c => c.CredentialsUsername == newAccountInfo.CredentialsUsername).ToListAsync().ConfigureAwait(false);
        }

        public static async Task<List<Credential>> GetCredentials(PcHealthContext dbContext, Credential credential)
        {
            return await dbContext.Credentials.Where(c => c.CredentialsUsername == credential.CredentialsUsername).ToListAsync().ConfigureAwait(false);
        }

        public static async Task CreateNewAdmin(PcHealthContext dbContext, NewAccountInfo newAccountInfo)
        {
            var newAdmin = new Admin()
            {
                AdminFirstName = newAccountInfo.AdminFirstName,
                AdminLastName = newAccountInfo.AdminLastName,
                AdminCredentialsUsername = newAccountInfo.CredentialsUsername
            };
            await dbContext.Admins.AddAsync(newAdmin).ConfigureAwait(false);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public static async Task CreateNewCredentials(PcHealthContext dbContext, NewAccountInfo newAccountInfo)
        {
            var (salt, passwordHash) = Services.HashServices.Encrypt(newAccountInfo.CredentialsPassword);
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[5];
            rng.GetBytes(buff);
            var pcCredentialsPassword = Convert.ToBase64String(buff);
            var newCredential = new Credential()
            {
                CredentialsUsername = newAccountInfo.CredentialsUsername,
                CredentialsPassword = passwordHash,
                CredentialsSalt = salt,
                PcCredentialPassword = pcCredentialsPassword
            };
            await dbContext.Credentials.AddAsync(newCredential).ConfigureAwait(false);
            StaticStorageServices.AdminMapper.Add(newAccountInfo.CredentialsUsername, pcCredentialsPassword);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public static async Task<string> GetPasswordSalt(PcHealthContext dbContext, Credential credential)
        {
            var credentials = dbContext.Credentials;
            var neededCred = await credentials.Where(c => c.CredentialsUsername == credential.CredentialsUsername)
                .Select(c => c.CredentialsSalt).FirstAsync().ConfigureAwait(false);
            return neededCred;
        }

        public static async Task<string> GetPasswordFromDb(PcHealthContext dbContext, Credential credential)
        {
            var credentials = dbContext.Credentials;
            var neededCred = await credentials.Where(c => c.CredentialsUsername == credential.CredentialsUsername)
                .Select(c => c.CredentialsPassword).FirstAsync().ConfigureAwait(false);
            return neededCred;
        }

        

        public static async Task UpdatePcInDatabase(PcHealthContext db, DiagnosticData diagnosticData)
        {
            var pc = await db.Pcs.Where(p => p.PcId.Equals(diagnosticData.PcId)).FirstOrDefaultAsync().ConfigureAwait(false);
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
            var admins = await dbContext.Credentials.ToListAsync().ConfigureAwait(false);
            foreach (var admin in admins)
            {
                StaticStorageServices.PcMapper.Add(admin.CredentialsUsername, new Dictionary<string, DiagnosticData>());
                StaticStorageServices.AdminMapper.Add(admin.CredentialsUsername, admin.PcCredentialPassword);
            }

            var adminHasPc = await dbContext.AdminHasPcs.ToListAsync().ConfigureAwait(false);
            foreach (var adminPc in adminHasPc)
            {
                StaticStorageServices.PcMapper[adminPc.AdminCredentialsUsername].Add(adminPc.PcId, new DiagnosticData());
            }
            var pcs = await dbContext.Pcs.ToListAsync().ConfigureAwait(false);
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
