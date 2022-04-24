using System;
using System.Collections.Generic;

namespace MergeCommonElementsDistinct
{
    class Program
    {
        static void Main(string[] args)
        {
            var demos = new List<ICodeDemo>
            {
                new MergeCommonElementsDistinct(),
                new CodeTestListOperations()
            };

            demos.ForEach(demo => 
                Console.WriteLine(demo.Run()));
        }
    }
}
