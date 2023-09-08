using System;
using System.IO;
using System.Security.Cryptography;
using ICSharpCode.SharpZipLib.Zip;

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

                    Console.WriteLine("Do you want to encrypt this file? (yes/no)");
                    var userResponse = Console.ReadLine();
                    if (userResponse?.ToLower() == "yes")
                    {
                        Console.WriteLine("Enter an encryption passphrase (minimum 8 characters):");
                        var passphrase = Console.ReadLine();
                        if (!string.IsNullOrEmpty(passphrase) && passphrase.Length >= 8)
                        {
                            EncryptFile(file, passphrase);
                        }
                        else
                        {
                            Console.WriteLine("Invalid passphrase. File was not encrypted.");
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

        private static void EncryptFile(string filePath, string passphrase)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath + ".zip", FileMode.Create, FileAccess.Write))
                using (ZipOutputStream zipStream = new ZipOutputStream(fs))
                {
                    zipStream.SetLevel(3); // 0-9, 9 being the highest compression
                    zipStream.Password = passphrase;  // Optional. Password is not set on stream

                    byte[] buffer = new byte[4096];

                    ZipEntry entry = new ZipEntry(Path.GetFileName(filePath));
                    zipStream.PutNextEntry(entry);

                    using (FileStream fsRead = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        int sourceBytes;
                        do
                        {
                            sourceBytes = fsRead.Read(buffer, 0, buffer.Length);
                            zipStream.Write(buffer, 0, sourceBytes);
                        } while (sourceBytes > 0);
                    }

                    zipStream.Finish();
                    zipStream.Close();

                    File.Delete(filePath);  // Delete the original file after creating the encrypted zip

                    Console.WriteLine($"{filePath} has been encrypted and zipped successfully.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error encrypting {filePath}. Message: {e.Message}");
            }
        }
    }
}