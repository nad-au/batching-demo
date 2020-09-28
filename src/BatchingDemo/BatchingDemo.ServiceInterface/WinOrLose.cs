using System;

namespace BatchingDemo.ServiceInterface
{
    public interface IWinOrLose
    {
        Outcome Next();
    }

    public class WinOrLose : IWinOrLose
    {
        private readonly int winProbabilityPct;
        private Random random;

        public WinOrLose(int winProbabilityPct)
        {
            this.winProbabilityPct = winProbabilityPct;
            random = new Random();
        }
        
        public Outcome Next()
        {
            var num = random.Next(1, 101);
            return num <= winProbabilityPct ? Outcome.Win : Outcome.Lose;
        }
    }
    
    public enum Outcome { Win, Lose }

    public static class Probability
    {
        public const int High = 80;
        public const int Even = 50;
        public const int Low = 20;
    }
}