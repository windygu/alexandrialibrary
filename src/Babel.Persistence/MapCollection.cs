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

namespace Telesophy.Babel.Persistence
{
	public class MapCollection : 
		KeyedCollection<Type, IMap>,
		IMapCollection
	{
		#region Constructors
		public MapCollection()
		{
		}
		#endregion

		#region Protected Methods
		protected override Type GetKeyForItem(IMap item)
		{
			if (item != null)
				return item.Type;
			else return null;
		}
		#endregion

		#region Public Methods
		public new IMap this[Type key]
		{
			get
			{
				if (base.Contains(key))
				{
					return base[key];
				}
				else throw new KeyNotFoundException();
			}
		}

		public new IMap this[int index]
		{
			get
			{
				if (index >= 0 && index < base.Count)
				{
					return base[index];
				}
				else throw new IndexOutOfRangeException();
			}
		}
		#endregion
	}
}
