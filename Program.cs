using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MergeCommonElementsDistinct
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new MergeCommonElementsDistinct().RunMerge());
        }
    }

    public class MergeCommonElementsDistinct
    {
        internal class Element
        {
            internal int Value { get; }
            internal bool Flagged { get; set; }

            internal Element(int value)
            {
                Value = value;
                Flagged = false;
            }

            internal static IList<Element> InitFromValues(IList<int> values)
            {
                return values.Select(value => new Element(value)).ToList();
            }
        }

        private readonly IList<Element> _listA;
        private readonly IList<Element> _listB;
        private readonly IList<Element> _listC;
        private IEnumerable<IList<Element>> JoinedLists => new List<IList<Element>> { _listA, _listB, _listC };

        public MergeCommonElementsDistinct()
        {
            _listA = Element.InitFromValues(new List<int> { 1, 2, 3 });
            _listB = Element.InitFromValues(new List<int> { 3, 2, 5, 1 });
            _listC = Element.InitFromValues(new List<int> { 3, 7, 2, 2, 8 });
        }

        public string RunMerge()
        {
            var mergedList = new List<int>();

            foreach (var list in JoinedLists)
            {
                foreach (var element in list)
                {
                    if (IsValuePresentInAllLists(element.Value, JoinedLists))
                    {
                        mergedList.Add(element.Value);
                    }
                }
            }

            return PrintOutput(mergedList);
        }

        private static string PrintOutput(IEnumerable<int> list)
        {
            var sb = new StringBuilder(string.Empty);

            foreach (var i in list)
            {
                sb.Append(i + " ");
            }

            return sb.ToString();
        }

        private static bool IsValuePresentInAllLists(int value, IEnumerable<IList<Element>> lists)
        {
            return lists.All(list => IsValuePresentInList(value, list));
        }

        private static bool IsValuePresentInList(int value, IEnumerable<Element> list)
        {
            var isPresent = false;

            foreach (var element in list)
            {
                if (!element.Flagged && element.Value == value)
                {
                    element.Flagged = isPresent = true;
                }
            }

            return isPresent;
        }
    }
}
