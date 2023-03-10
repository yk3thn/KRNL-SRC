using System;
using System.Drawing;

namespace ScintillaNET
{
	// Token: 0x0200001C RID: 28
	public class Indicator
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00004E80 File Offset: 0x00003080
		public int End(int position)
		{
			position = Helpers.Clamp(position, 0, this.scintilla.TextLength);
			position = this.scintilla.Lines.CharToBytePosition(position);
			position = this.scintilla.DirectMessage(2509, new IntPtr(this.Index), new IntPtr(position)).ToInt32();
			return this.scintilla.Lines.ByteToCharPosition(position);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00004EF0 File Offset: 0x000030F0
		public int Start(int position)
		{
			position = Helpers.Clamp(position, 0, this.scintilla.TextLength);
			position = this.scintilla.Lines.CharToBytePosition(position);
			position = this.scintilla.DirectMessage(2508, new IntPtr(this.Index), new IntPtr(position)).ToInt32();
			return this.scintilla.Lines.ByteToCharPosition(position);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00004F60 File Offset: 0x00003160
		public int ValueAt(int position)
		{
			position = Helpers.Clamp(position, 0, this.scintilla.TextLength);
			position = this.scintilla.Lines.CharToBytePosition(position);
			return this.scintilla.DirectMessage(2507, new IntPtr(this.Index), new IntPtr(position)).ToInt32();
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00004FC0 File Offset: 0x000031C0
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00004FF0 File Offset: 0x000031F0
		public int Alpha
		{
			get
			{
				return this.scintilla.DirectMessage(2524, new IntPtr(this.Index)).ToInt32();
			}
			set
			{
				value = Helpers.Clamp(value, 0, 255);
				this.scintilla.DirectMessage(2523, new IntPtr(this.Index), new IntPtr(value));
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00005022 File Offset: 0x00003222
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00005044 File Offset: 0x00003244
		public IndicatorFlags Flags
		{
			get
			{
				return (IndicatorFlags)((int)this.scintilla.DirectMessage(2685, new IntPtr(this.Index)));
			}
			set
			{
				this.scintilla.DirectMessage(2684, new IntPtr(this.Index), new IntPtr((int)value));
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00005078 File Offset: 0x00003278
		// (set) Token: 0x06000058 RID: 88 RVA: 0x000050B0 File Offset: 0x000032B0
		public Color ForeColor
		{
			get
			{
				return ColorTranslator.FromWin32(this.scintilla.DirectMessage(2083, new IntPtr(this.Index)).ToInt32());
			}
			set
			{
				int value2 = ColorTranslator.ToWin32(value);
				this.scintilla.DirectMessage(2082, new IntPtr(this.Index), new IntPtr(value2));
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000050E8 File Offset: 0x000032E8
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00005120 File Offset: 0x00003320
		public Color HoverForeColor
		{
			get
			{
				return ColorTranslator.FromWin32(this.scintilla.DirectMessage(2683, new IntPtr(this.Index)).ToInt32());
			}
			set
			{
				int value2 = ColorTranslator.ToWin32(value);
				this.scintilla.DirectMessage(2682, new IntPtr(this.Index), new IntPtr(value2));
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00005156 File Offset: 0x00003356
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00005178 File Offset: 0x00003378
		public IndicatorStyle HoverStyle
		{
			get
			{
				return (IndicatorStyle)((int)this.scintilla.DirectMessage(2681, new IntPtr(this.Index)));
			}
			set
			{
				this.scintilla.DirectMessage(2680, new IntPtr(this.Index), new IntPtr((int)value));
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000051A9 File Offset: 0x000033A9
		// (set) Token: 0x0600005E RID: 94 RVA: 0x000051B1 File Offset: 0x000033B1
		public int Index { get; private set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000051BC File Offset: 0x000033BC
		// (set) Token: 0x06000060 RID: 96 RVA: 0x000051EC File Offset: 0x000033EC
		public int OutlineAlpha
		{
			get
			{
				return this.scintilla.DirectMessage(2559, new IntPtr(this.Index)).ToInt32();
			}
			set
			{
				value = Helpers.Clamp(value, 0, 255);
				this.scintilla.DirectMessage(2558, new IntPtr(this.Index), new IntPtr(value));
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000061 RID: 97 RVA: 0x0000521E File Offset: 0x0000341E
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00005240 File Offset: 0x00003440
		public IndicatorStyle Style
		{
			get
			{
				return (IndicatorStyle)((int)this.scintilla.DirectMessage(2081, new IntPtr(this.Index)));
			}
			set
			{
				this.scintilla.DirectMessage(2080, new IntPtr(this.Index), new IntPtr((int)value));
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00005271 File Offset: 0x00003471
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00005298 File Offset: 0x00003498
		public bool Under
		{
			get
			{
				return this.scintilla.DirectMessage(2511, new IntPtr(this.Index)) != IntPtr.Zero;
			}
			set
			{
				IntPtr lParam = value ? new IntPtr(1) : IntPtr.Zero;
				this.scintilla.DirectMessage(2510, new IntPtr(this.Index), lParam);
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000052D3 File Offset: 0x000034D3
		public Indicator(Scintilla scintilla, int index)
		{
			this.scintilla = scintilla;
			this.Index = index;
		}

		// Token: 0x040000CC RID: 204
		private readonly Scintilla scintilla;

		// Token: 0x040000CD RID: 205
		public const int ValueBit = 16777216;

		// Token: 0x040000CE RID: 206
		public const int ValueMask = 16777215;
	}
}
