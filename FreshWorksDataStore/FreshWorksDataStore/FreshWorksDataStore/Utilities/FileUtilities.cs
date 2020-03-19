using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshWorksDataStore.Utilities
{
    public static class FileUtilities
    {
        /// <summary>
        /// Get the file size in Bytes, Kilo Bytes, Mega Byes, Giga Bytes, Tera Bytes
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetFileSize(string filePath)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = new FileInfo(filePath).Length;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }

            return string.Format("{0:0.##} {1}", len, sizes[order]);
        }

        /// <summary>
        /// Checks whether the file size is less than 1 GB or not
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool IsFileSizeLessThanOneGB(string filePath)
        {
            string fileSize = GetFileSize(filePath);
            double size = double.Parse(fileSize.Split(' ')[0]);
            string sizeType = fileSize.Split(' ')[1];
            if (sizeType == "GB" || sizeType == "TB")
            {
                if (size > 1)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
