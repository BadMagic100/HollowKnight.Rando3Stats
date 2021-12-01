using RandomizerMod.Randomization;
using System.Linq;
using Rando = RandomizerMod.RandomizerMod;

namespace HollowKnight.Rando3Stats.Stats
{
    public class GeoShopChecksSeen : PercentageStatistic
    {

        public GeoShopChecksSeen(string label) : base(label) { }

        public override int GetObtained()
        {
            return LogicManager.ShopNames
                .Where(ItemManager.GetRandomizedLocations().Contains)
                .Sum(shop =>
                {
                    if (RandoStats.Instance?.Settings.CountShopItemsIndividually == true)
                    {
                        return Rando.Instance.Settings.GetAllItemsPlacedAt(shop)
                            .Where(Rando.Instance.Settings.CheckItemFound)
                            .Count();
                    }
                    else
                    {
                        return Rando.Instance.Settings.CheckLocationFound(shop) ? 1 : 0;
                    }
                });
        }

        public override int GetTotal()
        {
            return LogicManager.ShopNames
                .Where(ItemManager.GetRandomizedLocations().Contains)
                .Sum(shop =>
                {
                    if (RandoStats.Instance?.Settings.CountShopItemsIndividually == true)
                    {
                        return Rando.Instance.Settings.GetAllItemsPlacedAt(shop)
                            .Count();
                    }
                    else
                    {
                        return 1;
                    }
                });
        }
    }
}
