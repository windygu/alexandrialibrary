﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Payments;

namespace LotR.Costs
{
    public class ChooseCardDestination
        : CostBase
    {
        public ChooseCardDestination(ICard source)
            : base("Choose whether this card goes on the top or bottom of the deck", source)
        {
        }

        public override bool IsMetBy(IPayment payment)
        {
            if (payment == null)
                return false;

            var choice = payment as IChooseCardDestinationPayment;
            if (choice == null)
                return false;

            return true;
        }
    }
}
