using System;

namespace MyNamespace
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                await NetworkSecurityScanner.Scanner.StartScanAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}