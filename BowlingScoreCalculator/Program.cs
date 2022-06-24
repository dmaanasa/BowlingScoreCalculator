using BowlingScoreCalculator.Model;
using System;

namespace BowlingScoreCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome");
            Round.GetRound();
            while (GetInput() == "Y")
            {
                Round.GetRound();
            }
        }

        private static string GetInput()
        {
            Console.WriteLine("Do you want to play another game?(Y/N)");
            string input = Console.ReadLine();
            while (input!="Y" && input!="N")
            {
                Console.WriteLine("Enter Y or N");
                input = Console.ReadLine();
            }
            return input;
        }
    }
}
