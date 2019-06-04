namespace JPWeb.UI
{
    public static class StringExtensions
    {
        public static string TruncateStringAndAddEllipses(this string @string, int maximumLength)
        {
            if(@string == null)
            {
                return null;
            }

            return @string.Length > maximumLength ? @string.Substring(0, maximumLength) + "..." : @string;            
        }
    }
}
