﻿using System;
using System.IO;

namespace MyCodeLibrary.SystemInfo
{
    /// <summary>
    /// Returns information about the currently running program.
    /// </summary>
    public static class ProgramInfo
    {
        /// <summary>
        /// Returns the title of the currently running program.
        /// </summary>
        public static String Title
        {
            get
            {
                var attributes = System.Reflection.Assembly.GetCallingAssembly().GetCustomAttributes(typeof(System.Reflection.AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    var titleAttribute = (System.Reflection.AssemblyTitleAttribute)attributes[0];
                    if (!String.IsNullOrEmpty(titleAttribute.Title)) return titleAttribute.Title;
                }
                return Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        /// <summary>
        /// Returns the version number of the currently running program.
        /// </summary>
        public static String Version 
        {
            get { return System.Reflection.Assembly.GetCallingAssembly().GetName().Version.ToString(); }
        }

        /// <summary>
        /// Returns the description of the currently running program.
        /// </summary>
        public static String Description
        {
            get
            {
                var attributes = System.Reflection.Assembly.GetCallingAssembly().GetCustomAttributes(typeof(System.Reflection.AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0) return String.Empty;
                return ((System.Reflection.AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        /// <summary>
        /// Returns the product name of the currently running program.
        /// </summary>
        public static String Product
        {
            get
            {
                var attributes = System.Reflection.Assembly.GetCallingAssembly().GetCustomAttributes(typeof(System.Reflection.AssemblyProductAttribute), false);
                if (attributes.Length == 0) return String.Empty;
                return ((System.Reflection.AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        /// <summary>
        /// Returns the copyright info of the currently running program.
        /// </summary>
        public static String Copyright
        {
            get
            {
                var attributes = System.Reflection.Assembly.GetCallingAssembly().GetCustomAttributes(typeof(System.Reflection.AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0) return String.Empty;
                return ((System.Reflection.AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        /// <summary>
        /// Returns the company name of the currently running program.
        /// </summary>
        public static String Company
        {
            get
            {
                var attributes = System.Reflection.Assembly.GetCallingAssembly().GetCustomAttributes(typeof(System.Reflection.AssemblyCompanyAttribute), false);
                if (attributes.Length == 0) return String.Empty;
                return ((System.Reflection.AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

        /// <summary>
        /// Returns the folder path of the currently running program.
        /// </summary>
        public static String StartupPath 
        {
            get { return System.Windows.Forms.Application.StartupPath; }
        }

        /// <summary>
        /// Returns the full path of the currently running program.
        /// </summary>
        public static String ExecutablePath
        {
            get { return System.Windows.Forms.Application.ExecutablePath; }
        }

        /// <summary>
        /// Allows an Embedded resource to be extracted.
        /// </summary>
        /// <param name="resourceName"></param>
        /// <param name="fileToExtractTo"></param>
        public static void SaveResourceToDisk(String resourceName, String fileToExtractTo)
        {
            var s = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            using (var resourceFile = new FileStream(fileToExtractTo, FileMode.Create))
            {
                var b = new byte[s.Length + 1];
                s.Read(b, 0, Convert.ToInt32(s.Length));
                resourceFile.Write(b, 0, Convert.ToInt32(b.Length - 1));
                resourceFile.Flush();
            }
        }
    }
}
