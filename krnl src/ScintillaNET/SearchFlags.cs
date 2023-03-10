using System;

namespace ScintillaNET
{
	// Token: 0x02000043 RID: 67
	[Flags]
	public enum SearchFlags
	{
		// Token: 0x040007D7 RID: 2007
		None = 0,
		// Token: 0x040007D8 RID: 2008
		MatchCase = 4,
		// Token: 0x040007D9 RID: 2009
		WholeWord = 2,
		// Token: 0x040007DA RID: 2010
		WordStart = 1048576,
		// Token: 0x040007DB RID: 2011
		Regex = 2097152,
		// Token: 0x040007DC RID: 2012
		Posix = 4194304,
		// Token: 0x040007DD RID: 2013
		Cxx11Regex = 8388608
	}
}
