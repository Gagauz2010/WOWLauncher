using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launcher.Utils
{
    class Utils
    {
        public static FileAttributes RemoveAttribute(FileAttributes attributes, FileAttributes attributesToRemove)
        {
            return attributes & ~attributesToRemove;
        }

        /// <summary>
        /// Convert bytes value to GB/MB/KB
        /// </summary>
        /// <param name="value">Byte value to convert into GB/MB/KB</param>
        /// <returns>Converted bytes value like GB/MB/KB</returns>
        public static String GetSize(long bytes)
        {
            try
            {
                if (bytes >= 1073741824)
                    return string.Format("{0:0.00}ГБ", double.Parse(bytes.ToString()) / 1024 / 1024 / 1024);
                else if (bytes >= 1048576)
                    return string.Format("{0:0.00}МБ", double.Parse(bytes.ToString()) / 1024 / 1024);
                else
                    return string.Format("{0:0}КБ", double.Parse(bytes.ToString()) / 1024);
            }
            catch
            {
                return "∞ Б";
            }
        }
    }
}
