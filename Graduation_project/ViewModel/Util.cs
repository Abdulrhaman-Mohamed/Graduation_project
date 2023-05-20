using System.Text.RegularExpressions;
using Repo_Core.Models;
using Image = System.Drawing.Image;

namespace Graduation_project.ViewModel
{
    public static class Util
    {
        public static bool IsValidDate(int year, int month, int day)
        {
            if (year is < 1900 or > 2100) return false;

            DateTime date;

            if (DateTime.TryParse($"{year}-{month}-{day}", out date)) return true;

            return false;
        }
        public static bool ValidateName(string name)
        {
            // A valid name should contain only letters, spaces, hyphens, and numbers
            // and should not start with a space or hyphen
            string pattern = @"^[A-Za-z0-9]+([ -][A-Za-z0-9]+)*([ ]?[A-Za-z0-9]*)$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(name);
        }

        private static byte[] HexStringToBytes(string hexString)
        {
            // convert hex string to bytes  
            byte[] bytes = new byte[hexString.Length / 2];
            for (int i = 0; i < hexString.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
            }
            return bytes;
        }

        public static string SaveBytes(string hexString, string folderName)
        {
            byte[] imageBytes = HexStringToBytes(hexString);
            MemoryStream ms = new MemoryStream(imageBytes);

            // generate image name 
            var imageName = $"{Guid.NewGuid()}.jpg";


            Image image = Image.FromStream(ms);

            if (!Directory.Exists($"wwwroot/{folderName}"))
                Directory.CreateDirectory($"wwwroot/{folderName}");

            var path = $"wwwroot/{folderName}/{imageName}";

            image.Save(path);

            return path;
        }
    }
}
