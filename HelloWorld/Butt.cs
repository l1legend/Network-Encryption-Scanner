using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Assessment
{
    public interface IWordsProvider
    {
        List<string> GetWordList();
    }
}


namespace NetworkSecurityScanner
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter target IP address:");
            string targetIpAddress = Console.ReadLine(); // Read IP from user input

            Console.WriteLine("Starting scan for flag.txt...");

            List<string> wordList = Assessment.Words.GetWordList();

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

                if (response.IsSuccessStatusCode && await response.Content.ReadAsStringAsync() != "")
                {
                    Console.WriteLine($"flag.txt found at: {targetUrl}");
                }
            }
            catch (Exception e) // Catch all exceptions
            {
                Console.WriteLine($"Error fetching {targetUrl}. Message: {e.Message}");
            }
        }
    }
}