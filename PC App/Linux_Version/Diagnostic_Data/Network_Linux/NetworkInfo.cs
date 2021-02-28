using System.Net.NetworkInformation;

namespace Network_Linux
{
    /// <summary>
    /// Linux Network Info
    /// </summary>
    public static class NetworkInfo
    {
        /// <summary>
        /// Getter for network Bytes Sent
        /// </summary>
        public static double AvgNetworkBytesSent => UpdateNetworkInfo().AvgBytesSent;

        /// <summary>
        /// Getter for network Bytes Received
        /// </summary>
        public static double AvgNetworkBytesReceived => UpdateNetworkInfo().AvgBytesReceived;
        
        
        private static (double AvgBytesSent, double AvgBytesReceived) UpdateNetworkInfo()
        {
            if (!NetworkInterface.GetIsNetworkAvailable()) return (0, 0);
            var interfaces = NetworkInterface.GetAllNetworkInterfaces();
            var bytesSentCounter = 0.0;
            var bytesReceivedCounter = 0.0;
            foreach(var ni in interfaces)
            {
                bytesReceivedCounter += ni.GetIPv4Statistics().BytesReceived;
                bytesSentCounter += ni.GetIPv4Statistics().BytesSent;
            }
            
            var _avgNetworkBytesReceived = bytesReceivedCounter / (interfaces.Length - 1);
            var _avgNetworkBytesSent = bytesSentCounter / (interfaces.Length - 1);
            return (_avgNetworkBytesSent, _avgNetworkBytesReceived);
        }
        
            
    }
}
