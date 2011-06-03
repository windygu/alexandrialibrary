﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Repositories
{
    public abstract class SearchBase<T>
        : ISearch
    {
        protected SearchBase(string name, string whereClause, string orderByClause, IEnumerable<string> columns)
            : this(name, whereClause, orderByClause, columns, false)
        {
        }

        protected SearchBase(string name, string whereClause, string orderByClause, IEnumerable<string> columns, bool isDefault)
        {
            this.name = name;
            this.whereClause = whereClause;
            this.orderByClause = orderByClause;
            this.columns = columns;
            this.isDefault = isDefault;
        }

        private readonly string name;
        private readonly string whereClause;
        private readonly string orderByClause;
        private readonly IEnumerable<string> columns;
        private readonly bool isDefault;

        public string Name
        {
            get { return name; }
        }

        public Type BaseType
        {
            get { return typeof(T); }
        }

        public string WhereClause
        {
            get { return whereClause; }
        }

        public string OrderByClause
        {
            get { return orderByClause; }
        }

        public bool IsDefault
        {
            get { return isDefault; }
        }

        public IEnumerable<string> Columns
        {
            get { return columns; }
        }

        public IFilter GetFilter()
        {
            return GetFilter(null);
        }

        public IFilter GetFilter(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return new Filter(whereClause, orderByClause, parameters);
        }
    }
}