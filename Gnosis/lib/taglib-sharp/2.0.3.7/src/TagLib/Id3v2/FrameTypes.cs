//
// FrameTypes.cs:
//
// Author:
//   Brian Nickel (brian.nickel@gmail.com)
//
// Copyright (C) 2007 Brian Nickel
//
// This library is free software; you can redistribute it and/or modify
// it  under the terms of the GNU Lesser General Public License version
// 2.1 as published by the Free Software Foundation.
//
// This library is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307
// USA
//

using System;

namespace TagLib.Id3v2 {
	/// <summary>
	///    This static class provides a collection of common frame types so
	///    they don't have to be created and destroyed countless times.
	/// </summary>
	internal static class FrameType {
		public static readonly ReadOnlyByteVector APIC = "APIC";
		public static readonly ReadOnlyByteVector COMM = "COMM";
		public static readonly ReadOnlyByteVector EQUA = "EQUA";
		public static readonly ReadOnlyByteVector GEOB = "GEOB";
		public static readonly ReadOnlyByteVector MCDI = "MCDI";
		public static readonly ReadOnlyByteVector PCNT = "PCNT";
		public static readonly ReadOnlyByteVector POPM = "POPM";
		public static readonly ReadOnlyByteVector PRIV = "PRIV";
		public static readonly ReadOnlyByteVector RVA2 = "RVA2";
		public static readonly ReadOnlyByteVector RVAD = "RVAD";
		public static readonly ReadOnlyByteVector SYLT = "SYLT";
		public static readonly ReadOnlyByteVector TALB = "TALB";
		public static readonly ReadOnlyByteVector TBPM = "TBPM";
		public static readonly ReadOnlyByteVector TCOM = "TCOM";
		public static readonly ReadOnlyByteVector TCON = "TCON";
		public static readonly ReadOnlyByteVector TCOP = "TCOP";
		public static readonly ReadOnlyByteVector TCMP = "TCMP";
        public static readonly ReadOnlyByteVector TDAT = "TDAT";
        public static readonly ReadOnlyByteVector TDRC = "TDRC";
        public static readonly ReadOnlyByteVector TDRL = "TDRL"; //DP - added 6/16/2011
		public static readonly ReadOnlyByteVector TEXT = "TEXT";
		public static readonly ReadOnlyByteVector TIT1 = "TIT1";
		public static readonly ReadOnlyByteVector TIT2 = "TIT2";
        public static readonly ReadOnlyByteVector TIT3 = "TIT3";
		public static readonly ReadOnlyByteVector TIME = "TIME";
        public static readonly ReadOnlyByteVector TLAN = "TLAN";
        public static readonly ReadOnlyByteVector TMOO = "TMOO"; //DP - added 6/16/2011
        public static readonly ReadOnlyByteVector TOAL = "TOAL"; //DP - added 6/16/2011
		public static readonly ReadOnlyByteVector TOLY = "TOLY";
		public static readonly ReadOnlyByteVector TOPE = "TOPE";
		public static readonly ReadOnlyByteVector TPE1 = "TPE1";
		public static readonly ReadOnlyByteVector TPE2 = "TPE2";
		public static readonly ReadOnlyByteVector TPE3 = "TPE3";
		public static readonly ReadOnlyByteVector TPE4 = "TPE4";
		public static readonly ReadOnlyByteVector TPOS = "TPOS";
		public static readonly ReadOnlyByteVector TRCK = "TRCK";
		public static readonly ReadOnlyByteVector TRDA = "TRDA";
		public static readonly ReadOnlyByteVector TSIZ = "TSIZ";
        public static readonly ReadOnlyByteVector TSOA = "TSOA"; //DP - added 6/16/2011
        public static readonly ReadOnlyByteVector TSOP = "TSOP"; //DP - added 6/16/2011
        public static readonly ReadOnlyByteVector TSOT = "TSOT"; //DP - added 6/16/2011
        public static readonly ReadOnlyByteVector TSST = "TSST"; //DP - added 6/16/2011
		public static readonly ReadOnlyByteVector TXXX = "TXXX";
		public static readonly ReadOnlyByteVector TYER = "TYER";
		public static readonly ReadOnlyByteVector UFID = "UFID";
		public static readonly ReadOnlyByteVector USER = "USER";
		public static readonly ReadOnlyByteVector USLT = "USLT";
	}
}
