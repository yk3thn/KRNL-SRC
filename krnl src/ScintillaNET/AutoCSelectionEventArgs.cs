using System;

namespace ScintillaNET
{
	// Token: 0x02000004 RID: 4
	public class AutoCSelectionEventArgs : EventArgs
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020EA File Offset: 0x000002EA
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000020F2 File Offset: 0x000002F2
		public int Char { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020FB File Offset: 0x000002FB
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002103 File Offset: 0x00000303
		public ListCompletionMethod ListCompletionMethod { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000210C File Offset: 0x0000030C
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

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002148 File Offset: 0x00000348
		public unsafe string Text
		{
			get
			{
				if (this.text == null)
				{
					int num = 0;
					while (((byte*)((void*)this.textPtr))[num] != 0)
					{
						num++;
					}
					this.text = Helpers.GetString(this.textPtr, num, this.scintilla.Encoding);
				}
				return this.text;
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002198 File Offset: 0x00000398
		public AutoCSelectionEventArgs(Scintilla scintilla, int bytePosition, IntPtr text, int ch, ListCompletionMethod listCompletionMethod)
		{
			this.scintilla = scintilla;
			this.bytePosition = bytePosition;
			this.textPtr = text;
			this.Char = ch;
			this.ListCompletionMethod = listCompletionMethod;
		}

		// Token: 0x04000006 RID: 6
		private readonly Scintilla scintilla;

		// Token: 0x04000007 RID: 7
		private readonly IntPtr textPtr;

		// Token: 0x04000008 RID: 8
		private readonly int bytePosition;

		// Token: 0x04000009 RID: 9
		private int? position;

		// Token: 0x0400000A RID: 10
		private string text;
	}
}
