#region License (MIT)
/***************************************************************************
 *  Copyright (C) 2007 Dan Poage
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
using System.Text;

using Telesophy.Alexandria.Model;
using Telesophy.Alexandria.MusicBrainz;

using Telesophy.Alexandria.Extensions.CompactDisc;

namespace Telesophy.Alexandria.Clients.Ankh.Controllers
{
	[CLSCompliant(false)]
	public class TrackSource : ITrackSource
	{
		#region Constructors
		public TrackSource(Uri path)
		{
			this.path = path;
		}
		
		[CLSCompliant(false)]
		public TrackSource(Uri path, AspiDeviceInfo deviceInfo) : this(path)
		{
			this.deviceInfo = deviceInfo;
		}
		#endregion
	
		#region Private Fields
		private IMediaSetFactory factory = new SimpleAlbumFactory();
		private Uri path;
		private AspiDeviceInfo deviceInfo;
		#endregion
			
		#region ITrackSource Members
		public Uri Path
		{
			get { return path; }
		}
		
		[CLSCompliant(false)]
		public AspiDeviceInfo DeviceInfo
		{
			get { return deviceInfo; }
		}
		
		public IList<IMediaItem> GetAudioTracks()
		{
			try
			{
				IMediaSet album = factory.GetMediaSet(path);
				return album.Items; 
			}
			catch (Exception ex)
			{
				string x = ex.Message;
			}
			return null;
		}
		#endregion
	}
}