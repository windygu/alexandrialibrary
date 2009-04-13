﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources
{
	public class Link : Resource, ILink
	{
		public Link(Uri id, LinkType type, IEntity subject, IEntity obj)
			: this(id, type, subject, obj, 0)
		{
		}

		public Link(Uri id, LinkType type, IEntity subject, IEntity obj, int sequence)
			: base(id)
		{
			this.type = type;
			this.subject = subject;
			this.obj = obj;
			this.sequence = sequence;
		}

		#region Private Members

		private LinkType type;
		private IEntity subject;
		private IEntity obj;
		private int sequence;

		#endregion

		#region ILink Members

		public ILinkType Type
		{
			get { return type; }
		}

		public IEntity Subject
		{
			get { return subject; }
		}

		public IEntity Object
		{
			get { return obj; }
			set { obj = value; }
		}

		public int Sequence
		{
			get { return sequence; }
			set { sequence = value; }
		}

		#endregion
	}
}
