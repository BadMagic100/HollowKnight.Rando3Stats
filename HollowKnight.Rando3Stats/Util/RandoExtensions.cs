using RandomizerMod;
using RandomizerMod.Randomization;
using System.Collections.Generic;
using System.Linq;
using Modding;
using Rando = RandomizerMod.RandomizerMod;

namespace HollowKnight.Rando3Stats.Util
{
    public static class RandoExtensions
    {
        private static readonly SimpleLogger log = new SimpleLogger("RandoStats:Extensions");

        public static IEnumerable<string> GetAllItemsPlacedAt(this SaveSettings settings, string location)
        {
            return settings.ItemPlacements.Where(x => x.Item2 == location).Select(x => x.Item1);
        }

        public static HashSet<string> GetRandomizedItemsWithDupes(bool logInfo=false)
        {
            // ItemManager.GetRandomizedItems doesn't account for dupes, so we get to do this the hard way...
            // we know every randomized item is at a randomized location though, so we can leverage that to filter
            // out the vanilla placements.
            HashSet<string> randomizedLocations = ItemManager.GetRandomizedLocations();
            IEnumerable<string> items = Rando.Instance.Settings.ItemPlacements
                .Where(placement => randomizedLocations.Contains(placement.Item2))
                .Select(x => x.Item1);
            HashSet<string> uniqueItems = new(items);
            
            if (logInfo) log.Log($"Before set: {items.Count()}; After set: {uniqueItems.Count()}");
            foreach (string item in uniqueItems)
            {
                ReqDef def = LogicManager.GetItemDef(item);
                if (logInfo) log.Log($"{item} ({def.pool} -> {ExtraPools.GetPoolOf(item)})");
            }

            return uniqueItems;
        }
    }
}
