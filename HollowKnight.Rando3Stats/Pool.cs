using System;
using Rando = RandomizerMod.RandomizerMod;

namespace HollowKnight.Rando3Stats
{
    /// <summary>
    /// A location pool, indicating whether items at that type of location are randomized.
    /// </summary>
    public class Pool
    {
        public static Pool Dreamers => new("Dreamer", () => Rando.Instance.Settings.RandomizeDreamers);

        public static Pool Skills => new("Skill", () => Rando.Instance.Settings.RandomizeSkills);

        public static Pool Charms => new("Charm", () => Rando.Instance.Settings.RandomizeCharms);

        public static Pool Keys => new("Key", () => Rando.Instance.Settings.RandomizeKeys);

        public static Pool MaskShards => new("Mask", () => Rando.Instance.Settings.RandomizeMaskShards);

        public static Pool VesselFragments => new("Vessel", () => Rando.Instance.Settings.RandomizeVesselFragments);

        public static Pool PaleOre => new("Ore", () => Rando.Instance.Settings.RandomizePaleOre);

        public static Pool CharmNotches => new("Notch", () => Rando.Instance.Settings.RandomizeCharmNotches);

        public static Pool GeoChests => new("Geo", () => Rando.Instance.Settings.RandomizeGeoChests);

        public static Pool Relics => new("Relic", () => Rando.Instance.Settings.RandomizeRelics);

        public static Pool RancidEggs => new("Egg", () => Rando.Instance.Settings.RandomizeRancidEggs);

        public static Pool Stags => new("Stag", () => Rando.Instance.Settings.RandomizeStags);

        public static Pool Maps => new("Map", () => Rando.Instance.Settings.RandomizeMaps);

        public static Pool WhisperingRoots => new("Root", () => Rando.Instance.Settings.RandomizeWhisperingRoots);

        public static Pool Grubs => new("Grub", () => Rando.Instance.Settings.RandomizeGrubs);

        public static Pool Lifeblood => new("Cocoon", () => Rando.Instance.Settings.RandomizeLifebloodCocoons);

        public static Pool SoulTotems => new("Soul", () => Rando.Instance.Settings.RandomizeSoulTotems);

        public static Pool PalaceTotems => new("PalaceSoul", () => Rando.Instance.Settings.RandomizePalaceTotems);

        public static Pool HunterJournal => new("Journal", () => Rando.Instance.Settings.RandomizeJournalEntries);

        public static Pool PalaceJournal => new("PalaceJournal", () => Rando.Instance.Settings.RandomizePalaceEntries);

        public static Pool LoreTablets => new("Lore", () => Rando.Instance.Settings.RandomizeLoreTablets);

        public static Pool PalaceLore => new("PalaceLore", () => Rando.Instance.Settings.RandomizePalaceTablets);

        // focus check is at the same location as the lore check, so it doesn't get placed if lore tablets are rando'd
        public static Pool Focus => new("Focus", () => Rando.Instance.Settings.RandomizeFocus && !Rando.Instance.Settings.RandomizeLoreTablets);

        public static Pool GrimmkinFlames => new("Flame", () => Rando.Instance.Settings.RandomizeGrimmkinFlames);

        public static Pool GeoRocks => new("Rock", () => Rando.Instance.Settings.RandomizeRocks);

        public static Pool BossGeo => new("Boss_Geo", () => Rando.Instance.Settings.RandomizeBossGeo);

        public static Pool BossEssence => new("Essence_Boss", () => Rando.Instance.Settings.RandomizeBossEssence);

        public static Pool JunkChests => new("JunkPitChest", () => Rando.Instance.Settings.RandomizeJunkPitChests);

        public static Pool SplitClaw => new("SplitClaw", () => Rando.Instance.Settings.RandomizeClawPieces);

        public static Pool SplitCloak => new("SplitCloakLocation", () => Rando.Instance.Settings.RandomizeCloakPieces);

        public static Pool EggShop => new("EggShopLocation", () => Rando.Instance.Settings.EggShop);

        public static Pool Mimics => new("Mimic", () => Rando.Instance.Settings.RandomizeMimics);

        public static Pool ElevatorPass => new("ElevatorPass", () => Rando.Instance.Settings.ElevatorPass);

        public string Name { get; private set; }

        private readonly Func<bool> isRandomizedPredicate;
        public bool IsRandomized => isRandomizedPredicate();

        private Pool(string poolName, Func<bool> isRandomizedPredicate)
        {
            Name = poolName;
            this.isRandomizedPredicate = isRandomizedPredicate;
        }
    }
}
