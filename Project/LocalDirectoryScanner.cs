using NetworkScanner;

namespace NetworkScanner
{
    internal static class LocalDirectoryScanner
    {
        /// <summary>
        /// Initiates the local directory scan starting from the user's profile directory.
        /// </summary>
        /// <param name="filename">Name of the file to search for.</param>
        public static void CheckLocalDirectories(string filename)
        {
            string userProfileDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            SearchAndPrintFiles(userProfileDirectory, filename);
        }

        /// <summary>
        /// Recursively searches for a given filename in a directory and prints its content.
        /// </summary>
        /// <param name="directory">Directory to search in.</param>
        /// <param name="filename">Name of the file to search for.</param>
        private static void SearchAndPrintFiles(string directory, string filename)
        {
            if (string.IsNullOrEmpty(directory) || string.IsNullOrEmpty(filename))
            {
                return;
            }

            try
            {
                // Print files that match the filename in the current directory
                foreach (var file in Directory.GetFiles(directory, filename))
                {
                    Console.WriteLine($"Found {filename} at {file}");
                    Console.WriteLine($"Content of {filename}:");
                    Console.WriteLine(File.ReadAllText(file));

                    bool validResponse = false;
                    while (!validResponse)
                    {
                        Console.WriteLine("Do you want to encrypt this file? (yes/no)");
                        var userResponse = Console.ReadLine()?.ToLower();

                        if (userResponse == "yes")
                        {
                            Console.WriteLine("Enter an encryption passphrase (minimum 8 characters):");
                            var passphrase = Console.ReadLine();
                            if (!string.IsNullOrEmpty(passphrase) && passphrase.Length >= 8)
                            {
                                FileEncrypter.EncryptFile(file, passphrase);
                                validResponse = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid passphrase. Passphrase must be at least 8 characters long.");
                            }
                        }
                        else if (userResponse == "no")
                        {
                            validResponse = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
                        }
                    }
                }

                // Recursively search in subdirectories
                foreach (var dir in Directory.GetDirectories(directory))
                {
                    SearchAndPrintFiles(dir, filename);
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine($"Access denied to directory: {directory}. Skipping...");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error accessing {directory}: {e.Message}");
            }
        }
    }
}