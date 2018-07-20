using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberGuessingGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Driver.Run();
        }
    }

    class Driver
    {
        public static void Run()
        {
            Game.StartGame();
        }
    }

    class Game
    {
        public static void StartGame()
        {
            bool newGame = false;
            do
            {
                ArrayList guesses = new ArrayList();
                Random numGenerator = new Random();
                int correctNum;
                int guess;
                int diff = Prompt.GetDiffNum();
                bool endGame = false;
                if(diff == 1)
                {
                    correctNum = numGenerator.Next(1, 11);
                    do
                    {
                        Console.WriteLine("Attempts: " + guesses.Count);
                        guess = Prompt.GetEasyNum(guesses.Count);
                        if (guesses.Contains(guess))
                        {
                            Console.WriteLine("You guessed this number already.");
                        }
                        else if(guess > correctNum)
                        {
                            Console.WriteLine("The guess was higher than the number.");
                            guesses.Add(guess);
                        }else if(guess < correctNum)
                        {
                            Console.WriteLine("The guess was lower than the number.");
                            guesses.Add(guess);
                        }else
                        {
                            Console.WriteLine("You correctly guessed the number with " + guesses.Count + " attempts.");
                            guesses.Add(guess);
                            endGame = true;
                        }
                        if (guesses.Count == 5 && !guesses.Contains(correctNum))
                        {
                            Console.WriteLine("5 attempts used, The answer was: " + correctNum);
                            endGame = true;
                        }
                    } while (!endGame);
                }else if(diff == 2)
                {
                    correctNum = numGenerator.Next(1, 51);
                    do
                    {
                        Console.WriteLine("Attempts: " + guesses.Count);
                        guess = Prompt.GetMedNum(guesses.Count);
                        if (guesses.Contains(guess))
                        {
                            Console.WriteLine("You guessed this number already.");
                        }
                        else if (guess > correctNum)
                        {
                            Console.WriteLine("The guess was higher than the number.");
                            guesses.Add(guess);
                        }
                        else if (guess < correctNum)
                        {
                            Console.WriteLine("The guess was lower than the number.");
                            guesses.Add(guess);
                        }
                        else
                        {
                            Console.WriteLine("You correctly guessed the number with " + guesses.Count + " attempts.");
                            guesses.Add(guess);
                            endGame = true;
                        }
                        if (guesses.Count == 5 && !guesses.Contains(correctNum))
                        {
                            Console.WriteLine("Max attempts used, The answer was: " + correctNum);
                            endGame = true;
                        }
                    } while (!endGame);
                }
                else
                {
                    correctNum = numGenerator.Next(1, 101);
                    do
                    {
                        Console.WriteLine("Attempts: " + guesses.Count);
                        guess = Prompt.GetHardNum(guesses.Count);
                        if (guesses.Contains(guess))
                        {
                            Console.WriteLine("You guessed this number already.");
                        }
                        else if (guess > correctNum)
                        {
                            Console.WriteLine("The guess was higher than the number.");
                            guesses.Add(guess);
                        }
                        else if (guess < correctNum)
                        {
                            Console.WriteLine("The guess was lower than the number.");
                            guesses.Add(guess);
                        }
                        else
                        {
                            Console.WriteLine("You correctly guessed the number with " + guesses.Count + " attempts.");
                            guesses.Add(guess);
                            endGame = true;
                        }
                        if(guesses.Count == 5 && !guesses.Contains(correctNum))
                        {
                            Console.WriteLine("Max attempts used, The answer was: " + correctNum);
                            endGame = true;
                        }
                    } while (!endGame);
                }
                int contNum = Prompt.GetContGame();
                newGame = contNum == 1 ? true : false;
            } while (newGame);
            Console.WriteLine("Thank You for playing the Number Guessing Game");
        }
    }

    class Prompt
    {
        public static int GetDiffNum()
        {
            int num = 1;
            do
            {
                if(num > 3 || num < 1)
                {
                    Console.WriteLine("Invalid Input");
                }
                    Console.Write("Please enter a number for difficulty 1(Easy), 2(Medium), or 3(Hard): ");
                    string tmpNum = Console.ReadLine();
                    if (int.TryParse(tmpNum, out num))
                    {
                        num = int.Parse(tmpNum);
                    }
                    else
                    {
                        num = 4;
                    }
            } while (num > 3 || num < 1);
            
            return num;
        }

        
        public static int GetEasyNum(int attempts)
        {
            int num = 1;
            do
            {
                if (num > 10 || num < 1)
                {
                    Console.WriteLine("Invalid Input");
                    Console.WriteLine("Attempts: " + attempts);
                }
                Console.Write("Please enter a number 1-10: ");
                string tmpNum = Console.ReadLine();
                if (int.TryParse(tmpNum, out num))
                  {
                     num = int.Parse(tmpNum);
                  }
                else
                  {
                     num = 11;
                  }
            } while (num > 10 || num < 1);

            return num;
        }

        public static int GetMedNum(int attempts)
        {
            int num = 1;
            do
            {
                if (num > 50 || num < 1)
                {
                    Console.WriteLine("Invalid Input");
                    Console.WriteLine("Attempts: " + attempts);
                }
                Console.Write("Please enter a number 1-50: ");
                string tmpNum = Console.ReadLine();
                if (int.TryParse(tmpNum, out num))
                {
                    num = int.Parse(tmpNum);
                }
                else
                {
                    num = 51;
                }
            } while (num > 50 || num < 1);

            return num;
        }

        public static int GetHardNum(int attempts)
        {
            int num = 1;
            do
            {
                if (num > 100 || num < 1)
                {
                    Console.WriteLine("Invalid Input");
                    Console.WriteLine("Attempts: " + attempts);
                }
                Console.Write("Please enter a number 1-100: ");
                string tmpNum = Console.ReadLine();
                if (int.TryParse(tmpNum, out num))
                {
                    num = int.Parse(tmpNum);
                }
                else
                {
                    num = 101;
                }
            } while (num > 100 || num < 1);

            return num;
        }

        public static int GetContGame()
        {
            int num = 1;
            do
            {
                if (num != 2 && num != 1)
                {
                    Console.WriteLine("Invalid Input");
                }
                Console.Write("Please enter a number to start a new game 1(Continue) or 2(End): ");
                string tmpNum = Console.ReadLine();
                if (int.TryParse(tmpNum, out num))
                {
                    num = int.Parse(tmpNum);
                }
                else
                {
                    num = 4;
                }
            } while (num != 2 && num != 1);

            return num;
        }
    }
}
