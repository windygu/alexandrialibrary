﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sophia.Core
{
	public interface IMessage
	{
		MediaType ContentType { get; }
		string Content { get; }
	}
}