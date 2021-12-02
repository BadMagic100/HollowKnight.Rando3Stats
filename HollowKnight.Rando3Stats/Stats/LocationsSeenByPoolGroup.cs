using RandomizerMod.Randomization;
using System.Linq;
using Rando = RandomizerMod.RandomizerMod;

namespace HollowKnight.Rando3Stats.Stats
{
    public class LocationsSeenByPoolGroup : PercentageStatistic
    {
        public static LocationsSeenByPoolGroup[] GetAllPoolGroups()
        {
            return new LocationsSeenByPoolGroup[]
                {
                    new(LogicalPoolGrouping.Dreamers),
                    new(LogicalPoolGrouping.Skills),
                    new(LogicalPoolGrouping.Charms),
                    new(LogicalPoolGrouping.Keys),
                    new(LogicalPoolGrouping.MaskShards),
                    new(LogicalPoolGrouping.VesselFragments),
                    new(LogicalPoolGrouping.PaleOre),
                    new(LogicalPoolGrouping.CharmNotches),
                    new(LogicalPoolGrouping.Chests),
                    new(LogicalPoolGrouping.Relics),
                    new(LogicalPoolGrouping.RancidEggs),
                    new(LogicalPoolGrouping.StagStations),
                    new(LogicalPoolGrouping.Maps),
                    new(LogicalPoolGrouping.WhisperingRoots),
                    new(LogicalPoolGrouping.Grubs),
                    new(LogicalPoolGrouping.Lifeblood),
                    new(LogicalPoolGrouping.SoulTotems),
                    new(LogicalPoolGrouping.LoreTablets),
                    new(LogicalPoolGrouping.JournalEntries),
                    new(LogicalPoolGrouping.GrimmFlames),
                    new(LogicalPoolGrouping.GeoRocks),
                    new(LogicalPoolGrouping.BossGeo),
                    new(LogicalPoolGrouping.BossEssence),
                    new(LogicalPoolGrouping.EggShop)
                };
        }

        private readonly LogicalPoolGrouping pools;

        public bool IsEnabled
        {
            get => pools.IsEnabled;
        }

        public LocationsSeenByPoolGroup(LogicalPoolGrouping pools) : base(pools.Name)
        {
            this.pools = pools;
        }

        public override int GetObtained()
        {
            return ItemManager.GetRandomizedLocations()
                .Where(x => !LogicManager.ShopNames.Contains(x))
                .Where(x => pools.Pools.Select(y => y.Name).Contains(LogicManager.GetItemDef(x).pool))
                .Count(Rando.Instance.Settings.CheckLocationFound);
        }

        public override int GetTotal()
        {
            return ItemManager.GetRandomizedLocations()
                .Where(x => !LogicManager.ShopNames.Contains(x))
                .Where(x => pools.Pools.Select(y => y.Name).Contains(LogicManager.GetItemDef(x).pool))
                .Count();
        }
    }
}
