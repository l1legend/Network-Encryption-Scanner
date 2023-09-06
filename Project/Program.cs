using System;
using NetworkSecurityScanner; // Add this to access the StartScan method
using System.Threading.Tasks;

namespace MyNamespace
{
    class Program
    {
        static void Main()
        {
            try
            {
                NetworkSecurityScanner.Program.StartScan().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                Console.WriteLine(ex.StackTrace);  // This will print details of the error for debugging
            }

        }
    }
}