using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MergeCommonElementsDistinct
{
    class ThreadInTryCatchTest
    {
        public static void Test_6_BackgroundThread()
        {
            var threadSleep1 = new Thread(WorkSleep) { IsBackground = true };

            threadSleep1.Start();
        }

        public static void Test_5_ThreadAbort()
        {
            var threadSleep1 = new Thread(WorkSleep);
            var threadSleep2 = new Thread(WorkSleep);

            threadSleep1.Start();
            Thread.Sleep(200);
            threadSleep2.Start();
            threadSleep1.Abort();
        }

        public static void Test_4_ThreadSleep()
        {
            var threadSleep = new Thread(WorkSleep);
            var threadX = new Thread(WorkX);
            var threadY = new Thread(WorkY);

            threadSleep.Start();
            threadX.Start();
            threadY.Start();
        }

        public static void Test_3_AsynchronousWithJoin()
        {
            var threadX = new Thread(WorkX);
            var threadSleep = new Thread(WorkSleep);

            threadSleep.Start();
            threadSleep.Join();

            threadX.Start();
        }

        public static void Test_2_Asynchronous()
        {
            var threadX = new Thread(WorkX);
            var threadY = new Thread(WorkY);

            threadX.Start();
            threadY.Start();
        }

        public static void Test_1_Synchronous()
        {
            WorkX();
            WorkY();
        }

        static void WorkX()
        {
            for (int i = 1; i <= 3; i++)
            {
                Console.WriteLine("Work X " + i.ToString());
            }
        }

        static void WorkY()
        {
            for (int i = 1; i <= 3; i++)
            {
                Console.WriteLine("Work Y " + i.ToString());
            }
        }

        static void WorkSleep()
        {
            for (int i = 1; i <= 3; i++)
            {
                Console.WriteLine("Work Sleep " + i.ToString());

                Thread.Sleep(200);
            }
        }
    }
    
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
