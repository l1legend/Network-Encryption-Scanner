using NetworkScanner;

namespace NetworkScannerApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                await SecurityScanner.StartScanAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}