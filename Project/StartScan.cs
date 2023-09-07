using System.Net.Sockets;
using Assessment;
using System.IO;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

namespace NetworkSecurityScanner
{
    internal class Program
    {
        private static readonly HttpClient client = new HttpClient();

        private static bool IsTargetReachable(string ipAddress, int port = 80, int timeout = 5000)
        {
            using (var client = new TcpClient())
            {
                try
                {
                    var task = client.ConnectAsync(ipAddress, port);
                    return Task.WhenAny(task, Task.Delay(timeout)).Result == task;
                }
                catch
                {
                    return false;
                }
            }
        }

        public static async Task StartScan()
        {
            Console.WriteLine("Enter target IP address:");
            string targetIpAddress = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(targetIpAddress) || !IPAddress.TryParse(targetIpAddress, out _))
            {
                Console.WriteLine("You didn't enter a valid IP address. Exiting...");
                return;
            }

            if (!IsTargetReachable(targetIpAddress))
            {
                Console.WriteLine($"Unable to connect to target IP: {targetIpAddress}. Exiting...");
                return;
            }

            try
            {
                HttpResponseMessage rootResponse = await client.GetAsync($"http://{targetIpAddress}/");
                if (!rootResponse.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Unable to get a positive HTTP response from target IP: {targetIpAddress}. Exiting...");
                    return;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error making an HTTP request to {targetIpAddress}. Message: {e.Message}");
                return;
            }

            Console.WriteLine($"Successfully connected to {targetIpAddress}. Proceeding with scan...");

            Console.WriteLine("Enter the name of the file (including its .extension) to scan for:");
            string filename = Console.ReadLine();

            if (string.IsNullOrEmpty(filename) || !filename.Contains("."))
            {
                Console.WriteLine("You didn't enter a valid filename or its extension. (For example enter password.txt instead of password");
            }

            // Check the local directories first
            CheckLocalDirectories(filename);

            // Then check the remote server
            await CheckForFile("", targetIpAddress, filename);
        }

        private static void CheckLocalDirectories(string filename)
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


        private static async Task CheckForFile(string path, string ipAddress, string filename)
        {
            string targetUrl = $"http://{ipAddress}{path}/{filename}";
            try
            {
                HttpResponseMessage response = await client.GetAsync(targetUrl);
                if (response.IsSuccessStatusCode)
                {
                    string fileContent = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(fileContent))
                    {
                        Console.WriteLine($"password.txt found at: {targetUrl}\nContent of password.txt:\n{fileContent}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error fetching {targetUrl}. Message: {e.Message}");
            }
        }
    }
}



//using System.Net.Sockets;
//using Assessment;
//using System.IO;
//using System.Net;

//namespace NetworkSecurityScanner
//{
//    internal class Program
//    {
//        private static readonly HttpClient client = new HttpClient();

//        private static bool IsTargetReachable(string ipAddress, int port = 80, int timeout = 5000)
//        {
//            using (var client = new TcpClient())
//            {
//                try
//                {
//                    var task = client.ConnectAsync(ipAddress, port);
//                    return Task.WhenAny(task, Task.Delay(timeout)).Result == task;
//                }
//                catch
//                {
//                    return false;
//                }
//            }
//        }

//        public static async Task StartScan()
//        {
//            Console.WriteLine("Enter target IP address:");
//            string targetIpAddress = Console.ReadLine();

//            if (string.IsNullOrWhiteSpace(targetIpAddress) || !IPAddress.TryParse(targetIpAddress, out _))
//            {
//                Console.WriteLine("You didn't enter a valid IP address. Exiting...");
//                return;
//            }

//            if (!IsTargetReachable(targetIpAddress))
//            {
//                Console.WriteLine($"Unable to connect to target IP: {targetIpAddress}. Exiting...");
//                return;
//            }

//            try
//            {
//                HttpResponseMessage rootResponse = await client.GetAsync($"http://{targetIpAddress}/");
//                if (!rootResponse.IsSuccessStatusCode)
//                {
//                    Console.WriteLine($"Unable to get a positive HTTP response from target IP: {targetIpAddress}. Exiting...");
//                    return;
//                }
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine($"Error making an HTTP request to {targetIpAddress}. Message: {e.Message}");
//                return;
//            }

//            Console.WriteLine($"Successfully connected to {targetIpAddress}. Proceeding with scan...");

//            Console.WriteLine("Enter the name of the file (including its .exten to scan for:");
//            string filename = Console.ReadLine();

//            if (string.IsNullOrEmpty(filename) || !filename.Contains("."))
//            {
//                Console.WriteLine("You didn't enter a valid filename or its extension. (For example enter password.txt instead of password");
//            }

//            CheckLocalDirectories(filename);
//            await CheckForFile("", targetIpAddress, filename);

//            var wordList = new Words().GetWordList();
//            var tasks = wordList.Select(path => CheckForFile(path, targetIpAddress, filename)).ToArray();
//            await Task.WhenAll(tasks);

//            Console.WriteLine("Scan completed.");
//        }

//        private static void CheckLocalDirectories(string filename)
//        {
//            string desktopDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
//            try
//            {
//                SearchAndPrintFiles(desktopDirectory, filename);
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine($"Error while checking local directories: {e.Message}");
//            }
//        }

//        private static void SearchAndPrintFiles(string directory, string filename)
//        {
//            try
//            {
//                foreach (var file in Directory.GetFiles(directory, filename))
//                {
//                    Console.WriteLine($"Found {filename} at {file}\nContent of {filename}:\n{File.ReadAllText(file)}");
//                }

//                foreach (var dir in Directory.GetDirectories(directory))
//                {
//                    SearchAndPrintFiles(dir, filename);
//                }
//            }
//            catch (UnauthorizedAccessException)
//            {
//                No action needed.Simply skip directories that you don't have access to.
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine($"Error accessing {directory}: {e.Message}");
//            }
//        }

//        private static async Task CheckForFile(string path, string ipAddress, string filename)
//        {
//            string targetUrl = $"http://{ipAddress}{path}/{filename}";
//            try
//            {
//                HttpResponseMessage response = await client.GetAsync(targetUrl);
//                if (response.IsSuccessStatusCode)
//                {
//                    string fileContent = await response.Content.ReadAsStringAsync();
//                    if (!string.IsNullOrEmpty(fileContent))
//                    {
//                        Console.WriteLine($"password.txt found at: {targetUrl}\nContent of password.txt:\n{fileContent}");
//                    }
//                }
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine($"Error fetching {targetUrl}. Message: {e.Message}");
//            }
//        }
//    }
//}