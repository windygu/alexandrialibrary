﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GnosisTests
{
    public interface IEntityCache<T>
        where T : IEntity
    {
        IEnumerable<T> Entities { get; }

        T Get(uint id);
        void Add(T entity);
        void Add(IEnumerable<T> entities);
        void Remove(uint id);
        void Remove(IEnumerable<uint> ids);
    }
}
