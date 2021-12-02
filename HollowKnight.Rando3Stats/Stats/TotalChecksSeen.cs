using Modding;
using RandomizerMod.Randomization;
using System.Collections.Generic;
using System.Linq;
using Rando = RandomizerMod.RandomizerMod;

namespace HollowKnight.Rando3Stats.Stats
{
    public class TotalChecksSeen : PercentageStatistic
    {
        public TotalChecksSeen(string label) : base(label)
        {
        }

        private int AdjustCountForShops(int initialCount, HashSet<string> randomizedLocations, bool countAll)
        {
            int count = initialCount;
            foreach (string shop in LogicManager.ShopNames)
            {
                // verify the shop is randomized (in theory it always is, but better not to make assumptions)
                if (randomizedLocations.Contains(shop))
                {
                    // don't count the shop itself, but do count the items in it. If we're not counting everything,
                    // we should only do this if we found the location (i.e. bought at least 1 item)
                    if (countAll || Rando.Instance.Settings.CheckLocationFound(shop))
                    {
                        count--;
                    }
                    IEnumerable<string> itemsToCount = Rando.Instance.Settings.GetAllItemsPlacedAt(shop)
                        .Where(x => countAll || Rando.Instance.Settings.CheckItemFound(x));
                    count += itemsToCount.Count();
                }
            }
            return count;
        }

        public override int GetObtained()
        {
            HashSet<string> locations = ItemManager.GetRandomizedLocations();
            int totalCounter = locations
                .Where(Rando.Instance.Settings.CheckLocationFound)
                .Count();
            if (RandoStats.Instance?.Settings.CountShopItemsIndividually == true)
            {
                totalCounter = AdjustCountForShops(totalCounter, locations, false);
            }
            return totalCounter;
        }

        public override int GetTotal()
        {
            HashSet<string> locations = ItemManager.GetRandomizedLocations();
            int totalCounter = locations.Count;
            if (RandoStats.Instance?.Settings.CountShopItemsIndividually == true)
            {
                totalCounter = AdjustCountForShops(totalCounter, locations, true);
            }
            return totalCounter;
        }
    }
}
