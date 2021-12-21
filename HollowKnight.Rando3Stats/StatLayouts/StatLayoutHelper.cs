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

        // assuming that each position is only visited once, so no need to cache
        internal static Layout? GetLayoutForPosition(GameObject canvas, StatPosition pos)
        {
            if (pos == StatPosition.None)
            {
                return null;
            }
            else
            {
                HorizontalAlignment desiredHorizontal = pos.ToString() switch
                {
                    var left when left.EndsWith("Left") => HorizontalAlignment.Left,
                    var center when center.EndsWith("Center") => HorizontalAlignment.Center,
                    var right when right.EndsWith("Right") => HorizontalAlignment.Right,
                    _ => throw new NotImplementedException($"Can't infer horizontal alignment from {pos}")
                };
                VerticalAlignment desiredVertical = pos.ToString() switch
                {
                    var top when top.StartsWith("Top") => VerticalAlignment.Top,
                    var bottom when bottom.StartsWith("Bottom") => VerticalAlignment.Bottom,
                    _ => throw new NotImplementedException($"Can't infer vertical alignment from {pos}")
                };
                return new VerticalStackLayout(canvas, VERTICAL_SPACING * 1.5f, desiredHorizontal, desiredVertical);
            }
        }

        internal static void SetPanelPosition(Layout? panel)
        {
            if (panel == null) return;

            float x = panel.HorizontalAlignment switch
            {
                HorizontalAlignment.Left => HORIZONTAL_PADDING,
                HorizontalAlignment.Center => GuiManager.ReferenceSize.x / 2,
                HorizontalAlignment.Right => GuiManager.ReferenceSize.x - HORIZONTAL_PADDING,
                _ => throw new NotImplementedException($"Can't handle horizontal alignment {panel.HorizontalAlignment}")
            };
            float y = panel.VerticalAlignment switch
            {
                VerticalAlignment.Top => VERTICAL_PADDING,
                VerticalAlignment.Center => GuiManager.ReferenceSize.y / 2,
                VerticalAlignment.Bottom => GuiManager.ReferenceSize.y - VERTICAL_PADDING,
                _ => throw new NotImplementedException($"Can't handle vertical alignment {panel.VerticalAlignment}")
            };
            panel.PositionAt(new Vector2(x, y));
        }

        internal static StatLayoutFactoryBase? GetLayoutBuilderFromSettings(StatLayoutData data)
        {
            string className = $"{data.Stat}StatLayout";
            Type layoutClass = Type.GetType($"HollowKnight.Rando3Stats.StatLayouts.{className}");
            if (data.Stat == null || layoutClass == null || !typeof(StatLayoutFactoryBase).IsAssignableFrom(layoutClass))
            {
                log.LogWarn($"Encountered a stat data without a valid target stat - cannot display: {data.Stat}");
                return null;
            }

            ConstructorInfo ctor = layoutClass.GetConstructor(new Type[] { typeof(HashSet<string>) });
            return (StatLayoutFactoryBase)ctor.Invoke(new object[] { data.EnabledSubcategories });
        }

        internal static int GetDynamicGridColumnsForPosition(StatPosition position) => position switch
        {
            StatPosition.TopCenter => 6,
            _ => 2
        };
    }
}
