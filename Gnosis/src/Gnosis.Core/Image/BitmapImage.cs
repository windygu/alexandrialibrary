﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Image
{
    public class BitmapImage
        : ImageBase
    {
        public BitmapImage(Uri location, IContentType contentType)
            : base(location, contentType)
        {
        }
    }
}