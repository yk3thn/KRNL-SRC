using System;

namespace ScintillaNET
{
	// Token: 0x02000022 RID: 34
	public class InsertCheckEventArgs : EventArgs
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000053A5 File Offset: 0x000035A5
		// (set) Token: 0x06000071 RID: 113 RVA: 0x000053AD File Offset: 0x000035AD
		internal int? CachedPosition { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000053B6 File Offset: 0x000035B6
		// (set) Token: 0x06000073 RID: 115 RVA: 0x000053BE File Offset: 0x000035BE
		internal string CachedText { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000053C8 File Offset: 0x000035C8
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

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00005414 File Offset: 0x00003614
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00005448 File Offset: 0x00003648
		public unsafe string Text
		{
			get
			{
				if (this.CachedText == null)
				{
					this.CachedText = Helpers.GetString(this.textPtr, this.byteLength, this.scintilla.Encoding);
				}
				return this.CachedText;
			}
			set
			{
				this.CachedText = (value ?? string.Empty);
				byte[] bytes = Helpers.GetBytes(this.CachedText, this.scintilla.Encoding, false);
				fixed (byte* ptr = bytes)
				{
					this.scintilla.DirectMessage(2672, new IntPtr(bytes.Length), new IntPtr((void*)ptr));
				}
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000054B7 File Offset: 0x000036B7
		public InsertCheckEventArgs(Scintilla scintilla, int bytePosition, int byteLength, IntPtr text)
		{
			this.scintilla = scintilla;
			this.bytePosition = bytePosition;
			this.byteLength = byteLength;
			this.textPtr = text;
		}

		// Token: 0x040000EC RID: 236
		private readonly Scintilla scintilla;

		// Token: 0x040000ED RID: 237
		private readonly int bytePosition;

		// Token: 0x040000EE RID: 238
		private readonly int byteLength;

		// Token: 0x040000EF RID: 239
		private readonly IntPtr textPtr;
	}
}
