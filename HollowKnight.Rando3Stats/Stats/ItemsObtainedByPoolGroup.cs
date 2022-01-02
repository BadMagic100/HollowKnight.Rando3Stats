using HollowKnight.Rando3Stats.Util;
using Modding;
using System.Linq;
using Rando = RandomizerMod.RandomizerMod;

namespace HollowKnight.Rando3Stats.Stats
{
    public class ItemsObtainedByPoolGroup : PercentageStatistic, IToggleableStatistic
    {
        private static readonly SimpleLogger log = new("RandoStats:ItemsObtainedByPoolGroup");

        public static ItemsObtainedByPoolGroup[] GetAllPoolGroups()
        {
            return new ItemsObtainedByPoolGroup[]
                {
                    new(LogicalPoolGrouping.Dreamers),
                    new(LogicalPoolGrouping.SkillItems),
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
                    new(LogicalPoolGrouping.GrubItems),
                    new(LogicalPoolGrouping.Lifeblood),
                    new(LogicalPoolGrouping.SoulTotems),
                    new(LogicalPoolGrouping.LoreTablets),
                    new(LogicalPoolGrouping.JournalEntries),
                    new(LogicalPoolGrouping.GrimmFlames),
                    new(LogicalPoolGrouping.GeoRocks),
                    new(LogicalPoolGrouping.BossGeo),
                    new(LogicalPoolGrouping.BossEssence),
                    new("Egg Shop Geo", LogicalPoolGrouping.EggShop),
                    new(LogicalPoolGrouping.CursedJunkItems)
                };
        }

        private readonly LogicalPoolGrouping poolGroup;

        public bool IsEnabled
        {
            get
            {
                if(poolGroup.IsEnabled)
                {
                    int total = GetTotal();
                    if (total == 0)
                    {
                        log.LogWarn($"Disabling pool group {poolGroup.Name} with no randomized items - assuming cursed logic");
                        return false;
                    }
                    return true;
                }
                return false;
            }
        }

        protected override string StatNamespace => $"{base.StatNamespace}.{Label.Replace(" ", "")}";

        public ItemsObtainedByPoolGroup(LogicalPoolGrouping pools) : base(pools.Name, true)
        {
            poolGroup = pools;
        }

        public ItemsObtainedByPoolGroup(string alternateName, LogicalPoolGrouping pools) : base(alternateName)
        {
            poolGroup = pools;
        }

        protected override int GetObtained()
        {
            return RandoExtensions.GetRandomizedItemsWithDupes()
                .Where(x => poolGroup.Pools.Select(y => y.Name).Contains(ExtraPools.GetPoolOf(x)))
                .Count(Rando.Instance.Settings.CheckItemFound);
        }

        protected override int GetTotal()
        {
            return RandoExtensions.GetRandomizedItemsWithDupes()
                .Where(x => poolGroup.Pools.Select(y => y.Name).Contains(ExtraPools.GetPoolOf(x)))
                .Count();
        }
    }
}
