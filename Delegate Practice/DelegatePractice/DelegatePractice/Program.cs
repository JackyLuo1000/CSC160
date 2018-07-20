using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatePractice
{
    public static class PublishMath
    {
        public static MathDelegate math;

        public static void UpdateMath(params int[] nums)
        {
            if (math != null)
            {
                math.Invoke(nums);
            }
        }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            Driver.Run();
        }
    }

    public class Driver
    {
        public static void Run()
        {
            List<int> nums = Prompt();
            PrintMath(nums);
        }

        public static List<int> Prompt()
        {
            List<int> nums = new List<int>();
            do
            {
                Console.Write("Please enter a list of numbers seperated by a comma and a space with no trailing comma: ");
                string input = Console.ReadLine();
                if(input == null)
                {
                    Console.WriteLine("Error needs to have a list of numbers.");
                }
                else
                {
                    List<string> inputs = input.Split(new string[] { ", " }, StringSplitOptions.None).ToList<string>();
                    if(int.TryParse(inputs.First(), out int testnum))
                    if(inputs.Count == 1)
                    {
                        Console.WriteLine("Error needs to have more than one number in the proper format");
                    }
                    else
                    {
                        for(int i = 0; i < inputs.Count(); i++)
                        {
                            if(i > 0)
                            {
                                if (inputs.ElementAt(i).Equals("0"))
                                {
                                    throw new ArgumentException("Cannot divide a number by zero");
                                }
                            }
                            if (int.TryParse(inputs.ElementAt(i), out int num))
                            {
                                num = int.Parse(inputs.ElementAt(i));
                                nums.Add(num);
                            }
                            else
                            {
                                throw new ArgumentException("Cannot enter any words or characters");
                            }
                        }
                    }
                }
            } while (nums.Count <= 1);
            return nums;
        }

        

        public static void PrintMath(List<int> nums)
        {
            PublishMath.math += ParamMath.Add;
            PublishMath.math += ParamMath.Subtract;
            PublishMath.math += ParamMath.Multiply;
            PublishMath.math += ParamMath.Divide;
            PublishMath.math += ParamMath.Mod;

            PublishMath.UpdateMath(nums.ToArray());
        }
    }
}
