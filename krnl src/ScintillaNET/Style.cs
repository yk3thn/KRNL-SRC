using System;
using System.Drawing;
using System.Text;

namespace ScintillaNET
{
	// Token: 0x02000047 RID: 71
	public class Style
	{
		// Token: 0x06000329 RID: 809 RVA: 0x0000D270 File Offset: 0x0000B470
		public void CopyTo(Style destination)
		{
			if (destination == null)
			{
				return;
			}
			destination.BackColor = this.BackColor;
			destination.Case = this.Case;
			destination.FillLine = this.FillLine;
			destination.Font = this.Font;
			destination.ForeColor = this.ForeColor;
			destination.Hotspot = this.Hotspot;
			destination.Italic = this.Italic;
			destination.Size = this.Size;
			destination.SizeF = this.SizeF;
			destination.Underline = this.Underline;
			destination.Visible = this.Visible;
			destination.Weight = this.Weight;
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000D314 File Offset: 0x0000B514
		// (set) Token: 0x0600032B RID: 811 RVA: 0x0000D350 File Offset: 0x0000B550
		public Color BackColor
		{
			get
			{
				return ColorTranslator.FromWin32(this.scintilla.DirectMessage(2482, new IntPtr(this.Index), IntPtr.Zero).ToInt32());
			}
			set
			{
				if (value.IsEmpty)
				{
					value = Color.White;
				}
				int value2 = ColorTranslator.ToWin32(value);
				this.scintilla.DirectMessage(2052, new IntPtr(this.Index), new IntPtr(value2));
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0000D396 File Offset: 0x0000B596
		// (set) Token: 0x0600032D RID: 813 RVA: 0x0000D3C4 File Offset: 0x0000B5C4
		public bool Bold
		{
			get
			{
				return this.scintilla.DirectMessage(2483, new IntPtr(this.Index), IntPtr.Zero) != IntPtr.Zero;
			}
			set
			{
				IntPtr lParam = value ? new IntPtr(1) : IntPtr.Zero;
				this.scintilla.DirectMessage(2053, new IntPtr(this.Index), lParam);
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000D400 File Offset: 0x0000B600
		// (set) Token: 0x0600032F RID: 815 RVA: 0x0000D438 File Offset: 0x0000B638
		public StyleCase Case
		{
			get
			{
				return (StyleCase)this.scintilla.DirectMessage(2489, new IntPtr(this.Index), IntPtr.Zero).ToInt32();
			}
			set
			{
				this.scintilla.DirectMessage(2060, new IntPtr(this.Index), new IntPtr((int)value));
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000D469 File Offset: 0x0000B669
		// (set) Token: 0x06000331 RID: 817 RVA: 0x0000D498 File Offset: 0x0000B698
		public bool FillLine
		{
			get
			{
				return this.scintilla.DirectMessage(2487, new IntPtr(this.Index), IntPtr.Zero) != IntPtr.Zero;
			}
			set
			{
				IntPtr lParam = value ? new IntPtr(1) : IntPtr.Zero;
				this.scintilla.DirectMessage(2057, new IntPtr(this.Index), lParam);
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000D4D4 File Offset: 0x0000B6D4
		// (set) Token: 0x06000333 RID: 819 RVA: 0x0000D560 File Offset: 0x0000B760
		public unsafe string Font
		{
			get
			{
				int num = this.scintilla.DirectMessage(2486, new IntPtr(this.Index), IntPtr.Zero).ToInt32();
				byte[] array = new byte[num];
				fixed (byte* ptr = array)
				{
					this.scintilla.DirectMessage(2486, new IntPtr(this.Index), new IntPtr((void*)ptr));
				}
				return Encoding.UTF8.GetString(array, 0, num);
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = "Verdana";
				}
				fixed (byte* bytes = Helpers.GetBytes(value, Encoding.UTF8, true))
				{
					this.scintilla.DirectMessage(2056, new IntPtr(this.Index), new IntPtr((void*)bytes));
				}
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000D5C4 File Offset: 0x0000B7C4
		// (set) Token: 0x06000335 RID: 821 RVA: 0x0000D600 File Offset: 0x0000B800
		public Color ForeColor
		{
			get
			{
				return ColorTranslator.FromWin32(this.scintilla.DirectMessage(2481, new IntPtr(this.Index), IntPtr.Zero).ToInt32());
			}
			set
			{
				if (value.IsEmpty)
				{
					value = Color.Black;
				}
				int value2 = ColorTranslator.ToWin32(value);
				this.scintilla.DirectMessage(2051, new IntPtr(this.Index), new IntPtr(value2));
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0000D646 File Offset: 0x0000B846
		// (set) Token: 0x06000337 RID: 823 RVA: 0x0000D674 File Offset: 0x0000B874
		public bool Hotspot
		{
			get
			{
				return this.scintilla.DirectMessage(2493, new IntPtr(this.Index), IntPtr.Zero) != IntPtr.Zero;
			}
			set
			{
				IntPtr lParam = value ? new IntPtr(1) : IntPtr.Zero;
				this.scintilla.DirectMessage(2409, new IntPtr(this.Index), lParam);
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0000D6AF File Offset: 0x0000B8AF
		// (set) Token: 0x06000339 RID: 825 RVA: 0x0000D6B7 File Offset: 0x0000B8B7
		public int Index { get; private set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600033A RID: 826 RVA: 0x0000D6C0 File Offset: 0x0000B8C0
		// (set) Token: 0x0600033B RID: 827 RVA: 0x0000D6EC File Offset: 0x0000B8EC
		public bool Italic
		{
			get
			{
				return this.scintilla.DirectMessage(2484, new IntPtr(this.Index), IntPtr.Zero) != IntPtr.Zero;
			}
			set
			{
				IntPtr lParam = value ? new IntPtr(1) : IntPtr.Zero;
				this.scintilla.DirectMessage(2054, new IntPtr(this.Index), lParam);
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600033C RID: 828 RVA: 0x0000D728 File Offset: 0x0000B928
		// (set) Token: 0x0600033D RID: 829 RVA: 0x0000D75D File Offset: 0x0000B95D
		public int Size
		{
			get
			{
				return this.scintilla.DirectMessage(2485, new IntPtr(this.Index), IntPtr.Zero).ToInt32();
			}
			set
			{
				this.scintilla.DirectMessage(2055, new IntPtr(this.Index), new IntPtr(value));
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600033E RID: 830 RVA: 0x0000D784 File Offset: 0x0000B984
		// (set) Token: 0x0600033F RID: 831 RVA: 0x0000D7C0 File Offset: 0x0000B9C0
		public float SizeF
		{
			get
			{
				return (float)this.scintilla.DirectMessage(2062, new IntPtr(this.Index), IntPtr.Zero).ToInt32() / 100f;
			}
			set
			{
				int value2 = (int)(value * 100f);
				this.scintilla.DirectMessage(2061, new IntPtr(this.Index), new IntPtr(value2));
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000D7F8 File Offset: 0x0000B9F8
		// (set) Token: 0x06000341 RID: 833 RVA: 0x0000D824 File Offset: 0x0000BA24
		public bool Underline
		{
			get
			{
				return this.scintilla.DirectMessage(2488, new IntPtr(this.Index), IntPtr.Zero) != IntPtr.Zero;
			}
			set
			{
				IntPtr lParam = value ? new IntPtr(1) : IntPtr.Zero;
				this.scintilla.DirectMessage(2059, new IntPtr(this.Index), lParam);
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0000D85F File Offset: 0x0000BA5F
		// (set) Token: 0x06000343 RID: 835 RVA: 0x0000D88C File Offset: 0x0000BA8C
		public bool Visible
		{
			get
			{
				return this.scintilla.DirectMessage(2491, new IntPtr(this.Index), IntPtr.Zero) != IntPtr.Zero;
			}
			set
			{
				IntPtr lParam = value ? new IntPtr(1) : IntPtr.Zero;
				this.scintilla.DirectMessage(2074, new IntPtr(this.Index), lParam);
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000344 RID: 836 RVA: 0x0000D8C8 File Offset: 0x0000BAC8
		// (set) Token: 0x06000345 RID: 837 RVA: 0x0000D8FD File Offset: 0x0000BAFD
		public int Weight
		{
			get
			{
				return this.scintilla.DirectMessage(2064, new IntPtr(this.Index), IntPtr.Zero).ToInt32();
			}
			set
			{
				this.scintilla.DirectMessage(2063, new IntPtr(this.Index), new IntPtr(value));
			}
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000D921 File Offset: 0x0000BB21
		public Style(Scintilla scintilla, int index)
		{
			this.scintilla = scintilla;
			this.Index = index;
		}

		// Token: 0x040007E6 RID: 2022
		public const int Default = 32;

		// Token: 0x040007E7 RID: 2023
		public const int LineNumber = 33;

		// Token: 0x040007E8 RID: 2024
		public const int CallTip = 38;

		// Token: 0x040007E9 RID: 2025
		public const int IndentGuide = 37;

		// Token: 0x040007EA RID: 2026
		public const int BraceLight = 34;

		// Token: 0x040007EB RID: 2027
		public const int BraceBad = 35;

		// Token: 0x040007EC RID: 2028
		public const int FoldDisplayText = 39;

		// Token: 0x040007ED RID: 2029
		private readonly Scintilla scintilla;

		// Token: 0x02000069 RID: 105
		public static class Ada
		{
			// Token: 0x0400087E RID: 2174
			public const int Default = 0;

			// Token: 0x0400087F RID: 2175
			public const int CommentLine = 10;

			// Token: 0x04000880 RID: 2176
			public const int Number = 3;

			// Token: 0x04000881 RID: 2177
			public const int Word = 1;

			// Token: 0x04000882 RID: 2178
			public const int String = 7;

			// Token: 0x04000883 RID: 2179
			public const int Character = 5;

			// Token: 0x04000884 RID: 2180
			public const int Delimiter = 4;

			// Token: 0x04000885 RID: 2181
			public const int Label = 9;

			// Token: 0x04000886 RID: 2182
			public const int Identifier = 2;

			// Token: 0x04000887 RID: 2183
			public const int StringEol = 8;

			// Token: 0x04000888 RID: 2184
			public const int CharacterEol = 6;

			// Token: 0x04000889 RID: 2185
			public const int Illegal = 11;
		}

		// Token: 0x0200006A RID: 106
		public static class Asm
		{
			// Token: 0x0400088A RID: 2186
			public const int Default = 0;

			// Token: 0x0400088B RID: 2187
			public const int Comment = 1;

			// Token: 0x0400088C RID: 2188
			public const int CommentBlock = 11;

			// Token: 0x0400088D RID: 2189
			public const int Number = 2;

			// Token: 0x0400088E RID: 2190
			public const int MathInstruction = 7;

			// Token: 0x0400088F RID: 2191
			public const int String = 3;

			// Token: 0x04000890 RID: 2192
			public const int Character = 12;

			// Token: 0x04000891 RID: 2193
			public const int CpuInstruction = 6;

			// Token: 0x04000892 RID: 2194
			public const int Register = 8;

			// Token: 0x04000893 RID: 2195
			public const int Operator = 4;

			// Token: 0x04000894 RID: 2196
			public const int Identifier = 5;

			// Token: 0x04000895 RID: 2197
			public const int StringEol = 13;

			// Token: 0x04000896 RID: 2198
			public const int Directive = 9;

			// Token: 0x04000897 RID: 2199
			public const int DirectiveOperand = 10;

			// Token: 0x04000898 RID: 2200
			public const int ExtInstruction = 14;

			// Token: 0x04000899 RID: 2201
			public const int CommentDirective = 15;
		}

		// Token: 0x0200006B RID: 107
		public static class BlitzBasic
		{
			// Token: 0x0400089A RID: 2202
			public const int Default = 0;

			// Token: 0x0400089B RID: 2203
			public const int Comment = 1;

			// Token: 0x0400089C RID: 2204
			public const int Number = 2;

			// Token: 0x0400089D RID: 2205
			public const int Keyword = 3;

			// Token: 0x0400089E RID: 2206
			public const int String = 4;

			// Token: 0x0400089F RID: 2207
			public const int Preprocessor = 5;

			// Token: 0x040008A0 RID: 2208
			public const int Operator = 6;

			// Token: 0x040008A1 RID: 2209
			public const int Identifier = 7;

			// Token: 0x040008A2 RID: 2210
			public const int Date = 8;

			// Token: 0x040008A3 RID: 2211
			public const int StringEol = 9;

			// Token: 0x040008A4 RID: 2212
			public const int Keyword2 = 10;

			// Token: 0x040008A5 RID: 2213
			public const int Keyword3 = 11;

			// Token: 0x040008A6 RID: 2214
			public const int Keyword4 = 12;

			// Token: 0x040008A7 RID: 2215
			public const int Constant = 13;

			// Token: 0x040008A8 RID: 2216
			public const int Asm = 14;

			// Token: 0x040008A9 RID: 2217
			public const int Label = 15;

			// Token: 0x040008AA RID: 2218
			public const int Error = 16;

			// Token: 0x040008AB RID: 2219
			public const int HexNumber = 17;

			// Token: 0x040008AC RID: 2220
			public const int BinNumber = 18;

			// Token: 0x040008AD RID: 2221
			public const int CommentBlock = 19;

			// Token: 0x040008AE RID: 2222
			public const int DocLine = 20;

			// Token: 0x040008AF RID: 2223
			public const int DocBlock = 21;

			// Token: 0x040008B0 RID: 2224
			public const int DocKeyword = 22;
		}

		// Token: 0x0200006C RID: 108
		public static class Batch
		{
			// Token: 0x040008B1 RID: 2225
			public const int Default = 0;

			// Token: 0x040008B2 RID: 2226
			public const int Comment = 1;

			// Token: 0x040008B3 RID: 2227
			public const int Word = 2;

			// Token: 0x040008B4 RID: 2228
			public const int Label = 3;

			// Token: 0x040008B5 RID: 2229
			public const int Hide = 4;

			// Token: 0x040008B6 RID: 2230
			public const int Command = 5;

			// Token: 0x040008B7 RID: 2231
			public const int Identifier = 6;

			// Token: 0x040008B8 RID: 2232
			public const int Operator = 7;
		}

		// Token: 0x0200006D RID: 109
		public static class Cpp
		{
			// Token: 0x040008B9 RID: 2233
			public const int Default = 0;

			// Token: 0x040008BA RID: 2234
			public const int Comment = 1;

			// Token: 0x040008BB RID: 2235
			public const int CommentLine = 2;

			// Token: 0x040008BC RID: 2236
			public const int CommentDoc = 3;

			// Token: 0x040008BD RID: 2237
			public const int Number = 4;

			// Token: 0x040008BE RID: 2238
			public const int Word = 5;

			// Token: 0x040008BF RID: 2239
			public const int String = 6;

			// Token: 0x040008C0 RID: 2240
			public const int Character = 7;

			// Token: 0x040008C1 RID: 2241
			public const int Uuid = 8;

			// Token: 0x040008C2 RID: 2242
			public const int Preprocessor = 9;

			// Token: 0x040008C3 RID: 2243
			public const int Operator = 10;

			// Token: 0x040008C4 RID: 2244
			public const int Identifier = 11;

			// Token: 0x040008C5 RID: 2245
			public const int StringEol = 12;

			// Token: 0x040008C6 RID: 2246
			public const int Verbatim = 13;

			// Token: 0x040008C7 RID: 2247
			public const int Regex = 14;

			// Token: 0x040008C8 RID: 2248
			public const int CommentLineDoc = 15;

			// Token: 0x040008C9 RID: 2249
			public const int Word2 = 16;

			// Token: 0x040008CA RID: 2250
			public const int CommentDocKeyword = 17;

			// Token: 0x040008CB RID: 2251
			public const int CommentDocKeywordError = 18;

			// Token: 0x040008CC RID: 2252
			public const int GlobalClass = 19;

			// Token: 0x040008CD RID: 2253
			public const int StringRaw = 20;

			// Token: 0x040008CE RID: 2254
			public const int TripleVerbatim = 21;

			// Token: 0x040008CF RID: 2255
			public const int HashQuotedString = 22;

			// Token: 0x040008D0 RID: 2256
			public const int PreprocessorComment = 23;

			// Token: 0x040008D1 RID: 2257
			public const int PreprocessorCommentDoc = 24;

			// Token: 0x040008D2 RID: 2258
			public const int UserLiteral = 25;

			// Token: 0x040008D3 RID: 2259
			public const int TaskMarker = 26;

			// Token: 0x040008D4 RID: 2260
			public const int EscapeSequence = 27;
		}

		// Token: 0x0200006E RID: 110
		public static class Css
		{
			// Token: 0x040008D5 RID: 2261
			public const int Default = 0;

			// Token: 0x040008D6 RID: 2262
			public const int Tag = 1;

			// Token: 0x040008D7 RID: 2263
			public const int Class = 2;

			// Token: 0x040008D8 RID: 2264
			public const int PseudoClass = 3;

			// Token: 0x040008D9 RID: 2265
			public const int UnknownPseudoClass = 4;

			// Token: 0x040008DA RID: 2266
			public const int Operator = 5;

			// Token: 0x040008DB RID: 2267
			public const int Identifier = 6;

			// Token: 0x040008DC RID: 2268
			public const int UnknownIdentifier = 7;

			// Token: 0x040008DD RID: 2269
			public const int Value = 8;

			// Token: 0x040008DE RID: 2270
			public const int Comment = 9;

			// Token: 0x040008DF RID: 2271
			public const int Id = 10;

			// Token: 0x040008E0 RID: 2272
			public const int Important = 11;

			// Token: 0x040008E1 RID: 2273
			public const int Directive = 12;

			// Token: 0x040008E2 RID: 2274
			public const int DoubleString = 13;

			// Token: 0x040008E3 RID: 2275
			public const int SingleString = 14;

			// Token: 0x040008E4 RID: 2276
			public const int Identifier2 = 15;

			// Token: 0x040008E5 RID: 2277
			public const int Attribute = 16;

			// Token: 0x040008E6 RID: 2278
			public const int Identifier3 = 17;

			// Token: 0x040008E7 RID: 2279
			public const int PseudoElement = 18;

			// Token: 0x040008E8 RID: 2280
			public const int ExtendedIdentifier = 19;

			// Token: 0x040008E9 RID: 2281
			public const int ExtendedPseudoClass = 20;

			// Token: 0x040008EA RID: 2282
			public const int ExtendedPseudoElement = 21;

			// Token: 0x040008EB RID: 2283
			public const int Media = 22;

			// Token: 0x040008EC RID: 2284
			public const int Variable = 23;
		}

		// Token: 0x0200006F RID: 111
		public static class Fortran
		{
			// Token: 0x040008ED RID: 2285
			public const int Default = 0;

			// Token: 0x040008EE RID: 2286
			public const int Comment = 1;

			// Token: 0x040008EF RID: 2287
			public const int Number = 2;

			// Token: 0x040008F0 RID: 2288
			public const int String1 = 3;

			// Token: 0x040008F1 RID: 2289
			public const int String2 = 4;

			// Token: 0x040008F2 RID: 2290
			public const int StringEol = 5;

			// Token: 0x040008F3 RID: 2291
			public const int Operator = 6;

			// Token: 0x040008F4 RID: 2292
			public const int Identifier = 7;

			// Token: 0x040008F5 RID: 2293
			public const int Word = 8;

			// Token: 0x040008F6 RID: 2294
			public const int Word2 = 9;

			// Token: 0x040008F7 RID: 2295
			public const int Word3 = 10;

			// Token: 0x040008F8 RID: 2296
			public const int Preprocessor = 11;

			// Token: 0x040008F9 RID: 2297
			public const int Operator2 = 12;

			// Token: 0x040008FA RID: 2298
			public const int Label = 13;

			// Token: 0x040008FB RID: 2299
			public const int Continuation = 14;
		}

		// Token: 0x02000070 RID: 112
		public static class FreeBasic
		{
			// Token: 0x040008FC RID: 2300
			public const int Default = 0;

			// Token: 0x040008FD RID: 2301
			public const int Comment = 1;

			// Token: 0x040008FE RID: 2302
			public const int Number = 2;

			// Token: 0x040008FF RID: 2303
			public const int Keyword = 3;

			// Token: 0x04000900 RID: 2304
			public const int String = 4;

			// Token: 0x04000901 RID: 2305
			public const int Preprocessor = 5;

			// Token: 0x04000902 RID: 2306
			public const int Operator = 6;

			// Token: 0x04000903 RID: 2307
			public const int Identifier = 7;

			// Token: 0x04000904 RID: 2308
			public const int Date = 8;

			// Token: 0x04000905 RID: 2309
			public const int StringEol = 9;

			// Token: 0x04000906 RID: 2310
			public const int Keyword2 = 10;

			// Token: 0x04000907 RID: 2311
			public const int Keyword3 = 11;

			// Token: 0x04000908 RID: 2312
			public const int Keyword4 = 12;

			// Token: 0x04000909 RID: 2313
			public const int Constant = 13;

			// Token: 0x0400090A RID: 2314
			public const int Asm = 14;

			// Token: 0x0400090B RID: 2315
			public const int Label = 15;

			// Token: 0x0400090C RID: 2316
			public const int Error = 16;

			// Token: 0x0400090D RID: 2317
			public const int HexNumber = 17;

			// Token: 0x0400090E RID: 2318
			public const int BinNumber = 18;

			// Token: 0x0400090F RID: 2319
			public const int CommentBlock = 19;

			// Token: 0x04000910 RID: 2320
			public const int DocLine = 20;

			// Token: 0x04000911 RID: 2321
			public const int DocBlock = 21;

			// Token: 0x04000912 RID: 2322
			public const int DocKeyword = 22;
		}

		// Token: 0x02000071 RID: 113
		public static class Html
		{
			// Token: 0x04000913 RID: 2323
			public const int Default = 0;

			// Token: 0x04000914 RID: 2324
			public const int Tag = 1;

			// Token: 0x04000915 RID: 2325
			public const int TagUnknown = 2;

			// Token: 0x04000916 RID: 2326
			public const int Attribute = 3;

			// Token: 0x04000917 RID: 2327
			public const int AttributeUnknown = 4;

			// Token: 0x04000918 RID: 2328
			public const int Number = 5;

			// Token: 0x04000919 RID: 2329
			public const int DoubleString = 6;

			// Token: 0x0400091A RID: 2330
			public const int SingleString = 7;

			// Token: 0x0400091B RID: 2331
			public const int Other = 8;

			// Token: 0x0400091C RID: 2332
			public const int Comment = 9;

			// Token: 0x0400091D RID: 2333
			public const int Entity = 10;

			// Token: 0x0400091E RID: 2334
			public const int TagEnd = 11;

			// Token: 0x0400091F RID: 2335
			public const int XmlStart = 12;

			// Token: 0x04000920 RID: 2336
			public const int XmlEnd = 13;

			// Token: 0x04000921 RID: 2337
			public const int Script = 14;

			// Token: 0x04000922 RID: 2338
			public const int Asp = 15;

			// Token: 0x04000923 RID: 2339
			public const int AspAt = 16;

			// Token: 0x04000924 RID: 2340
			public const int CData = 17;

			// Token: 0x04000925 RID: 2341
			public const int Question = 18;

			// Token: 0x04000926 RID: 2342
			public const int Value = 19;

			// Token: 0x04000927 RID: 2343
			public const int XcComment = 20;
		}

		// Token: 0x02000072 RID: 114
		public static class Json
		{
			// Token: 0x04000928 RID: 2344
			public const int Default = 0;

			// Token: 0x04000929 RID: 2345
			public const int Number = 1;

			// Token: 0x0400092A RID: 2346
			public const int String = 2;

			// Token: 0x0400092B RID: 2347
			public const int StringEol = 3;

			// Token: 0x0400092C RID: 2348
			public const int PropertyName = 4;

			// Token: 0x0400092D RID: 2349
			public const int EscapeSequence = 5;

			// Token: 0x0400092E RID: 2350
			public const int LineComment = 6;

			// Token: 0x0400092F RID: 2351
			public const int BlockComment = 7;

			// Token: 0x04000930 RID: 2352
			public const int Operator = 8;

			// Token: 0x04000931 RID: 2353
			public const int Uri = 9;

			// Token: 0x04000932 RID: 2354
			public const int CompactIRI = 10;

			// Token: 0x04000933 RID: 2355
			public const int Keyword = 11;

			// Token: 0x04000934 RID: 2356
			public const int LdKeyword = 12;

			// Token: 0x04000935 RID: 2357
			public const int Error = 13;
		}

		// Token: 0x02000073 RID: 115
		public static class Lisp
		{
			// Token: 0x04000936 RID: 2358
			public const int Default = 0;

			// Token: 0x04000937 RID: 2359
			public const int Comment = 1;

			// Token: 0x04000938 RID: 2360
			public const int Number = 2;

			// Token: 0x04000939 RID: 2361
			public const int Keyword = 3;

			// Token: 0x0400093A RID: 2362
			public const int KeywordKw = 4;

			// Token: 0x0400093B RID: 2363
			public const int Symbol = 5;

			// Token: 0x0400093C RID: 2364
			public const int String = 6;

			// Token: 0x0400093D RID: 2365
			public const int StringEol = 8;

			// Token: 0x0400093E RID: 2366
			public const int Identifier = 9;

			// Token: 0x0400093F RID: 2367
			public const int Operator = 10;

			// Token: 0x04000940 RID: 2368
			public const int Special = 11;

			// Token: 0x04000941 RID: 2369
			public const int MultiComment = 12;
		}

		// Token: 0x02000074 RID: 116
		public static class Lua
		{
			// Token: 0x04000942 RID: 2370
			public const int Default = 0;

			// Token: 0x04000943 RID: 2371
			public const int Comment = 1;

			// Token: 0x04000944 RID: 2372
			public const int CommentLine = 2;

			// Token: 0x04000945 RID: 2373
			public const int CommentDoc = 3;

			// Token: 0x04000946 RID: 2374
			public const int Number = 4;

			// Token: 0x04000947 RID: 2375
			public const int Word = 5;

			// Token: 0x04000948 RID: 2376
			public const int String = 6;

			// Token: 0x04000949 RID: 2377
			public const int Character = 7;

			// Token: 0x0400094A RID: 2378
			public const int LiteralString = 8;

			// Token: 0x0400094B RID: 2379
			public const int Preprocessor = 9;

			// Token: 0x0400094C RID: 2380
			public const int Operator = 10;

			// Token: 0x0400094D RID: 2381
			public const int Identifier = 11;

			// Token: 0x0400094E RID: 2382
			public const int StringEol = 12;

			// Token: 0x0400094F RID: 2383
			public const int Word2 = 13;

			// Token: 0x04000950 RID: 2384
			public const int Word3 = 14;

			// Token: 0x04000951 RID: 2385
			public const int Word4 = 15;

			// Token: 0x04000952 RID: 2386
			public const int Word5 = 16;

			// Token: 0x04000953 RID: 2387
			public const int Word6 = 17;

			// Token: 0x04000954 RID: 2388
			public const int Word7 = 18;

			// Token: 0x04000955 RID: 2389
			public const int Word8 = 19;

			// Token: 0x04000956 RID: 2390
			public const int Label = 20;
		}

		// Token: 0x02000075 RID: 117
		public static class Pascal
		{
			// Token: 0x04000957 RID: 2391
			public const int Default = 0;

			// Token: 0x04000958 RID: 2392
			public const int Identifier = 1;

			// Token: 0x04000959 RID: 2393
			public const int Comment = 2;

			// Token: 0x0400095A RID: 2394
			public const int Comment2 = 3;

			// Token: 0x0400095B RID: 2395
			public const int CommentLine = 4;

			// Token: 0x0400095C RID: 2396
			public const int Preprocessor = 5;

			// Token: 0x0400095D RID: 2397
			public const int Preprocessor2 = 6;

			// Token: 0x0400095E RID: 2398
			public const int Number = 7;

			// Token: 0x0400095F RID: 2399
			public const int HexNumber = 8;

			// Token: 0x04000960 RID: 2400
			public const int Word = 9;

			// Token: 0x04000961 RID: 2401
			public const int String = 10;

			// Token: 0x04000962 RID: 2402
			public const int StringEol = 11;

			// Token: 0x04000963 RID: 2403
			public const int Character = 12;

			// Token: 0x04000964 RID: 2404
			public const int Operator = 13;

			// Token: 0x04000965 RID: 2405
			public const int Asm = 14;
		}

		// Token: 0x02000076 RID: 118
		public static class Perl
		{
			// Token: 0x04000966 RID: 2406
			public const int Default = 0;

			// Token: 0x04000967 RID: 2407
			public const int Error = 1;

			// Token: 0x04000968 RID: 2408
			public const int CommentLine = 2;

			// Token: 0x04000969 RID: 2409
			public const int Pod = 3;

			// Token: 0x0400096A RID: 2410
			public const int Number = 4;

			// Token: 0x0400096B RID: 2411
			public const int Word = 5;

			// Token: 0x0400096C RID: 2412
			public const int String = 6;

			// Token: 0x0400096D RID: 2413
			public const int Character = 7;

			// Token: 0x0400096E RID: 2414
			public const int Punctuation = 8;

			// Token: 0x0400096F RID: 2415
			public const int Preprocessor = 9;

			// Token: 0x04000970 RID: 2416
			public const int Operator = 10;

			// Token: 0x04000971 RID: 2417
			public const int Identifier = 11;

			// Token: 0x04000972 RID: 2418
			public const int Scalar = 12;

			// Token: 0x04000973 RID: 2419
			public const int Array = 13;

			// Token: 0x04000974 RID: 2420
			public const int Hash = 14;

			// Token: 0x04000975 RID: 2421
			public const int SymbolTable = 15;

			// Token: 0x04000976 RID: 2422
			public const int VariableIndexer = 16;

			// Token: 0x04000977 RID: 2423
			public const int Regex = 17;

			// Token: 0x04000978 RID: 2424
			public const int RegSubst = 18;

			// Token: 0x04000979 RID: 2425
			public const int BackTicks = 20;

			// Token: 0x0400097A RID: 2426
			public const int DataSection = 21;

			// Token: 0x0400097B RID: 2427
			public const int HereDelim = 22;

			// Token: 0x0400097C RID: 2428
			public const int HereQ = 23;

			// Token: 0x0400097D RID: 2429
			public const int HereQq = 24;

			// Token: 0x0400097E RID: 2430
			public const int HereQx = 25;

			// Token: 0x0400097F RID: 2431
			public const int StringQ = 26;

			// Token: 0x04000980 RID: 2432
			public const int StringQq = 27;

			// Token: 0x04000981 RID: 2433
			public const int StringQx = 28;

			// Token: 0x04000982 RID: 2434
			public const int StringQr = 29;

			// Token: 0x04000983 RID: 2435
			public const int StringQw = 30;

			// Token: 0x04000984 RID: 2436
			public const int PodVerb = 31;

			// Token: 0x04000985 RID: 2437
			public const int SubPrototype = 40;

			// Token: 0x04000986 RID: 2438
			public const int FormatIdent = 41;

			// Token: 0x04000987 RID: 2439
			public const int Format = 42;

			// Token: 0x04000988 RID: 2440
			public const int StringVar = 43;

			// Token: 0x04000989 RID: 2441
			public const int XLat = 44;

			// Token: 0x0400098A RID: 2442
			public const int RegexVar = 54;

			// Token: 0x0400098B RID: 2443
			public const int RegSubstVar = 55;

			// Token: 0x0400098C RID: 2444
			public const int BackticksVar = 57;

			// Token: 0x0400098D RID: 2445
			public const int HereQqVar = 61;

			// Token: 0x0400098E RID: 2446
			public const int HereQxVar = 62;

			// Token: 0x0400098F RID: 2447
			public const int StringQqVar = 64;

			// Token: 0x04000990 RID: 2448
			public const int StringQxVar = 65;

			// Token: 0x04000991 RID: 2449
			public const int StringQrVar = 66;
		}

		// Token: 0x02000077 RID: 119
		public static class PhpScript
		{
			// Token: 0x04000992 RID: 2450
			public const int ComplexVariable = 104;

			// Token: 0x04000993 RID: 2451
			public const int Default = 118;

			// Token: 0x04000994 RID: 2452
			public const int HString = 119;

			// Token: 0x04000995 RID: 2453
			public const int SimpleString = 120;

			// Token: 0x04000996 RID: 2454
			public const int Word = 121;

			// Token: 0x04000997 RID: 2455
			public const int Number = 122;

			// Token: 0x04000998 RID: 2456
			public const int Variable = 123;

			// Token: 0x04000999 RID: 2457
			public const int Comment = 124;

			// Token: 0x0400099A RID: 2458
			public const int CommentLine = 125;

			// Token: 0x0400099B RID: 2459
			public const int HStringVariable = 126;

			// Token: 0x0400099C RID: 2460
			public const int Operator = 127;
		}

		// Token: 0x02000078 RID: 120
		public static class PowerShell
		{
			// Token: 0x0400099D RID: 2461
			public const int Default = 0;

			// Token: 0x0400099E RID: 2462
			public const int Comment = 1;

			// Token: 0x0400099F RID: 2463
			public const int String = 2;

			// Token: 0x040009A0 RID: 2464
			public const int Character = 3;

			// Token: 0x040009A1 RID: 2465
			public const int Number = 4;

			// Token: 0x040009A2 RID: 2466
			public const int Variable = 5;

			// Token: 0x040009A3 RID: 2467
			public const int Operator = 6;

			// Token: 0x040009A4 RID: 2468
			public const int Identifier = 7;

			// Token: 0x040009A5 RID: 2469
			public const int Keyword = 8;

			// Token: 0x040009A6 RID: 2470
			public const int Cmdlet = 9;

			// Token: 0x040009A7 RID: 2471
			public const int Alias = 10;

			// Token: 0x040009A8 RID: 2472
			public const int Function = 11;

			// Token: 0x040009A9 RID: 2473
			public const int User1 = 12;

			// Token: 0x040009AA RID: 2474
			public const int CommentStream = 13;

			// Token: 0x040009AB RID: 2475
			public const int HereString = 14;

			// Token: 0x040009AC RID: 2476
			public const int HereCharacter = 15;

			// Token: 0x040009AD RID: 2477
			public const int CommentDocKeyword = 16;
		}

		// Token: 0x02000079 RID: 121
		public static class Properties
		{
			// Token: 0x040009AE RID: 2478
			public const int Default = 0;

			// Token: 0x040009AF RID: 2479
			public const int Comment = 1;

			// Token: 0x040009B0 RID: 2480
			public const int Section = 2;

			// Token: 0x040009B1 RID: 2481
			public const int Assignment = 3;

			// Token: 0x040009B2 RID: 2482
			public const int DefVal = 4;

			// Token: 0x040009B3 RID: 2483
			public const int Key = 5;
		}

		// Token: 0x0200007A RID: 122
		public static class PureBasic
		{
			// Token: 0x040009B4 RID: 2484
			public const int Default = 0;

			// Token: 0x040009B5 RID: 2485
			public const int Comment = 1;

			// Token: 0x040009B6 RID: 2486
			public const int Number = 2;

			// Token: 0x040009B7 RID: 2487
			public const int Keyword = 3;

			// Token: 0x040009B8 RID: 2488
			public const int String = 4;

			// Token: 0x040009B9 RID: 2489
			public const int Preprocessor = 5;

			// Token: 0x040009BA RID: 2490
			public const int Operator = 6;

			// Token: 0x040009BB RID: 2491
			public const int Identifier = 7;

			// Token: 0x040009BC RID: 2492
			public const int Date = 8;

			// Token: 0x040009BD RID: 2493
			public const int StringEol = 9;

			// Token: 0x040009BE RID: 2494
			public const int Keyword2 = 10;

			// Token: 0x040009BF RID: 2495
			public const int Keyword3 = 11;

			// Token: 0x040009C0 RID: 2496
			public const int Keyword4 = 12;

			// Token: 0x040009C1 RID: 2497
			public const int Constant = 13;

			// Token: 0x040009C2 RID: 2498
			public const int Asm = 14;

			// Token: 0x040009C3 RID: 2499
			public const int Label = 15;

			// Token: 0x040009C4 RID: 2500
			public const int Error = 16;

			// Token: 0x040009C5 RID: 2501
			public const int HexNumber = 17;

			// Token: 0x040009C6 RID: 2502
			public const int BinNumber = 18;

			// Token: 0x040009C7 RID: 2503
			public const int CommentBlock = 19;

			// Token: 0x040009C8 RID: 2504
			public const int DocLine = 20;

			// Token: 0x040009C9 RID: 2505
			public const int DocBlock = 21;

			// Token: 0x040009CA RID: 2506
			public const int DocKeyword = 22;
		}

		// Token: 0x0200007B RID: 123
		public static class Python
		{
			// Token: 0x040009CB RID: 2507
			public const int Default = 0;

			// Token: 0x040009CC RID: 2508
			public const int CommentLine = 1;

			// Token: 0x040009CD RID: 2509
			public const int Number = 2;

			// Token: 0x040009CE RID: 2510
			public const int String = 3;

			// Token: 0x040009CF RID: 2511
			public const int Character = 4;

			// Token: 0x040009D0 RID: 2512
			public const int Word = 5;

			// Token: 0x040009D1 RID: 2513
			public const int Triple = 6;

			// Token: 0x040009D2 RID: 2514
			public const int TripleDouble = 7;

			// Token: 0x040009D3 RID: 2515
			public const int ClassName = 8;

			// Token: 0x040009D4 RID: 2516
			public const int DefName = 9;

			// Token: 0x040009D5 RID: 2517
			public const int Operator = 10;

			// Token: 0x040009D6 RID: 2518
			public const int Identifier = 11;

			// Token: 0x040009D7 RID: 2519
			public const int CommentBlock = 12;

			// Token: 0x040009D8 RID: 2520
			public const int StringEol = 13;

			// Token: 0x040009D9 RID: 2521
			public const int Word2 = 14;

			// Token: 0x040009DA RID: 2522
			public const int Decorator = 15;
		}

		// Token: 0x0200007C RID: 124
		public static class Ruby
		{
			// Token: 0x040009DB RID: 2523
			public const int Default = 0;

			// Token: 0x040009DC RID: 2524
			public const int Error = 1;

			// Token: 0x040009DD RID: 2525
			public const int CommentLine = 2;

			// Token: 0x040009DE RID: 2526
			public const int Pod = 3;

			// Token: 0x040009DF RID: 2527
			public const int Number = 4;

			// Token: 0x040009E0 RID: 2528
			public const int Word = 5;

			// Token: 0x040009E1 RID: 2529
			public const int String = 6;

			// Token: 0x040009E2 RID: 2530
			public const int Character = 7;

			// Token: 0x040009E3 RID: 2531
			public const int ClassName = 8;

			// Token: 0x040009E4 RID: 2532
			public const int DefName = 9;

			// Token: 0x040009E5 RID: 2533
			public const int Operator = 10;

			// Token: 0x040009E6 RID: 2534
			public const int Identifier = 11;

			// Token: 0x040009E7 RID: 2535
			public const int Regex = 12;

			// Token: 0x040009E8 RID: 2536
			public const int Global = 13;

			// Token: 0x040009E9 RID: 2537
			public const int Symbol = 14;

			// Token: 0x040009EA RID: 2538
			public const int ModuleName = 15;

			// Token: 0x040009EB RID: 2539
			public const int InstanceVar = 16;

			// Token: 0x040009EC RID: 2540
			public const int ClassVar = 17;

			// Token: 0x040009ED RID: 2541
			public const int BackTicks = 18;

			// Token: 0x040009EE RID: 2542
			public const int DataSection = 19;

			// Token: 0x040009EF RID: 2543
			public const int HereDelim = 20;

			// Token: 0x040009F0 RID: 2544
			public const int HereQ = 21;

			// Token: 0x040009F1 RID: 2545
			public const int HereQq = 22;

			// Token: 0x040009F2 RID: 2546
			public const int HereQx = 23;

			// Token: 0x040009F3 RID: 2547
			public const int StringQ = 24;

			// Token: 0x040009F4 RID: 2548
			public const int StringQq = 25;

			// Token: 0x040009F5 RID: 2549
			public const int StringQx = 26;

			// Token: 0x040009F6 RID: 2550
			public const int StringQr = 27;

			// Token: 0x040009F7 RID: 2551
			public const int StringQw = 28;

			// Token: 0x040009F8 RID: 2552
			public const int WordDemoted = 29;

			// Token: 0x040009F9 RID: 2553
			public const int StdIn = 30;

			// Token: 0x040009FA RID: 2554
			public const int StdOut = 31;

			// Token: 0x040009FB RID: 2555
			public const int StdErr = 40;
		}

		// Token: 0x0200007D RID: 125
		public static class Smalltalk
		{
			// Token: 0x040009FC RID: 2556
			public const int Default = 0;

			// Token: 0x040009FD RID: 2557
			public const int String = 1;

			// Token: 0x040009FE RID: 2558
			public const int Number = 2;

			// Token: 0x040009FF RID: 2559
			public const int Comment = 3;

			// Token: 0x04000A00 RID: 2560
			public const int Symbol = 4;

			// Token: 0x04000A01 RID: 2561
			public const int Binary = 5;

			// Token: 0x04000A02 RID: 2562
			public const int Bool = 6;

			// Token: 0x04000A03 RID: 2563
			public const int Self = 7;

			// Token: 0x04000A04 RID: 2564
			public const int Super = 8;

			// Token: 0x04000A05 RID: 2565
			public const int Nil = 9;

			// Token: 0x04000A06 RID: 2566
			public const int Global = 10;

			// Token: 0x04000A07 RID: 2567
			public const int Return = 11;

			// Token: 0x04000A08 RID: 2568
			public const int Special = 12;

			// Token: 0x04000A09 RID: 2569
			public const int KwsEnd = 13;

			// Token: 0x04000A0A RID: 2570
			public const int Assign = 14;

			// Token: 0x04000A0B RID: 2571
			public const int Character = 15;

			// Token: 0x04000A0C RID: 2572
			public const int SpecSel = 16;
		}

		// Token: 0x0200007E RID: 126
		public static class Sql
		{
			// Token: 0x04000A0D RID: 2573
			public const int Default = 0;

			// Token: 0x04000A0E RID: 2574
			public const int Comment = 1;

			// Token: 0x04000A0F RID: 2575
			public const int CommentLine = 2;

			// Token: 0x04000A10 RID: 2576
			public const int CommentDoc = 3;

			// Token: 0x04000A11 RID: 2577
			public const int Number = 4;

			// Token: 0x04000A12 RID: 2578
			public const int Word = 5;

			// Token: 0x04000A13 RID: 2579
			public const int String = 6;

			// Token: 0x04000A14 RID: 2580
			public const int Character = 7;

			// Token: 0x04000A15 RID: 2581
			public const int SqlPlus = 8;

			// Token: 0x04000A16 RID: 2582
			public const int SqlPlusPrompt = 9;

			// Token: 0x04000A17 RID: 2583
			public const int Operator = 10;

			// Token: 0x04000A18 RID: 2584
			public const int Identifier = 11;

			// Token: 0x04000A19 RID: 2585
			public const int SqlPlusComment = 13;

			// Token: 0x04000A1A RID: 2586
			public const int CommentLineDoc = 15;

			// Token: 0x04000A1B RID: 2587
			public const int Word2 = 16;

			// Token: 0x04000A1C RID: 2588
			public const int CommentDocKeyword = 17;

			// Token: 0x04000A1D RID: 2589
			public const int CommentDocKeywordError = 18;

			// Token: 0x04000A1E RID: 2590
			public const int User1 = 19;

			// Token: 0x04000A1F RID: 2591
			public const int User2 = 20;

			// Token: 0x04000A20 RID: 2592
			public const int User3 = 21;

			// Token: 0x04000A21 RID: 2593
			public const int User4 = 22;

			// Token: 0x04000A22 RID: 2594
			public const int QuotedIdentifier = 23;

			// Token: 0x04000A23 RID: 2595
			public const int QOperator = 24;
		}

		// Token: 0x0200007F RID: 127
		public static class Markdown
		{
			// Token: 0x04000A24 RID: 2596
			public const int Default = 0;

			// Token: 0x04000A25 RID: 2597
			public const int LineBegin = 1;

			// Token: 0x04000A26 RID: 2598
			public const int Strong1 = 2;

			// Token: 0x04000A27 RID: 2599
			public const int Strong2 = 3;

			// Token: 0x04000A28 RID: 2600
			public const int Em1 = 4;

			// Token: 0x04000A29 RID: 2601
			public const int Em2 = 5;

			// Token: 0x04000A2A RID: 2602
			public const int Header1 = 6;

			// Token: 0x04000A2B RID: 2603
			public const int Header2 = 7;

			// Token: 0x04000A2C RID: 2604
			public const int Header3 = 8;

			// Token: 0x04000A2D RID: 2605
			public const int Header4 = 9;

			// Token: 0x04000A2E RID: 2606
			public const int Header5 = 10;

			// Token: 0x04000A2F RID: 2607
			public const int Header6 = 11;

			// Token: 0x04000A30 RID: 2608
			public const int PreChar = 12;

			// Token: 0x04000A31 RID: 2609
			public const int UListItem = 13;

			// Token: 0x04000A32 RID: 2610
			public const int OListItem = 14;

			// Token: 0x04000A33 RID: 2611
			public const int BlockQuote = 15;

			// Token: 0x04000A34 RID: 2612
			public const int Strikeout = 16;

			// Token: 0x04000A35 RID: 2613
			public const int HRule = 17;

			// Token: 0x04000A36 RID: 2614
			public const int Link = 18;

			// Token: 0x04000A37 RID: 2615
			public const int Code = 19;

			// Token: 0x04000A38 RID: 2616
			public const int Code2 = 20;

			// Token: 0x04000A39 RID: 2617
			public const int CodeBk = 21;
		}

		// Token: 0x02000080 RID: 128
		public static class R
		{
			// Token: 0x04000A3A RID: 2618
			public const int Default = 0;

			// Token: 0x04000A3B RID: 2619
			public const int Comment = 1;

			// Token: 0x04000A3C RID: 2620
			public const int KWord = 2;

			// Token: 0x04000A3D RID: 2621
			public const int BaseKWord = 3;

			// Token: 0x04000A3E RID: 2622
			public const int OtherKWord = 4;

			// Token: 0x04000A3F RID: 2623
			public const int Number = 5;

			// Token: 0x04000A40 RID: 2624
			public const int String = 6;

			// Token: 0x04000A41 RID: 2625
			public const int String2 = 7;

			// Token: 0x04000A42 RID: 2626
			public const int Operator = 8;

			// Token: 0x04000A43 RID: 2627
			public const int Identifier = 9;

			// Token: 0x04000A44 RID: 2628
			public const int Infix = 10;

			// Token: 0x04000A45 RID: 2629
			public const int InfixEol = 11;
		}

		// Token: 0x02000081 RID: 129
		public static class Vb
		{
			// Token: 0x04000A46 RID: 2630
			public const int Default = 0;

			// Token: 0x04000A47 RID: 2631
			public const int Comment = 1;

			// Token: 0x04000A48 RID: 2632
			public const int Number = 2;

			// Token: 0x04000A49 RID: 2633
			public const int Keyword = 3;

			// Token: 0x04000A4A RID: 2634
			public const int String = 4;

			// Token: 0x04000A4B RID: 2635
			public const int Preprocessor = 5;

			// Token: 0x04000A4C RID: 2636
			public const int Operator = 6;

			// Token: 0x04000A4D RID: 2637
			public const int Identifier = 7;

			// Token: 0x04000A4E RID: 2638
			public const int Date = 8;

			// Token: 0x04000A4F RID: 2639
			public const int StringEol = 9;

			// Token: 0x04000A50 RID: 2640
			public const int Keyword2 = 10;

			// Token: 0x04000A51 RID: 2641
			public const int Keyword3 = 11;

			// Token: 0x04000A52 RID: 2642
			public const int Keyword4 = 12;

			// Token: 0x04000A53 RID: 2643
			public const int Constant = 13;

			// Token: 0x04000A54 RID: 2644
			public const int Asm = 14;

			// Token: 0x04000A55 RID: 2645
			public const int Label = 15;

			// Token: 0x04000A56 RID: 2646
			public const int Error = 16;

			// Token: 0x04000A57 RID: 2647
			public const int HexNumber = 17;

			// Token: 0x04000A58 RID: 2648
			public const int BinNumber = 18;

			// Token: 0x04000A59 RID: 2649
			public const int CommentBlock = 19;

			// Token: 0x04000A5A RID: 2650
			public const int DocLine = 20;

			// Token: 0x04000A5B RID: 2651
			public const int DocBlock = 21;

			// Token: 0x04000A5C RID: 2652
			public const int DocKeyword = 22;
		}

		// Token: 0x02000082 RID: 130
		public static class VbScript
		{
			// Token: 0x04000A5D RID: 2653
			public const int Default = 0;

			// Token: 0x04000A5E RID: 2654
			public const int Comment = 1;

			// Token: 0x04000A5F RID: 2655
			public const int Number = 2;

			// Token: 0x04000A60 RID: 2656
			public const int Keyword = 3;

			// Token: 0x04000A61 RID: 2657
			public const int String = 4;

			// Token: 0x04000A62 RID: 2658
			public const int Preprocessor = 5;

			// Token: 0x04000A63 RID: 2659
			public const int Operator = 6;

			// Token: 0x04000A64 RID: 2660
			public const int Identifier = 7;

			// Token: 0x04000A65 RID: 2661
			public const int Date = 8;

			// Token: 0x04000A66 RID: 2662
			public const int StringEol = 9;

			// Token: 0x04000A67 RID: 2663
			public const int Keyword2 = 10;

			// Token: 0x04000A68 RID: 2664
			public const int Keyword3 = 11;

			// Token: 0x04000A69 RID: 2665
			public const int Keyword4 = 12;

			// Token: 0x04000A6A RID: 2666
			public const int Constant = 13;

			// Token: 0x04000A6B RID: 2667
			public const int Asm = 14;

			// Token: 0x04000A6C RID: 2668
			public const int Label = 15;

			// Token: 0x04000A6D RID: 2669
			public const int Error = 16;

			// Token: 0x04000A6E RID: 2670
			public const int HexNumber = 17;

			// Token: 0x04000A6F RID: 2671
			public const int BinNumber = 18;

			// Token: 0x04000A70 RID: 2672
			public const int CommentBlock = 19;

			// Token: 0x04000A71 RID: 2673
			public const int DocLine = 20;

			// Token: 0x04000A72 RID: 2674
			public const int DocBlock = 21;

			// Token: 0x04000A73 RID: 2675
			public const int DocKeyword = 22;
		}

		// Token: 0x02000083 RID: 131
		public static class Verilog
		{
			// Token: 0x04000A74 RID: 2676
			public const int Default = 0;

			// Token: 0x04000A75 RID: 2677
			public const int Comment = 1;

			// Token: 0x04000A76 RID: 2678
			public const int CommentLine = 2;

			// Token: 0x04000A77 RID: 2679
			public const int CommentLineBang = 3;

			// Token: 0x04000A78 RID: 2680
			public const int Number = 4;

			// Token: 0x04000A79 RID: 2681
			public const int Word = 5;

			// Token: 0x04000A7A RID: 2682
			public const int String = 6;

			// Token: 0x04000A7B RID: 2683
			public const int Word2 = 7;

			// Token: 0x04000A7C RID: 2684
			public const int Word3 = 8;

			// Token: 0x04000A7D RID: 2685
			public const int Preprocessor = 9;

			// Token: 0x04000A7E RID: 2686
			public const int Operator = 10;

			// Token: 0x04000A7F RID: 2687
			public const int Identifier = 11;

			// Token: 0x04000A80 RID: 2688
			public const int StringEol = 12;

			// Token: 0x04000A81 RID: 2689
			public const int User = 19;

			// Token: 0x04000A82 RID: 2690
			public const int CommentWord = 20;

			// Token: 0x04000A83 RID: 2691
			public const int Input = 21;

			// Token: 0x04000A84 RID: 2692
			public const int Output = 22;

			// Token: 0x04000A85 RID: 2693
			public const int InOut = 23;

			// Token: 0x04000A86 RID: 2694
			public const int PortConnect = 24;
		}

		// Token: 0x02000084 RID: 132
		public static class Xml
		{
			// Token: 0x04000A87 RID: 2695
			public const int Default = 0;

			// Token: 0x04000A88 RID: 2696
			public const int Tag = 1;

			// Token: 0x04000A89 RID: 2697
			public const int TagUnknown = 2;

			// Token: 0x04000A8A RID: 2698
			public const int Attribute = 3;

			// Token: 0x04000A8B RID: 2699
			public const int AttributeUnknown = 4;

			// Token: 0x04000A8C RID: 2700
			public const int Number = 5;

			// Token: 0x04000A8D RID: 2701
			public const int DoubleString = 6;

			// Token: 0x04000A8E RID: 2702
			public const int SingleString = 7;

			// Token: 0x04000A8F RID: 2703
			public const int Other = 8;

			// Token: 0x04000A90 RID: 2704
			public const int Comment = 9;

			// Token: 0x04000A91 RID: 2705
			public const int Entity = 10;

			// Token: 0x04000A92 RID: 2706
			public const int TagEnd = 11;

			// Token: 0x04000A93 RID: 2707
			public const int XmlStart = 12;

			// Token: 0x04000A94 RID: 2708
			public const int XmlEnd = 13;

			// Token: 0x04000A95 RID: 2709
			public const int Script = 14;

			// Token: 0x04000A96 RID: 2710
			public const int Asp = 15;

			// Token: 0x04000A97 RID: 2711
			public const int AspAt = 16;

			// Token: 0x04000A98 RID: 2712
			public const int CData = 17;

			// Token: 0x04000A99 RID: 2713
			public const int Question = 18;

			// Token: 0x04000A9A RID: 2714
			public const int Value = 19;

			// Token: 0x04000A9B RID: 2715
			public const int XcComment = 20;
		}
	}
}
