﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Phases.Resource
{
    public interface IDrawCardsStep
        : IPhaseStep
    {
        bool CanDrawCards { get; set; }
        byte NumberOfCardsToDraw { get; set; }
    }
}
