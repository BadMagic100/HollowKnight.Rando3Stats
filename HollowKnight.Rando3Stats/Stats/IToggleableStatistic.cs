using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HollowKnight.Rando3Stats.Stats
{
    public interface IToggleableStatistic : IRandomizerStatistic
    {
        public bool IsEnabled { get; }
    }
}
