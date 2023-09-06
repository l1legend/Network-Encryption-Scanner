using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Assessment; //Assessment.dll

namespace NetworkSecurityScanner
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();
        static readonly string targetIpAddress = "192.168.1.10"; // Hardcoded IP address

        static void Main(string[] args)
        {
            Console.WriteLine("Starting scan for flag.txt...");

            List<string> wordList = Words.GetWordList(); // Assuming GetWordList() returns a List<string>

            foreach (string path in wordList)
            {
                CheckForFlagFile(path);
            }

            Console.WriteLine("Scan completed.");
        }

        static async void CheckForFlagFile(string path)
        {
            string targetUrl = $"http://{targetIpAddress}{path}/flag.txt"; // Use the hardcoded IP

            try
            {
                HttpResponseMessage response = await client.GetAsync(targetUrl);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Console.WriteLine($"flag.txt found at: {targetUrl}");
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error fetching {targetUrl}. Message: {e.Message}");
            }
        }
    }
}