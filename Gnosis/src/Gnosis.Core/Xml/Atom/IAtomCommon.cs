﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.W3c;

namespace Gnosis.Core.Xml.Atom
{
    public interface IAtomCommon
        : IElement
    {
        Uri BaseId { get; }
        ILanguageTag Lang { get; }
    }
}
