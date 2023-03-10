using System;

namespace ScintillaNET
{
	// Token: 0x02000008 RID: 8
	public class ChangeAnnotationEventArgs : EventArgs
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000231F File Offset: 0x0000051F
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002327 File Offset: 0x00000527
		public int Line { get; private set; }

		// Token: 0x06000017 RID: 23 RVA: 0x00002330 File Offset: 0x00000530
		public ChangeAnnotationEventArgs(int line)
		{
			this.Line = line;
		}
	}
}
