using System;
using Rando = RandomizerMod.RandomizerMod;

namespace HollowKnight.Rando3Stats
{
    /// <summary>
    /// A location pool, indicating whether items at that type of location are randomized.
    /// </summary>
    public class Pool
    {
        /// <summary>
        /// Dreamer items and locations (including world sense). Dupe dreamer is included by <see cref="Util.ExtraPools.GetPoolOf"/>
        /// </summary>
        public static Pool Dreamers => new("Dreamer", () => Rando.Instance.Settings.RandomizeDreamers);

        /// <summary>
        /// Skill items and locations
        /// </summary>
        public static Pool Skills => new("Skill", () => Rando.Instance.Settings.RandomizeSkills);

        /// <summary>
        /// Charm items and locations
        /// </summary>
        public static Pool Charms => new("Charm", () => Rando.Instance.Settings.RandomizeCharms);

        /// <summary>
        /// Key items and locations
        /// </summary>
        public static Pool Keys => new("Key", () => Rando.Instance.Settings.RandomizeKeys);

        /// <summary>
        /// Mask shard items and locations
        /// </summary>
        public static Pool MaskShards => new("Mask", () => Rando.Instance.Settings.RandomizeMaskShards);

        /// <summary>
        /// Vessel fragment items and locations
        /// </summary>
        public static Pool VesselFragments => new("Vessel", () => Rando.Instance.Settings.RandomizeVesselFragments);

        /// <summary>
        /// Pale ore items and locations
        /// </summary>
        public static Pool PaleOre => new("Ore", () => Rando.Instance.Settings.RandomizePaleOre);

        /// <summary>
        /// Charm notch items and locations
        /// </summary>
        public static Pool CharmNotches => new("Notch", () => Rando.Instance.Settings.RandomizeCharmNotches);

        /// <summary>
        /// Geo chest items and locations
        /// </summary>
        public static Pool GeoChests => new("Geo", () => Rando.Instance.Settings.RandomizeGeoChests);

        /// <summary>
        /// Relic items and locations
        /// </summary>
        public static Pool Relics => new("Relic", () => Rando.Instance.Settings.RandomizeRelics);

        /// <summary>
        /// Rancid egg items (in non-egg shop rando) and locations (always).
        /// </summary>
        public static Pool RancidEggs => new("Egg", () => Rando.Instance.Settings.RandomizeRancidEggs);

        /// <summary>
        /// Stag station items and locations.
        /// </summary>
        public static Pool Stags => new("Stag", () => Rando.Instance.Settings.RandomizeStags);

        /// <summary>
        /// Map items and locations
        /// </summary>
        public static Pool Maps => new("Map", () => Rando.Instance.Settings.RandomizeMaps);

        /// <summary>
        /// Whispering root items and locations.
        /// </summary>
        public static Pool WhisperingRoots => new("Root", () => Rando.Instance.Settings.RandomizeWhisperingRoots);

        /// <summary>
        /// Grub locations
        /// </summary>
        public static Pool GrubLocationOnly => new("Grub", () => Rando.Instance.Settings.RandomizeGrubs);

        /// <summary>
        /// Grub items (jars/shinies)
        /// </summary>
        public static Pool GrubItemOnly => new("GrubItem", () => Rando.Instance.Settings.RandomizeGrubs);

        /// <summary>
        /// Lifeblood cocoon items and locations.
        /// </summary>
        public static Pool Lifeblood => new("Cocoon", () => Rando.Instance.Settings.RandomizeLifebloodCocoons);

        /// <summary>
        /// Overworld soul totem items and locations
        /// </summary>
        public static Pool SoulTotems => new("Soul", () => Rando.Instance.Settings.RandomizeSoulTotems);

        /// <summary>
        /// Palace soul totem items and locations
        /// </summary>
        public static Pool PalaceTotems => new("PalaceSoul", () => Rando.Instance.Settings.RandomizePalaceTotems);

        /// <summary>
        /// Overworld journal entry items and locations
        /// </summary>
        public static Pool HunterJournal => new("Journal", () => Rando.Instance.Settings.RandomizeJournalEntries);

        /// <summary>
        /// Palace journal entry item and location
        /// </summary>
        public static Pool PalaceJournal => new("PalaceJournal", () => Rando.Instance.Settings.RandomizePalaceEntries);

        /// <summary>
        /// Overworld lore tablet items and locations
        /// </summary>
        public static Pool LoreTablets => new("Lore", () => Rando.Instance.Settings.RandomizeLoreTablets);

