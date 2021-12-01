using RandomizerMod.Randomization;
using System.Collections.Generic;
using System.Linq;
using Rando = RandomizerMod.RandomizerMod;

namespace HollowKnight.Rando3Stats.Stats
{
    public class ChecksSeenByPoolGroup : PercentageStatistic
    {
        public static ChecksSeenByPoolGroup[] GetAllPoolGroups()
        {
            return new ChecksSeenByPoolGroup[]
                {
                    new("Dreamers", Pool.Dreamers),
                    new("Skills", Pool.Skills, Pool.SplitClaw, Pool.SplitCloak),
                    new("Charms", Pool.Charms),
                    new("Keys", Pool.Keys, Pool.ElevatorPass),
                    new("Mask Shards", Pool.MaskShards),
                    new("Vessel Fragments", Pool.VesselFragments),
                    new("Pale Ore", Pool.PaleOre),
                    new("Charm Notches", Pool.CharmNotches),
                    new("Chests", Pool.GeoChests, Pool.JunkChests),
                    new("Relics", Pool.Relics),
                    new("Rancid Eggs", Pool.RancidEggs),
                    new("Stag Stations", Pool.Stags),
                    new("Maps", Pool.Maps),
                    new("Whispering Roots", Pool.WhisperingRoots),
                    new("Grubs", Pool.Grubs, Pool.Mimics),
                    new("Lifeblood", Pool.Lifeblood),
                    new("Soul Totems", Pool.SoulTotems, Pool.PalaceTotems),
                    new("Lore Tablets", Pool.LoreTablets, Pool.PalaceLore, Pool.Focus),
                    new("Journal Entries", Pool.HunterJournal, Pool.PalaceJournal),
                    new("Grimm Flames", Pool.GrimmkinFlames),
                    new("Geo Rocks", Pool.GeoRocks),
                    new("Boss Geo", Pool.BossGeo),
                    new("Boss Essence", Pool.BossEssence),
                    new("Egg Shop", Pool.EggShop)
                };
        }

        private readonly IEnumerable<Pool> pools;

        public bool IsEnabled
        {
            get => pools.Any(x => x.IsRandomized);
        }

        public ChecksSeenByPoolGroup(string label, params Pool[] pools) : base(label)
        {
            this.pools = pools;
        }

        public override int GetObtained()
        {
            return ItemManager.GetRandomizedLocations()
                .Where(x => !LogicManager.ShopNames.Contains(x))
                .Where(x => pools.Select(y => y.Name).Contains(LogicManager.GetItemDef(x).pool))
                .Count(Rando.Instance.Settings.CheckLocationFound);
        }

        public override int GetTotal()
        {
            return ItemManager.GetRandomizedLocations()
                .Where(x => !LogicManager.ShopNames.Contains(x))
                .Where(x => pools.Select(y => y.Name).Contains(LogicManager.GetItemDef(x).pool))
                .Count();
        }
    }
}
