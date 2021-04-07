using System;
using System.Threading;
using Services;

namespace PC_App.Services
{
    public static class GetPostServices
    {
        public static void PostDiagnosticGetTime()
        {
            while (true)
            {
                try
                {
                    PostServices.PostDiagnosticData("http://omarkar1011-001-site1.dtempurl.com/api/Post/PostDiagnosticDataFromPc");
                    Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}