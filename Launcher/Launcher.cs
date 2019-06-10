using Reusables;
using Samples;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Launcher
{
    public class Launcher
    {
        private readonly List<ILaunchableSample> _codeSamples;

        public Launcher()
        {
            _codeSamples = new List<ILaunchableSample>
            {
                new ThreadingTest(),
                new MergeCommonElementsDistinct()
            };
        }

        public void Run()
        {
            MainLoop();
        }

        private void MainLoop()
        {
            while (true)
            {
                Console.Clear();
                PrintAvaliableCodeSamples();

                var input = Console.ReadLine();

                Console.Clear();

                var sampleNameToRun = TryParseInputIntoSampleNameToRun(input);

                if (IsEndingCondition(sampleNameToRun)) break;

                Console.WriteLine("Running sample: " + sampleNameToRun);
                Console.WriteLine("Press any key to clear and return.");
                Console.WriteLine("================================================");
                Console.WriteLine("");

                var sampleResult = RunSample(sampleNameToRun);

                Console.WriteLine(sampleResult);
                Console.ReadKey();
            }

            Console.WriteLine("Sample runner closing");
        }

        private void PrintAvaliableCodeSamples()
        {
            Console.WriteLine("Select the sample to run by entering it's number. Press Enter to Close.");

            var samples = GetAllSamplesNames();
            var i = 1;

            foreach(var sample in samples)
            {
                Console.WriteLine(i++ + " " + sample);
            }
        }

        private string TryParseInputIntoSampleNameToRun(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;

            var parseResult = int.TryParse(input, out int sampleNumber);

            if (!parseResult || !IsSampleNumberValid(sampleNumber)) return string.Empty;

            return GetSampleNameToRun(sampleNumber);
        }

        private string GetSampleNameToRun(int sampleNumber)
        {
            return GetAllSamplesNames()[sampleNumber - 1];
        }

        private bool IsEndingCondition(string sampleNameToRun)
        {
            return string.IsNullOrEmpty(sampleNameToRun);
        }

        private List<string> GetAllSamplesNames()
        {
            return _codeSamples.Select(s => s.GetName()).ToList();
        }

        private bool IsSampleNumberValid(int sampleNumber)
        {
            return sampleNumber >= 1 || sampleNumber <= GetAllSamplesNames().Count;
        }

        private string RunSample(string sampleName)
        {
            var sample = _codeSamples.FirstOrDefault(s => string.Equals(s.GetName(), sampleName));

            return sample == null ? "Sample not found" : sample.Run();
        }
    }
}
