using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IMediaContainer : IResource
	{
		IList<IAudio> AudioItems { get; }
		IList<IVideo> VideoItems { get; }
		IList<IImage> ImageItems { get; }
		IList<IText> TextItems { get; }
		IDictionary<string, IMetadata> MetadataItems { get; }
	}
}