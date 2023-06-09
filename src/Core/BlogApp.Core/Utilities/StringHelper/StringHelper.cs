using System;
using System.Text.RegularExpressions;

namespace BlogApp.Core.Utilities.StringHelper
{
	public static class StringHelper
	{
        public static string RemoveHtml(string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                return "";
            }
            String result = Regex.Replace(text, @"<[^>]*>", String.Empty);
            if (String.IsNullOrEmpty(result))
            {
                return "";
            }
            if (result.Length >= 30)
            {
                result = result.Substring(0, 30) + "...";
            }
            return result;
        }
    }
}

