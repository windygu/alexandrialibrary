﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;

using log4net;

using Gnosis.Core;
using Gnosis.Core.Attributes;
using Gnosis.Alexandria.Models;

namespace Gnosis.Alexandria.Repositories
{
    public abstract class RepositoryBase<T>
    {
        protected RepositoryBase(IContext context)
        {
            this.context = context;
            //this.database = "Catalog.db";

            Initialize();
        }

        private static readonly ILog log = LogManager.GetLogger(typeof(RepositoryBase<T>));
        private readonly IContext context;
        private string database = "Catalog.db";

        private void Initialize()
        {
            try
            {
                var commandBuilder = new CreateCommandBuilder(typeof(T), CreateDefault());

                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var command = commandBuilder.GetCommand(connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("RepositoryBase.Initialize", ex);
            }
        }

        protected abstract T CreateDefault();
        protected abstract IEnumerable<T> Create(IDataReader reader);
        protected abstract CommandBuilder GetSaveCommandBuilder(IEnumerable<T> items);

        protected IContext Context
        {
            get { return context; }
        }

        protected IDbConnection GetConnection()
        {
            return new SQLiteConnection(string.Format("Data Source={0};Version=3;", database));
        }

        protected ITimeStamp GetTimeStamp(IDataReader reader)
        {
            var createdBy = new Uri(reader["TimeStamp_CreatedBy"].ToString());
            var createdDate = DateTime.Parse(reader["TimeStamp_CreatedDate"].ToString());
            var lastAccessedBy = new Uri(reader["TimeStamp_LastAccessedBy"].ToString());
            var lastAccessedDate = DateTime.Parse(reader["TimeStamp_LastAccessedDate"].ToString());
            var lastModifiedBy = new Uri(reader["TimeStamp_LastModifiedBy"].ToString());
            var lastModifiedDate = DateTime.Parse(reader["TimeStamp_LastModifiedDate"].ToString());

            return new TimeStamp(createdBy, createdDate, lastAccessedBy, lastAccessedDate, lastModifiedBy, lastModifiedDate);
        }

        //protected static IDbCommand GetCommand(IDbConnection connection, string commandText, IEnumerable<KeyValuePair<string, object>> parameters)
        //{
        //    var command = connection.CreateCommand();
        //    command.CommandText = commandText;

        //    foreach (var parameter in parameters)
        //        AddParameter(command, parameter.Key, parameter.Value);

        //    return command;
        //}

        protected int Execute(CommandBuilder commandBuilder)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = commandBuilder.GetCommand(connection))
                {
                    return command.ExecuteNonQuery();
                }
            }
        }

        protected IEnumerable<T> Select()
        {
            return Select(string.Empty);
        }

        protected IEnumerable<T> Select(string whereClause)
        {
            return Select(whereClause, null);
        }

        protected IEnumerable<T> Select(string whereClause, string parameterName, object parameterValue)
        {
            return Select(whereClause, new Dictionary<string, object> { { parameterName, parameterValue } });
        }

        protected IEnumerable<T> Select(string whereClause, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            //var items = new List<T>();

            var commandBuilder = new SelectCommandBuilder(typeof(T), whereClause);

            if (parameters != null)
            {
                foreach (var pair in parameters)
                    commandBuilder.AddParameter(pair.Key, pair.Value);
            }

            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = commandBuilder.GetCommand(connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        return Create(reader); 
                        //while (reader.Read())
                        //{
                        //    var item = Create(reader);
                        //    items.Add(item);
                        //}
                    }
                }
            }

            //return items;
        }

        public void Save(IEnumerable<T> items)
        {
            var builder = GetSaveCommandBuilder(items);
            Execute(builder);
        }
    }
}
