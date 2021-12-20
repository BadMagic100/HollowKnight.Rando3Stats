using HollowKnight.Rando3Stats.Stats;
using HollowKnight.Rando3Stats.UI;
using Modding;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace HollowKnight.Rando3Stats.StatLayouts
{
    internal static class StatLayoutHelper
    {
        private static readonly SimpleLogger log = new("RandoStats:StatLayoutHelper");

        internal const float HORIZONTAL_PADDING = 5;
        internal const float VERTICAL_PADDING = 10;

        internal const float VERTICAL_SPACING = 8;
        internal const float HORIZONTAL_SPACING = 10;

        internal const int FONT_SIZE_H1 = 25;
        internal const int FONT_SIZE_H2 = 18;
        internal const int FONT_SIZE_H3 = 15;

        internal static Layout GetLabeledStatText(GameObject canvas, IRandomizerStatistic stat)
        {
            string header = stat.GetHeader();
            string text = stat.GetContent();
            Layout statStack = new VerticalStackLayout(canvas, 5f, HorizontalAlignment.Center);
            statStack.Children.Add(new AlignedText(canvas, header, GuiManager.Instance.TrajanBold, FONT_SIZE_H2, "Stat_" + header)
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            });
            statStack.Children.Add(new AlignedText(canvas, text, GuiManager.Instance.TrajanNormal, FONT_SIZE_H3, "StatValue_" + header)
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            });
            return statStack;
        }

        internal static StatLayoutBase? GetLayoutBuilderFromSettings(StatLayoutData data)
        {
            string className = $"{data.Stat}StatLayout";
            Type layoutClass = Type.GetType($"HollowKnight.Rando3Stats.StatLayouts.{className}");
            if (data.Stat == null || layoutClass == null || !typeof(StatLayoutBase).IsAssignableFrom(layoutClass))
            {
                log.LogWarn($"Encountered a stat data without a valid target stat - cannot display: {data.Stat}");
                return null;
            }

            ConstructorInfo ctor = layoutClass.GetConstructor(new Type[] { typeof(HashSet<string>) });
            return (StatLayoutBase)ctor.Invoke(new object[] { data.EnabledSubcategories });
        }

        internal static int GetDynamicGridColumnsForPosition(StatPosition position) => position switch
        {
            StatPosition.TopCenter => 6,
            _ => 2
        };
    }
}
