using System;
using System.Diagnostics;
namespace Network_Windows
{
    public class NetworkInfo
    {
        public static PerformanceCounter bandwidthCounter = new PerformanceCounter("Network Interface", "Current Bandwidth"); //creates a performance counter for network interface to monitor current bandwith
        public static float bandwidth = 0;//sets bandwith value to 0
        public static PerformanceCounter dataSentCounter = new PerformanceCounter("Network Interface", "Bytes Sent/sec"); //creates a performance counter for network interface to monitor data sent
        public static PerformanceCounter dataReceivedCounter = new PerformanceCounter("Network Interface", "Bytes Received/sec");////creates a performance counter for network interface to monitor data received
        public static void printNetworkUsage() //prints a percentage showing network traffic
        {
            PerformanceCounterCategory category = new PerformanceCounterCategory("Network Interface"); //creates a category object representing the network interface
            String[] models = category.GetInstanceNames(); //creates an array that has the models of network devices
            foreach (String model in models) //this for loop sets the model for each counter and prints the network usage for each model
            {
                bandwidthCounter.InstanceName = model;
                bandwidth = bandwidthCounter.NextValue(); //sets bandwith to bandwidth counter's value
                dataSentCounter.InstanceName = model;
                dataReceivedCounter.InstanceName = model;
                float datasent = dataSentCounter.NextValue(); //sets datasent to datasentCounter's value
                float datareceived = dataReceivedCounter.NextValue(); //sets data received to dataReceived's value
                if (bandwidth != 0)
                {
                    double utilization = (8 * (datasent + datareceived)) / bandwidth * 100; // calculates the percentage of network traffic
                    Console.WriteLine("Traffic utilization percentage on model : " + model + " is " + utilization + "%");
                }
            }
        }
    }
}