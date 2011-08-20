﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.Rss
{
    public class RssCategory
        : Element, IRssCategory
    {
        public RssCategory(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public Uri Domain
        {
            get { return GetAttributeUri("domain"); }
        }

        public new string Name
        {
            get { return GetContentString(); }
        }
    }
}