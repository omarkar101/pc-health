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
        private double _networkBytesSent;

        /// <summary>
        /// Getter for network Bytes Sent
        /// </summary>
        public double NetworkBytesSent => UpdateNetworkInfo().BytesSent;

        /// <summary>
        /// Stores network Bytes Received 
        /// </summary>
        private double _networkBytesReceived;

        /// <summary>
        /// Getter for network Bytes Received
        /// </summary>
        public double NetworkBytesReceived => UpdateNetworkInfo().BytesReceived;

        /// <summary>
        /// Constructor which updates the Network Info
        /// </summary>
        public NetworkInfo()
        {
            UpdateNetworkInfo();
        }

        private (double BytesSent, double BytesReceived) UpdateNetworkInfo()
        {
            if (!NetworkInterface.GetIsNetworkAvailable()) return (0, 0);
            var interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach(var ni in interfaces)
            {
                _networkBytesReceived = ni.GetIPv4Statistics().BytesReceived;
                _networkBytesSent = ni.GetIPv4Statistics().BytesSent;
            }
            return (_networkBytesSent, _networkBytesReceived);
        }
        
            
    }
}