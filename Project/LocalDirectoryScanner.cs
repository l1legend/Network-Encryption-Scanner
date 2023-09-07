using System;
using System.IO;

namespace NetworkSecurityScanner
{
    internal static class LocalDirectoryScanner
    {
        public static void CheckLocalDirectories(string filename)
        {
            string desktopDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            SearchAndPrintFiles(desktopDirectory, filename);
        }

        private static void SearchAndPrintFiles(string directory, string filename)
        {
            try
            {
                foreach (var file in Directory.GetFiles(directory, filename))
                {
                    Console.WriteLine($"Found {filename} at {file}");
                    Console.WriteLine($"Content of {filename}:");
                    Console.WriteLine(File.ReadAllText(file));
                }

                // Recursively search in subdirectories
                foreach (var dir in Directory.GetDirectories(directory))
                {
                    SearchAndPrintFiles(dir, filename);
                }
            }
            catch (UnauthorizedAccessException)
            {
                // We don't have access to this directory, log a message and skip it.
                Console.WriteLine($"Access denied to directory: {directory}. Skipping...");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error accessing {directory}: {e.Message}");
            }
        }
    }
}