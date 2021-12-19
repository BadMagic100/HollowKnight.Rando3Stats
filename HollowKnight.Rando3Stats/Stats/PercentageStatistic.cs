using System;

namespace HollowKnight.Rando3Stats.Stats
{
    public abstract class PercentageStatistic : IRandomizerStatistic
    {
        public string Label { get; private set; }

        private bool shouldRegister;
        protected virtual string StatNamespace { get => GetType().Name; }

        public PercentageStatistic(string label, bool register = false)
        {
            Label = label;
            shouldRegister = register;
        }

        protected abstract int GetObtained();
        protected abstract int GetTotal();

        public string GetContent()
        {
            // these computations may be expensive (in fact, they likely are). Just do them once.
            int obtained = GetObtained();
            int total = GetTotal();
            // just do floor division consistent with Rando's "vanilla" counting - they do floor(itemsFound/itemsPlaced*100).
            // ref: https://github.com/homothetyhk/HollowKnight.RandomizerMod/blob/e9ef1ee1c903d3da5d74a6c389b167caf29ba6c1/RandomizerMod3.0/RandomizerMod.cs#L375
            double rawPercent = (double)obtained / total * 100;
            int percent = (int)Math.Floor(rawPercent);
            string content = $"{percent}% ({obtained}/{total})";
            if (shouldRegister)
            {
                StatFormatRegistry.SetStat($"{StatNamespace}:Full", content);
                StatFormatRegistry.SetStat($"{StatNamespace}:Percent", $"{percent}%");
                StatFormatRegistry.SetStat($"{StatNamespace}:Obtained", obtained.ToString());
                StatFormatRegistry.SetStat($"{StatNamespace}:Total", total.ToString());
            }
            return content;
        }

        public string GetHeader()
        {
            return Label;
        }
    }
}
