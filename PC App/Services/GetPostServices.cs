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
                    PostServices.PostDiagnosticData("https://localhost:44335/api/Post/PostDiagnosticDataFromPc");
                    Thread.Sleep(500);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}