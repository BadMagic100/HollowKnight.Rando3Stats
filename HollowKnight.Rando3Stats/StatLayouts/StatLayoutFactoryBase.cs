using HollowKnight.Rando3Stats.Stats;
using HollowKnight.Rando3Stats.UI;
using System.Collections.Generic;
using UnityEngine;

namespace HollowKnight.Rando3Stats.StatLayouts
{
    /// <summary>
    /// Base class to build configurable layouts for collections of stats
    /// </summary>
    internal abstract class StatLayoutFactoryBase
    {
        /// <summary>
        /// A set of subcategories of stats to display
        /// </summary>
        protected HashSet<string> EnabledSubcategories { get; private set; }

        /// <summary>
        /// Constructs a layout factory. Note that to work with <see cref="StatLayoutHelper.GetLayoutBuilderFromSettings(StatLayoutData)"/>,
        /// you're expected to implement a constructor with this signature.
        /// </summary>
        /// <param name="enabledSubcategories">A set of subcategories of stats to display</param>
        public StatLayoutFactoryBase(HashSet<string> enabledSubcategories)
        {
            EnabledSubcategories = enabledSubcategories;
        }

        /// <summary>
        /// Determines whether the stat is eligible to display given the current randomizer settings
        /// </summary>
        public abstract bool ShouldDisplayForRandoSettings();

        /// <summary>
        /// Gets the top-level section header for the stat group, such as "Items Obtained"
        /// </summary>
        protected abstract string GetSectionHeader();
        /// <summary>
        /// Gets all the statistics that should display unconditionally.
        /// </summary>
        protected abstract IEnumerable<IRandomizerStatistic> GetRootStatistics();
        /// <summary>
        /// Gets all subcategories that are allowed for this stat group. These cases, and only these cases, should be handled by
        /// <see cref="GetStatisticsForSubcategory(string)"/>.
        /// </summary>
        protected abstract IEnumerable<string> GetAllowedSubcategories();
        /// <summary>
        /// Gets all the statistics that should display given a subcategory. This expects you to
        /// </summary>
        /// <param name="subcategory">The subcategory name.</param>
        protected abstract IEnumerable<IRandomizerStatistic> GetStatisticsForSubcategory(string subcategory);

        /// <summary>
        /// Gets whether the stat is explicitly disabled - in other words, if it is both toggleable and disabled.
        /// </summary>
        /// <param name="stat">The stat to check.</param>
        private bool IsDisabled(IRandomizerStatistic stat)
        {
            return stat is IToggleableStatistic toggle && !toggle.IsEnabled;
        }

        /// <summary>
        /// Computes the stats only, without creating any UI elements or layout. This is specifically for adding
        /// stats to the stat registry.
        /// </summary>
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

        /// <summary>
        /// Computes the stats and builds them out in the specified layout.
        /// </summary>
        /// <param name="canvas">The visual parent to draw the UI on</param>
        /// <param name="subcategoryColumns">The number of columns allocated to subcategories' dynamic layouts.</param>
        /// <returns></returns>
        public Layout BuildLayout(GameObject canvas, int subcategoryColumns)
        {
            VerticalStackLayout layout = new(canvas, StatLayoutHelper.VERTICAL_SPACING, HorizontalAlignment.Center, name: GetType().Name);
            layout.Children.Add(new TextObject(canvas, GetSectionHeader(), GuiManager.Instance.TrajanBold, StatLayoutHelper.FONT_SIZE_H1)
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
