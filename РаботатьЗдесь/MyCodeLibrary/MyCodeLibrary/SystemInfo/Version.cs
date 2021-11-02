﻿using System;
using System.Globalization;
using System.Windows.Forms;

namespace MyCodeLibrary.SystemInfo
{
    /// <summary>
    /// Gets the full version of the operating system running on this Computer.
    /// </summary>
    ///
    public static class Version
    {
        /// <summary>
        /// Gets the full version of the operating system running on this Computer. Uses the deprecated OSVersion.
        /// </summary>
        ///
        [Obsolete("MainOSV is deprecated, please use Main instead.")]
        public static String MainOSV 
        {
            get { return Environment.OSVersion.Version.ToString();}
        }

        /// <summary>
        /// Gets the full version of the operating system running on this Computer. Uses the newer WMI.
        /// </summary>
        ///
        public static String Main
        {
            get { return GetVersionInfo(VersionType.Main);}
        }

        /// <summary>
        /// Gets the major version of the operating system running on this Computer. Uses the deprecated OSVersion.
        /// </summary>
        [Obsolete("MajorOSV is deprecated, please use Major instead.")]
        public static int MajorOSV 
        { 
            get { return Environment.OSVersion.Version.Major;}
        }

        /// <summary>
        /// Gets the major version of the operating system running on this Computer. Uses the newer WMI.
        /// </summary>
        public static int Major 
        {
            get { return Convert.ToInt32(GetVersionInfo(VersionType.Major), CultureInfo.CurrentCulture);}
        }

        /// <summary>
        /// Gets the minor version of the operating system running on this Computer. Uses the deprecated OSVersion.
        /// </summary>
        [Obsolete("MinorOSV is deprecated, please use Minor instead.")]
        public static int MinorOSV
        {
            get { return Environment.OSVersion.Version.Minor;}
        }

        /// <summary>
        /// Gets the minor version of the operating system running on this Computer. Uses the newer WMI.
        /// </summary>
        public static int Minor 
        {
            get { return Convert.ToInt32(GetVersionInfo(VersionType.Minor), CultureInfo.CurrentCulture);}
        }

        /// <summary>
        /// Gets the build version of the operating system running on this Computer. Uses the deprecated OSVersion.
        /// </summary>
        [Obsolete("BuildOSV is deprecated, please use Build instead.")]
        public static int BuildOSV 
        {
            get { return Environment.OSVersion.Version.Build;}
        }

        /// <summary>
        /// Gets the build version of the operating system running on this Computer. Uses the newer WMI.
        /// </summary>
        public static int Build 
        {
            get { return Convert.ToInt32(GetVersionInfo(VersionType.Build), CultureInfo.CurrentCulture);}
        }

        /// <summary>
        /// Gets the revision version of the operating system running on this Computer. Uses the deprecated OSVersion.
        /// </summary>
        [Obsolete("BuildOSV is deprecated, please use Build instead.")]
        public static int RevisionOSV 
        {
            get { return Environment.OSVersion.Version.Revision;}
        }

        /// <summary>
        /// Gets the revision version of the operating system running on this Computer. Uses the newer WMI.
        /// </summary>
        public static int Revision 
        {
            get { return Convert.ToInt32(GetVersionInfo(VersionType.Revision), CultureInfo.CurrentCulture);}
        }

        /// <summary>
        /// Return a numeric value representing OS version. Uses the deprecated OSVersion.
        /// </summary>
        /// <returns>(OSMajorVersion * 10 + OSMinorVersion)</returns>
        [Obsolete("IntNumOSV is deprecated, please use IntNum instead.")]
        public static int NumberOSV 
        {
            get { return (MajorOSV * 10 + MinorOSV);}
        }

        /// <summary>
        /// Return a numeric value representing OS version. Uses the newer WMI.
        /// </summary>
        /// <returns>(OSMajorVersion * 10 + OSMinorVersion)</returns>
        public static int Number
        {
            get { return (Major * 10 + Minor); }
        }

        internal static String GetVersionInfo(VersionType type)
        {
            try
            {
                String VersionString = String.Empty;
                using (var objMOS = new System.Management.ManagementObjectSearcher("SELECT * FROM  Win32_OperatingSystem"))
                {
                    foreach (var o in objMOS.Get()) { VersionString = o["Version"].ToString(); }//todo: check here
                }

                var Temp = String.Empty;
                var IndexOfPeriod = VersionString.IndexOf(".", StringComparison.CurrentCulture);
                var Major = VersionString.Substring(0, IndexOfPeriod);
                Temp = VersionString.Substring(Major.Length + 1);
                var Minor = Temp.Substring(0, IndexOfPeriod - 1);
                Temp = VersionString.Substring(Major.Length + 1 + Minor.Length + 1);
                String Build;
                String Revision;
                if (Temp.Contains("."))
                {
                    Build = Temp.Substring(0, IndexOfPeriod - 1);
                    Revision = VersionString.Substring(Major.Length + 1 + Minor.Length + 1 + Build.Length + 1);
                }
                else
                {
                    Build = Temp;
                    Revision = "0";
                }
                

                var ReturnString = "0";
                switch (type)
                {
                    case VersionType.Main:
                        ReturnString = VersionString;
                        break;

                    case VersionType.Major:
                        ReturnString = Major;
                        break;

                    case VersionType.Minor:
                        ReturnString = Minor;
                        break;

                    case VersionType.Build:
                        ReturnString = Build;
                        break;

                    case VersionType.Revision:
                        ReturnString = Revision;
                        break;
                }

                if (String.IsNullOrEmpty(ReturnString)) return "0";
                return ReturnString;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "0";
            }
        }

        internal enum VersionType
        {
            Main,
            Major,
            Minor,
            Build,
            Revision
        }
    }
}
