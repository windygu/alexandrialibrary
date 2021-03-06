﻿#region License (MIT)
/***************************************************************************
 *  Copyright (C) 2008 Dan Poage
 ****************************************************************************/

/*  THIS FILE IS LICENSED UNDER THE MIT LICENSE AS OUTLINED IMMEDIATELY BELOW: 
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a
 *  copy of this software and associated documentation files (the "Software"),  
 *  to deal in the Software without restriction, including without limitation  
 *  the rights to use, copy, modify, merge, publish, distribute, sublicense,  
 *  and/or sell copies of the Software, and to permit persons to whom the  
 *  Software is furnished to do so, subject to the following conditions:
 *
 *  The above copyright notice and this permission notice shall be included in 
 *  all copies or substantial portions of the Software.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 *  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 *  DEALINGS IN THE SOFTWARE.
 */
#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Telesophy.Babel.Persistence
{
	public class Tuple : Dictionary<string, object>, INamedItem
	{
		#region Constructors
		public Tuple(string name, string identifierName)
		{
			this.name = name;
			this.identifierName = identifierName;
		}
		
		public Tuple(string name, Association association)
		{
			this.name = name;
			this.association = association;
		}
		#endregion
		
		#region Private Fields
		private string name;
		private string identifierName;
		private Association association;
		#endregion
		
		#region Public Properties
		public string Name
		{
			get { return name; }
		}
		
		public string IdentifierName
		{
			get { return identifierName; }
		}
		
		public object IdentifierValue
		{
			get
			{
				if (!string.IsNullOrEmpty(identifierName))
					return this[identifierName];
				else return null;
			}
		}
		
		public Association Association
		{
			get { return association; }
		}
		#endregion
		
		#region Public Methods
		public object GetAssociatedParentId()
		{
			if (Association != null)
			{
				return this[Association.ParentFieldName];
			}
			else throw new InvalidOperationException("Cannnot get associated ParentId - this tuple does not have an association");
		}
		#endregion
	}
}
