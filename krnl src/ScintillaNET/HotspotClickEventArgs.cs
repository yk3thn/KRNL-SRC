using System;
using System.Windows.Forms;

namespace ScintillaNET
{
	// Token: 0x02000018 RID: 24
	public class HotspotClickEventArgs : EventArgs
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00004E16 File Offset: 0x00003016
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00004E1E File Offset: 0x0000301E
		public Keys Modifiers { get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00004E27 File Offset: 0x00003027
		public int Position
		{
			get
			{
				if (this.position == null)
				{
					this.position = new int?(this.scintilla.Lines.ByteToCharPosition(this.bytePosition));
				}
				return this.position.Value;
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00004E62 File Offset: 0x00003062
		public HotspotClickEventArgs(Scintilla scintilla, Keys modifiers, int bytePosition)
		{
			this.scintilla = scintilla;
			this.bytePosition = bytePosition;
			this.Modifiers = modifiers;
		}

		// Token: 0x040000BE RID: 190
		private readonly Scintilla scintilla;

		// Token: 0x040000BF RID: 191
		private readonly int bytePosition;

		// Token: 0x040000C0 RID: 192
		private int? position;
	}
}
