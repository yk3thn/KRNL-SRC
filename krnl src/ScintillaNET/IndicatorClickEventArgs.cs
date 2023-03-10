using System;
using System.Windows.Forms;

namespace ScintillaNET
{
	// Token: 0x0200001E RID: 30
	public class IndicatorClickEventArgs : IndicatorReleaseEventArgs
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000068 RID: 104 RVA: 0x0000533A File Offset: 0x0000353A
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00005342 File Offset: 0x00003542
		public Keys Modifiers { get; private set; }

		// Token: 0x0600006A RID: 106 RVA: 0x0000534B File Offset: 0x0000354B
		public IndicatorClickEventArgs(Scintilla scintilla, Keys modifiers, int bytePosition) : base(scintilla, bytePosition)
		{
			this.Modifiers = modifiers;
		}
	}
}
