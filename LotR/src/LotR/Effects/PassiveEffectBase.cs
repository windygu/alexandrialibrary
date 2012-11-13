﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.Effects
{
    public abstract class PassiveEffectBase
        : EffectBase, IPassiveEffect
    {
        protected PassiveEffectBase(string description, ISource source)
            : base("Passive Effect", description, source)
        {
        }

        protected PassiveEffectBase(string name, string description, ISource source)
            : base(name, description, source)
        {
        }

        public override string ToString()
        {
            return Description;
        }
    }
}
