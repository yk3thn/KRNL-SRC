using System;

namespace ScintillaNET
{
	// Token: 0x0200000E RID: 14
	public class DwellEventArgs : EventArgs
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002446 File Offset: 0x00000646
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

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002481 File Offset: 0x00000681
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002489 File Offset: 0x00000689
		public int X { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002492 File Offset: 0x00000692
		// (set) Token: 0x06000029 RID: 41 RVA: 0x0000249A File Offset: 0x0000069A
		public int Y { get; private set; }

		// Token: 0x0600002A RID: 42 RVA: 0x000024A3 File Offset: 0x000006A3
		public DwellEventArgs(Scintilla scintilla, int bytePosition, int x, int y)
		{
			this.scintilla = scintilla;
			this.bytePosition = bytePosition;
			this.X = x;
			this.Y = y;
			if (bytePosition < 0)
			{
				this.position = new int?(bytePosition);
			}
		}

		// Token: 0x04000091 RID: 145
		private readonly Scintilla scintilla;

		// Token: 0x04000092 RID: 146
		private readonly int bytePosition;

		// Token: 0x04000093 RID: 147
		private int? position;
	}
}
