using System.Text.RegularExpressions;

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

    }
}
