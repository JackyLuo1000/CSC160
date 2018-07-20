using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamsDemo_A
{
    class Program
    {
        static void Main(string[] args)
        {
            string s1 = "Hello.";
            string s2 = "is";
            string s3 = "it";
            string s4 = "me";
            string s5 = "you're";
            string s6 = "looking";
            string s7 = "for?";

            string[] stuff = { "I", "can", "see", "it", "in", "your", "eyes" };

            PrintStrings(stuff);
        }

        public static void PrintStrings(params string[] words)
        {
            foreach(string word in words)
            {
                Console.WriteLine(word);
            }
        }
    }
}
