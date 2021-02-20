using System;

namespace Memory_Linux
{
    /// <summary>
    /// A class that is responsible for memmory informaiton on PCs using Linux OS
    /// </summary>
    public class MemoryInfo
    {
        /// <summary>
        /// A string that stores memmory file location
        /// </summary>
        private string memoryLoc;
        /// <summary>
        /// String array that stores memmory information
        /// </summary>
        private string[] memoryInfoLines;
        /// <summary>
        /// Default constructor that stores the default location of the memmory information file
        /// </summary>
        public MemoryInfo()
        {
            memoryLoc = "/proc/meminfo";
            memoryInfoLines = System.IO.File.ReadAllLines(memoryLoc);
        }
        /// <summary>
        /// A method to print memmory information on the screen
        /// </summary>
        public void printMemoryInfo()
        {
            foreach(var line in memoryInfoLines)
            {
                Console.WriteLine(line);
            }
        }
        /// <summary>
        /// Getter/Setter for the file location
        /// </summary>
        /// <value>gets/sets file location</value>
        public string MemoryInfoLoc { get => memoryLoc; set => memoryLoc = value; }
        /// <summary>
        /// Getter/Setter to the info inside the located file
        /// </summary>
        /// <value></value>
        public string[] MemoryInfoLines { get => memoryInfoLines; set => memoryInfoLines = value; }
    }
}