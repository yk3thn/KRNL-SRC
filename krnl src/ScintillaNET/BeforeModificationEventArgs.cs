using System;

namespace ScintillaNET
{
	// Token: 0x02000006 RID: 6
	public class BeforeModificationEventArgs : EventArgs
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021C5 File Offset: 0x000003C5
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000021CD File Offset: 0x000003CD
		internal int? CachedPosition { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000021D6 File Offset: 0x000003D6
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000021DE File Offset: 0x000003DE
		internal string CachedText { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000021E8 File Offset: 0x000003E8
		public int Position
		{
			get
			{
				if (this.CachedPosition == null)
				{
					this.CachedPosition = new int?(this.scintilla.Lines.ByteToCharPosition(this.bytePosition));
				}
				return this.CachedPosition.Value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002234 File Offset: 0x00000434
		// (set) Token: 0x06000012 RID: 18 RVA: 0x0000223C File Offset: 0x0000043C
		public ModificationSource Source { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002248 File Offset: 0x00000448
		public unsafe virtual string Text
		{
			get
			{
				if (this.Source != ModificationSource.User)
				{
					return null;
				}
				if (this.CachedText == null)
				{
					if (this.textPtr == IntPtr.Zero)
					{
						IntPtr value = this.scintilla.DirectMessage(2643, new IntPtr(this.bytePosition), new IntPtr(this.byteLength));
						this.CachedText = new string((sbyte*)((void*)value), 0, this.byteLength, this.scintilla.Encoding);
					}
					else
					{
						this.CachedText = Helpers.GetString(this.textPtr, this.byteLength, this.scintilla.Encoding);
					}
				}
				return this.CachedText;
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022F2 File Offset: 0x000004F2
		public BeforeModificationEventArgs(Scintilla scintilla, ModificationSource source, int bytePosition, int byteLength, IntPtr text)
		{
			this.scintilla = scintilla;
			this.bytePosition = bytePosition;
			this.byteLength = byteLength;
			this.textPtr = text;
			this.Source = source;
		}

		// Token: 0x04000012 RID: 18
		private readonly Scintilla scintilla;

		// Token: 0x04000013 RID: 19
		private readonly int bytePosition;

		// Token: 0x04000014 RID: 20
		private readonly int byteLength;

		// Token: 0x04000015 RID: 21
		private readonly IntPtr textPtr;
	}
}
