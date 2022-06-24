using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingScoreCalculator.Model
{
    public class Round
    {
        public int Score { get; private set; }

        private List<Frame> Frames;

        private Round()
        {
             Frames = new List<Frame>();
        }

        private bool IsRoundDone()
        {
            if (Frames.Count<10)
            {
                return false;
            }
            if (Frames.Count==11)
            {
                return true;
            }
            else if(Frames.Count==10 && (Frames[9].GetOutcome()==Outcome.strike || Frames[9].GetOutcome() == Outcome.spare))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static Round GetRound()
        {
            Round round = new Round();  
            while (!round.IsRoundDone())
            {
                Console.WriteLine("----------"+(round.Frames.Count+1)+"--------");
                round.AddFrame();
                round.PrintCurrentScore();
            }
            return round;
        }

        private void AddFrame()
        {
            Frames.Add(Frame.GetFrame(IsFinalShotSingleShot(),Frames.Count==10));
            UpdateScore();
        }

        private bool IsFinalShotSingleShot()
        {
            if (Frames.Count<10)
            {
                return false;
            }
            Frame latestFrame = Frames[9];
            return latestFrame.GetOutcome()==Outcome.spare;
        }

        private void UpdateScore()
        {
            Frame latestFrame = Frames[Frames.Count-1];
            Outcome outcome = latestFrame.GetOutcome();
            int currScore = (latestFrame.FirstShotScore + latestFrame.SecondShotScore);
            if (!latestFrame.IsBonusFrame)
            {
                switch (outcome)
                {
                    case Outcome.strike:
                        Score += 10;
                        break;
                    case Outcome.spare:
                        Score += 10;
                        break;
                    case Outcome.open:
                        Score += currScore;
                        break;
                }
            }
            if (Frames.Count > 1 )
            {
                Frame prevFrame = Frames[Frames.Count - 2];
                Outcome prevOutcome = prevFrame.GetOutcome();

                switch (prevOutcome)
                {
                    case Outcome.strike:
                        Score += currScore;
                        break;
                    case Outcome.spare:
                        Score += latestFrame.FirstShotScore;
                        break;
                }

                if (Frames.Count > 2 && prevOutcome==Outcome.strike)
                {
                    Frame grandPrevFrame = Frames[Frames.Count - 3];
                    Outcome grandPrevOutcome = grandPrevFrame.GetOutcome();

                    switch (grandPrevOutcome)
                    {
                        case Outcome.strike:
                            Score += latestFrame.FirstShotScore;
                            break;
                    }
                }
            }
            
        }

        private void PrintCurrentScore()
        {
            Console.WriteLine("=========================================");
            Console.WriteLine("Current Score:");
            foreach (Frame frame in Frames)
            {
                Console.Write("||"+frame.ToString());
            }
            Console.WriteLine("||");
            Console.WriteLine("Score: "+ Score);
            Console.WriteLine("=========================================");
        }
    }
}
