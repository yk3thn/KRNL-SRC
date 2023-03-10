using System;
using System.Drawing;

namespace ScintillaNET
{
	// Token: 0x02000029 RID: 41
	public class Margin
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00006C2C File Offset: 0x00004E2C
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x00006C64 File Offset: 0x00004E64
		public Color BackColor
		{
			get
			{
				return ColorTranslator.FromWin32(this.scintilla.DirectMessage(2251, new IntPtr(this.Index)).ToInt32());
			}
			set
			{
				if (value.IsEmpty)
				{
					value = Color.Black;
				}
				int value2 = ColorTranslator.ToWin32(value);
				this.scintilla.DirectMessage(2250, new IntPtr(this.Index), new IntPtr(value2));
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00006CAA File Offset: 0x00004EAA
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00006CCC File Offset: 0x00004ECC
		public MarginCursor Cursor
		{
			get
			{
				return (MarginCursor)((int)this.scintilla.DirectMessage(2249, new IntPtr(this.Index)));
			}
			set
			{
				this.scintilla.DirectMessage(2248, new IntPtr(this.Index), new IntPtr((int)value));
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00006CFD File Offset: 0x00004EFD
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00006D05 File Offset: 0x00004F05
		public int Index { get; private set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00006D0E File Offset: 0x00004F0E
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00006D38 File Offset: 0x00004F38
		public bool Sensitive
		{
			get
			{
				return this.scintilla.DirectMessage(2247, new IntPtr(this.Index)) != IntPtr.Zero;
			}
			set
			{
				IntPtr lParam = value ? new IntPtr(1) : IntPtr.Zero;
				this.scintilla.DirectMessage(2246, new IntPtr(this.Index), lParam);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00006D73 File Offset: 0x00004F73
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00006D98 File Offset: 0x00004F98
		public MarginType Type
		{
			get
			{
				return (MarginType)((int)this.scintilla.DirectMessage(2241, new IntPtr(this.Index)));
			}
			set
			{
				this.scintilla.DirectMessage(2240, new IntPtr(this.Index), new IntPtr((int)value));
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00006DCC File Offset: 0x00004FCC
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00006DFC File Offset: 0x00004FFC
		public int Width
		{
			get
			{
				return this.scintilla.DirectMessage(2243, new IntPtr(this.Index)).ToInt32();
			}
			set
			{
				value = Helpers.ClampMin(value, 0);
				this.scintilla.DirectMessage(2242, new IntPtr(this.Index), new IntPtr(value));
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00006E2C File Offset: 0x0000502C
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00006E5C File Offset: 0x0000505C
		public uint Mask
		{
			get
			{
				return (uint)this.scintilla.DirectMessage(2245, new IntPtr(this.Index)).ToInt32();
			}
			set
			{
				this.scintilla.DirectMessage(2244, new IntPtr(this.Index), new IntPtr((int)value));
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00006E8D File Offset: 0x0000508D
		public Margin(Scintilla scintilla, int index)
		{
			this.scintilla = scintilla;
			this.Index = index;
		}

		// Token: 0x04000124 RID: 292
		private readonly Scintilla scintilla;
	}
}
