using HollowKnight.Rando3Stats.Stats;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HollowKnight.Rando3Stats.StatLayouts
{
    internal class LocationsCheckedStatLayout : StatLayoutFactoryBase
    {
        public LocationsCheckedStatLayout(HashSet<string> enabledSubcategories) : base(enabledSubcategories)
        {
        }

        public override bool ShouldDisplayForRandoSettings() => true;

        protected override IEnumerable<string> GetAllowedSubcategories() => new string[] { "ByPool" };

        protected override IEnumerable<IRandomizerStatistic> GetRootStatistics() => new IRandomizerStatistic[] 
        { 
            new TotalLocationsChecked("Total")
        };

        protected override string GetSectionHeader() => "Locations Found";

        protected override IEnumerable<IRandomizerStatistic> GetStatisticsForSubcategory(string subcategory) => subcategory switch
        {
            "ByPool" => LocationsCheckedByPoolGroup.GetAllPoolGroups().Concat(new IRandomizerStatistic[] 
            { 
                new GeoShopChecksSeen("Geo Shops") 
            }),
            _ => throw new NotImplementedException($"Subcategory {subcategory} not implemented. This probably means it was added to GetAllowedSubcategories but not here.")
        };
    }
}
