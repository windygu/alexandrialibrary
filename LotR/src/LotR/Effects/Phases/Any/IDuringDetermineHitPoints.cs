﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States.Phases.Any;

namespace LotR.Effects.Phases.Any
{
    public interface IDuringDetermineHitPoints
        : IEffect
    {
        void DuringDetermineHitPoints(IDetermineHitPoints state);
    }
}