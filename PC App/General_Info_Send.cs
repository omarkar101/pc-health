using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using PC_App.General_Info;
using PC_App.Client_Side;

namespace PC_App
{
    public static class General_Info_Send
    {
        public static async Task Start<T>(T msg, int port = 9000)
        {
            var endPoint = new IPEndPoint(IPAddress.Loopback, port);

            var socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            socket.Connect(endPoint);

            var networkStream = new NetworkStream(socket, true);
            
            await SendAsync.Start(networkStream, msg).ConfigureAwait(false);
            
            
        }
    }
}