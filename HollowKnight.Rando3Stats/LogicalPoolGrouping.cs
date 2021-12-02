using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HollowKnight.Rando3Stats
{
    public class LogicalPoolGrouping
    {
        public static LogicalPoolGrouping Dreamers => new("Dreamers", Pool.Dreamers);
        public static LogicalPoolGrouping Skills => new("Skills", Pool.Skills, Pool.SplitClaw, Pool.SplitCloak);
        public static LogicalPoolGrouping Charms => new("Charms", Pool.Charms);
        public static LogicalPoolGrouping Keys => new("Keys", Pool.Keys, Pool.ElevatorPass);
        public static LogicalPoolGrouping MaskShards => new("Mask Shards", Pool.MaskShards);
        public static LogicalPoolGrouping VesselFragments => new("Vessel Fragments", Pool.VesselFragments);
        public static LogicalPoolGrouping PaleOre => new("Pale Ore", Pool.PaleOre);
        public static LogicalPoolGrouping CharmNotches => new("Charm Notches", Pool.CharmNotches);
        public static LogicalPoolGrouping Chests => new("Chests", Pool.GeoChests, Pool.JunkChests);
        public static LogicalPoolGrouping Relics => new("Relics", Pool.Relics);
        public static LogicalPoolGrouping RancidEggs => new("Rancid Eggs", Pool.RancidEggs);
        public static LogicalPoolGrouping StagStations => new("Stag Stations", Pool.Stags);
        public static LogicalPoolGrouping Maps => new("Maps", Pool.Maps);
        public static LogicalPoolGrouping WhisperingRoots => new("Whispering Roots", Pool.WhisperingRoots);
        public static LogicalPoolGrouping Grubs => new("Grubs", Pool.Grubs, Pool.Mimics);
        public static LogicalPoolGrouping Lifeblood => new("Lifeblood", Pool.Lifeblood);
        public static LogicalPoolGrouping SoulTotems => new("Soul Totems", Pool.SoulTotems, Pool.PalaceTotems);
        public static LogicalPoolGrouping LoreTablets => new("Lore Tablets", Pool.LoreTablets, Pool.PalaceLore, Pool.Focus);
        public static LogicalPoolGrouping JournalEntries => new("Journal Entries", Pool.HunterJournal, Pool.PalaceJournal);
        public static LogicalPoolGrouping GrimmFlames => new("Grimm Flames", Pool.GrimmkinFlames);
        public static LogicalPoolGrouping GeoRocks => new("Geo Rocks", Pool.GeoRocks);
        public static LogicalPoolGrouping BossGeo => new("Boss Geo", Pool.BossGeo);
        public static LogicalPoolGrouping BossEssence => new("Boss Essence", Pool.BossEssence);
        public static LogicalPoolGrouping EggShop => new("Egg Shop", Pool.EggShop);

        public IEnumerable<Pool> Pools { get; private set; }

        public bool IsEnabled
        {
            get => Pools.Any(x => x.IsRandomized);
        }

        public string Name { get; private set; }

        private LogicalPoolGrouping(string name, params Pool[] pools)
        {
            Pools = pools;
            Name = name;
        }
    }
}
