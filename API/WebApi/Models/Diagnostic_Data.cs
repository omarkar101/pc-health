namespace WebApi.Models {

    public class Diagnostic_Data{
        
        public float CpuUsage{get;set;}

        public float TotalFreeDiskSpace{get;set;}

        public float DiskUsage{get;set;}

        public double MemoryUsage{get;set;}

        public double AvgNetworkBytesSent {get; set;}

        public double AvgNetworkBytesReceived {get; set;}

    }
}