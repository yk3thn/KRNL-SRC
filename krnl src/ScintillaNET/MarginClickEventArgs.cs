using System;
using System.Windows.Forms;

namespace ScintillaNET
{
	// Token: 0x0200002A RID: 42
	public class MarginClickEventArgs : EventArgs
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00006EA3 File Offset: 0x000050A3
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00006EAB File Offset: 0x000050AB
		public int Margin { get; private set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00006EB4 File Offset: 0x000050B4
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00006EBC File Offset: 0x000050BC
		public Keys Modifiers { get; private set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00006EC5 File Offset: 0x000050C5
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

		// Token: 0x060000D7 RID: 215 RVA: 0x00006F00 File Offset: 0x00005100
		public MarginClickEventArgs(Scintilla scintilla, Keys modifiers, int bytePosition, int margin)
		{
			this.scintilla = scintilla;
			this.bytePosition = bytePosition;
			this.Modifiers = modifiers;
			this.Margin = margin;
		}

		// Token: 0x04000126 RID: 294
		private readonly Scintilla scintilla;

		// Token: 0x04000127 RID: 295
		private readonly int bytePosition;

		// Token: 0x04000128 RID: 296
		private int? position;
	}
}
