using System;
using System.Security.Cryptography;
using System.Text;

namespace WinToolkit.Classes
{
   public static class Extensions
   {

       public const StringComparison DefaultComparison = StringComparison.OrdinalIgnoreCase;
        /// <summary>
        /// Returns a value indicating whether a specified substring occurs within this string.
        /// </summary>
        /// <param name="source">A sequence in which to locate a value.</param>
        /// <param name="value">The value to locate in the sequence.</param>
        /// <param name="comparisonType">One of the enumeration values that determines how this string and value are compared.</param>
        /// <returns>true if the value parameter occurs within this string, or if value is the empty string (""); otherwise, false.</returns>
        public static bool Contains(this string source, String value, StringComparison comparisonType)
        {
            return source.IndexOf(value, comparisonType) >= 0;
        }
        /// <summary>
        /// Returns a value indicating whether a specified substring occurs within this string.
        /// </summary>
        /// <param name="source">A sequence in which to locate a value.</param>
        /// <param name="value">The value to locate in the sequence.</param>
        /// <returns>true if the value parameter occurs within this string, or if value is the empty string (""); otherwise, false.</returns>
        public static bool ContainsIgnoreCase(this string source, string value, StringComparison comparisonType = DefaultComparison)
        {
            return source.IndexOf(value, comparisonType ) >= 0;
        }

        /// <summary>
        /// Determines whether the end of this string instance matches the specified string when compared using the specified comparison option.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="value">The string to compare to the substring at the end of this instance.</param>
        /// <param name="comparisonType">One of the enumeration values that determines how this string and value are compared.</param>
        /// <returns>true if value matches the end of this instance; otherwise, false.</returns>
        public static bool EndsWithIgnoreCase(this string source, string value, StringComparison comparisonType = DefaultComparison )
        {
            return source.EndsWith(value, comparisonType);
        }

        /// <summary>
        /// Determines whether two String objects have the same value.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="value">Determines whether two String objects have the same value.</param>
        /// <param name="comparisonType">One of the enumeration values that determines how this string and value are compared.</param>
        /// <returns>true if value matches the beginning of this instance; otherwise, false.</returns>
        public static bool EqualsIgnoreCase(this String source, string value, StringComparison comparisonType = DefaultComparison)
        {
            return string.Equals(source, value, comparisonType);
        }

        /// <summary>
        /// Determines whether the beginning of this string instance matches the specified string when compared using the specified comparison option.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="value">The string to compare to the substring at the end of this instance.</param>
        /// <param name="comparisonType">One of the enumeration values that determines how this string and value are compared.</param>
        /// <returns>true if value matches the beginning of this instance; otherwise, false.</returns>
        public static bool StartsWithIgnoreCase(this string source, string value, StringComparison comparisonType = DefaultComparison)
        {
            return source.StartsWith(value, comparisonType);
        }

        /// <summary>
        ///    Determines whether or not the input is numeric.
        /// </summary>
        /// <param name="value">The string you wish to check.</param>
        /// <returns>True if it is numeric.</returns>
        public static bool IsNumeric(this string value)
        {
            double num;
            return Double.TryParse(value.Trim(), out num);
        }

        /// <summary>
        ///    Checks if the input has any non-ASCII characters.
        /// </summary>
        /// <param name="inputString">The string that needs to be checked.</param>
        /// <returns>True if none ascii characters detected.</returns>
        public static bool ContainsForeignCharacters(this string inputString)
        {
            string asAscii =
                Encoding.ASCII.GetString(Encoding.Convert(Encoding.UTF8,
                    Encoding.GetEncoding(Encoding.ASCII.EncodingName, new EncoderReplacementFallback(String.Empty),
                        new DecoderExceptionFallback()), Encoding.UTF8.GetBytes(inputString)));
            return asAscii != inputString;
        }

        /// <summary>
        ///    Converts a string into an MD5 value.
        /// </summary>
        /// <param name="stringToConvert">The string you wish to be converted into an MD5 value.</param>
        /// <returns>A unique MD5 string.</returns>
        // ReSharper disable once InconsistentNaming
        public static string CreateMD5(this string stringToConvert)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(stringToConvert);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            var sb = new StringBuilder();
            foreach (byte t in hash)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
        }

        /// <summary>
        ///    Replaces text ignoring case.
        /// </summary>
        /// <param name="input">Text to be inspected</param>
        /// <param name="replace">Text to search for</param>
        /// <param name="replaceWith">New text for 'replace'</param>
        /// <param name="trimEnds">Removes any spaces at end and beginning</param>
        /// <returns>Newly created string</returns>
        public static string ReplaceIgnoreCase(this string input, string replace, string replaceWith, bool trimEnds = false)
        {
            //Tries a standard string replace.
            string standardReplace = input.Replace(replace, replaceWith);
            if (!input.ContainsIgnoreCase(replace))
            {
                if (trimEnds)
                    return standardReplace.Trim();
                return standardReplace;
            }

            //If that fails. It will then try a more thorough method.
            try
            {
                string newString = input;
                var sb = new StringBuilder();

                int previousIndex = 0;
                int index = newString.IndexOf(replace, DefaultComparison);
                while (index != -1)
                {
                    sb.Append(newString.Substring(previousIndex, index - previousIndex));
                    sb.Append(replaceWith);
                    index += replace.Length;

                    previousIndex = index;
                    index = newString.IndexOf(replace, index, DefaultComparison);
                }
                sb.Append(newString.Substring(previousIndex));

                if (trimEnds)
                    return sb.ToString().Trim();
                return sb.ToString();
            }
            catch (Exception)
            {
                //new SmallError("String Replacement", Ex, string.Format("input: '{0}'\nReplace: '{1};\nReplaceWith: '{2}'\nOutPut: '{3}'", input, replace, replaceWith, NewString)).Upload();
            }
            return input;
        }
    }
}
