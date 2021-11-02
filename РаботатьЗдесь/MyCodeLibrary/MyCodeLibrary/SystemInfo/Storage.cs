﻿
using System;
using System.IO;
using System.Collections.Generic;

namespace MyCodeLibrary.SystemInfo
{
    /// <summary>
    /// Storage Information
    /// </summary>
    public static class Storage
    {
        /// <summary>
        /// Returns list of installed drives and their information
        /// </summary>
        public static List<DriveObject> InstalledDrives
        {
            get
            {
                List<DriveObject> Drives = new List<DriveObject>();
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
                            Drives.Add(newdrive);
                            if (drive.Name.Trim() == SystemDrivePath) SystemDrive = newdrive;
                        }
                    }
                }
                return Drives;
            }
        }

        /// <summary>
        /// Returns information about the drive Windows is installed on.
        /// </summary>
        public static DriveObject SystemDrive { get; internal set; }

        internal static String SystemDrivePath = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System));
        /// <summary>
        /// Returns the drive size of the drive Windows is installed on.
        /// </summary>
        public static String GetSystemDriveSize
        {
            get
            {
                try
                {
                    foreach (DriveInfo drive in DriveInfo.GetDrives())
                    {
                        return drive.IsReady && drive.Name == SystemDrivePath ? CNumberProcessor.ConvertBytes(Convert.ToDouble(drive.TotalSize)) : String.Empty;
                    }
                    return String.Empty;
                }
                catch (NullReferenceException)
                {
                    return String.Empty;
                }
            }
        }

        /// <summary>
        /// Returns the free space of drive of the drive Windows is installed on.
        /// </summary>
        public static String GetSystemDriveFreeSpace
        {
            get
            {
                try
                {
                    foreach (DriveInfo drive in DriveInfo.GetDrives())
                    {
                        return drive.IsReady && drive.Name == SystemDrivePath ? CNumberProcessor.ConvertBytes(Convert.ToDouble(drive.TotalFreeSpace)) : String.Empty;
                    }
                    return String.Empty;
                }
                catch (NullReferenceException)
                {
                    return String.Empty;
                }
            }
        }
    }
}
