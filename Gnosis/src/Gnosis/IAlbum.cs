﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IAlbum
        : IApplication
    {
        string Title { get; }
        DateTime Released { get; }
        
        Uri Artist { get; }
        string ArtistName { get; }
        
        Uri Thumbnail { get; }
    }
}
