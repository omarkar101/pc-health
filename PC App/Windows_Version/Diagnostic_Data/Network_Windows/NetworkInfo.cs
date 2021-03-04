using System;
using System.Diagnostics;
using System.Threading;
namespace Network_Windows
{
    public static class NetworkInfo
    {
        public static double NetworkPercentage {get => updateNetwork()[0];}
        public static double DataSent {get => updateNetwork()[1];}
        public static double DataReceived{get =>updateNetwork()[2];}
        
        private static double[] updateNetwork() //prints a percentage showing network traffic
        {
            double[] result = new double[3];
            PerformanceCounter bandwidthCounter = new PerformanceCounter(); //creates a performance counter for network interface to monitor current bandwith
            float bandwidth = 0;//sets bandwith value to 0
            PerformanceCounter dataSentCounter = new PerformanceCounter(); //creates a performance counter for network interface to monitor data sent
            PerformanceCounter dataReceivedCounter = new PerformanceCounter();//creates a performance counter for network interface to monitor data received
            double NetworkTrafficPercentage = 0;
            bandwidthCounter.CategoryName = "Network Interface";
            bandwidthCounter.CounterName = "Current Bandwidth";
            dataSentCounter.CategoryName = "Network Interface";
            dataSentCounter.CounterName = "Bytes Sent/sec";
            dataReceivedCounter.CategoryName = "Network Interface";
            dataReceivedCounter.CounterName = "Bytes Received/sec";
            PerformanceCounterCategory category = new PerformanceCounterCategory("Network Interface"); //creates a category object representing the network interface
            String[] models = new String[category.GetInstanceNames().Length]; //array that has the models of network devices
            models = category.GetInstanceNames();
            double datasent = 0;
            double datareceived =0;
            foreach (String model in models) //this for loop sets the model for each counter and prints the network usage for each model
            {
                bandwidthCounter.InstanceName = model;
                bandwidth = bandwidthCounter.NextValue(); //sets bandwith to bandwidth counter's value
                dataSentCounter.InstanceName = model;
                dataReceivedCounter.InstanceName = model;
                float firstdatasent = dataSentCounter.NextValue(); //sets datasent to datasentCounter's value
                float firstdatareceived = dataReceivedCounter.NextValue(); //sets data received to dataReceived's value
                System.Threading.Thread.Sleep(500);
                datareceived = dataReceivedCounter.NextValue();
                datasent = dataSentCounter.NextValue();
                bandwidth = bandwidthCounter.NextValue();
                if (bandwidth != 0 & (datareceived != 0 | datasent != 0))
                {
                    NetworkTrafficPercentage = (8 * (datasent + datareceived)) / bandwidth * 100; // calculates the percentage of network traffic
                    result[0] = NetworkTrafficPercentage;
                    result[1] = datasent;
                    result[2] = datareceived;
                }
            }
            
            return result;
        }
    }
}