﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Payments
{
    public interface IChooseEnemyPayment
        : IPayment
    {
        IEnemyInPlay Enemy { get; }
    }
}
