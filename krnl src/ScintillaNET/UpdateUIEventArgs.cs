using System;

namespace ScintillaNET
{
	// Token: 0x02000050 RID: 80
	public class UpdateUIEventArgs : EventArgs
	{
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0000DB09 File Offset: 0x0000BD09
		// (set) Token: 0x0600035A RID: 858 RVA: 0x0000DB11 File Offset: 0x0000BD11
		public UpdateChange Change { get; private set; }

		// Token: 0x0600035B RID: 859 RVA: 0x0000DB1A File Offset: 0x0000BD1A
		public UpdateUIEventArgs(UpdateChange change)
		{
			this.Change = change;
		}
	}
}
