using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingScoreCalculator.Model
{
    /// <summary>
    /// Represents the state of the pins by the end of a frame
    /// </summary>
    internal enum Outcome{
        strike,
        spare,
        open
    }

    /// <summary>
    /// Represents one frame in a the game
    /// </summary>
    internal class Frame
    {
        public int FirstShotScore { get; private set; }

        public int SecondShotScore { get; private set; }

        public bool IsBonusFrame { get; private set; }

        private Frame(int firstShot, int secondShot, bool isBonus)
        {
            FirstShotScore = firstShot;    
            SecondShotScore = secondShot;
            IsBonusFrame = isBonus; 
        }

        /// <summary>
        /// Helps determine the outcome a frame
        /// </summary>
        /// <returns></returns>
        public Outcome GetOutcome()
        {
            if (FirstShotScore==10 && !IsBonusFrame)
            {
                return Outcome.strike;
            }else if ((SecondShotScore + FirstShotScore) == 10 && !IsBonusFrame)
            {
                return Outcome.spare;
            }else
            {
                return Outcome.open;
            }
        }

        public override string ToString() 
        {
            return GetOutcome() switch
            {
                Outcome.strike => "X- ",
                Outcome.spare => FirstShotScore + "-" + "/",
                Outcome.open => FirstShotScore + "-" + SecondShotScore,
                _ => "",
            };
        }

        public static Frame GetFrame(bool isSingleShot=false, bool isBonusFrame=false)
        {
            Console.WriteLine("Enter first shot of a frame: ");
            int firstShot = GetIntegerInput();
            int secondShot=0;

            if ((isBonusFrame || firstShot < 10) && !isSingleShot)
            {
                Console.WriteLine("Enter second shot of a frame: ");
                secondShot = GetIntegerInput();
                while (!isBonusFrame && (firstShot + secondShot) > 10)
                {
                    Console.WriteLine("First Shot and Second Shot should less than 10.");
                    secondShot = GetIntegerInput();
                }
            }
            return new Frame(firstShot, secondShot, isBonusFrame);
        }

        private static int GetIntegerInput()
        {
            while (true)
            {
                string input = Console.ReadLine();
                if(int.TryParse(input, out int value) && value<=10 && value>=0)
                {
                    return value;
                }
                else
                {
                    Console.WriteLine("Enter a valid score. ");
                }
            }
        }
    }
}
