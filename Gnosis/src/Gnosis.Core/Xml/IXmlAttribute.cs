﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml
{
    public interface IXmlAttribute
    {
        IXmlQualifiedName Name { get; }
        string Value { get; }
    }
}
