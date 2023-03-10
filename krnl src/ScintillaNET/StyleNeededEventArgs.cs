using System;

namespace ScintillaNET
{
	// Token: 0x0200004A RID: 74
	public class StyleNeededEventArgs : EventArgs
	{
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0000D983 File Offset: 0x0000BB83
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

		// Token: 0x0600034D RID: 845 RVA: 0x0000D9BE File Offset: 0x0000BBBE
		public StyleNeededEventArgs(Scintilla scintilla, int bytePosition)
		{
			this.scintilla = scintilla;
			this.bytePosition = bytePosition;
		}

		// Token: 0x040007F5 RID: 2037
		private readonly Scintilla scintilla;

		// Token: 0x040007F6 RID: 2038
		private readonly int bytePosition;

		// Token: 0x040007F7 RID: 2039
		private int? position;
	}
}
