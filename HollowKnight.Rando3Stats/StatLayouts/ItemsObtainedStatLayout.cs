using HollowKnight.Rando3Stats.Stats;
using System;
using System.Collections.Generic;

namespace HollowKnight.Rando3Stats.StatLayouts
{
    internal class ItemsObtainedStatLayout : StatLayoutBase
    {
        public ItemsObtainedStatLayout(HashSet<string> enabledSubcategories) : base(enabledSubcategories)
        {
        }

        public override bool ShouldDisplayForRandoSettings() => true;

        protected override IEnumerable<string> GetAllowedSubcategories() => new string[] { "ByPool" };

        protected override IEnumerable<IRandomizerStatistic> GetRootStatistics() => new IRandomizerStatistic[]
        {
            new TotalItemsObtained("Total")
        };

        protected override string GetSectionHeader() => "Items Obtained";

        protected override IEnumerable<IRandomizerStatistic> GetStatisticsForSubcategory(string subcategory) => subcategory switch
        {
            "ByPool" => ItemsObtainedByPoolGroup.GetAllPoolGroups(),
            _ => throw new NotImplementedException($"Subcategory {subcategory} not implemented. This probably means it was added to GetAllowedSubcategories but not here.")
        };
    }
}
