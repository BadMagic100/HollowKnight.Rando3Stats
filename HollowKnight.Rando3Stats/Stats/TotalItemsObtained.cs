﻿using System;
using System.Linq;
using System.Collections.Generic;
using Modding;
using Rando = RandomizerMod.RandomizerMod;
using RandomizerMod.Randomization;
using HollowKnight.Rando3Stats.Util;

namespace HollowKnight.Rando3Stats.Stats
{
    public class TotalItemsObtained : PercentageStatistic
    {
        public TotalItemsObtained(string label) : base(label, true) { }

        protected override int GetObtained()
        {
            return RandoExtensions.GetRandomizedItemsWithDupes().Count(Rando.Instance.Settings.CheckItemFound);
        }

        protected override int GetTotal()
        {
            return RandoExtensions.GetRandomizedItemsWithDupes().Count();
        }
    }
}
