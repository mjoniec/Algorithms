using Reusables;
using System;
using System.Diagnostics;
using System.Threading;

namespace Samples
{
    public class ThreadingTest : ILaunchableSample
    {
        public string Run()
        {
            Test_1_Synchronous();
            Test_2_Asynchronous();
            Test_3_AsynchronousWithJoin();
            Test_4_ThreadSleep();
            Test_5_ThreadAbort();
            Test_6_BackgroundThread();
            Test_7_ThreadPoolTest();

            Console.WriteLine("Thread test end");

            return "Thread test end";
        }

        public string GetName()
        {
            return "ThreadTest";
        }

        public static void Test_7_ThreadPoolTest()
        {
            ThreadPoolTest.Test();
        }

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

        private class ThreadPoolTest
        {
            public static void Test()
            {
                var mywatch = new Stopwatch();

                Console.WriteLine("Thread Pool Execution");

                mywatch.Start();
                ProcessWithThreadPoolMethod();
                mywatch.Stop();

                Console.WriteLine("Time consumed by ProcessWithThreadPoolMethod is : " + mywatch.ElapsedTicks.ToString());
                mywatch.Reset();


                Console.WriteLine("Thread Execution");

                mywatch.Start();
                ProcessWithThreadMethod();
                mywatch.Stop();

                Console.WriteLine("Time consumed by ProcessWithThreadMethod is : " + mywatch.ElapsedTicks.ToString());
            }

            static void ProcessWithThreadPoolMethod()
            {
                for (int i = 0; i <= 10; i++)
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(Process));
                }
            }

            static void ProcessWithThreadMethod()
            {
                for (int i = 0; i <= 10; i++)
                {
                    Thread obj = new Thread(Process);
                    obj.Start();
                }
            }

            static void Process(object callback)
            {

            }
        }
    }
}

/* Test Thread Pool vs Thread 
Thread Pool Execution
Time consumed by ProcessWithThreadPoolMethod is : 3248
Thread Execution
Time consumed by ProcessWithThreadMethod is : 6410
test end
    */


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
