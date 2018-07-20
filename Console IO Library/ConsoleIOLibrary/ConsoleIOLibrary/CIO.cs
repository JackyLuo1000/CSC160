using System;
using System.Collections.Generic;

namespace CSC160_ConsoleMenu
{
    public static class CIO
    {
        /// <summary>
        /// Generates a console-based menu using the strings in options as the menu items.
        /// Automatically numbers each option starting at 1 and incrementing by 1.
        /// Reserves the number 0 for the "quit" option when withQuit is true.
        /// </summary>
        /// <param name="options">strings representing the menu options</param>
        /// <param name="withQuit">adds option 0 for "quit" when true</param>
        /// <returns>the int of the selection made by the user</returns>
        public static int PromptForMenuSelection(IEnumerable<string> options, bool withQuit)
        {
            bool alpha = true;
            bool beta = true;
            int select = 0;
            int i = 0;
            do
            {
                try
                {
                    foreach (String phrase in options)
                    {
                        i++;
                        Console.WriteLine((i) + ". " + phrase);
                    }
                    if (withQuit)
                    {
                        String quit = "0. Quit";
                        Console.WriteLine(quit);
                        Console.Write("Please select an option number: ");
                        do
                        {
                            String input = Console.ReadLine();
                            select = int.Parse(input);
                            if (select >= 0 && select <= i)
                            {
                                beta = false;
                                i = 1;
                                alpha = false;
                            }
                            else
                            {
                                Console.Write("Please enter valid number: ");
                            }
                        } while (beta);
                    }
                    else if (!withQuit)
                    {
                        Console.Write("Please select an option number: ");
                        do
                        {
                            String input = Console.ReadLine();
                            select = int.Parse(input);
                            if (select > 0 && select <= i)
                            {
                                beta = false;
                                alpha = false;
                            }
                            else
                            {
                                Console.Write("Please enter valid number: ");
                            }
                        } while (beta);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid Input");
                    i = 0;
                }
            } while (alpha);
            return select;
        }

        /// <summary>
        /// Generates a prompt that expects the user to enter one of two responses that will equate
        /// to a boolean value. The trueString represents the case insensitive response that will equate to true. 
        /// The falseString acts similarly, but for a false boolean value.
        /// 	Example: Assume this method is called with a trueString argument of "yes" and a falseString
        /// 	argument of "no". If the user enters "YES", the method returns true. If the user enters "no",
        /// 	the method returns false. All other inputs are considered invalid, the user will be informed, 
        /// 	and the prompt will repeat.
        /// </summary>
        /// <param name="message">the prompt to be displayed to the user</param>
        /// <param name="trueString">the case insensitive value that will evaluate to true</param>
        /// <param name="falseString">the case insensitive value that will evaluate to false</param>
        /// <returns>the boolean value</returns>
        public static bool PromptForBool(string message, string trueString, string falseString)
        {
            bool result = false;
            bool notValid = false;
            do
            {
                try
                {
                    notValid = false;
                    Console.Write(message);
                    string input = Console.ReadLine();
                    if (input.Equals(trueString, StringComparison.InvariantCultureIgnoreCase))
                    {
                        result = true;
                    }
                    else if (input.Equals(falseString, StringComparison.InvariantCultureIgnoreCase))
                    {
                        result = false;
                    }
                    else
                    {
                        Console.WriteLine("Please provide valid input.");
                        notValid = true;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Please provide valid input.");
                    notValid = true;
                }

            } while (notValid);
            return result;
        }

        /// <summary>
        /// Generates a prompt that expects a numeric input representing a byte value.
        /// This method loops until valid input is given.
        /// </summary>
        /// <param name="message">the prompt to be displayed to the user</param>
        /// <param name="min">the inclusive minimum boundary</param>
        /// <param name="max">the inclusive maximum boundary</param>
        /// <returns>the byte value</returns>
        public static byte PromptForByte(string message, byte min, byte max)
        {
            if(min > max)
            {
                throw new ArgumentException("Min cannot be greater than max.");
            }
            byte num = 1;
            bool notValid = false;
            do
            {
                try
                {
                    notValid = false;
                    Console.Write(message);
                    string tmpNum = Console.ReadLine();
                    num = byte.Parse(tmpNum);
                    if (num < min || num > max)
                    {
                        Console.WriteLine("Your number was out of range.");
                        notValid = true;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Please input a valid number.");
                    notValid = true;
                }

            } while (notValid);
            return num;
        }

        /// <summary>
        /// Generates a prompt that expects a numeric input representing a short value.
        /// This method loops until valid input is given.
        /// </summary>
        /// <param name="message">the prompt to be displayed to the user</param>
        /// <param name="min">the inclusive minimum boundary</param>
        /// <param name="max">the inclusive maximum boundary</param>
        /// <returns>the short value</returns>
        public static short PromptForShort(string message, short min, short max)
        {
            short num = 1;
            bool notValid = false;
            do
            {
                try
                {
                    notValid = false;
                    Console.Write(message);
                    string tmpNum = Console.ReadLine();
                    num = short.Parse(tmpNum);
                    if (num < min || num > max)
                    {
                        Console.WriteLine("Your number was out of range.");
                        notValid = true;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Please input a valid number.");
                    notValid = true;
                }

            } while (notValid);
            return num;
        }

        /// <summary>
        /// Generates a prompt that expects a numeric input representing a int value.
        /// This method loops until valid input is given.
        /// </summary>
        /// <param name="message">the prompt to be displayed to the user</param>
        /// <param name="min">the inclusive minimum boundary</param>
        /// <param name="max">the inclusive maximum boundary</param>
        /// <returns>the int value</returns>
        public static int PromptForInt(string message, int min, int max)
        {
            int num = 1;
            bool notValid = false;
            do
            {
                try
                {
                    notValid = false;
                    Console.Write(message);
                    string tmpNum = Console.ReadLine();
                    num = int.Parse(tmpNum);
                    if (num < min || num > max)
                    {
                        Console.WriteLine("Your number was out of range.");
                        notValid = true;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Please input a valid number.");
                    notValid = true;
                }

            } while (notValid);
            return num;
        }

        /// <summary>
        /// Generates a prompt that expects a numeric input representing a long value.
        /// This method loops until valid input is given.
        /// </summary>
        /// <param name="message">the prompt to be displayed to the user</param>
        /// <param name="min">the inclusive minimum boundary</param>
        /// <param name="max">the inclusive maximum boundary</param>
        /// <returns>the long value</returns>
        public static long PromptForLong(string message, long min, long max)
        {
            long num = 1;
            bool notValid = false;
            do
            {
                try
                {
                    notValid = false;
                    Console.Write(message);
                    string tmpNum = Console.ReadLine();
                    num = long.Parse(tmpNum);
                    if (num < min || num > max)
                    {
                        Console.WriteLine("Your number was out of range.");
                        notValid = true;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Please input a valid number.");
                    notValid = true;
                }

            } while (notValid);
            return num;
        }

        /// <summary>
        /// Generates a prompt that expects a numeric input representing a float value.
        /// This method loops until valid input is given.
        /// </summary>
        /// <param name="message">the prompt to be displayed to the user</param>
        /// <param name="min">the inclusive minimum boundary</param>
        /// <param name="max">the inclusive maximum boundary</param>
        /// <returns>the float value</returns>
        public static float PromptForFloat(string message, float min, float max)
        {
            float num = 1;
            bool notValid = false;
            do
            {
                try
                {
                    notValid = false;
                    Console.Write(message);
                    string tmpNum = Console.ReadLine();
                    num = float.Parse(tmpNum);
                    if (num < min || num > max)
                    {
                        Console.WriteLine("Your number was out of range.");
                        notValid = true;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Please input a valid number.");
                    notValid = true;
                }

            } while (notValid);
            return num;
        }

        /// <summary>
        /// Generates a prompt that expects a numeric input representing a double value.
        /// This method loops until valid input is given.
        /// </summary>
        /// <param name="message">the prompt to be displayed to the user</param>
        /// <param name="min">the inclusive minimum boundary</param>
        /// <param name="max">the inclusive maximum boundary</param>
        /// <returns>the double value</returns>
        public static double PromptForDouble(string message, double min, double max)
        {
            double num = 1;
            bool notValid = false;
            do
            {
                try
                {
                    notValid = false;
                    Console.Write(message);
                    string tmpNum = Console.ReadLine();
                    num = double.Parse(tmpNum);
                    if (num < min || num > max)
                    {
                        Console.WriteLine("Your number was out of range.");
                        notValid = true;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Please input a valid number.");
                    notValid = true;
                }

            } while (notValid);
            return num;
        }

        /// <summary>
        /// Generates a prompt that expects a numeric input representing a decimal value.
        /// This method loops until valid input is given.
        /// </summary>
        /// <param name="message">the prompt to be displayed to the user</param>
        /// <param name="min">the inclusive minimum boundary</param>
        /// <param name="max">the inclusive maximum boundary</param>
        /// <returns>the decimal value</returns>
        public static decimal PromptForDecimal(string message, decimal min, decimal max)
        {
            decimal num = 1;
            bool notValid = false;
            do
            {
                try
                {
                    notValid = false;
                    Console.Write(message);
                    string tmpNum = Console.ReadLine();
                    num = decimal.Parse(tmpNum);
                    if (num < min || num > max)
                    {
                        Console.WriteLine("Your number was out of range.");
                        notValid = true;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Please input a valid number.");
                    notValid = true;
                }

            } while (notValid);
            return num;
        }

        /// <summary>
        /// Generates a prompt that allows the user to enter any response and returns the string.
        /// When allowEmpty is true, empty responses are valid. When false, responses must contain
        /// at least one character (including whitespace).
        /// </summary>
        /// <param name="message">the prompt to be displayed to the user.</param>
        /// <param name="allowEmpty">when true, makes empty responses valid</param>
        /// <returns>the input from the user as a String</returns>
        public static string PromptForInput(string message, bool allowEmpty)
        {
            string result = "";
            bool notValid = false;
            do
            {
                try
                {
                    notValid = false;
                    Console.Write(message);
                    string input = Console.ReadLine();
                    if (allowEmpty)
                    {
                        result = input;
                    }
                    else
                    {
                        if (input.Equals(""))
                        {
                            Console.WriteLine("Please enter at least one character.");
                            notValid = true;
                        }
                        else
                        {
                            result = input;
                        }
                    }

                }
                catch (Exception)
                {
                    Console.WriteLine("Please enter a valid input.");
                    notValid = true;
                }
            } while (notValid);
            return result;
        }

        /// <summary>
        /// Generates a prompt that expects a single character input representing a char value.
        /// This method loops until valid input is given.
        /// </summary>
        /// <param name="message">the prompt to be displayed to the user</param>
        /// <param name="min">the inclusive minimum boundary</param>
        /// <param name="max">the inclusive maximum boundary</param>
        /// <returns>the char value</returns>
        public static char PromptForChar(string message, char min, char max)
        {
            char result = 'a';
            bool notValid = false;
            do
            {
                try
                {
                    notValid = false;
                    Console.Write(message);
                    string input = Console.ReadLine();
                    if (input.Length != 1)
                    {
                        Console.WriteLine("Please enter 1 character.");
                        notValid = true;
                    }
                    else
                    {
                        result = char.Parse(input);
                        if(result < min || result > max)
                        {
                            Console.WriteLine("Please enter a valid character.");
                            notValid = true;
                        }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Please enter a valid character.");
                    notValid = true;
                }
            } while (notValid);
            return result;
        }

        public static long Add(int num1, int num2)
        {
            long result = 0;
            result = num1 + num2;
            return result;
        }
    }
}
