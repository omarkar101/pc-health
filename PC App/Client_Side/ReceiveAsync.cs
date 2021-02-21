using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace PC_App.Client_Side
{
    /// <summary>
    /// Receives a Byte message from the server
    /// </summary>
    public static class ReceiveAsync
    {
        /// <summary>
        /// Main function that receives the message.
        /// </summary>
        /// <param name="networkStream"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>The decoded msg received</returns>
        public static async Task<T> Start<T>(NetworkStream networkStream)
        {
            var headerBytes = await ReadAsync(networkStream, 4);
            
            var bodyLength = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(headerBytes));

            var bodyBytes = await ReadAsync(networkStream, bodyLength);
            
            return Decode<T>(bodyBytes);
        }
        
        /// <summary>
        /// Decodes the data received using Json Deserializer from bytes to T message
        /// </summary>
        /// <param name="body"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>Deserialized Message of type T</returns>
        private static T Decode<T>(byte[] body)
        {
            var deserializer = new DataContractJsonSerializer(typeof(T));
            var memoryStream = new MemoryStream(body);
            return (T) deserializer.ReadObject(memoryStream);
        }
        
        /// <summary>
        /// Reads the bytes of the message received from the network stream 
        /// </summary>
        /// <param name="networkStream"></param>
        /// <param name="bytesToRead"></param>
        /// <returns>Bytes of the message received</returns>
        /// <exception cref="Exception"></exception>
        private static async Task<byte[]> ReadAsync(Stream networkStream, int bytesToRead)
        {
            var buffer = new byte[bytesToRead];
            var bytesRead = 0;

            while(bytesRead < bytesToRead)
            {
                var bytesReceived = await networkStream.ReadAsync(buffer.AsMemory(bytesRead, bytesToRead - bytesRead)).ConfigureAwait(false);
                if(bytesReceived == 0)
                {
                    throw new Exception("socket closed");
                }
                bytesRead += bytesReceived;
            }
            return buffer;
        }
    }
}