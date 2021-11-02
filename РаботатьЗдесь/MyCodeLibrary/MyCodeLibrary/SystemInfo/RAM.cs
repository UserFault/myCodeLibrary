using System;

namespace MyCodeLibrary.SystemInfo
{
    /// <summary>
    /// RAM Information
    /// </summary>
    public static class RAM
    {
        /// <summary>
        /// Returns the total ram installed on the Computer.
        /// </summary>
        public static String GetTotalRam
        {
            get
            {
                try
                {
                    long installedMemory = 0;
                    NativeMethods.GetPhysicallyInstalledSystemMemory(out installedMemory);
                    return CNumberProcessor.ConvertKilobytes((double)installedMemory);
                }
                catch (NullReferenceException)
                {
                    return String.Empty;
                }
            }
        }
    }
}
