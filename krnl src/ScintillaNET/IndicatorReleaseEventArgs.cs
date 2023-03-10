using System;

namespace ScintillaNET
{
	// Token: 0x0200001D RID: 29
	public class IndicatorReleaseEventArgs : EventArgs
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000052E9 File Offset: 0x000034E9
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

		// Token: 0x06000067 RID: 103 RVA: 0x00005324 File Offset: 0x00003524
		public IndicatorReleaseEventArgs(Scintilla scintilla, int bytePosition)
		{
			this.scintilla = scintilla;
			this.bytePosition = bytePosition;
		}

		// Token: 0x040000D0 RID: 208
		private readonly Scintilla scintilla;

		// Token: 0x040000D1 RID: 209
		private readonly int bytePosition;

		// Token: 0x040000D2 RID: 210
		private int? position;
	}
}
