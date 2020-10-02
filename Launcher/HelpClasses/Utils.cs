using System.IO;
using System.Net;
using Launcher.Controls.Update;

namespace Launcher.HelpClasses
{
    internal class Utilities
    {
        public class File
        {
            public static FileAttributes RemoveAttribute(FileAttributes attributes, FileAttributes attributesToRemove)
            {
                return attributes & ~attributesToRemove;
            }
        }

        public class Updater
        {
            public static string GetPath(string gamePath, string item)
            {
                if (item.ToLower().Contains(".mpq") && item.Contains("-ruRU-"))
                    return Path.Combine(gamePath, $@"Data\ruRU\{item}"); 

                return item.ToLower().Contains(".mpq") 
                    ? Path.Combine(gamePath, $@"Data\{item}") 
                    : Path.Combine(gamePath, item);
            }

            public static string DetectSize(long value)
            {
                var amount = SpeedUnitsExtension.GetFormattedSize(value, "{0:0.00}");
                var abbr = SpeedUnitsExtension.GetUnitAbbr(value);
                return $"{amount}{abbr}";
            }

            public static string DetectSize(long value, out int speedType)
            {
                var units = SpeedUnitsExtension.GetUnit(value);
                switch (units)
                {
                    case SpeedUnits.GBit:
                        speedType = 2;
                        break;
                    case SpeedUnits.MBit:
                        speedType = 1;
                        break;
                    default:
                        speedType = 0;
                        break;
                }
                return SpeedUnitsExtension.GetFormattedSize(value, "{0:0}");
            }
        }

        public class Network
        {
            /// <summary>
            /// Internet connection checker
            /// </summary>
            /// <returns>Returns true if connection exist</returns>
            public static bool IsInternetConnectionAvailable()
            {
                try
                {
                    using (var client = new WebClient())
                    using (client.OpenRead("https://clients3.google.com/generate_204"))
                    {
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
