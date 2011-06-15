﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.Commands;

namespace Gnosis.Core.Queries
{
    public class Query<T>
        : IQuery<T> where T : IEntity
    {
        public Query(IDbConnection connection, ILogger logger, IFactory factory, IFilter filter)
        {
            var entityInfo = new EntityInfo(typeof(T));

            this.connection = connection;
            this.logger = logger;
            this.factory = factory;
            this.builder = new CommandBuilder(entityInfo.Name, entityInfo.Type);
            this.whereClause = filter.WhereClause;
            this.orderByClause = filter.OrderByClause;
            this.parameters = filter.Parameters;

            builder.AddStatement(new SelectStatement(entityInfo, filter));
            foreach (var parameter in filter.Parameters)
                builder.AddParameter(parameter.Key, parameter.Value);

            AddChildStatements(builder, entityInfo, filter);
        }

        private readonly IDbConnection connection;
        private readonly ILogger logger;
        private readonly IFactory factory;
        private readonly ICommandBuilder builder;
        private readonly string whereClause;
        private readonly string orderByClause;
        private readonly IEnumerable<KeyValuePair<string, object>> parameters;

        private void AddChildStatements(ICommandBuilder parentBuilder, EntityInfo entityInfo, IFilter filter)
        {
            foreach (var childInfo in entityInfo.Children)
            {
                var childBuilder = new CommandBuilder(childInfo.Name, childInfo.Type);
                childBuilder.AddStatement(new SelectStatement(childInfo, filter));
                foreach (var parameter in filter.Parameters)
                    childBuilder.AddParameter(parameter.Key, parameter.Value);

                parentBuilder.AddChild(childBuilder);

                AddChildStatements(childBuilder, childInfo, filter);
            }

            foreach (var valueInfo in entityInfo.Values)
            {
                var valueBuilder = new CommandBuilder(valueInfo.Name, valueInfo.Type);
                valueBuilder.AddStatement(new SelectStatement(valueInfo, filter));
                foreach (var parameter in filter.Parameters)
                    valueBuilder.AddParameter(parameter.Key, parameter.Value);

                parentBuilder.AddChild(valueBuilder);
            }
        }

        private void AddChildren(IDbConnection connection, ICommandBuilder parentBuilder, IEnumerable<IEntity> parents)
        {
            logger.Info("  Query.AddChildren");
            foreach (var childBuilder in parentBuilder.Children)
            {
                if (childBuilder.Type == null)
                    continue;

                var isChildType = childBuilder.Type.IsChildType();

                var children = new Dictionary<Guid, IList<IChild>>();
                var values = new Dictionary<Guid, IList<IValue>>();
                var allChildren = new List<IEntity>();

                using (var command = childBuilder.GetCommand(connection))
                {
                    logger.Debug("    " + command.CommandText.Trim());
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (isChildType)
                            {
                                var child = factory.CreateChild(childBuilder.Type, reader);

                                if (!children.ContainsKey(child.Parent))
                                    children.Add(child.Parent, new List<IChild>());

                                children[child.Parent].Add(child);
                                allChildren.Add(child);
                            }
                            else
                            {
                                var value = factory.CreateValue(childBuilder.Type, reader);

                                if (!values.ContainsKey(value.Parent))
                                    values.Add(value.Parent, new List<IValue>());
                                
                                values[value.Parent].Add(value);
                            }
                        }
                    }
                }

                if (isChildType)
                {
                    foreach (var parent in parents)
                    {
                        if (children.ContainsKey(parent.Id))
                        {
                            var theseChildren = children[parent.Id];
                            parent.InitializeChildren(childBuilder.Name, theseChildren);
                        }
                    }

                    //factory.AddChildren(childBuilder.Name, parents, children);

                    AddChildren(connection, childBuilder, allChildren);
                }
                else
                {
                    foreach (var parent in parents)
                    {
                        if (values.ContainsKey(parent.Id))
                        {
                            var theseValues = values[parent.Id];
                            parent.InitializeValues(childBuilder.Name, theseValues);
                        }
                    }

                    //factory.AddValues(childBuilder.Name, parents, values);
                }
            }
        }

        public IEnumerable<T> Execute()
        {
            logger.Info("Query.Execute");

            var items = new List<T>();

            var command = builder.GetCommand(connection);
            logger.Debug("    " + command.CommandText.Trim());
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var item = factory.CreateEntity<T>(reader);
                    if (item != null)
                    {
                        items.Add(item);
                    }
                }
            }

            AddChildren(connection, builder, items.Cast<IEntity>());

            logger.Debug("  return items. count=" + items.Count);
            return items;
        }
    }
}
