using System;

namespace Memory
{
    public class MemoryInfo
    {
        private string memoryLoc;
        private string[] memoryInfoLines;
        public MemoryInfo()
        {
            memoryLoc = "/proc/meminfo";
            memoryInfoLines = System.IO.File.ReadAllLines(memoryLoc);
        }

        public MemoryInfo(string memoryLoc, string[] memoryInfoLines)
        {
            this.memoryLoc = memoryLoc;
            this.memoryInfoLines = memoryInfoLines;
        }

        public void printMemoryInfo()
        {
            foreach(var line in memoryInfoLines)
            {
                Console.WriteLine(line);
            }
        }
        public string MemoryInfoLoc { get => memoryLoc; set => memoryLoc = value; }
        public string[] MemoryInfoLines { get => memoryInfoLines; set => memoryInfoLines = value; }
    }
}