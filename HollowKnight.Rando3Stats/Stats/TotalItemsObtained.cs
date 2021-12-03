using System;
using System.Linq;
using System.Collections.Generic;
using Modding;
using Rando = RandomizerMod.RandomizerMod;
using RandomizerMod.Randomization;

namespace HollowKnight.Rando3Stats.Stats
{
    public class TotalItemsObtained : PercentageStatistic
    {
        public TotalItemsObtained(string label) : base(label) { }

        private HashSet<string> GetRandomizedItemsWithDupes()
        {
            // ItemManager.GetRandomizedItems doesn't account for dupes, so we get to do this the hard way...
            // we know every randomized item is at a randomized location though, so we can leverage that to filter
            // out the vanilla placements.
            HashSet<string> randomizedLocations = ItemManager.GetRandomizedLocations();
            IEnumerable<string> items = Rando.Instance.Settings.ItemPlacements
                .Where(placement => randomizedLocations.Contains(placement.Item2))
                .Select(x => x.Item1);
            HashSet<string> uniqueItems = new(items);

            var l = new SimpleLogger("RandoStats:TotalItemsObtained");
            l.Log($"Before set: {items.Count()}; After set: {uniqueItems.Count()}");

            return uniqueItems;
        }

        public override int GetObtained()
        {
            return GetRandomizedItemsWithDupes().Count(Rando.Instance.Settings.CheckItemFound);
        }

        public override int GetTotal()
        {
            return GetRandomizedItemsWithDupes().Count();
        }
    }
}
