using System.Net;
using System.Net.Sockets;


namespace NetworkScanner
{
    internal class SecurityScanner
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task StartScanAsync()
        {
            string? targetIpAddress = PromptForIPAddress();
            if (string.IsNullOrEmpty(targetIpAddress))
            {
                Console.WriteLine("Invalid IP address entered. Exiting...");
                return;
            }

            if (!IsTargetReachable(targetIpAddress))
            {
                Console.WriteLine($"Unable to connect to target IP: {targetIpAddress}. Exiting...");
                return;
            }

            string? filename = PromptForFilename();
            if (string.IsNullOrEmpty(filename))
            {
                Console.WriteLine("Invalid filename entered. Exiting...");
                return;
            }

            // Assuming LocalDirectoryScanner.CheckLocalDirectories(filename) exists elsewhere in your project
            LocalDirectoryScanner.CheckLocalDirectories(filename);

            await CheckForFileAtUrl(targetIpAddress, "", filename);
        }

        private static string? PromptForIPAddress()
        {
            Console.WriteLine("Enter target IP address:");
            string? ipAddress = Console.ReadLine();
            return IPAddress.TryParse(ipAddress, out _) ? ipAddress : null;
        }

        private static string? PromptForFilename()
        {
            Console.WriteLine("Enter the name of the file (including its .extension) to scan for:");
            return Console.ReadLine();
        }

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

        private static async Task CheckForFileAtUrl(string ipAddress, string path, string filename)
        {
            string targetUrl = $"http://{ipAddress}{path}/{filename}";
            try
            {
                HttpResponseMessage response = await client.GetAsync(targetUrl);
                if (response.IsSuccessStatusCode)
                {
                    string fileContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"{filename} found at: {targetUrl}\nContent of {filename}:\n{fileContent}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error fetching {targetUrl}. Message: {e.Message}");
            }
        }
    }
}