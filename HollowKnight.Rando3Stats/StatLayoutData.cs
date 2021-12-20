using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace HollowKnight.Rando3Stats
{
    // Be careful about modifying this, as an enum gets serialized as a number.
    // This means we could break people's save data by changing the order of these.
    // To alleviate this, we can explicitly set the numeric value rather than letting
    // the compiler do it.

    [JsonConverter(typeof(StringEnumConverter))]
    public enum StatPosition
    {
        TopLeft,
        TopCenter,
        TopRight,
        BottomLeft,
        BottomRight,
        None
    }

    public class StatLayoutData
    {
        public string? Stat { get; set; } = null;
        public HashSet<string> EnabledSubcategories { get; set; } = new();
        public StatPosition Position { get; set; } = StatPosition.None;
        public int Order { get; set; } = 0;

        [JsonConstructor]
        public StatLayoutData() { }

        public StatLayoutData(string stat, HashSet<string> enabledSubcategories, StatPosition position, int sortOrder)
        {
            Position = position;
            EnabledSubcategories = enabledSubcategories;
            Stat = stat;
            Order = sortOrder;
        }
    }
}
