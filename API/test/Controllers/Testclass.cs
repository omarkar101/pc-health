using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
namespace test{
    public class WebHelper
{
   public async Task<stats> MakeGetRequest(HttpClient client, string route)
   {
       
      try{
          
            var statTask = client.GetStreamAsync("https://localhost:5001/api/Test/Hi");
            var retrievedstats = await JsonSerializer.DeserializeAsync<stats>(await statTask);
            return retrievedstats;
      }
      catch (Exception ex){
         return new stats();
    }
  }
}
}