using System;
using System.Windows.Forms;

namespace ScintillaNET
{
	// Token: 0x0200000D RID: 13
	public class DoubleClickEventArgs : EventArgs
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000023B4 File Offset: 0x000005B4
		// (set) Token: 0x06000020 RID: 32 RVA: 0x000023BC File Offset: 0x000005BC
		public int Line { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000023C5 File Offset: 0x000005C5
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000023CD File Offset: 0x000005CD
		public Keys Modifiers { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000023D6 File Offset: 0x000005D6
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

		// Token: 0x06000024 RID: 36 RVA: 0x00002411 File Offset: 0x00000611
		public DoubleClickEventArgs(Scintilla scintilla, Keys modifiers, int bytePosition, int line)
		{
			this.scintilla = scintilla;
			this.bytePosition = bytePosition;
			this.Modifiers = modifiers;
			this.Line = line;
			if (bytePosition == -1)
			{
				this.position = new int?(-1);
			}
		}

		// Token: 0x0400008C RID: 140
		private readonly Scintilla scintilla;

		// Token: 0x0400008D RID: 141
		private readonly int bytePosition;

		// Token: 0x0400008E RID: 142
		private int? position;
	}
}
