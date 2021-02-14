using System;
using System.Diagnostics;
namespace Network_Windows
{
    public class NetworkInfo
    {
        /// <summary>
        /// Creates a performance Counter object for network interface to monitor current bandwith
        /// </summary>
        private PerformanceCounter bandwidthCounter;
        /// <summary>
        /// Sets bandwith value to 0
        /// </summary>
        private float bandwidth;
        /// <summary>
        /// Creates a performance counter for network interface to monitor data sent
        /// </summary>
        private PerformanceCounter dataSentCounter; 
        /// <summary>
        /// creates a performance counter for network interface to monitor data received
        /// </summary>
        private PerformanceCounter dataReceivedCounter;
        /// <summary>
        /// A variable to store the percentage of network traffic
        /// </summary>
        private double NetworkTrafficPercentage;
        private String[] models;
        public PerformanceCounter BandwidthCounter { get => bandwidthCounter; set => bandwidthCounter = value; }
        public float Bandwidth { get => bandwidth; set => bandwidth = value; }
        public PerformanceCounter DataSentCounter { get => dataSentCounter; set => dataSentCounter = value; }
        public PerformanceCounter DataReceivedCounter { get => dataReceivedCounter; set => dataReceivedCounter = value; }
        public string[] Models { get => models; set => models = value; }

        public NetworkInfo()
        {
            bandwidthCounter = new PerformanceCounter();
            bandwidth = BandwidthCounter.NextValue(); ;
            NetworkTrafficPercentage = updateNetworkUsage();
            dataSentCounter = new PerformanceCounter();
            dataReceivedCounter = new PerformanceCounter();
            BandwidthCounter.CategoryName = "Network Interface";
            BandwidthCounter.CounterName = "Current Bandwith";
            DataSentCounter.CategoryName = "Network Interface";
            DataSentCounter.CounterName = "Bytes Sent/sec";
            DataReceivedCounter.CategoryName = "Network Interface";
            DataReceivedCounter.CounterName = "Bytes Received/sec";
            PerformanceCounterCategory category = new PerformanceCounterCategory("Network Interface"); //creates a category object representing the network interface
            Models = category.GetInstanceNames(); //array that has the models of network devices
        }

        /// <summary>
        /// Prints network traffic percentage
        /// Contains a for loop to set the model for each counter and print the network usage for each model
        /// </summary>
        /// <returns></returns>
        public double updateNetworkUsage() 
        {

            foreach (String model in Models) 
            {
                BandwidthCounter.InstanceName = model;
                Bandwidth = BandwidthCounter.NextValue(); 
                DataSentCounter.InstanceName = model;
                DataReceivedCounter.InstanceName = model;
                float datasent = DataSentCounter.NextValue(); 
                float datareceived = DataReceivedCounter.NextValue();
                if (Bandwidth != 0)
                {
                    NetworkTrafficPercentage = (8 * (datasent + datareceived)) / Bandwidth * 100;
                }
            }
            return NetworkTrafficPercentage;
        }
    }
}