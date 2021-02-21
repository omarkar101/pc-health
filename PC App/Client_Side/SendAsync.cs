using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace PC_App.Client_Side
{
    /// <summary>
    /// Sends a message of type T to the server
    /// </summary>
    public static class SendAsync
    {
        /// <summary>
        /// Main function that sends the message.
        /// </summary>
        /// <param name="networkStream"></param>
        /// <param name="msg"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task Start<T>(NetworkStream networkStream, T msg)
        {
            var (header, body) = Encode(msg);
            await networkStream.WriteAsync(header, 0, header.Length).ConfigureAwait(false);
            await networkStream.WriteAsync(body, 0, body.Length).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Encodes the Message to bytes inorder to be sent through the network stream
        /// </summary>
        /// <param name="msg"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static (byte[] header, byte[] body) Encode<T>(T msg)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            var memoryStream = new MemoryStream();
            
            serializer.WriteObject(memoryStream, msg);
            memoryStream.Position = 0;
            var sr = new StreamReader(memoryStream);
            
            var bodyBytes = System.Text.Encoding.UTF8.GetBytes(sr.ReadToEnd());
    
            var headerBytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(bodyBytes.Length));
            

            return (headerBytes, bodyBytes);
        }
        
    }
}