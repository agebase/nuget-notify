using Microsoft.Azure.WebJobs;

namespace NugetNotify.WebJob
{
    public class Program
    {
        public static void Main()
        {
            var host = new JobHost();
            host.Call(typeof(Functions).GetMethod("CheckPackages"));
        }
    }
}