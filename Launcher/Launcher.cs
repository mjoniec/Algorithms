using System.Collections.Generic;
using System.Linq;

namespace Launcher
{
    public class Launcher
    {
        private readonly List<ICodeSample> _codeSamples;

        public Launcher()
        {
            _codeSamples = new List<ICodeSample>
            {
                new MergeCommonElementsDistinct(),
                new ThreadTest()
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
