using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;
using Assessment;

namespace Assessment
{
    public interface IWordsProvider
    {
        List<string> GetWordList();
    }

    public class Words : IWordsProvider
    {
        public List<string> GetWordList()
        {            
            return new List<string>();
        }
    }
}

namespace NetworkSecurityScanner
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();
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
                        // Successfully connected
                        return true;
                    }
                    else
                    {
                        // Timed out
                        return false;
                    }
                }
                catch
                {
                    // Exception occurred (like refused connection)
                    return false;
                }
            }
        }

        public static async Task StartScan()
        {
            Console.WriteLine("Enter target IP address:");
            string targetIpAddress = Console.ReadLine(); // Read IP from user input
            if (!IsTargetReachable(targetIpAddress))
            {
                Console.WriteLine($"Unable to connect to target IP: {targetIpAddress}. Exiting...");
                return;
            }

            // Checking HTTP connectivity
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

            await CheckForFlagFile("", targetIpAddress); // directly call the check for the root directory

            List<string> wordList = new Assessment.Words().GetWordList();

            var tasks = wordList.Select(path => CheckForFlagFile(path, targetIpAddress)).ToArray();
            await Task.WhenAll(tasks); // Wait for all checks to complete

            Console.WriteLine("Scan completed.");
        }

        static async Task CheckForFlagFile(string path, string ipAddress)
        {
            string targetUrl = $"http://{ipAddress}{path}/flag.txt";

            try
            {
                HttpResponseMessage response = await client.GetAsync(targetUrl);

                // If the file is successfully fetched
                if (response.IsSuccessStatusCode)
                {
                    string fileContent = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(fileContent))
                    {
                        Console.WriteLine($"flag.txt found at: {targetUrl}");
                        Console.WriteLine($"Content of flag.txt:\n{fileContent}");
                    }
                }
            }
            catch (Exception e) // Catch all exceptions
            {
                Console.WriteLine($"Error fetching {targetUrl}. Message: {e.Message}");
            }
        }
    }
}