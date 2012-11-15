﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Costs;
using LotR.Effects.Payments;
using LotR.Effects.Phases;
using LotR.Effects.Modifiers;
using LotR.States;
using LotR.States.Phases.Any;

namespace LotR.Cards.Player.Heroes
{
    public class Eowyn
        : HeroCardBase
    {
        public Eowyn()
            : base("Eowyn", CardSet.Core, 7, Sphere.Spirit, 9, 4, 1, 1, 3)
        {
            AddTrait(Trait.Noble);
            AddTrait(Trait.Rohan);

            AddEffect(new DiscardACardToAddOneWillpower(this));
        }

        public class DiscardACardToAddOneWillpower
            : ActionCharacterAbilityBase
        {
            public DiscardACardToAddOneWillpower(Eowyn source)
                : base("Discard 1 card from your hand to give Eowyn +1 Willpower until the end of the phase. This effect may be triggered by each player once per round.", source)
            {
            }

            public override IEffectOptions GetOptions(IGame game)
            {
                var limit = new Limit(PlayerScope.AnyPlayer, TimeScope.Round, 1);
                var cost = new DiscardCardsFromHand(Source, game, 1);
                return new EffectOptions(null, cost, limit);
            }

            public override string Resolve(IGame game, IEffectOptions options)
            {
                var controller = game.GetController(CardSource.Id);
                if (controller == null)
                    return GetCancelledString();

                var willpowerful = controller.CardsInPlay.OfType<IWillpowerfulInPlay>().Where(x => x.Card.Id == Source.Id).FirstOrDefault();
                if (willpowerful == null)
                    return GetCancelledString();

                game.AddEffect(new WillpowerModifier(game.CurrentPhase.Code, Source, willpowerful, TimeScope.Phase, 1));

                return ToString();
            }
        }
    }
}
