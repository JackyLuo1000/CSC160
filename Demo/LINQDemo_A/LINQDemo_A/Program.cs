using ExtMethods;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQDemo_A
{
    class Program
    {
        private static  int[] nums = { 0, 2, 24, 42, 1337, -1337, 5, 93, 0, 256, 1900, 1776, 333, 27, 13, 666, -7, -10, -100, 9001, -9001 };
        static void Main(string[] args)
        {
            //List<int> evenNums = new List<int>();
            //foreach (int num in nums)
            //{
            //    if (num.IsEven())
            //    {
            //        evenNums.Add(num);
            //    }
            //}

            //LINQ Comprehension Syntax
            //var evenNums = from int num in nums
            //               where num.IsEven()
            //               select num;

            //LINQ Extension Syntax
            var evenNums = nums.Where(num => num % 2 == 0);

            //LINQ Desugarized Syntax
            //var evenNums = Enumerable.Where(nums, Predicate1);
            
            evenNums.Print();
            Console.WriteLine($"Number of Evens: {evenNums.Count()}");
        }

        public static bool Predicate1(int num)
        {
            return num % 2 == 0;
        }
    }
}
