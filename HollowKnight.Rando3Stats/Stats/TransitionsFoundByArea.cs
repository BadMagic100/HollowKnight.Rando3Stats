using RandomizerMod.Randomization;
using System.Linq;
using Rando = RandomizerMod.RandomizerMod;

namespace HollowKnight.Rando3Stats.Stats
{
    public class TransitionsFoundByArea : PercentageStatistic, IToggleableStatistic
    {
        public static TransitionsFoundByArea[] GetAllAreas()
        {
            return new TransitionsFoundByArea[]
            {
                new(LogicalAreaGrouping.AREA_NAME_CLIFFS),
                new(LogicalAreaGrouping.AREA_NAME_CROSSROADS),
                new(LogicalAreaGrouping.AREA_NAME_GREENPATH),
                new(LogicalAreaGrouping.AREA_NAME_FUNGAL),
                new(LogicalAreaGrouping.AREA_NAME_CANYON),
                new(LogicalAreaGrouping.AREA_NAME_CITY),
                new(LogicalAreaGrouping.AREA_NAME_WATERWAYS),
                new(LogicalAreaGrouping.AREA_NAME_PEAKS),
                new(LogicalAreaGrouping.AREA_NAME_GROUNDS),
                new(LogicalAreaGrouping.AREA_NAME_DEEPNEST),
                new(LogicalAreaGrouping.AREA_NAME_BASIN),
                new(LogicalAreaGrouping.AREA_NAME_EDGE),
                new(LogicalAreaGrouping.AREA_NAME_PALACE),
                new(LogicalAreaGrouping.AREA_NAME_GARDENS)
            };
        }

        public bool IsEnabled => Rando.Instance.Settings.RandomizeAreas || Rando.Instance.Settings.RandomizeRooms;

        protected override string StatNamespace => $"{base.StatNamespace}.{Label.Replace(" ", "").Replace("'", "")}";

        private string areaName;

        public TransitionsFoundByArea(string areaName) : base(areaName, true) 
        {
            this.areaName = areaName;
        }

        protected override int GetObtained()
        {
            return Rando.Instance.Settings.GetTransitionsFound()
                .Where(x => LogicalAreaGrouping.GetLogicalAreaOf(x) == areaName)
                .Count();
        }

        protected override int GetTotal()
        {
            return LogicManager.TransitionNames()
                .Where(x => LogicalAreaGrouping.GetLogicalAreaOf(x) == areaName)
                .Count();
        }
    }
}
