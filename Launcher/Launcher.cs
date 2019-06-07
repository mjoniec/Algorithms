using Reusables;
using Samples;
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
                new DocxToPdfConverter(),
                new ThreadingTest(),
                new MergeCommonElementsDistinct()
            };
        }

        public List<string> GetAllSamplesNames()
        {
            return _codeSamples.Select(s => s.GetName()).ToList();
        }

        public string RunSample(string sampleName)
        {
            var sample = _codeSamples.FirstOrDefault(s => string.Equals(s.GetName(), sampleName));

            return sample == null ? "Sample not found" : sample.Run();
        }
    }
}
