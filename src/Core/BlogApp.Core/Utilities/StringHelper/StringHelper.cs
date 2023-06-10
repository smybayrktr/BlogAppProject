using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Tokens;

namespace BlogApp.Core.Utilities.StringHelper
{
    public static class StringHelper
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

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
        public static string GenerateRandomPassword()
        {
            var random = new Random();
            var password = new char[12];

            for (int i = 0; i < password.Length; i++)
            {
                password[i] = chars[random.Next(chars.Length)];
            }

            return new string(password);
        }
    }
}

