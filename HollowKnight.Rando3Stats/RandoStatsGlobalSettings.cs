using Modding;
using System.Collections.Generic;

namespace HollowKnight.Rando3Stats
{
    public class RandoStatsGlobalSettings : ModSettings
    {
        // consistent with Rando's "vanilla" counting - they do floor(itemsFound/itemsPlaced*100), so each shop item should be counted by default.
        // ref: https://github.com/homothetyhk/HollowKnight.RandomizerMod/blob/e9ef1ee1c903d3da5d74a6c389b167caf29ba6c1/RandomizerMod3.0/RandomizerMod.cs#L375
        public bool CountShopItemsIndividually { get; set; } = true;

        public string CompletionFormatString { get; set; } = "$RACING_EXTENDED$";

        // default stats
        public List<StatLayoutData> StatConfig { get; set; } = new(new StatLayoutData[] {
            new("LocationsChecked", new(new string[] { "ByPool" }), StatPosition.TopLeft, 0),
            new("ItemsObtained", new(new string[] { "ByPool" }), StatPosition.TopRight, 0),
            new("TransitionsFound", new(new string[] { "ByArea" }), StatPosition.BottomRight, 0)
        });
    }
}
