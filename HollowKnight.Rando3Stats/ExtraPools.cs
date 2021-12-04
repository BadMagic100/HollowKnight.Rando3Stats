using RandomizerMod.Randomization;

namespace HollowKnight.Rando3Stats
{
    public static class ExtraPools
    {
        public const string CUSTOM_POOL_CURSED = "CUSTOM_CursedJunkItem";

        /// <summary>
        /// Gets the pool of the given item and corrects for the "Fake" pool, which includes the dupe dreamer and cursed mode 1 geo and nothing items.
        /// This may find pool names that are NOT defined by "vanilla" randomizer; those pool names should be defined as constants in this class.
        /// </summary>
        /// <param name="item">The randomizer item name</param>
        public static string GetPoolOf(string item)
        {
            string pool = LogicManager.GetItemDef(item).pool;
            if (pool != "Fake")
            {
                return pool;
            }
            else if (item == "Dreamer_(1)")
            {
                return "Dreamer";
            }
            else
            {
                return CUSTOM_POOL_CURSED;
            }
        }
    }
}
