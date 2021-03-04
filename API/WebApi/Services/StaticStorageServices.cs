using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Services
{
    public static class StaticStorageServices
    {
        public static Dictionary<string, Diagnostic_Data> PC_Mapper = new Dictionary<string, Diagnostic_Data>();
    }
}