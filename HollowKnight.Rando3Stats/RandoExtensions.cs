using RandomizerMod;
using System.Collections.Generic;
using System.Linq;

namespace HollowKnight.Rando3Stats
{
    public static class RandoExtensions
    {
        public static IEnumerable<string> GetAllItemsPlacedAt(this SaveSettings settings, string location)
        {
            return settings.ItemPlacements.Where(x => x.Item2 == location).Select(x => x.Item1);
        }
    }
}
