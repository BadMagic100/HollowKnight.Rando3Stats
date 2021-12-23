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

        /// <summary>
        /// Generates a layout with the stat's header and the stat's content in a sligntly smaller font
        /// </summary>
        /// <param name="canvas">The canvas to draw the elements on</param>
        /// <param name="stat">The stat to compute and draw</param>
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

        /// <summary>
        /// Given a position, generates a stack layout with the correct alignment for that position
        /// </summary>
        /// <param name="canvas">The canvas to draw the elements on</param>
        /// <param name="pos">The stat position</param>
        /// <returns></returns>
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

        /// <summary>
        /// Sets the panel's anchoring position to the correct edge of the screen given its alignment and arranges its children.
        /// </summary>
        /// <param name="panel">The panel to position</param>
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

        /// <summary>
        /// Parses layout data from settings to generate a stat layout builder
        /// </summary>
        /// <param name="data">The stat layout data from global settings</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the number of columns to allocate to dynamic grids in a given stat position
        /// </summary>
        /// <param name="position">The position to check</param>
        internal static int GetDynamicGridColumnsForPosition(StatPosition position) => position switch
        {
            StatPosition.TopCenter => 6,
            _ => 2
        };
    }
}
