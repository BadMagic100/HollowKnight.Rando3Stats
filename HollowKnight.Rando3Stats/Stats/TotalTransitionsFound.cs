using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HollowKnight.Rando3Stats.Util;
using Modding;
using RandomizerMod.Randomization;
using Rando = RandomizerMod.RandomizerMod;

namespace HollowKnight.Rando3Stats.Stats
{
    public class TotalTransitionsFound : PercentageStatistic, IToggleableStatistic
    {
        public TotalTransitionsFound(string label) : base(label) { }

        public bool IsEnabled
        {
            get => Rando.Instance.Settings.RandomizeAreas || Rando.Instance.Settings.RandomizeRooms;
        }

        protected override int GetObtained()
        {
            return Rando.Instance.Settings.GetTransitionsFound().Length;
        }

        protected override int GetTotal()
        {
            // this only returns randomized transitions
            return LogicManager.TransitionNames().Length;
        }
    }
}
