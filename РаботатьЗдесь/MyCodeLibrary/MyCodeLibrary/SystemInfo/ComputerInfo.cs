using System;
using System.IO;

namespace MyCodeLibrary.SystemInfo
{
    /// <summary>
    /// Creates an object that holds info about the computer.
    /// </summary>
    public class ComputerInfo
    {
        /// <summary>
        /// Constructor initializes values();
        /// </summary>
        public ComputerInfo()
        {
            Hardware = ReinitializeHardware();
            OS = ReinitalizeOS();
        }

        /// <summary>
        /// List of posible computers
        /// </summary>
        public enum ComputerList
        {
            /// <summary>
            /// Localhost
            /// </summary>
            Localhost
        }

        /// <summary>
        /// Returns information about the Computers hardware.
        /// </summary>
        public HWObject Hardware { get; set; }
        /// <summary>
        /// Returns information about the Computers operating system.
        /// </summary>
        public OSObject OS { get; set; }

        /// <summary>
        /// Initalizes the hardware class.
        /// </summary>
        /// <returns></returns>
        public static HWObject ReinitializeHardware()
        {
            String error = String.Empty;
            var Hardware = new HWObject
            {
                SystemOEM = OEM.Name,
                ProductName = OEM.ProductName,
                #region BIOS
                BIOS = new BIOSObject
                {
                    Name = BIOS.Name,
                    ReleaseDate = BIOS.ReleaseDate,
                    Vendor = BIOS.Vendor,
                    Version = BIOS.Version
                },
                #endregion
                #region Network
                Network = new NetworkObject
                {
                    ConnectionStatus = Network.ConnectionStatus,
                    InternalIPAddress = Network.InternalIPAddress,
                    ExternalIPAddress = Network.ExternalIPAddress(out error)
                },
                #endregion
                Processor = new ProcessorObject { Name = Processor.Name, Cores = Processor.Cores },
                RAM = new RAMObject { TotalInstalled = RAM.GetTotalRam }
            };

            #region Storage
            var Storage = new StorageObject();

            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                var drivetype = String.Empty;
                var ActiveDrive = false;
                if (drive.IsReady)
                {
                    if (drive.DriveType == DriveType.Fixed)
                    {
                        try
                        {
                            if (drive.TotalSize != 0.0 && drive.TotalFreeSpace != 0.0)
                            {
                                ActiveDrive = true; drivetype = "Fixed";
                            }
                        }
                        catch (Exception) { throw; }
                    }
                    if (drive.DriveType == DriveType.Removable)
                    {
                        try
                        {
                            if (drive.TotalSize != 0.0 && drive.TotalFreeSpace != 0.0)
                            {
                                ActiveDrive = true; drivetype = "Removable";
                            }
                        }
                        catch (Exception) { throw; }
                    }

                    if (ActiveDrive)
                    {
                        var newdrive = new DriveObject
                        {
                            Name = drive.Name,
                            Format = drive.DriveFormat,
                            Label = drive.VolumeLabel,
                            TotalSize = CNumberProcessor.ConvertBytes(Convert.ToDouble(drive.TotalSize)),
                            TotalFree = CNumberProcessor.ConvertBytes(Convert.ToDouble(drive.AvailableFreeSpace)),
                            DriveType = drivetype
                        };
                        Storage.InstalledDrives.Add(newdrive);
                        if (drive.Name.Trim() == MyCodeLibrary.SystemInfo.Storage.SystemDrivePath)
                        {
                            Storage.SystemDrive = newdrive;
                        }
                    }
                }
            }

            Hardware.Storage = Storage;
            #endregion

            return Hardware;
        }
        /// <summary>
        /// Initalizes the software class.
        /// </summary>
        /// <returns></returns>
        public static OSObject ReinitalizeOS()
        {
            return new OSObject
            {
                ComputerName = Name.ComputerNameActive,
                ComputerNamePending = Name.ComputerNamePending,
                DomainName = UserInfo.CurrentDomainName,
                LoggedInUserName = UserInfo.LoggedInUserName,
                RegisteredOrganization = UserInfo.RegisteredOrganization,
                RegisteredOwner = UserInfo.RegisteredOwner,
                InstallInfo = new InstallInfoObject
                {
                    ActivationStatus = CheckIf.IsActivatedWMI,
                    Architecture = Architecture.asString,
                    NameExpanded = Name.StringExpanded,
                    Name = Name.asString,
                    ProductKey = ProductKey.Key,
                    ServicePack = ServicePack.String,
                    ServicePackNumber = ServicePack.Number,
                    Version = new VersionObject
                    {
                        Build = Version.Build,
                        Main = Version.Main,
                        Major = Version.Major,
                        Minor = Version.Minor,
                        Number = Version.Number,
                        Revision = Version.Revision
                    }
                }
            };
        }
    }
}
