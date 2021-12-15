using SereCore;

namespace HollowKnight.Rando3Stats
{
    public class RandoStatsGlobalSettings : BaseSettings
    {
        public bool CountShopItemsIndividually
        {
            // consistent with Rando's "vanilla" counting - they do floor(itemsFound/itemsPlaced*100), so each shop item should be counted by default.
            // ref: https://github.com/homothetyhk/HollowKnight.RandomizerMod/blob/e9ef1ee1c903d3da5d74a6c389b167caf29ba6c1/RandomizerMod3.0/RandomizerMod.cs#L375
            get => GetBool(true);
            set => SetBool(value);
        }

        public string CompletionFormatString
        {
            get => GetString("$RACING_EXTENDED$");
            set => SetString(value);
        }
    }
}
