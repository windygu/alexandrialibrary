﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.States.Areas
{
    public interface IArea
    {
        ICard GetCard(Guid id);
        ICardInPlay<ICard> GetCardInPlay(Guid id);
    }
}
