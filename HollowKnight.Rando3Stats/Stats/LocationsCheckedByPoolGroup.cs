using RandomizerMod.Randomization;
using System.Linq;
using Rando = RandomizerMod.RandomizerMod;

namespace HollowKnight.Rando3Stats.Stats
{
    public class LocationsCheckedByPoolGroup : PercentageStatistic, IToggleableStatistic
    {
        public static LocationsCheckedByPoolGroup[] GetAllPoolGroups()
        {
            return new LocationsCheckedByPoolGroup[]
                {
                    new(LogicalPoolGrouping.Dreamers),
                    new(LogicalPoolGrouping.SkillLocations),
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
                    new(LogicalPoolGrouping.GrubLocations),
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

        private readonly LogicalPoolGrouping poolGroup;

        public bool IsEnabled
        {
            get => poolGroup.IsEnabled;
        }

        protected override string StatNamespace => $"{base.StatNamespace}.{Label.Replace(" ", "")}";

        public LocationsCheckedByPoolGroup(LogicalPoolGrouping pools) : base(pools.Name, true)
        {
            poolGroup = pools;
        }

        public LocationsCheckedByPoolGroup(string alternateName, LogicalPoolGrouping pools) : base(alternateName)
        {
            poolGroup = pools;
        }

        protected override int GetObtained()
        {
            return ItemManager.GetRandomizedLocations()
                .Where(x => !LogicManager.ShopNames.Contains(x))
                .Where(x => poolGroup.Pools.Select(y => y.Name).Contains(ExtraPools.GetPoolOf(x)))
                .Count(Rando.Instance.Settings.CheckLocationFound);
        }

        protected override int GetTotal()
        {
            return ItemManager.GetRandomizedLocations()
                .Where(x => !LogicManager.ShopNames.Contains(x))
                .Where(x => poolGroup.Pools.Select(y => y.Name).Contains(ExtraPools.GetPoolOf(x)))
                .Count();
        }
    }
}
