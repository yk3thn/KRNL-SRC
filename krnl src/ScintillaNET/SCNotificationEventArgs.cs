using System;

namespace ScintillaNET
{
	// Token: 0x02000041 RID: 65
	internal sealed class SCNotificationEventArgs : EventArgs
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600012C RID: 300 RVA: 0x0000784F File Offset: 0x00005A4F
		// (set) Token: 0x0600012D RID: 301 RVA: 0x00007857 File Offset: 0x00005A57
		public NativeMethods.SCNotification SCNotification { get; private set; }

		// Token: 0x0600012E RID: 302 RVA: 0x00007860 File Offset: 0x00005A60
		public SCNotificationEventArgs(NativeMethods.SCNotification scn)
		{
			this.SCNotification = scn;
		}
	}
}
