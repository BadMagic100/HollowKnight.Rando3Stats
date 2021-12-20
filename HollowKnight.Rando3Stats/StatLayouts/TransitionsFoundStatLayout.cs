using HollowKnight.Rando3Stats.Stats;
using System;
using System.Collections.Generic;

namespace HollowKnight.Rando3Stats.StatLayouts
{
    internal class TransitionsFoundStatLayout : StatLayoutBase
    {
        private readonly TotalTransitionsFound transitionsFound;

        public TransitionsFoundStatLayout(HashSet<string> enabledSubcategories) : base(enabledSubcategories)
        {
            transitionsFound = new TotalTransitionsFound("Total");
        }

        public override bool ShouldDisplayForRandoSettings() => transitionsFound.IsEnabled;

        protected override IEnumerable<string> GetAllowedSubcategories() => new string[]
        {
            "ByArea"
        };

        protected override IEnumerable<IRandomizerStatistic> GetRootStatistics() => new IRandomizerStatistic[]
        {
            transitionsFound
        };

        protected override string GetSectionHeader() => "Transitions Found";

        protected override IEnumerable<IRandomizerStatistic> GetStatisticsForSubcategory(string subcategory) => subcategory switch {
            "ByArea" => TransitionsFoundByArea.GetAllAreas(),
            _ => throw new NotImplementedException($"Subcategory {subcategory} not implemented. This probably means it was added to GetAllowedSubcategories but not here.")
        };
}
}
