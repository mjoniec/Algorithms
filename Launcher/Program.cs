using System;
using System.Linq;

namespace Launcher
{
    class Program
    {
        static void Main(string[] args)
        {
            var launcher = new Launcher();
            var samples = launcher.GetAllSamplesNames();
            var sampleName = samples.First();

            var result = launcher.RunSample(sampleName);

            Console.WriteLine(result);
        }
    }
}
