﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources
{
	public interface IAggregateBuilder<T>
		where T : IAggregate
	{
		IEnumerable<IValueBuilder> ValueBuilders { get; }
		IEnumerable<ILinkBuilder> LinkBuilders { get; }
		T CreateAggregate(IEntity root);
	}
}
