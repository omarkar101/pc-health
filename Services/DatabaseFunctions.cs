using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    class DatabaseFunctions<T>
    {
        public static List<T> GetCredentials(DbContext dbContext)
        {
            return dbContext.Credentials.Where(c => c.CredentialsUsername == newAccountInfo.CredentialsUsername).ToList();
        }
    }
}
