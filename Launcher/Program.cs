using System;

namespace Launcher
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var samplesRunner = new SamplesRunner();
            var samples = samplesRunner.GetAllSamplesNames();
            var sampleName = samples.First();

            var result = samplesRunner.RunSample(sampleName);

            Console.WriteLine(result);
        }
    }
}
