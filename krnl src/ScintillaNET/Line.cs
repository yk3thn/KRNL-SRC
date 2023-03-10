using System;

namespace ScintillaNET
{
	// Token: 0x02000024 RID: 36
	public class Line
	{
		// Token: 0x06000078 RID: 120 RVA: 0x000054DC File Offset: 0x000036DC
		public void EnsureVisible()
		{
			this.scintilla.DirectMessage(2232, new IntPtr(this.Index));
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000054FA File Offset: 0x000036FA
		public void FoldChildren(FoldAction action)
		{
			this.scintilla.DirectMessage(2238, new IntPtr(this.Index), new IntPtr((int)action));
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000551E File Offset: 0x0000371E
		public void FoldLine(FoldAction action)
		{
			this.scintilla.DirectMessage(2237, new IntPtr(this.Index), new IntPtr((int)action));
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00005544 File Offset: 0x00003744
		public int GetLastChild(int level)
		{
			return this.scintilla.DirectMessage(2224, new IntPtr(this.Index), new IntPtr(level)).ToInt32();
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000557A File Offset: 0x0000377A
		public void Goto()
		{
			this.scintilla.DirectMessage(2024, new IntPtr(this.Index));
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00005598 File Offset: 0x00003798
		public MarkerHandle MarkerAdd(int marker)
		{
			marker = Helpers.Clamp(marker, 0, this.scintilla.Markers.Count - 1);
			IntPtr value = this.scintilla.DirectMessage(2043, new IntPtr(this.Index), new IntPtr(marker));
			return new MarkerHandle
			{
				Value = value
			};
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000055F4 File Offset: 0x000037F4
		public void MarkerAddSet(uint markerMask)
		{
			this.scintilla.DirectMessage(2466, new IntPtr(this.Index), new IntPtr((int)markerMask));
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00005625 File Offset: 0x00003825
		public void MarkerDelete(int marker)
		{
			marker = Helpers.Clamp(marker, -1, this.scintilla.Markers.Count - 1);
			this.scintilla.DirectMessage(2044, new IntPtr(this.Index), new IntPtr(marker));
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00005664 File Offset: 0x00003864
		public uint MarkerGet()
		{
			return (uint)this.scintilla.DirectMessage(2046, new IntPtr(this.Index)).ToInt32();
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00005694 File Offset: 0x00003894
		public int MarkerNext(uint markerMask)
		{
			return this.scintilla.DirectMessage(2047, new IntPtr(this.Index), new IntPtr((int)markerMask)).ToInt32();
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000056CC File Offset: 0x000038CC
		public int MarkerPrevious(uint markerMask)
		{
			return this.scintilla.DirectMessage(2048, new IntPtr(this.Index), new IntPtr((int)markerMask)).ToInt32();
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00005704 File Offset: 0x00003904
		public void ToggleFold()
		{
			this.scintilla.DirectMessage(2231, new IntPtr(this.Index));
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00005724 File Offset: 0x00003924
		public unsafe void ToggleFoldShowText(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				this.scintilla.DirectMessage(2700, new IntPtr(this.Index), IntPtr.Zero);
				return;
			}
			fixed (byte* bytes = Helpers.GetBytes(text, this.scintilla.Encoding, true))
			{
				this.scintilla.DirectMessage(2700, new IntPtr(this.Index), new IntPtr((void*)bytes));
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000057AC File Offset: 0x000039AC
		public int AnnotationLines
		{
			get
			{
				return this.scintilla.DirectMessage(2546, new IntPtr(this.Index)).ToInt32();
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000086 RID: 134 RVA: 0x000057DC File Offset: 0x000039DC
		// (set) Token: 0x06000087 RID: 135 RVA: 0x0000580C File Offset: 0x00003A0C
		public int AnnotationStyle
		{
			get
			{
				return this.scintilla.DirectMessage(2543, new IntPtr(this.Index)).ToInt32();
			}
			set
			{
				value = Helpers.Clamp(value, 0, this.scintilla.Styles.Count - 1);
				this.scintilla.DirectMessage(2542, new IntPtr(this.Index), new IntPtr(value));
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000088 RID: 136 RVA: 0x0000584C File Offset: 0x00003A4C
		// (set) Token: 0x06000089 RID: 137 RVA: 0x0000592C File Offset: 0x00003B2C
		public unsafe byte[] AnnotationStyles
		{
			get
			{
				int num = this.scintilla.DirectMessage(2541, new IntPtr(this.Index)).ToInt32();
				if (num == 0)
				{
					return new byte[0];
				}
				byte[] array = new byte[num + 1];
				byte[] array2 = new byte[num + 1];
				byte[] array3 = array;
				byte* ptr;
				if (array == null || array3.Length == 0)
				{
					ptr = null;
				}
				else
				{
					fixed (byte* ptr = &array3[0])
					{
					}
				}
				fixed (byte* ptr2 = array2)
				{
					this.scintilla.DirectMessage(2541, new IntPtr(this.Index), new IntPtr((void*)ptr));
					this.scintilla.DirectMessage(2545, new IntPtr(this.Index), new IntPtr((void*)ptr2));
					return Helpers.ByteToCharStyles(ptr2, ptr, num, this.scintilla.Encoding);
				}
			}
			set
			{
				int num = this.scintilla.DirectMessage(2541, new IntPtr(this.Index)).ToInt32();
				if (num == 0)
				{
					return;
				}
				fixed (byte* ptr = new byte[num + 1])
				{
					this.scintilla.DirectMessage(2541, new IntPtr(this.Index), new IntPtr((void*)ptr));
					fixed (byte* ptr2 = Helpers.CharToByteStyles(value ?? new byte[0], ptr, num, this.scintilla.Encoding))
					{
						this.scintilla.DirectMessage(2544, new IntPtr(this.Index), new IntPtr((void*)ptr2));
					}
				}
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00005A08 File Offset: 0x00003C08
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00005A9C File Offset: 0x00003C9C
		public unsafe string AnnotationText
		{
			get
			{
				int num = this.scintilla.DirectMessage(2541, new IntPtr(this.Index)).ToInt32();
				if (num == 0)
				{
					return string.Empty;
				}
				fixed (byte* ptr = new byte[num + 1])
				{
					this.scintilla.DirectMessage(2541, new IntPtr(this.Index), new IntPtr((void*)ptr));
					return Helpers.GetString(new IntPtr((void*)ptr), num, this.scintilla.Encoding);
				}
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.scintilla.DirectMessage(2541, new IntPtr(this.Index), IntPtr.Zero);
					return;
				}
				fixed (byte* bytes = Helpers.GetBytes(value, this.scintilla.Encoding, true))
				{
					this.scintilla.DirectMessage(2540, new IntPtr(this.Index), new IntPtr((void*)bytes));
				}
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00005B24 File Offset: 0x00003D24
		public int ContractedFoldNext
		{
			get
			{
				return this.scintilla.DirectMessage(2618, new IntPtr(this.Index)).ToInt32();
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00005B54 File Offset: 0x00003D54
		public int DisplayIndex
		{
			get
			{
				return this.scintilla.DirectMessage(2220, new IntPtr(this.Index)).ToInt32();
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00005B84 File Offset: 0x00003D84
		public int EndPosition
		{
			get
			{
				return this.Position + this.Length;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00005B93 File Offset: 0x00003D93
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00005BBC File Offset: 0x00003DBC
		public bool Expanded
		{
			get
			{
				return this.scintilla.DirectMessage(2230, new IntPtr(this.Index)) != IntPtr.Zero;
			}
			set
			{
				IntPtr lParam = value ? new IntPtr(1) : IntPtr.Zero;
				this.scintilla.DirectMessage(2229, new IntPtr(this.Index), lParam);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00005BF8 File Offset: 0x00003DF8
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00005C30 File Offset: 0x00003E30
		public int FoldLevel
		{
			get
			{
				return this.scintilla.DirectMessage(2223, new IntPtr(this.Index)).ToInt32() & 4095;
			}
			set
			{
				int num = (int)this.FoldLevelFlags;
				num |= value;
				this.scintilla.DirectMessage(2222, new IntPtr(this.Index), new IntPtr(num));
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00005C6C File Offset: 0x00003E6C
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00005CA4 File Offset: 0x00003EA4
		public FoldLevelFlags FoldLevelFlags
		{
			get
			{
				return (FoldLevelFlags)(this.scintilla.DirectMessage(2223, new IntPtr(this.Index)).ToInt32() & -4096);
			}
			set
			{
				int num = this.FoldLevel;
				num |= (int)value;
				this.scintilla.DirectMessage(2222, new IntPtr(this.Index), new IntPtr(num));
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00005CE0 File Offset: 0x00003EE0
		public int FoldParent
		{
			get
			{
				return this.scintilla.DirectMessage(2225, new IntPtr(this.Index)).ToInt32();
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00005D10 File Offset: 0x00003F10
		public int Height
		{
			get
			{
				return this.scintilla.DirectMessage(2279, new IntPtr(this.Index)).ToInt32();
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00005D40 File Offset: 0x00003F40
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00005D48 File Offset: 0x00003F48
		public int Index { get; private set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00005D51 File Offset: 0x00003F51
		public int Length
		{
			get
			{
				return this.scintilla.Lines.CharLineLength(this.Index);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00005D6C File Offset: 0x00003F6C
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00005D9C File Offset: 0x00003F9C
		public int MarginStyle
		{
			get
			{
				return this.scintilla.DirectMessage(2533, new IntPtr(this.Index)).ToInt32();
			}
			set
			{
				value = Helpers.Clamp(value, 0, this.scintilla.Styles.Count - 1);
				this.scintilla.DirectMessage(2532, new IntPtr(this.Index), new IntPtr(value));
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00005DDC File Offset: 0x00003FDC
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00005EBC File Offset: 0x000040BC
		public unsafe byte[] MarginStyles
		{
			get
			{
				int num = this.scintilla.DirectMessage(2531, new IntPtr(this.Index)).ToInt32();
				if (num == 0)
				{
					return new byte[0];
				}
				byte[] array = new byte[num + 1];
				byte[] array2 = new byte[num + 1];
				byte[] array3 = array;
				byte* ptr;
				if (array == null || array3.Length == 0)
				{
					ptr = null;
				}
				else
				{
					fixed (byte* ptr = &array3[0])
					{
					}
				}
				fixed (byte* ptr2 = array2)
				{
					this.scintilla.DirectMessage(2531, new IntPtr(this.Index), new IntPtr((void*)ptr));
					this.scintilla.DirectMessage(2535, new IntPtr(this.Index), new IntPtr((void*)ptr2));
					return Helpers.ByteToCharStyles(ptr2, ptr, num, this.scintilla.Encoding);
				}
			}
			set
			{
				int num = this.scintilla.DirectMessage(2531, new IntPtr(this.Index)).ToInt32();
				if (num == 0)
				{
					return;
				}
				fixed (byte* ptr = new byte[num + 1])
				{
					this.scintilla.DirectMessage(2531, new IntPtr(this.Index), new IntPtr((void*)ptr));
					fixed (byte* ptr2 = Helpers.CharToByteStyles(value ?? new byte[0], ptr, num, this.scintilla.Encoding))
					{
						this.scintilla.DirectMessage(2534, new IntPtr(this.Index), new IntPtr((void*)ptr2));
					}
				}
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00005F98 File Offset: 0x00004198
		// (set) Token: 0x0600009F RID: 159 RVA: 0x0000602C File Offset: 0x0000422C
		public unsafe string MarginText
		{
			get
			{
				int num = this.scintilla.DirectMessage(2531, new IntPtr(this.Index)).ToInt32();
				if (num == 0)
				{
					return string.Empty;
				}
				fixed (byte* ptr = new byte[num + 1])
				{
					this.scintilla.DirectMessage(2531, new IntPtr(this.Index), new IntPtr((void*)ptr));
					return Helpers.GetString(new IntPtr((void*)ptr), num, this.scintilla.Encoding);
				}
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.scintilla.DirectMessage(2530, new IntPtr(this.Index), IntPtr.Zero);
					return;
				}
				fixed (byte* bytes = Helpers.GetBytes(value, this.scintilla.Encoding, true))
				{
					this.scintilla.DirectMessage(2530, new IntPtr(this.Index), new IntPtr((void*)bytes));
				}
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000060B1 File Offset: 0x000042B1
		public int Position
		{
			get
			{
				return this.scintilla.Lines.CharPositionFromLine(this.Index);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x000060CC File Offset: 0x000042CC
		public unsafe string Text
		{
			get
			{
				IntPtr wParam = this.scintilla.DirectMessage(2167, new IntPtr(this.Index));
				IntPtr lParam = this.scintilla.DirectMessage(2350, new IntPtr(this.Index));
				IntPtr intPtr = this.scintilla.DirectMessage(2643, wParam, lParam);
				if (intPtr == IntPtr.Zero)
				{
					return string.Empty;
				}
				return new string((sbyte*)((void*)intPtr), 0, lParam.ToInt32(), this.scintilla.Encoding);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00006158 File Offset: 0x00004358
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00006188 File Offset: 0x00004388
		public int Indentation
		{
			get
			{
				return this.scintilla.DirectMessage(2127, new IntPtr(this.Index)).ToInt32();
			}
			set
			{
				this.scintilla.DirectMessage(2126, new IntPtr(this.Index), new IntPtr(value));
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000061AC File Offset: 0x000043AC
		public bool Visible
		{
			get
			{
				return this.scintilla.DirectMessage(2228, new IntPtr(this.Index)) != IntPtr.Zero;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000061D4 File Offset: 0x000043D4
		public int WrapCount
		{
			get
			{
				return this.scintilla.DirectMessage(2235, new IntPtr(this.Index)).ToInt32();
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00006204 File Offset: 0x00004404
		public Line(Scintilla scintilla, int index)
		{
			this.scintilla = scintilla;
			this.Index = index;
		}

		// Token: 0x04000111 RID: 273
		private readonly Scintilla scintilla;
	}
}
