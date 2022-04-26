using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTestInterview
{
    public class CodeTestOnNumbers
    {
        private readonly List<int> _testNumbers = new() { 5, 9, 3, 0, 4, 2, 4, 9, 7 };

        public void Run()
        {
            Print(() => _testNumbers);
            Print(MaxTwoElementsUnique);
            Console.WriteLine(
                AreEveryElementLargerByOne(
                    new List<int>{ 1, 2 }, 
                    new List<int> { 2, 3 }));
        }

        //default order
        //asc desc
        //reverse
        //remove duplicates (hashset ?)
        //take - sublist from front
        List<int> MaxTwoElementsUnique()
        {
            var test = new List<int>(_testNumbers);

            test.SortDescending();

            return test
                .Distinct()
                .Take(2)
                .ToList();
        }

        bool AreEveryElementLargerByOne(List<int> listA, List<int> listB)
        {
            var output = listA.SequenceEqual(listB, new TestComparer());

            return output;
        }

        static void Print(Func<List<int>> func)
        {
            Console.WriteLine(string.Join(" ", func()));
        }
    }

    class TestComparer : IEqualityComparer<int>
    {
        public bool Equals(int x, int y)
        {
            return x + 1 == y;
        }

        public int GetHashCode(int obj)
        {
            throw new NotImplementedException();
        }
    }

    public static class MyListExtensions
    {
        public static void Push<T>(this List<T> list, T item)
        {
            list.Insert(list.Count - 1, item);
        }

        public static void SortDescending<T>(this List<T> list)
        {
            list.Sort();
            list.Reverse();
        }
    }
}
