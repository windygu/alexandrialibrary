﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Repositories
{
    public interface ISearch
    {
        string Name { get; }
        string WhereClause { get; }
        string OrderByClause { get; }
        bool IsDefault { get; }
        IEnumerable<KeyValuePair<string, object>> Parameters { get; }
    }

    public interface ISearch<T>
        : ISearch
    {
    }
}
