using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatePractice
{
    public delegate void MathDelegate(params int[] nums);

    
    public static class ParamMath
    {
        public static void Add(params int[] nums)
        {
            int sum = 0;
            foreach(int num in nums)
            {
                sum += num;
            }
            Console.WriteLine($"Sum: {sum}");
        }

        public static void Subtract(params int[] nums)
        {
            int difference = nums.First();
            for(int i = 1; i < nums.Count(); i++)
            {
                difference -= nums.ElementAt(i);
            }
            Console.WriteLine($"Difference: {difference}");
        }

        public static void Multiply(params int[] nums)
        {
            int product = 1;
            foreach (int num in nums)
            {
                product *= num;
            }
            Console.WriteLine($"Product: {product}");
        }

        public static void Divide(params int[] nums)
        {
            int dividend = nums.First();
            for(int i = 1; i < nums.Count(); i++)
            {
                dividend /= nums.ElementAt(i);
            }
            Console.WriteLine($"Dividend: {dividend}");
        }

        public static void Mod(params int[] nums)
        {
            int remainder = nums.First();
            for (int i = 1; i < nums.Count(); i++)
            {
                remainder %= nums.ElementAt(i);
            }
            Console.WriteLine($"Remainder: {remainder}");
        }
    }
}
