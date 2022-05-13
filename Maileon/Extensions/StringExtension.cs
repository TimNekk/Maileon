namespace Maileon.Services
{
    /// <summary>
    /// Adds capitalization functionality to strings
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Makes first letter capital
        /// </summary>
        /// <param name="source">String</param>
        /// <returns>Capitalized string</returns>
        public static string Capitalize(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            // convert to char array of the string
            char[] letters = source.ToCharArray();
            // upper case the first char
            letters[0] = char.ToUpper(letters[0]);
            // return the array made of the new char array
            return new string(letters);
        }
    }
}