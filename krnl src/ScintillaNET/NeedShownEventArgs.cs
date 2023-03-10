using System;

namespace ScintillaNET
{
	// Token: 0x02000038 RID: 56
	public class NeedShownEventArgs : EventArgs
	{
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00007498 File Offset: 0x00005698
		public int Length
		{
			get
			{
				if (this.length == null)
				{
					int pos = this.bytePosition + this.byteLength;
					int num = this.scintilla.Lines.ByteToCharPosition(pos);
					this.length = new int?(num - this.Position);
				}
				return this.length.Value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000117 RID: 279 RVA: 0x000074F0 File Offset: 0x000056F0
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

		// Token: 0x06000118 RID: 280 RVA: 0x0000752B File Offset: 0x0000572B
		public NeedShownEventArgs(Scintilla scintilla, int bytePosition, int byteLength)
		{
			this.scintilla = scintilla;
			this.bytePosition = bytePosition;
			this.byteLength = byteLength;
		}

		// Token: 0x04000782 RID: 1922
		private readonly Scintilla scintilla;

		// Token: 0x04000783 RID: 1923
		private readonly int bytePosition;

		// Token: 0x04000784 RID: 1924
		private readonly int byteLength;

		// Token: 0x04000785 RID: 1925
		private int? position;

		// Token: 0x04000786 RID: 1926
		private int? length;
	}
}
