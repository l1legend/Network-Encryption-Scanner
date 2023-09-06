using System;
using NetworkSecurityScanner; // Add this to access the StartScan method
using System.Threading.Tasks;

namespace MyNamespace
{
    class Program
    {
        static void Main()
        {
            NetworkSecurityScanner.Program.StartScan().GetAwaiter().GetResult();
        }
    }
}