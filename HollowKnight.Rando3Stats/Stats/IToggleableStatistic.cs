namespace HollowKnight.Rando3Stats.Stats
{
    public interface IToggleableStatistic : IRandomizerStatistic
    {
        public bool IsEnabled { get; }
    }
}
