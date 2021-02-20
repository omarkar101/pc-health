using System;
using System.Diagnostics;
namespace Network_Windows
{
    public class NetworkInfo
    {
<<<<<<< HEAD:PC App/Windows_Version/Diagnostic_Data/Network/NetworkInfo.cs
        private PerformanceCounter bandwidthCounter = new PerformanceCounter(); //creates a performance counter for network interface to monitor current bandwith
        private float bandwidth;//sets bandwith value to 0
        private PerformanceCounter dataSentCounter = new PerformanceCounter(); //creates a performance counter for network interface to monitor data sent
        private PerformanceCounter dataReceivedCounter = new PerformanceCounter();//creates a performance counter for network interface to monitor data received
=======
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
>>>>>>> 4e86ff184bcc0530e3f113ab835bf2fdef8e635b:PC App/Windows_Version/Diagnostic_Data/Network_Windows/NetworkInfo.cs
        private double NetworkTrafficPercentage;
        public PerformanceCounter BandwidthCounter { get => bandwidthCounter; set => bandwidthCounter = value; }
        public float Bandwidth { get => bandwidth; set => bandwidth = value; }
        public PerformanceCounter DataSentCounter { get => dataSentCounter; set => dataSentCounter = value; }
        public PerformanceCounter DataReceivedCounter { get => dataReceivedCounter; set => dataReceivedCounter = value; }
        public NetworkInfo()
        {
            bandwidthCounter.CategoryName = "Network Interface";
            bandwidthCounter.CounterName = "Current Bandwidth";
            DataSentCounter.CategoryName = "Network Interface";
            DataSentCounter.CounterName = "Bytes Sent/sec";
            DataReceivedCounter.CategoryName = "Network Interface";
            DataReceivedCounter.CounterName = "Bytes Received/sec";
            
            NetworkTrafficPercentage = updateNetworkUsage();
            
        }

<<<<<<< HEAD:PC App/Windows_Version/Diagnostic_Data/Network/NetworkInfo.cs
        public double updateNetworkUsage() //prints a percentage showing network traffic
=======
        /// <summary>
        /// Prints network traffic percentage
        /// Contains a for loop to set the model for each counter and print the network usage for each model
        /// </summary>
        /// <returns></returns>
        public double updateNetworkUsage() 
>>>>>>> 4e86ff184bcc0530e3f113ab835bf2fdef8e635b:PC App/Windows_Version/Diagnostic_Data/Network_Windows/NetworkInfo.cs
        {
            PerformanceCounterCategory category = new PerformanceCounterCategory("Network Interface"); //creates a category object representing the network interface
            String[] models = new String[category.GetInstanceNames().Length]; //array that has the models of network devices
            models = category.GetInstanceNames();

<<<<<<< HEAD:PC App/Windows_Version/Diagnostic_Data/Network/NetworkInfo.cs
            foreach (String model in models) //this for loop sets the model for each counter and prints the network usage for each model
=======
            foreach (String model in Models) 
>>>>>>> 4e86ff184bcc0530e3f113ab835bf2fdef8e635b:PC App/Windows_Version/Diagnostic_Data/Network_Windows/NetworkInfo.cs
            {
                BandwidthCounter.InstanceName = model;
                Bandwidth = BandwidthCounter.NextValue(); 
                DataSentCounter.InstanceName = model;
                DataReceivedCounter.InstanceName = model;
<<<<<<< HEAD:PC App/Windows_Version/Diagnostic_Data/Network/NetworkInfo.cs
                float datasent = DataSentCounter.NextValue(); //sets datasent to datasentCounter's value
                float datareceived = DataReceivedCounter.NextValue(); //sets data received to dataReceived's value
                bandwidth = bandwidthCounter.NextValue();
=======
                float datasent = DataSentCounter.NextValue(); 
                float datareceived = DataReceivedCounter.NextValue();
>>>>>>> 4e86ff184bcc0530e3f113ab835bf2fdef8e635b:PC App/Windows_Version/Diagnostic_Data/Network_Windows/NetworkInfo.cs
                if (Bandwidth != 0)
                {
                    NetworkTrafficPercentage = (8 * (datasent + datareceived)) / Bandwidth * 100;
                }
            }
            return NetworkTrafficPercentage;
        }
    }
}