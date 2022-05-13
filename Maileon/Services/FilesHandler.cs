using System.Collections.Generic;
using System.IO;

namespace Maileon.Services
{
    /// <summary>
    /// Handles reading files and writing to files operations
    /// </summary>
    public static class FilesHandler
    {
        private static readonly string _namesFilePath = "Data/names.txt";
        private static readonly string _wordsFilePath = "Data/words.txt";
        
        /// <summary>
        /// List of available names
        /// </summary>
        public static readonly IReadOnlyList<string> Names;
        
        /// <summary>
        /// List of available words
        /// </summary>
        public static readonly IReadOnlyList<string> Words;

        /// <summary>
        /// Gets lists of available names and words
        /// </summary>
        static FilesHandler()
        {
            Names = ReadFile(_namesFilePath).Replace("\r", "").Split("\n");
            Words = ReadFile(_wordsFilePath).Replace("\r", "").Split("\n");
        }
        
        /// <summary>
        /// Writes to file
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="data">Data to write</param>
        public static void WriteFile(string path, string data)
        {
            using StreamWriter streamWriter = new(path);
            streamWriter.Write(data);
        }

        /// <summary>
        /// Reads file
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>Content of file</returns>
        public static string ReadFile(string path)
        {
            using StreamReader streamReader = new(path);
            return streamReader.ReadToEnd();
        }
    }
}