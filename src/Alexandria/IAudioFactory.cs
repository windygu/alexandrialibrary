using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IAudioFactory : IPlugin
	{
		IAudio GetAudio(Uri uri);
	}
}