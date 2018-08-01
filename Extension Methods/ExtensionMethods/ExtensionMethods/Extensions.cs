using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtMethods
{
    public static class Extensions
    {
        /// <summary>
        /// An extension method for ints to check whether it is even
        /// </summary>
        /// <param name="num">Extends/Targets the int type to check if the int is even</param>
        /// <returns>A bool value whether the number is even</returns>
        public static bool IsEven(this int num)
        {
            bool isEven = false;
            if(num % 2 == 0)
            {
                isEven = true;
            }
            return isEven;
        }

        /// <summary>
        /// An extension method for ints to check whether it is prime
        /// </summary>
        /// <param name="num">Extends/Targets the int type, to check if the int is prime</param>
        /// <returns>Returns a bool as to whether the int is prime</returns>
        public static bool IsPrime(this int num)
        {
            bool isPrime = false;
            if(num < 0)
            {
                num *= -1;
            }
            if (num <= 1)
            {
                isPrime = false;
            }else if (num == 2)
            {
                isPrime = true;
            }
            else
            {
                if (num % 2 == 0)
                {
                    return isPrime = false;
                }
                int root = (int)Math.Sqrt((double)num);
                for (int i = 3; i <= root; i += 2)
                {
                    if (num % i != 0)
                    {
                        isPrime = true;
                    }
                }
                if (num == 3)
                {
                    isPrime = true;
                }
            }
            return isPrime;
        }

        /// <summary>
        /// An extension method for IEnumerable to print out all of it's content with a ", " appended to split each element.
        /// </summary>
        /// <param name="collect">Extends/Targets IEnumerable to print out</param>
        public static void Print(this IEnumerable collect)
        {
            StringBuilder result = new StringBuilder();
            if(collect != null && collect.GetEnumerator().MoveNext())
            {
                foreach(var field in collect)
                {
                    if(field != null)
                    {
                        result.Append($"{field}, ");
                    }
                }
                if (result.Length > 2)
                {
                    result.Length -= 2;
                }
            }
            Console.Write(result.ToString().Trim());
        }

        /// <summary>
        /// An extension for int to raise the base number to the power of exponent
        /// </summary>
        /// <param name="num">Extends/Targets int to raise this base</param>
        /// <param name="exponent">The exponent to raise the base to this power</param>
        /// <returns>Returns a long from num raised to exponent</returns>
        public static long ToPower(this int num, int exponent)
        {
            if(exponent < 0)
            {
                throw new ArgumentException("Does not take in a negative power/exponent");
            }
            long result = 1;
            for(int i = 0; i < exponent; i++)
            {
                result *= num;
            }
            return result;
        }

        /// <summary>
        /// An extension to string to check if the string is a palindrome.
        /// </summary>
        /// <param name="phrase">Extends/Targets string that you want to check if palindrome</param>
        /// <returns>Returns a bool as to whether the string is a palindrome</returns>
        public static bool IsPalindrome(this string phrase)
        {
            bool isPalindrome = false;
            string testPhrase = phrase.ToLower().Replace(" ", "");
            string reverseTestPhrase = "";
            for (int i = testPhrase.Length - 1; i > -1; i--)
            {
                reverseTestPhrase += testPhrase[i];
            }
            if (testPhrase.Equals(reverseTestPhrase))
            {
                isPalindrome = true;
            }
            return isPalindrome;
        }

        /// <summary>
        /// An extension to string to shift each letter value by the shiftValue
        /// </summary>
        /// <param name="phrase">Extends/Target string to shift its values</param>
        /// <param name="shiftValue">The value to shift each character</param>
        /// <returns>Returns the string with the shifted letters</returns>
        public static string Shift(this string phrase, int shiftValue)
        {
            string result = "";
            foreach(char letter in phrase)
            {
                char newLetter = letter;
                if (shiftValue >= 0)
                {
                    for (int i = 0; i < shiftValue; i++)
                    {
                        newLetter += (char)1;
                        if(newLetter > 127)
                        {
                            newLetter = (char)32;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < (shiftValue*-1); i++)
                    {
                        newLetter -= (char)1;
                        if (newLetter < 32)
                        {
                            newLetter = (char)127;
                        }
                    }
                }
                result += newLetter;
            }
            return result;
        }
        
    }
}