        /// <summary>
        /// Palace lore tablet items and locations
        /// </summary>
        public static Pool PalaceLore => new("PalaceLore", () => Rando.Instance.Settings.RandomizePalaceTablets);

        /// <summary>
        /// Focus item. This is always marked as rando-enabled even if lore tablets are also randomized.
        /// </summary>
        public static Pool FocusItemOnly => new("Focus", () => Rando.Instance.Settings.RandomizeFocus);

        /// <summary>
        /// Focus location. Randomization logic does not place focus if lore tablets are also randomized.
        /// </summary>
        public static Pool FocusLocationOnly => new("Focus", () => Rando.Instance.Settings.RandomizeFocus && !Rando.Instance.Settings.RandomizeLoreTablets);

        /// <summary>
        /// Grimm flame items and locations
        /// </summary>
        public static Pool GrimmkinFlames => new("Flame", () => Rando.Instance.Settings.RandomizeGrimmkinFlames);

        /// <summary>
        /// Geo rock items and locations
        /// </summary>
        public static Pool GeoRocks => new("Rock", () => Rando.Instance.Settings.RandomizeRocks);

        /// <summary>
        /// Boss geo items and locations
        /// </summary>
        public static Pool BossGeo => new("Boss_Geo", () => Rando.Instance.Settings.RandomizeBossGeo);

        /// <summary>
        /// Boss essence items and locations
        /// </summary>
        public static Pool BossEssence => new("Essence_Boss", () => Rando.Instance.Settings.RandomizeBossEssence);

        /// <summary>
        /// Junk pit chest items and locations
        /// </summary>
        public static Pool JunkChests => new("JunkPitChest", () => Rando.Instance.Settings.RandomizeJunkPitChests);

        /// <summary>
        /// The 2 locations and item pool for split claw
        /// </summary>
        public static Pool SplitClaw => new("SplitClaw", () => Rando.Instance.Settings.RandomizeClawPieces);

        /// <summary>
        /// The item for split cloak
        /// </summary>
        public static Pool SplitCloakItemOnly => new("SplitCloak", () => Rando.Instance.Settings.RandomizeCloakPieces);

        /// <summary>
        /// The location of the additional shiny dropped by Hornet 1 in split cloak rando
        /// </summary>
        public static Pool SplitCloakLocationOnly => new("SplitCloakLocation", () => Rando.Instance.Settings.RandomizeCloakPieces);

        /// <summary>
        /// Rancid eggs generated by egg shop rando.
        /// </summary>
        public static Pool EggShopEggItems => new("EggShopItem", () => Rando.Instance.Settings.EggShop);
        
        /// <summary>
        /// The locations at egg shop. The items here are 5 450-geo items.
        /// </summary>
        public static Pool EggShopGeo => new("EggShopLocation", () => Rando.Instance.Settings.EggShop);

        /// <summary>
        /// Mimic locations. Apparently only counts if grubs are also on
        /// </summary>
        public static Pool MimicLocationOnly => new("Mimic", () => Rando.Instance.Settings.RandomizeGrubs && Rando.Instance.Settings.RandomizeMimics);

        /// <summary>
        /// Mimic items (jars/shinies)
        /// </summary>
        public static Pool MimicItemOnly => new("MimicItem", () => Rando.Instance.Settings.RandomizeGrubs && Rando.Instance.Settings.RandomizeMimics);

        /// <summary>
        /// The elevator pass location (elevator toll) and item.
        /// </summary>
        public static Pool ElevatorPass => new("ElevatorPass", () => Rando.Instance.Settings.ElevatorPass);

        /// <summary>
        /// The swim item
        /// </summary>
        public static Pool SwimItemOnly => new("Swim", () => Rando.Instance.Settings.RandomizeSwim);

        /// <summary>
        /// Cursed mask items
        /// </summary>
        public static Pool CursedMasksItemOnly => new("CursedMask", () => Rando.Instance.Settings.CursedMasks);

        /// <summary>
        /// Cursed notch items
        /// </summary>
        public static Pool CursedNotchesItemOnly => new("CursedNotch", () => Rando.Instance.Settings.CursedNotches);

        /// <summary>
        /// Cursed nail items
        /// </summary>
        public static Pool CursedNailItemOnly => new("CursedNail", () => Rando.Instance.Settings.CursedNail);

        /// <summary>
        /// Fake item pool for cursed 1-geo/nothing.
        /// </summary>
        public static Pool CursedJunkItems => new(ExtraPools.CUSTOM_POOL_CURSED, () => Rando.Instance.Settings.Cursed);

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
