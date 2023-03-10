using System;

namespace ScintillaNET
{
	// Token: 0x02000009 RID: 9
	public class CharAddedEventArgs : EventArgs
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000233F File Offset: 0x0000053F
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002347 File Offset: 0x00000547
		public int Char { get; private set; }

		// Token: 0x0600001A RID: 26 RVA: 0x00002350 File Offset: 0x00000550
		public CharAddedEventArgs(int ch)
		{
			this.Char = ch;
		}
	}
}
