using System;

namespace ScintillaNET
{
	// Token: 0x02000033 RID: 51
	public class ModificationEventArgs : BeforeModificationEventArgs
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x000072BD File Offset: 0x000054BD
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x000072C5 File Offset: 0x000054C5
		public int LinesAdded { get; private set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x000072CE File Offset: 0x000054CE
		public override string Text
		{
			get
			{
				if (base.CachedText == null)
				{
					base.CachedText = Helpers.GetString(this.textPtr, this.byteLength, this.scintilla.Encoding);
				}
				return base.CachedText;
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00007300 File Offset: 0x00005500
		public ModificationEventArgs(Scintilla scintilla, ModificationSource source, int bytePosition, int byteLength, IntPtr text, int linesAdded) : base(scintilla, source, bytePosition, byteLength, text)
		{
			this.scintilla = scintilla;
			this.bytePosition = bytePosition;
			this.byteLength = byteLength;
			this.textPtr = text;
			this.LinesAdded = linesAdded;
		}

		// Token: 0x04000168 RID: 360
		private readonly Scintilla scintilla;

		// Token: 0x04000169 RID: 361
		private readonly int bytePosition;

		// Token: 0x0400016A RID: 362
		private readonly int byteLength;

		// Token: 0x0400016B RID: 363
		private readonly IntPtr textPtr;
	}
}
