using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Media.Imaging;

namespace UploadClient.Tools
{
     class FileHelper
    {
        public static string CalculateMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        public static string GetHumanSize(string filePath)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = new FileInfo(filePath).Length;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }

            // Adjust the format string to your preferences. For example "{0:0.#}{1}" would
            // show a single decimal place, and no space.
            string result = String.Format("{0:0.##} {1}", len, sizes[order]);
            return result;
        }

        public static string GetHumanSize(double length)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            int order = 0;
            while (length >= 1024 && order < sizes.Length - 1)
            {
                order++;
                length = length / 1024;
            }

            // Adjust the format string to your preferences. For example "{0:0.#}{1}" would
            // show a single decimal place, and no space.
            string result = String.Format("{0:0.##} {1}", length, sizes[order]);
            return result;
        }

        public static string GetFileRootFolder(string downloadPath)
        {
            return GetParentFolder(downloadPath);
        }

        public static string GetExtansion(string filePath)
        {
            return Path.GetExtension(filePath).ToUpper();
        }

        public static string GetFileName(string filePath)
        {
            return Path.GetFileName(filePath);
        }

        public static string GetFileNameWithoutExtansion(string fileName)
        {
            return Path.GetFileNameWithoutExtension(fileName);
        }


        public static readonly List<string> MotherExtensions = new List<string> { ".RAW", ".MP3" };
        public static string GetMotherFilePath(string overwiewPath)
        {
            string overwiewNameWithoutExtansion = GetFileNameWithoutExtansion(overwiewPath);
            string parentFolder = GetParentFolder(overwiewPath);
            if (parentFolder == null) return null;

            string[] filesPath = System.IO.Directory.GetFiles(parentFolder);
            foreach (String filePath in filesPath)
            {
                string fileExtansion = FileHelper.GetExtansion(filePath);
                string fileNameWithoutExtansion = GetFileNameWithoutExtansion(filePath);
                if (overwiewNameWithoutExtansion.Equals(fileNameWithoutExtansion) && MotherExtensions.Contains(fileExtansion)) return filePath;
            }

            return null;
        }

        private static string GetParentFolder(string overwiewPath)
        {
            return System.IO.Directory.GetParent(overwiewPath).FullName;
        }

        public static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".BMP", ".GIF", ".PNG", ".TIF" };

       
        public static bool IsImage(string filePath)
        {
            if (ImageExtensions.Contains(Path.GetExtension(filePath).ToUpperInvariant()))
            {
                return true;
            }

            return false;
        }

        public static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
    }
}