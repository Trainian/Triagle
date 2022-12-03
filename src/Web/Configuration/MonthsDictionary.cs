namespace Web.Configuration
{
    public static class MonthsDictionary
    {
        private static Dictionary<int, string> dict = new Dictionary<int, string>()
        {
            { 1 , "Января" },
            { 2 , "Февраля" },
            { 3 , "Марта" },
            { 4 , "Апреля" },
            { 5 , "Мая" },
            { 6 , "Июня" },
            { 7 , "Июля" },
            { 8 , "Авгуса" },
            { 9 , "Сентября" },
            { 10 , "Октября" },
            { 11 , "Ноября" },
            { 12 , "Декабря" },
        };

        public static string GetMonths(int key)
        {
            return dict[key];
        }
    }
}
