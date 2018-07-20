using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchConsole
{
    /*
     * Creating Extension methods
     * 1) Nust be declared in a public, non-nested, non-generic class
     * 2) Extensions are always public and always static
     * 3) Extensions always at least one parameter
     * 4) The first parameter of an extension is ALWAYS the "this" parameter
     */ 
    public static class DemoExtensions
    {
        private static Random generator = new Random();
        public static int GetRandom(this int max, int min = 1)
        {
            
            //In the case where min is greater than max, throw a new ArgumentException with a useful message
            //Generate a random numer between min and max, both inclusive
            if (min > max)
            {
                throw new ArgumentException($"The min cannot be greater than the max; arguments: min = {min}, max = {max}");
            }

            int randInt = generator.Next(min, max + 1);

            return randInt;
        }
    }

    public class Program
    {

        /*Value Types
        * byte - 8 bits
        * short - 16 bits
        * int - 32 bits
        * long - 64 bits
        * 
        * float - 32 bits
        * double - 64 bits
        * decimal - 128 bits
        * 
        * char - 16 bits
        * bool - 8 bits
        */

        private static int num = 5;

        //Reference Types
        //string

        public static void Main(string[] args)
        {
            //Console.Write("Please, enter a small, whole number: ");
            //string input = Console.ReadLine();

            //num = int.Parse(input);
            //int i = num * 2;

            //Console.WriteLine("2 x " + num + " = " + i);

            Race3();
            
        }

        public static void Race1()
        {
            //Print Hello World where each character is on its own line
            string hello = "Hello, World!";
            Array hellos = hello.ToArray();
            foreach(char charec in hellos)
            {
                Console.WriteLine(charec);
            }
        }

        public static void Race2()
        {
            // print hello world in reverse on the same line
            string hello = "Hello, World!";
            for(int i = hello.Length-1; i > -1; i--)
            {
                Console.Write(hello[i]);
            }
            Console.WriteLine();
        }

        public static void Race3()
        {
            //Randomly select a number between 17 and 59 inclusive and print to the console
            
            Console.WriteLine(59.GetRandom(17));
        }

        public static void Greetings()
        {
            //Initilize and declare myAge as my age in years
            int myAge = 20;

            Console.Write("Please enter your first name: "); //Ask for user's first name
            string fname = Console.ReadLine(); //Stores user's first name as fname

            Console.Write("Please enter your last name: "); //Ask for user's last name
            string lname = Console.ReadLine(); //Stores user's first name as lname

            Console.Write("Please enter your age: "); //Ask for user's age
            string tmpAge = Console.ReadLine(); //Stores user's response as tmpAge
            int age = int.Parse(tmpAge); //Parse and store tmpAge as age

            string greet = age > myAge ? ", you are older than me." : age < myAge ? ", you are younger than me." : ", you are the same age as me.";

            //if(age > myAge) //Check for age as older than me
            //{
            //    greet = ", you are older than me.";
            //}else if(age < myAge) //Check for age as younger than me
            //{
            //    greet = ", you are younger than me.";
            //}
            //else //If not older or younger than user is the same age
            //{
            //    greet = ", you are the same age as me.";
            //}

            Console.WriteLine($"Hello {fname} {lname}{greet}"); //Print out greeting
        }

        public static void RandomNum()
        {
            Random gen = new Random();

            int[] nums = new int[10];

            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] = gen.Next(1, 101);
            }

            foreach (int num in nums)
            {
                Console.WriteLine(num);
            }
        }
    }
}
