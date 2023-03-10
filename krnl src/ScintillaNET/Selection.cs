using System;

namespace ScintillaNET
{
	// Token: 0x02000044 RID: 68
	public class Selection
	{
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000314 RID: 788 RVA: 0x0000CE84 File Offset: 0x0000B084
		// (set) Token: 0x06000315 RID: 789 RVA: 0x0000CECC File Offset: 0x0000B0CC
		public int Anchor
		{
			get
			{
				int num = this.scintilla.DirectMessage(2579, new IntPtr(this.Index)).ToInt32();
				if (num <= 0)
				{
					return num;
				}
				return this.scintilla.Lines.ByteToCharPosition(num);
			}
			set
			{
				value = Helpers.Clamp(value, 0, this.scintilla.TextLength);
				value = this.scintilla.Lines.CharToBytePosition(value);
				this.scintilla.DirectMessage(2578, new IntPtr(this.Index), new IntPtr(value));
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0000CF24 File Offset: 0x0000B124
		// (set) Token: 0x06000317 RID: 791 RVA: 0x0000CF54 File Offset: 0x0000B154
		public int AnchorVirtualSpace
		{
			get
			{
				return this.scintilla.DirectMessage(2583, new IntPtr(this.Index)).ToInt32();
			}
			set
			{
				value = Helpers.ClampMin(value, 0);
				this.scintilla.DirectMessage(2582, new IntPtr(this.Index), new IntPtr(value));
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0000CF84 File Offset: 0x0000B184
		// (set) Token: 0x06000319 RID: 793 RVA: 0x0000CFCC File Offset: 0x0000B1CC
		public int Caret
		{
			get
			{
				int num = this.scintilla.DirectMessage(2577, new IntPtr(this.Index)).ToInt32();
				if (num <= 0)
				{
					return num;
				}
				return this.scintilla.Lines.ByteToCharPosition(num);
			}
			set
			{
				value = Helpers.Clamp(value, 0, this.scintilla.TextLength);
				value = this.scintilla.Lines.CharToBytePosition(value);
				this.scintilla.DirectMessage(2576, new IntPtr(this.Index), new IntPtr(value));
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600031A RID: 794 RVA: 0x0000D024 File Offset: 0x0000B224
		// (set) Token: 0x0600031B RID: 795 RVA: 0x0000D054 File Offset: 0x0000B254
		public int CaretVirtualSpace
		{
			get
			{
				return this.scintilla.DirectMessage(2581, new IntPtr(this.Index)).ToInt32();
			}
			set
			{
				value = Helpers.ClampMin(value, 0);
				this.scintilla.DirectMessage(2580, new IntPtr(this.Index), new IntPtr(value));
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0000D084 File Offset: 0x0000B284
		// (set) Token: 0x0600031D RID: 797 RVA: 0x0000D0CC File Offset: 0x0000B2CC
		public int End
		{
			get
			{
				int num = this.scintilla.DirectMessage(2587, new IntPtr(this.Index)).ToInt32();
				if (num <= 0)
				{
					return num;
				}
				return this.scintilla.Lines.ByteToCharPosition(num);
			}
			set
			{
				value = Helpers.Clamp(value, 0, this.scintilla.TextLength);
				value = this.scintilla.Lines.CharToBytePosition(value);
				this.scintilla.DirectMessage(2586, new IntPtr(this.Index), new IntPtr(value));
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000D122 File Offset: 0x0000B322
		// (set) Token: 0x0600031F RID: 799 RVA: 0x0000D12A File Offset: 0x0000B32A
		public int Index { get; private set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000D134 File Offset: 0x0000B334
		// (set) Token: 0x06000321 RID: 801 RVA: 0x0000D17C File Offset: 0x0000B37C
		public int Start
		{
			get
			{
				int num = this.scintilla.DirectMessage(2585, new IntPtr(this.Index)).ToInt32();
				if (num <= 0)
				{
					return num;
				}
				return this.scintilla.Lines.ByteToCharPosition(num);
			}
			set
			{
				value = Helpers.Clamp(value, 0, this.scintilla.TextLength);
				value = this.scintilla.Lines.CharToBytePosition(value);
				this.scintilla.DirectMessage(2584, new IntPtr(this.Index), new IntPtr(value));
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000D1D2 File Offset: 0x0000B3D2
		public Selection(Scintilla scintilla, int index)
		{
			this.scintilla = scintilla;
			this.Index = index;
		}

		// Token: 0x040007DE RID: 2014
		private readonly Scintilla scintilla;
	}
}
