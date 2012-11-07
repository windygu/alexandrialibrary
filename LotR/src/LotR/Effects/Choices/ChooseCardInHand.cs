﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.States;

namespace LotR.Effects.Choices
{
    public class ChooseCardInHand<T>
        : ChoiceBase, IChooseCardInHand<T>
        where T : class, IPlayerCard
    {
        public ChooseCardInHand(string description, ISource source, IPlayer player, IEnumerable<T> cardsToChooseFrom)
            : base(description, source, player)
        {
            this.CardsToChooseFrom = cardsToChooseFrom;
        }

        public IEnumerable<T> CardsToChooseFrom
        {
            get;
            private set;
        }

        public T ChosenCard
        {
            get;
            set;
        }

        public override bool IsValid(IGame game)
        {
            return (ChosenCard != null && CardsToChooseFrom.Contains(ChosenCard));
        }
    }
}
