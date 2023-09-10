using System;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;  // Assuming you're using SharpZipLib

namespace NetworkScanner
{
    public static class FileEncrypter 
    {
        public static void EncryptFile(string filePath, string passphrase)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath + ".zip", FileMode.Create, FileAccess.Write))
                using (ZipOutputStream zipStream = new ZipOutputStream(fs))
                {
                    zipStream.SetLevel(3);
                    zipStream.Password = passphrase;

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

                    File.Delete(filePath);

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
