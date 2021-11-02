
using System;
using System.Runtime.InteropServices;


namespace MyCodeLibrary.SystemInfo
{
    /// <summary>
    /// Gets the product type of the operating system running on this Computer.
    /// </summary>
    public static class Edition
    {
        /// <summary>
        /// Returns the product type of the operating system running on this Computer.
        /// </summary>
        /// <returns>A String containing the the operating system product type.</returns>
        public static String asString
        {
            get
            {
                switch (Version.Major)
                {
                    case 5:
                        return GetVersion5();

                    case 6:
                    case 10:
                        return GetVersion6AndUp();
                }
                return String.Empty;
            }
        }

        /// <summary>
        /// Returns the product type from Windows 2000 to XP and Server 2000 to 2003
        /// </summary>
        /// <returns></returns>
        static String GetVersion5()
        {
            
            var osVersionInfo = new MyCodeLibrary.SystemInfo.NativeMethods.OSVERSIONINFOEX
            {
                dwOSVersionInfoSize = Marshal.SizeOf(typeof(MyCodeLibrary.SystemInfo.NativeMethods.OSVERSIONINFOEX))
            };
            if (!NativeMethods.GetVersionEx(ref osVersionInfo)) return String.Empty;

            CExceptionProcessor.ExceptionIfNull(osVersionInfo,"osVersionInfo Cannot Be Null!", "osVersionInfo");

            var Mask = (VERSuite)osVersionInfo.wSuiteMask;

            if (NativeMethods.GetSystemMetrics((int)OtherConsts.SMMediaCenter)) return " Media Center";
            if (NativeMethods.GetSystemMetrics((int)OtherConsts.SMTabletPC)) return " Tablet PC";
            if (CheckIf.IsServer)
            {
                if (Version.Minor == 0)
                {
                    if ((Mask & VERSuite.Datacenter) == VERSuite.Datacenter)
                    {
                        // Windows 2000 Datacenter Server
                        return " Datacenter Server";
                    }
                    if ((Mask & VERSuite.Enterprise) == VERSuite.Enterprise)
                    {
                        // Windows 2000 Advanced Server
                        return " Advanced Server";
                    }
                    // Windows 2000 Server
                    return " Server";
                }
                if (Version.Minor == 2)
                {
                    if ((Mask & VERSuite.Datacenter) == VERSuite.Datacenter)
                    {
                        // Windows Server 2003 Datacenter Edition
                        return " Datacenter Edition";
                    }
                    if ((Mask & VERSuite.Enterprise) == VERSuite.Enterprise)
                    {
                        // Windows Server 2003 Enterprise Edition
                        return " Enterprise Edition";
                    }
                    if ((Mask & VERSuite.StorageServer) == VERSuite.StorageServer)
                    {
                        // Windows Server 2003 Storage Edition
                        return " Storage Edition";
                    }
                    if ((Mask & VERSuite.ComputeServer) == VERSuite.ComputeServer)
                    {
                        // Windows Server 2003 Compute Cluster Edition
                        return " Compute Cluster Edition";
                    }
                    if ((Mask & VERSuite.Blade) == VERSuite.Blade)
                    {
                        // Windows Server 2003 Web Edition
                        return " Web Edition";
                    }
                    // Windows Server 2003 Standard Edition
                    return " Standard Edition";
                }
            }
            else
            {
                if ((Mask & VERSuite.EmbeddedNT) == VERSuite.EmbeddedNT)
                {
                    //Windows XP Embedded
                    return " Embedded";
                }
                // Windows XP / Windows 2000 Professional
                return (Mask & VERSuite.Personal) == VERSuite.Personal ? " Home" : " Professional";
            }
            return String.Empty;
        }

        /// <summary>
        /// Returns the product type from Windows Vista to 10 and Server 2008 to 2016
        /// </summary>
        /// <returns></returns>
        static String GetVersion6AndUp()
        {
            switch ((ProductEdition)getProductInfo())
            {
                case ProductEdition.Ultimate:
                case ProductEdition.UltimateE:
                case ProductEdition.UltimateN:
                    return "Ultimate";

                case ProductEdition.Professional:
                case ProductEdition.ProfessionalE:
                case ProductEdition.ProfessionalN:
                    return "Professional";

                case ProductEdition.HomePremium:
                case ProductEdition.HomePremiumE:
                case ProductEdition.HomePremiumN:
                    return "Home Premium";

                case ProductEdition.HomeBasic:
                case ProductEdition.HomeBasicE:
                case ProductEdition.HomeBasicN:
                    return "Home Basic";

                case ProductEdition.Enterprise:
                case ProductEdition.EnterpriseE:
                case ProductEdition.EnterpriseN:
                case ProductEdition.EnterpriseServerV:
                    return "Enterprise";

                case ProductEdition.Business:
                case ProductEdition.BusinessN:
                    return "Business";

                case ProductEdition.Starter:
                case ProductEdition.StarterE:
                case ProductEdition.StarterN:
                    return "Starter";

                case ProductEdition.ClusterServer:
                    return "Cluster Server";

                case ProductEdition.DatacenterServer:
                case ProductEdition.DatacenterServerV:
                    return "Datacenter";

                case ProductEdition.DatacenterServerCore:
                case ProductEdition.DatacenterServerCoreV:
                    return "Datacenter (Core installation)";

                case ProductEdition.EnterpriseServer:
                    return "Enterprise Server";

                case ProductEdition.EnterpriseServerCore:
                case ProductEdition.EnterpriseServerCoreV:
                    return "Enterprise (Core installation)";

                case ProductEdition.EnterpriseServerIA64:
                    return "Enterprise For Itanium-based Systems";

                case ProductEdition.SmallBusinessServer:
                    return "Small Business Server";

                //case SmallBusinessServerPremium:
                //  return "Small Business Server Premium Edition";

                case ProductEdition.ServerForSmallBusiness:
                case ProductEdition.ServerForSmallBusinessV:
                    return "Windows Essential Server Solutions";

                case ProductEdition.StandardServer:
                case ProductEdition.StandardServerV:
                    return "Standard";

                case ProductEdition.StandardServerCore:
                case ProductEdition.StandardServerCoreV:
                    return "Standard (Core installation)";

                case ProductEdition.WebServer:
                case ProductEdition.WebServerCore:
                    return "Web Server";

                case ProductEdition.MediumBusinessServerManagement:
                case ProductEdition.MediumBusinessServerMessaging:
                case ProductEdition.MediumBusinessServerSecurity:
                    return "Windows Essential Business Server";

                case ProductEdition.StorageEnterpriseServer:
                case ProductEdition.StorageExpressServer:
                case ProductEdition.StorageStandardServer:
                case ProductEdition.StorageWorkgroupServer:
                    return "Storage Server";
            }
            return String.Empty;
        }

        /// <summary>
        /// Gets the product type of the operating system running on this Computer.
        /// </summary>
        public static byte ProductType
        {
            get
            {
                var osVersionInfo = new MyCodeLibrary.SystemInfo.NativeMethods.OSVERSIONINFOEX
                {
                    dwOSVersionInfoSize = Marshal.SizeOf(typeof(NativeMethods.OSVERSIONINFOEX))
                };
                if (!NativeMethods.GetVersionEx(ref osVersionInfo)) 
                    return (int)(MyCodeLibrary.SystemInfo.ProductType.Undefined);//? наобум назначен енум
                return osVersionInfo.wProductType;
            }
        }

        static int getProductInfo()
        {
            return NativeMethods.getProductInfo(Version.Major, Version.Minor);
        }
    }
}
