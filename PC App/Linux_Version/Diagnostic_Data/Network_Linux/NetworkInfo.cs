using System.Net.NetworkInformation;

namespace PC_App.Linux_Version.Diagnostic_Data.Network_Linux
{
    /// <summary>
    /// Linux Network Info
    /// </summary>
    public class NetworkInfo
    {
        /// <summary>
        /// Stores network Bytes Sent
        /// </summary>
        private double _avgNetworkBytesSent;

        /// <summary>
        /// Getter for network Bytes Sent
        /// </summary>
        public double AvgNetworkBytesSent => UpdateNetworkInfo().AvgBytesSent;

        /// <summary>
        /// Stores network Bytes Received 
        /// </summary>
        private double _avgNetworkBytesReceived;

        /// <summary>
        /// Getter for network Bytes Received
        /// </summary>
        public double AvgNetworkBytesReceived => UpdateNetworkInfo().AvgBytesReceived;
        
        
        
        /// <summary>
        /// Constructor which updates the Network Info
        /// </summary>
        public NetworkInfo()
        {
            UpdateNetworkInfo();
        }
        
        
        private (double AvgBytesSent, double AvgBytesReceived) UpdateNetworkInfo()
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

            _avgNetworkBytesReceived = bytesReceivedCounter / (interfaces.Length - 1);
            _avgNetworkBytesSent = bytesSentCounter / (interfaces.Length - 1);
            return (_avgNetworkBytesSent, _avgNetworkBytesReceived);
        }
        
            
    }
}