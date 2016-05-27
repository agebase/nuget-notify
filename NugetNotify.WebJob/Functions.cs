using System.IO;
using Microsoft.Azure.WebJobs;

namespace NugetNotify.WebJob
{
    public class Functions
    {
        [NoAutomaticTrigger]
        public static void CheckPackages(TextWriter log)
        {
            log.WriteLine("Checking packages");
        }
    }
}