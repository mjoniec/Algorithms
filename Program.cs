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
            //ThreadInTryCatchTest.Test_1_Synchronous();
            //ThreadInTryCatchTest.Test_2_Asynchronous();
            //ThreadInTryCatchTest.Test_3_AsynchronousWithJoin();
            //ThreadInTryCatchTest.Test_4_ThreadSleep();
            //ThreadInTryCatchTest.Test_5_ThreadAbort();
            ThreadInTryCatchTest.Test_6_BackgroundThread();

            Console.WriteLine("test end");
        }
        
        //static void Main(string[] args)
        //{
        //    Console.WriteLine(new MergeCommonElementsDistinct().RunMerge());
        //}
    }

     /* Test 6 result - background thread quits when main one does
Work Sleep 1
test end
         */



        /* Test 5 result - one of the threads was aborted and its iteration nr 3 never occured
Work Sleep 1
Work Sleep 2
Work Sleep 1
test end  // abort was invoked by this moment
Work Sleep 2
Work Sleep 3
         */



        /* Test 4 result
Work X 1
Work Sleep 1
Work X 2
Work X 3
test end
Work Y 1
Work Y 2
Work Y 3
Work Sleep 2
Work Sleep 3
         */



        /* Test 3 result .Join()
Work Sleep 1
Work Sleep 2
Work Sleep 3
test end - main thread and all threads are made to wait until sleep thread finished
Work X 1
Work X 2
Work X 3
         */



        /* Test 2 result
Work X 1
Work Y 1
test end - main thread finished and remaining threads are still running - so they are not background threads
Work X 2
Work X 3
Work Y 2
Work Y 3
         */


        /* Test 1 result
Work X 1
Work X 2
Work X 3
Work Y 1
Work Y 2
Work Y 3
test end
         */
    
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
