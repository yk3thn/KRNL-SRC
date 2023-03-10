using System;

namespace ScintillaNET
{
	// Token: 0x0200004D RID: 77
	internal static class Tuple
	{
		// Token: 0x0600034E RID: 846 RVA: 0x0000D9D4 File Offset: 0x0000BBD4
		public static Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
		{
			return new Tuple<T1, T2>(item1, item2);
		}
	}
}
