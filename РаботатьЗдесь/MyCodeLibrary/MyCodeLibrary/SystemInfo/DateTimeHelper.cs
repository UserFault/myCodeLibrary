using System;
using System.Globalization;

namespace MyCodeLibrary.SystemInfo
{
    /// <summary>
    /// Returns data information
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// Returns current timestamp
        /// </summary>
        public static String CurrentShortTimeStamp 
        {
            get { return DateTime.Now.ToString("MM/dd/yy HH:mm:ss tt", CultureInfo.CurrentCulture);}
        }

        /// <summary>
        /// Returns current timestamp extended
        /// </summary>
        public static String CurrentFullTimeStamp
        {
            get { return DateTime.Now.ToString("ddd MMMM dd, yyyy hh:mm:ss tt", CultureInfo.CurrentCulture); }
        }
    }
}
