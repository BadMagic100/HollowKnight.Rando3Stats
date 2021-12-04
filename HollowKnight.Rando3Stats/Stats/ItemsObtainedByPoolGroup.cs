using HollowKnight.Rando3Stats.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rando = RandomizerMod.RandomizerMod;

namespace HollowKnight.Rando3Stats.Stats
{
    public class ItemsObtainedByPoolGroup : PercentageStatistic
    {
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
            get => poolGroup.IsEnabled;
        }

        public ItemsObtainedByPoolGroup(LogicalPoolGrouping pools) : base(pools.Name)
        {
            poolGroup = pools;
        }

        public ItemsObtainedByPoolGroup(string alternateName, LogicalPoolGrouping pools) : base(alternateName)
        {
            poolGroup = pools;
        }

        public override int GetObtained()
        {
            return RandoExtensions.GetRandomizedItemsWithDupes()
                .Where(x => poolGroup.Pools.Select(y => y.Name).Contains(ExtraPools.GetPoolOf(x)))
                .Count(Rando.Instance.Settings.CheckItemFound);
        }

        public override int GetTotal()
        {
            return RandoExtensions.GetRandomizedItemsWithDupes()
                .Where(x => poolGroup.Pools.Select(y => y.Name).Contains(ExtraPools.GetPoolOf(x)))
                .Count();
        }
    }
}
