using HollowKnight.Rando3Stats.Stats;
using HollowKnight.Rando3Stats.UI;
using System.Collections.Generic;
using UnityEngine;

namespace HollowKnight.Rando3Stats.StatLayouts
{
    internal abstract class StatLayoutBase
    {
        protected HashSet<string> EnabledSubcategories { get; private set; }

        public StatLayoutBase(HashSet<string> enabledSubcategories)
        {
            EnabledSubcategories = enabledSubcategories;
        }

        public abstract bool ShouldDisplayForRandoSettings();

        protected abstract string GetSectionHeader();
        protected abstract IEnumerable<IRandomizerStatistic> GetRootStatistics();
        protected abstract IEnumerable<string> GetAllowedSubcategories();
        protected abstract IEnumerable<IRandomizerStatistic> GetStatisticsForSubcategory(string subcategory);

        private bool IsDisabled(IRandomizerStatistic stat)
        {
            return stat is IToggleableStatistic toggle && !toggle.IsEnabled;
        }

        // for the specific purpose of adding things into the stat registry
        public void ComputeStatsOnly()
        {
            foreach (IRandomizerStatistic stat in GetRootStatistics())
            {
                stat.GetHeader();
                stat.GetContent();
            }
            foreach (string subcategory in GetAllowedSubcategories())
            {
                if (EnabledSubcategories.Contains(subcategory))
                {
                    foreach (IRandomizerStatistic stat in GetStatisticsForSubcategory(subcategory))
                    {
                        if (!IsDisabled(stat))
                        {
                            stat.GetHeader();
                            stat.GetContent();
                        }
                    }
                }
            }
        }

        public Layout BuildLayout(GameObject canvas, int subcategoryColumns)
        {
            VerticalStackLayout layout = new(canvas, StatLayoutHelper.VERTICAL_SPACING, HorizontalAlignment.Center, name: GetType().Name);
            layout.Children.Add(new AlignedText(canvas, GetSectionHeader(), GuiManager.Instance.TrajanBold, StatLayoutHelper.FONT_SIZE_H1)
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            });
            foreach (IRandomizerStatistic stat in GetRootStatistics())
            {
                layout.Children.Add(StatLayoutHelper.GetLabeledStatText(canvas, stat));
            }

            foreach (string subcategory in GetAllowedSubcategories())
            {
                if (EnabledSubcategories.Contains(subcategory))
                {
                    Layout subcategoryGroupLayout = new DynamicGridLayout(canvas,
                        StatLayoutHelper.HORIZONTAL_SPACING, StatLayoutHelper.VERTICAL_SPACING,
                        subcategoryColumns, HorizontalAlignment.Center, name: $"{GetType().Name}_{subcategory}");
                    foreach (IRandomizerStatistic stat in GetStatisticsForSubcategory(subcategory))
                    {
                        if (!IsDisabled(stat))
                        {
                            subcategoryGroupLayout.Children.Add(StatLayoutHelper.GetLabeledStatText(canvas, stat));
                        }
                    }
                    layout.Children.Add(subcategoryGroupLayout);
                }
            }

            return layout;
        }
    }
}
