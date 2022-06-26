using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ButodoProject.Core.Helper
{
    public static class Functions
    {
        public static double ToDouble(this string value)
        {
            return double.Parse(value);
        }

        public static string HtmlTagClean(string text)
        {
            if (text == null) return "";
            return Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
        }

        public static string SecondToTime(int totalSeconds)
        {
            var seconds = totalSeconds % 60;
            var minutes = (totalSeconds / 60) % 60;
            var hours = totalSeconds / 3600;

            var datetime = new DateTime().AddSeconds(totalSeconds);
            return datetime.ToString("mm:ss");
        }

        public static bool EmailControl(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static string Substring(string text, int maxLength)
        {
            text = HtmlTagClean(text);
            var newText = text;
            if (text.Length > maxLength)
            {
                var index = text.IndexOf(" ", maxLength, StringComparison.Ordinal);
                if (index >= 0) newText = text.Substring(0, index);
            }
            return newText;
        }

        public static string Substring(string text, int maxLength, string prepend)
        {
            text = HtmlTagClean(text);
            var newText = text;
            if (text.Length > maxLength)
            {
                var index = text.IndexOf(" ", maxLength, StringComparison.Ordinal);
                if (index >= 0) newText = text.Substring(0, index) + prepend;
            }
            return newText;
        }

        public static string GenerateCode(int lenght)
        {
            const string chars = "123456789ABCDEFGHJKLMNPRSTUVYZabcdefghjklmnprstuvyz";
            var code = "";
            var random = new System.Random();

            for (var i = 0; i < lenght; i++)
            {
                var row = random.Next(0, chars.Length - 1);
                code += chars[row];
            }
            return code;
        }
        public static string GenerateCodeOnlyNumber(int lenght)
        {
            const string chars = "1234567890";
            var code = "";
            var random = new System.Random();

            for (var i = 0; i < lenght; i++)
            {
                if (i == 0)
                {
                    var row = random.Next(0, chars.Length - 2);
                    code += chars[row];
                }
                else
                {
                    var row = random.Next(0, chars.Length - 1);
                    code += chars[row];
                }
            }
            return code;
        }
        public static string ToTitleCase(string text)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text);
        }
        public static Guid ToGuid(string id)
        {
            Guid result;
            Guid.TryParse(id, out result);
            return result;
        }

        public static string FileSizeSuffix(long value, int decimalPlaces = 1)
        {
            string[] sizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
            if (value < 0) { return "-" + FileSizeSuffix(-value); }
            if (value == 0) { return "0.0 bytes"; }

            // mag is 0 for bytes, 1 for KB, 2, for MB, etc.
            var mag = (int)Math.Log(value, 1024);

            // 1L << (mag * 10) == 2 ^ (10 * mag) 
            // [i.e. the number of bytes in the unit corresponding to mag]
            var adjustedSize = (decimal)value / (1L << (mag * 10));

            // make adjustment when the value is large enough that
            // it would round up to 1000 or more
            if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
            {
                mag += 1;
                adjustedSize /= 1024;
            }

            return string.Format("{0:n" + decimalPlaces + "} {1}", adjustedSize, sizeSuffixes[mag]);
        }


        public static string GenerateFileName(string text)
        {
            string str = text.RemoveAccent().ToLower();
            str = str.Replace('ı', 'i')
                .Replace('ş', 's')
                .Replace('ö', 'o')
                .Replace('ü', 'u')
                .Replace('ç', 'c')
                .Replace('ğ', 'g')
                .Replace('ç', 'c')
                .Replace('â', 'a')
                .Replace('ê', 'e')
                .Replace('î', 'i')
                .Replace('û', 'u')
                .Replace('ô', 'o');
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 120 ? str.Length : 120).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }

        public static string GenerateSlug(string text)
        {
            string str = text.RemoveAccent().ToLower();
            str = str.Replace('ı', 'i')
                .Replace('ş', 's')
                .Replace('ö', 'o')
                .Replace('ü', 'u')
                .Replace('ç', 'c')
                .Replace('ğ', 'g')
                .Replace('ç', 'c')
                .Replace('â', 'a')
                .Replace('ê', 'e')
                .Replace('î', 'i')
                .Replace('û', 'u')
                .Replace('ô', 'o');
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 120 ? str.Length : 120).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }

        public static string RemoveAccent(this string txt)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }
    }
}
