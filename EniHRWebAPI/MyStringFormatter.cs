using System;
using System.Linq;

namespace EniHRWebAPI
{
    public class MyStringFormatter
    {
        public static string Capitalize(string input)
        {
            if (String.IsNullOrWhiteSpace(input))
            {
                return "";
            }
            else
            {
                return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
            }
        }
    }
}
