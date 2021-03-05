using System.Net.NetworkInformation;

namespace PC_App.Common_Version.Diagnostic_Data.Network
{
    /// <summary>
    /// Linux Network Info
    /// </summary>
    public static class NetworkInfo
    {
        /// <summary>
        /// Getter for updated average network Bytes Sent
        /// </summary>
        public static double AvgNetworkBytesSent => UpdateNetworkInfo().AvgBytesSent;

        /// <summary>
        /// Getter for updated average network Bytes Received
        /// </summary>
        public static double AvgNetworkBytesReceived => UpdateNetworkInfo().AvgBytesReceived;
        
        /// <summary>
        /// Updates the Average Network Bytes sent and received
        /// </summary>
        /// <returns>updated Average bytes sent and received</returns>
        private static (double AvgBytesSent, double AvgBytesReceived) UpdateNetworkInfo()
        {
            if (!NetworkInterface.GetIsNetworkAvailable()) return (0, 0);
            var interfaces = NetworkInterface.GetAllNetworkInterfaces();
            var bytesSentCounter = 0.0;
            var bytesReceivedCounter = 0.0;
            var cnt_NonZero_interfaces = 0.0;
            foreach(var ni in interfaces)
            {
                var bytesReceived = ni.GetIPv4Statistics().BytesReceived;
                var bytesSent = ni.GetIPv4Statistics().BytesSent;
                bytesReceivedCounter += bytesReceived;
                bytesSentCounter += bytesSent;

                if (bytesReceived != 0 && bytesSent != 0) cnt_NonZero_interfaces++;
            }
            
            var _avgNetworkBytesReceived = bytesReceivedCounter / (cnt_NonZero_interfaces);
            var _avgNetworkBytesSent = bytesSentCounter / (cnt_NonZero_interfaces);
            return (_avgNetworkBytesSent, _avgNetworkBytesReceived);
        }
        
            
    }
}