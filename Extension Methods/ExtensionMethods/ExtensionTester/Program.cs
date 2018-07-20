using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtMethods;

namespace ExtensionTester
{
    class Program
    {
        static void Main(string[] args)
        {
            TestPrime();
        }

        static void TestEven()
        {
            //Test for an even with 2
            if (2.IsEven())
            {
                Console.WriteLine("2 is even");
            }
            else
            {
                Console.WriteLine("2 is false");
            }
            Console.WriteLine("Expected true: even");
            Console.WriteLine();
            //Test for odd with 3
            if (3.IsEven())
            {
                Console.WriteLine("3 is even");
            }
            else
            {
                Console.WriteLine("3 is odd");
            }
            Console.WriteLine("Expected false: odd");
            Console.WriteLine();
            //Test for an even with -4
            int test = -4;
            if (test.IsEven())
            {
                Console.WriteLine($"{test} is even");
            }
            else
            {
                Console.WriteLine($"{test} is false");
            }
            Console.WriteLine("Expected true: even");
            Console.WriteLine();
            //Test for odd with -9
            test = -9;
            if (test.IsEven())
            {
                Console.WriteLine($"{test} is even");
            }
            else
            {
                Console.WriteLine($"{test} is odd");
            }
            Console.WriteLine("Expected false: odd");
        }

        static void TestPrime()
        {
            //Test for not prime with 4
            if (4.IsPrime())
            {
                Console.WriteLine("4 is prime");
            }
            else
            {
                Console.WriteLine("4 is not prime");
            }
            Console.WriteLine("Expected false: not prime");
            Console.WriteLine();
            //Test for prime with 3
            if (3.IsPrime())
            {
                Console.WriteLine("3 is prime");
            }
            else
            {
                Console.WriteLine("3 is not prime");
            }
            Console.WriteLine("Expected true: prime");
            Console.WriteLine();
            //Test for prime with 101
            if (101.IsPrime())
            {
                Console.WriteLine("101 is prime");
            }
            else
            {
                Console.WriteLine("101 is not prime");
            }
            Console.WriteLine("Expected true: prime");
            Console.WriteLine();
            //Test for prime with -3
            int test = -3;
            if (test.IsPrime())
            {
                Console.WriteLine($"{test} is prime");
            }
            else
            {
                Console.WriteLine($"{test} is not prime");
            }
            Console.WriteLine("Expected false: not prime");
            Console.WriteLine();
            //Test for not prime with -9
            test = -9;
            if (test.IsPrime())
            {
                Console.WriteLine($"{test} is prime");
            }
            else
            {
                Console.WriteLine($"{test} is not prime");
            }
            Console.WriteLine("Expected false: not prime");
            Console.WriteLine();

        }

        static void TestPrint()
        {
            //Test print array of phrases
            string[] phrases = { "Alfa", "Bravo", "Charlie", "Delta", "Echo", "Foxtrot, Hotel" };
            Console.Write("Result: ");
            phrases.Print();
            Console.WriteLine($"Expected: {phrases[0]}, {phrases[1]}, {phrases[2]}, {phrases[3]}, {phrases[4]}, {phrases[5]}");
            Console.WriteLine();
            //Test print array of ints
            int[] nums = { 5, 50, 25, 6, 9 };
            Console.Write("Result: ");
            nums.Print();
            Console.WriteLine($"Expected: {nums[0]}, {nums[1]}, {nums[2]}, {nums[3]}, {nums[4]}");
        }

        static void TestToPower()
        {
            //Test 2 to the power of 2
            long num = 2.ToPower(2);
            Console.WriteLine($"2 to the 2nd power is {num}");
            Console.WriteLine("Expected: 4");
            Console.WriteLine();
            //Test 5 to the power of 3
            num = 5.ToPower(3);
            Console.WriteLine($"5 to the 3nd power is {num}");
            Console.WriteLine("Expected: 125");
            Console.WriteLine();
            //Test -4 to the power of 2
            int baseNum = -4;
            num = baseNum.ToPower(2);
            Console.WriteLine($"{baseNum} to the 2nd power is {num}");
            Console.WriteLine("Expected: 16");
            Console.WriteLine();
            //Test -4 to the power of 3
            num = baseNum.ToPower(3);
            Console.WriteLine($"{baseNum} to the 3nd power is {num}");
            Console.WriteLine("Expected: -64");
            Console.WriteLine();
        }

        static void TestPalindrome()
        {
            //Test palindrome with different casing
            string text = "EeVee";
            if (text.IsPalindrome())
            {
                Console.WriteLine($"'{text}' is a palindrome");
            }
            else
            {
                Console.WriteLine($"'{text}' is not a palindrome");
            }
            Console.WriteLine("Expected is a palindorome: true");
            Console.WriteLine();
            //Test for palindrome with whitespace
            text = "E ev ee";
            if (text.IsPalindrome())
            {
                Console.WriteLine($"'{text}' is a palindrome");
            }
            else
            {
                Console.WriteLine($"{text} is not a palindrome");
            }
            Console.WriteLine("Expected is a palindorome: true");
            Console.WriteLine();
            //Test for not palindrome
            text = "Mylo";
            if (text.IsPalindrome())
            {
                Console.WriteLine($"{text} is a palindrome");
            }
            else
            {
                Console.WriteLine($"{text} is not a palindrome");
            }
            Console.WriteLine("Expected is a not palindorome: false");
            Console.WriteLine();
        }

        static void TestShift()
        {
            //Test shift for positive
            string test = "ABCDE";
            Console.WriteLine("Result: " + test.Shift(2));
            Console.WriteLine("Expected: CDEFG");
            Console.WriteLine();
            //Test for shift negative
            Console.WriteLine("Result: " + test.Shift(-1));
            Console.WriteLine("Expected: @ABCD");
            Console.WriteLine();
            //Test for positive wrap around
            test = "vwxyz";
            Console.WriteLine("Result: " + test.Shift(26));
            Console.WriteLine("Expected: 01234");
            Console.WriteLine();
            //Test for negative wrap around
            test = "01234";
            Console.WriteLine("Result: " + test.Shift(-26));
            Console.WriteLine("Expected: vwxyz");
        }
    }
}
