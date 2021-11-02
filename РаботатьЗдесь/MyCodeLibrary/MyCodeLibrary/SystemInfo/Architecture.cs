using System;

namespace MyCodeLibrary.SystemInfo
{
    /// <summary>
    /// Determines if the current application is 32 or 64-bit.
    /// </summary>
    public static class Architecture
    {
        /// <summary>
        /// Determines if the current application is 32 or 64-bit.
        /// </summary>
        public static String asString 
        {
            get { return CheckIf.Is64BitOS ? "64 bit" : "32 bit"; }
        }

        /// <summary>
        /// Determines if the current application is 32 or 64-bit.
        /// </summary>
        public static int asNumber
        {
            get { return CheckIf.Is64BitOS ? 64 : 32; }
        }

    }
}
