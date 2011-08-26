﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.Atom
{
    public class AtomPublished
        : AtomDateConstruct, IAtomPublished
    {
        public AtomPublished(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }
    }
}
