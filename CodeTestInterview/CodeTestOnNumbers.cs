using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTestInterview
{
    public class CodeTestOnNumbers
    {
        private readonly List<int> _testNumbers = new() { 5, 7, 3, 0, 4, 2, 4, 9 };

        public void Run()
        {
            Print(Max());
        }

        int Max()
        {
            return _testNumbers.Max();
        }

        void Print(int result)
        {
            Console.WriteLine(result);
        }
    }
}
