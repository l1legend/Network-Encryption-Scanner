using System.Net.Sockets;
using Assessment;


// The NetworkSecurityScanner namespace contains logic 
// to scan a network to identify and read specific files.
namespace NetworkSecurityScanner
{
    class Program
    {
        // HttpClient instance to make HTTP requests.
        static readonly HttpClient client = new HttpClient();

        // Checks if a specific IP address and port is reachable within a given timeout.
        static bool IsTargetReachable(string ipAddress, int port = 80, int timeout = 5000)
        {
            using (var client = new TcpClient())
            {
                try
                {
                    var task = client.ConnectAsync(ipAddress, port);
                    var result = Task.WhenAny(task, Task.Delay(timeout)).Result;

                    if (result == task)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }

        // Starts the scanning process.
        public static async Task StartScan()
        {
            Console.WriteLine("Enter target IP address:");
            string targetIpAddress = Console.ReadLine();

            if (!IsTargetReachable(targetIpAddress))
            {
                Console.WriteLine($"Unable to connect to target IP: {targetIpAddress}. Exiting...");
                return;
            }

            // Try making an HTTP request to check connectivity.
            HttpResponseMessage rootResponse;
            try
            {
                rootResponse = await client.GetAsync($"http://{targetIpAddress}/");
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

            await CheckForFile("", targetIpAddress);

            List<string> wordList = new Words().GetWordList(); //From Assessment Namespace

            var tasks = wordList.Select(path => CheckForFile(path, targetIpAddress)).ToArray();
            await Task.WhenAll(tasks); // Wait for all checks to complete.

            Console.WriteLine("Scan completed.");
        }

        // Check if a specific file (password.txt) exists at a given path for a specific IP address.
        static async Task CheckForFile(string path, string ipAddress)
        {
            string targetUrl = $"http://{ipAddress}{path}/password.txt";

            try
            {
                HttpResponseMessage response = await client.GetAsync(targetUrl);

                if (response.IsSuccessStatusCode)
                {
                    string fileContent = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(fileContent))
                    {
                        Console.WriteLine($"password.txt found at: {targetUrl}");
                        Console.WriteLine($"Content of password.txt:\n{fileContent}");
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