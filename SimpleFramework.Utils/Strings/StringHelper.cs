namespace SimpleFramework.Utils.Strings
{
    public static class StringHelper
    {
        public static bool IsNumeric(this string theValue)
        {
            return long.TryParse(theValue, System.Globalization.NumberStyles.Integer, System.Globalization.NumberFormatInfo.InvariantInfo, out _);
        }
    }
}
