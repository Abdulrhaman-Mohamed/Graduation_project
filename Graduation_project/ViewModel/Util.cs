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

    }
}
