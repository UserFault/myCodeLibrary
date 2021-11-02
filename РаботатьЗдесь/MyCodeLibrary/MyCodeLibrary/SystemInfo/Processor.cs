using System;


namespace MyCodeLibrary.SystemInfo
{
    /// <summary>
    /// Processor Information
    /// </summary>
    public static class Processor
    {
        /// <summary>
        /// Returns the system processor name that is stored in the registry.
        /// </summary>
        public static String Name
        {
            get
            {
                const String key = "HARDWARE\\DESCRIPTION\\System\\CentralProcessor\\0";
                const String value = "ProcessorNameString";
                return RegistryInfo.getStringValue(HKEY.LOCAL_MACHINE, key, value);
            }
        }

        /// <summary>
        /// Returns the number of cores available on the system processor.
        /// </summary>
        public static int Cores
        {
            get
            {
                return Environment.ProcessorCount;
            }
        }
    }
}
