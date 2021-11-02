using System;


namespace MyCodeLibrary.SystemInfo
{
    /// <summary>
    /// Manufaturer Information
    /// </summary>
    public static class OEM
    {
        /// <summary>
        /// Returns the system manufacturer name that is stored in the registry.
        /// </summary>
        public static String Name
        {
            get
            {
                var key = @"HARDWARE\DESCRIPTION\System\BIOS";
                var value = "SystemManufacturer";
                var text = RegistryInfo.getStringValue(HKEY.LOCAL_MACHINE, key, value);
                if (String.IsNullOrEmpty(text))
                {
                    key = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\OEMInFormation";
                    value = "Manufacturer";
                    return RegistryInfo.getStringValue(HKEY.LOCAL_MACHINE, key, value);
                }
                return text;
            }
        }

        /// <summary>
        /// Returns the system product name that is stored in the registry.
        /// </summary>
        public static String ProductName
        {
            get
            {
                var key = "HARDWARE\\DESCRIPTION\\System\\BIOS";
                var value = "SystemProductName";
                var text = RegistryInfo.getStringValue(HKEY.LOCAL_MACHINE, key, value);
                if (String.IsNullOrEmpty(text))
                {
                    key = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\OEMInFormation";
                    value = "Model";
                    return RegistryInfo.getStringValue(HKEY.LOCAL_MACHINE, key, value);
                }
                return text;
            }
        }
    }
}
