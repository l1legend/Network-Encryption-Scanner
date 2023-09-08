using System;
using System.IO;
using System.Security.Cryptography;

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
                byte[] salt = new byte[8];
                new RNGCryptoServiceProvider().GetBytes(salt); // Random salt for key derivation
                Rfc2898DeriveBytes derivedKey = new Rfc2898DeriveBytes(passphrase, salt, 1000);
                byte[] key = derivedKey.GetBytes(32); // Derived 256-bit key

                byte[] iv = new byte[16]; // 128 bits IV (AES block size)

                byte[] encrypted;

                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = key;
                    aesAlg.IV = iv;

                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(File.ReadAllText(filePath));
                            }
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }

                File.WriteAllBytes(filePath, encrypted);
                Console.WriteLine($"{filePath} has been encrypted successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error encrypting {filePath}. Message: {e.Message}");
            }
        }
    }
}