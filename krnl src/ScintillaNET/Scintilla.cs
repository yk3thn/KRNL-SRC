using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using FlagsEnumTypeConverter;

namespace ScintillaNET
{
	// Token: 0x02000042 RID: 66
	[Docking(DockingBehavior.Ask)]
	public class Scintilla : Control
	{
		// Token: 0x0600012F RID: 303 RVA: 0x00007870 File Offset: 0x00005A70
		public void AddRefDocument(Document document)
		{
			IntPtr value = document.Value;
			this.DirectMessage(2376, IntPtr.Zero, value);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00007898 File Offset: 0x00005A98
		public void AddSelection(int caret, int anchor)
		{
			int textLength = this.TextLength;
			caret = Helpers.Clamp(caret, 0, textLength);
			anchor = Helpers.Clamp(anchor, 0, textLength);
			caret = this.Lines.CharToBytePosition(caret);
			anchor = this.Lines.CharToBytePosition(anchor);
			this.DirectMessage(2573, new IntPtr(caret), new IntPtr(anchor));
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000078F4 File Offset: 0x00005AF4
		public unsafe void AddText(string text)
		{
			byte[] bytes = Helpers.GetBytes(text ?? string.Empty, this.Encoding, false);
			fixed (byte* ptr = bytes)
			{
				this.DirectMessage(2001, new IntPtr(bytes.Length), new IntPtr((void*)ptr));
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000794D File Offset: 0x00005B4D
		public void AnnotationClearAll()
		{
			this.DirectMessage(2547);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000795C File Offset: 0x00005B5C
		public unsafe void AppendText(string text)
		{
			byte[] bytes = Helpers.GetBytes(text ?? string.Empty, this.Encoding, false);
			fixed (byte* ptr = bytes)
			{
				this.DirectMessage(2282, new IntPtr(bytes.Length), new IntPtr((void*)ptr));
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000079B8 File Offset: 0x00005BB8
		public void AssignCmdKey(Keys keyDefinition, Command sciCommand)
		{
			int value = Helpers.TranslateKeys(keyDefinition);
			this.DirectMessage(2070, new IntPtr(value), new IntPtr((int)sciCommand));
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000079E4 File Offset: 0x00005BE4
		public void AutoCCancel()
		{
			this.DirectMessage(2101);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000079F2 File Offset: 0x00005BF2
		public void AutoCComplete()
		{
			this.DirectMessage(2104);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00007A00 File Offset: 0x00005C00
		public unsafe void AutoCSelect(string select)
		{
			fixed (byte* bytes = Helpers.GetBytes(select, this.Encoding, true))
			{
				this.DirectMessage(2108, IntPtr.Zero, new IntPtr((void*)bytes));
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00007A4C File Offset: 0x00005C4C
		public unsafe void AutoCSetFillUps(string chars)
		{
			if (chars == null)
			{
				chars = string.Empty;
			}
			if (this.fillUpChars != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.fillUpChars);
				this.fillUpChars = IntPtr.Zero;
			}
			int num = this.Encoding.GetByteCount(chars) + 1;
			IntPtr value = Marshal.AllocHGlobal(num);
			fixed (string text = chars)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				this.Encoding.GetBytes(ptr, chars.Length, (byte*)((void*)value), num);
			}
			((byte*)((void*)value))[num - 1] = 0;
			this.fillUpChars = value;
			this.DirectMessage(2112, IntPtr.Zero, this.fillUpChars);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00007AF8 File Offset: 0x00005CF8
		public unsafe void AutoCShow(int lenEntered, string list)
		{
			if (string.IsNullOrEmpty(list))
			{
				return;
			}
			lenEntered = Helpers.ClampMin(lenEntered, 0);
			if (lenEntered > 0)
			{
				int num = this.DirectMessage(2008).ToInt32();
				int num2 = num;
				for (int i = 0; i < lenEntered; i++)
				{
					num2 = this.DirectMessage(2670, new IntPtr(num2), new IntPtr(-1)).ToInt32();
				}
				lenEntered = num - num2;
			}
			fixed (byte* bytes = Helpers.GetBytes(list, this.Encoding, true))
			{
				this.DirectMessage(2100, new IntPtr(lenEntered), new IntPtr((void*)bytes));
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00007BA8 File Offset: 0x00005DA8
		public unsafe void AutoCStops(string chars)
		{
			fixed (byte* bytes = Helpers.GetBytes(chars ?? string.Empty, Encoding.ASCII, true))
			{
				this.DirectMessage(2105, IntPtr.Zero, new IntPtr((void*)bytes));
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00007BFB File Offset: 0x00005DFB
		public void BeginUndoAction()
		{
			this.DirectMessage(2078);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00007C09 File Offset: 0x00005E09
		public void BraceBadLight(int position)
		{
			position = Helpers.Clamp(position, -1, this.TextLength);
			if (position > 0)
			{
				position = this.Lines.CharToBytePosition(position);
			}
			this.DirectMessage(2352, new IntPtr(position));
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00007C40 File Offset: 0x00005E40
		public void BraceHighlight(int position1, int position2)
		{
			int textLength = this.TextLength;
			position1 = Helpers.Clamp(position1, -1, textLength);
			if (position1 > 0)
			{
				position1 = this.Lines.CharToBytePosition(position1);
			}
			position2 = Helpers.Clamp(position2, -1, textLength);
			if (position2 > 0)
			{
				position2 = this.Lines.CharToBytePosition(position2);
			}
			this.DirectMessage(2351, new IntPtr(position1), new IntPtr(position2));
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00007CA4 File Offset: 0x00005EA4
		public int BraceMatch(int position)
		{
			position = Helpers.Clamp(position, 0, this.TextLength);
			position = this.Lines.CharToBytePosition(position);
			int num = this.DirectMessage(2353, new IntPtr(position), IntPtr.Zero).ToInt32();
			if (num > 0)
			{
				num = this.Lines.ByteToCharPosition(num);
			}
			return num;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00007CFF File Offset: 0x00005EFF
		public void CallTipCancel()
		{
			this.DirectMessage(2201);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00007D10 File Offset: 0x00005F10
		public void CallTipSetForeHlt(Color color)
		{
			int value = ColorTranslator.ToWin32(color);
			this.DirectMessage(2207, new IntPtr(value));
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00007D38 File Offset: 0x00005F38
		public unsafe void CallTipSetHlt(int hlStart, int hlEnd)
		{
			hlStart = Helpers.Clamp(hlStart, 0, this.lastCallTip.Length);
			hlEnd = Helpers.Clamp(hlEnd, 0, this.lastCallTip.Length);
			fixed (string text = this.lastCallTip)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				hlEnd = this.Encoding.GetByteCount(ptr + hlStart, hlEnd - hlStart);
				hlStart = this.Encoding.GetByteCount(ptr, hlStart);
				hlEnd += hlStart;
			}
			this.DirectMessage(2204, new IntPtr(hlStart), new IntPtr(hlEnd));
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00007DC8 File Offset: 0x00005FC8
		public void CallTipSetPosition(bool above)
		{
			IntPtr wParam = above ? new IntPtr(1) : IntPtr.Zero;
			this.DirectMessage(2213, wParam);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00007DF4 File Offset: 0x00005FF4
		public unsafe void CallTipShow(int posStart, string definition)
		{
			posStart = Helpers.Clamp(posStart, 0, this.TextLength);
			if (definition == null)
			{
				return;
			}
			this.lastCallTip = definition;
			posStart = this.Lines.CharToBytePosition(posStart);
			fixed (byte* bytes = Helpers.GetBytes(definition, this.Encoding, true))
			{
				this.DirectMessage(2200, new IntPtr(posStart), new IntPtr((void*)bytes));
			}
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00007E68 File Offset: 0x00006068
		public void CallTipTabSize(int tabSize)
		{
			tabSize = Helpers.ClampMin(tabSize, 0);
			this.DirectMessage(2212, new IntPtr(tabSize));
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00007E88 File Offset: 0x00006088
		public void ChangeLexerState(int startPos, int endPos)
		{
			int textLength = this.TextLength;
			startPos = Helpers.Clamp(startPos, 0, textLength);
			endPos = Helpers.Clamp(endPos, 0, textLength);
			startPos = this.Lines.CharToBytePosition(startPos);
			endPos = this.Lines.CharToBytePosition(endPos);
			this.DirectMessage(2617, new IntPtr(startPos), new IntPtr(endPos));
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00007EE4 File Offset: 0x000060E4
		public int CharPositionFromPoint(int x, int y)
		{
			int pos = this.DirectMessage(2561, new IntPtr(x), new IntPtr(y)).ToInt32();
			return this.Lines.ByteToCharPosition(pos);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00007F20 File Offset: 0x00006120
		public int CharPositionFromPointClose(int x, int y)
		{
			int num = this.DirectMessage(2562, new IntPtr(x), new IntPtr(y)).ToInt32();
			if (num >= 0)
			{
				num = this.Lines.ByteToCharPosition(num);
			}
			return num;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00007F5F File Offset: 0x0000615F
		public void ChooseCaretX()
		{
			this.DirectMessage(2399);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00007F6D File Offset: 0x0000616D
		public void Clear()
		{
			this.DirectMessage(2180);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00007F7B File Offset: 0x0000617B
		public void ClearAll()
		{
			this.DirectMessage(2004);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00007F8C File Offset: 0x0000618C
		public void ClearCmdKey(Keys keyDefinition)
		{
			int value = Helpers.TranslateKeys(keyDefinition);
			this.DirectMessage(2071, new IntPtr(value));
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00007FB2 File Offset: 0x000061B2
		public void ClearAllCmdKeys()
		{
			this.DirectMessage(2072);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00007FC0 File Offset: 0x000061C0
		public void ClearDocumentStyle()
		{
			this.DirectMessage(2005);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00007FCE File Offset: 0x000061CE
		public void ClearRegisteredImages()
		{
			this.DirectMessage(2408);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00007FDC File Offset: 0x000061DC
		public void ClearSelections()
		{
			this.DirectMessage(2571);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00007FEC File Offset: 0x000061EC
		public void Colorize(int startPos, int endPos)
		{
			int textLength = this.TextLength;
			startPos = Helpers.Clamp(startPos, 0, textLength);
			endPos = Helpers.Clamp(endPos, 0, textLength);
			startPos = this.Lines.CharToBytePosition(startPos);
			endPos = this.Lines.CharToBytePosition(endPos);
			this.DirectMessage(4003, new IntPtr(startPos), new IntPtr(endPos));
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00008048 File Offset: 0x00006248
		public void ConvertEols(Eol eolMode)
		{
			this.DirectMessage(2029, new IntPtr((int)eolMode));
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00008069 File Offset: 0x00006269
		public void Copy()
		{
			this.DirectMessage(2178);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00008077 File Offset: 0x00006277
		public void Copy(CopyFormat format)
		{
			Helpers.Copy(this, format, true, false, 0, 0);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00008084 File Offset: 0x00006284
		public void CopyAllowLine()
		{
			this.DirectMessage(2519);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00008092 File Offset: 0x00006292
		public void CopyAllowLine(CopyFormat format)
		{
			Helpers.Copy(this, format, true, true, 0, 0);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000080A0 File Offset: 0x000062A0
		public void CopyRange(int start, int end)
		{
			int textLength = this.TextLength;
			start = Helpers.Clamp(start, 0, textLength);
			end = Helpers.Clamp(end, 0, textLength);
			start = this.Lines.CharToBytePosition(start);
			end = this.Lines.CharToBytePosition(end);
			this.DirectMessage(2419, new IntPtr(start), new IntPtr(end));
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000080FC File Offset: 0x000062FC
		public void CopyRange(int start, int end, CopyFormat format)
		{
			int textLength = this.TextLength;
			start = Helpers.Clamp(start, 0, textLength);
			end = Helpers.Clamp(end, 0, textLength);
			if (start == end)
			{
				return;
			}
			start = this.Lines.CharToBytePosition(start);
			end = this.Lines.CharToBytePosition(end);
			Helpers.Copy(this, format, false, false, start, end);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00008150 File Offset: 0x00006350
		public Document CreateDocument()
		{
			IntPtr value = this.DirectMessage(2375);
			return new Document
			{
				Value = value
			};
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000817C File Offset: 0x0000637C
		public ILoader CreateLoader(int length)
		{
			length = Helpers.ClampMin(length, 0);
			IntPtr intPtr = this.DirectMessage(2632, new IntPtr(length));
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			return new Loader(intPtr, this.Encoding);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000081BF File Offset: 0x000063BF
		public void Cut()
		{
			this.DirectMessage(2177);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000081D0 File Offset: 0x000063D0
		public void DeleteRange(int position, int length)
		{
			int textLength = this.TextLength;
			position = Helpers.Clamp(position, 0, textLength);
			length = Helpers.Clamp(length, 0, textLength - position);
			int num = this.Lines.CharToBytePosition(position);
			int num2 = this.Lines.CharToBytePosition(position + length);
			this.DirectMessage(2645, new IntPtr(num), new IntPtr(num2 - num));
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00008230 File Offset: 0x00006430
		public unsafe string DescribeKeywordSets()
		{
			int num = this.DirectMessage(4017).ToInt32();
			byte[] array = new byte[num + 1];
			fixed (byte* ptr = array)
			{
				this.DirectMessage(4017, IntPtr.Zero, new IntPtr((void*)ptr));
			}
			return Encoding.ASCII.GetString(array, 0, num);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000829C File Offset: 0x0000649C
		public unsafe string DescribeProperty(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return string.Empty;
			}
			fixed (byte* bytes = Helpers.GetBytes(name, Encoding.ASCII, true))
			{
				int num = this.DirectMessage(4016, new IntPtr((void*)bytes), IntPtr.Zero).ToInt32();
				if (num == 0)
				{
					return string.Empty;
				}
				fixed (byte* ptr = new byte[num + 1])
				{
					this.DirectMessage(4016, new IntPtr((void*)bytes), new IntPtr((void*)ptr));
					return Helpers.GetString(new IntPtr((void*)ptr), num, Encoding.ASCII);
				}
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00008353 File Offset: 0x00006553
		internal IntPtr DirectMessage(int msg)
		{
			return this.DirectMessage(msg, IntPtr.Zero, IntPtr.Zero);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00008366 File Offset: 0x00006566
		internal IntPtr DirectMessage(int msg, IntPtr wParam)
		{
			return this.DirectMessage(msg, wParam, IntPtr.Zero);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00008375 File Offset: 0x00006575
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual IntPtr DirectMessage(int msg, IntPtr wParam, IntPtr lParam)
		{
			return Scintilla.DirectMessage(this.SciPointer, msg, wParam, lParam);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00008385 File Offset: 0x00006585
		private static IntPtr DirectMessage(IntPtr sciPtr, int msg, IntPtr wParam, IntPtr lParam)
		{
			return Scintilla.directFunction(sciPtr, msg, wParam, lParam);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00008398 File Offset: 0x00006598
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.reparent)
				{
					this.reparent = false;
					if (base.IsHandleCreated)
					{
						this.DestroyHandle();
					}
				}
				if (this.fillUpChars != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.fillUpChars);
					this.fillUpChars = IntPtr.Zero;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000083F4 File Offset: 0x000065F4
		public int DocLineFromVisible(int displayLine)
		{
			displayLine = Helpers.Clamp(displayLine, 0, this.Lines.Count);
			return this.DirectMessage(2221, new IntPtr(displayLine)).ToInt32();
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000842E File Offset: 0x0000662E
		public void DropSelection(int selection)
		{
			selection = Helpers.ClampMin(selection, 0);
			this.DirectMessage(2671, new IntPtr(selection));
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000844B File Offset: 0x0000664B
		public void EmptyUndoBuffer()
		{
			this.DirectMessage(2175);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00008459 File Offset: 0x00006659
		public void EndUndoAction()
		{
			this.DirectMessage(2079);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00008468 File Offset: 0x00006668
		public void ExecuteCmd(Command sciCommand)
		{
			this.DirectMessage((int)sciCommand);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000847F File Offset: 0x0000667F
		public void FoldAll(FoldAction action)
		{
			this.DirectMessage(2662, new IntPtr((int)action));
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00008493 File Offset: 0x00006693
		public void FoldDisplayTextSetStyle(FoldDisplayText style)
		{
			this.DirectMessage(2701, new IntPtr((int)style));
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000084A8 File Offset: 0x000066A8
		public unsafe int GetCharAt(int position)
		{
			position = Helpers.Clamp(position, 0, this.TextLength);
			position = this.Lines.CharToBytePosition(position);
			int num = this.DirectMessage(2670, new IntPtr(position), new IntPtr(1)).ToInt32();
			int num2 = num - position;
			if (num2 <= 1)
			{
				return this.DirectMessage(2007, new IntPtr(position)).ToInt32();
			}
			fixed (byte* ptr = new byte[num2 + 1])
			{
				NativeMethods.Sci_TextRange* ptr2 = stackalloc NativeMethods.Sci_TextRange[checked(unchecked((UIntPtr)1) * (UIntPtr)sizeof(NativeMethods.Sci_TextRange))];
				ptr2->chrg.cpMin = position;
				ptr2->chrg.cpMax = num;
				ptr2->lpstrText = new IntPtr((void*)ptr);
				this.DirectMessage(2162, IntPtr.Zero, new IntPtr((void*)ptr2));
				return (int)Helpers.GetString(new IntPtr((void*)ptr), num2, this.Encoding)[0];
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000859C File Offset: 0x0000679C
		public int GetColumn(int position)
		{
			position = Helpers.Clamp(position, 0, this.TextLength);
			position = this.Lines.CharToBytePosition(position);
			return this.DirectMessage(2129, new IntPtr(position)).ToInt32();
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000085E0 File Offset: 0x000067E0
		public int GetEndStyled()
		{
			int pos = this.DirectMessage(2028).ToInt32();
			return this.Lines.ByteToCharPosition(pos);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00008610 File Offset: 0x00006810
		private static string GetModulePath()
		{
			if (Scintilla.modulePath == null)
			{
				string path = typeof(Scintilla).Assembly.GetName().Version.ToString(3);
				Scintilla.modulePath = Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.GetTempPath(), "ScintillaNET"), path), (IntPtr.Size == 4) ? "x86" : "x64"), "SciLexer.dll");
				if (!File.Exists(Scintilla.modulePath))
				{
					string text = ((GuidAttribute)typeof(Scintilla).Assembly.GetCustomAttributes(typeof(GuidAttribute), false).GetValue(0)).Value.ToString();
					string name = string.Format(CultureInfo.InvariantCulture, "Global\\{{{0}}}", new object[]
					{
						text
					});
					using (Mutex mutex = new Mutex(false, name))
					{
						MutexAccessRule rule = new MutexAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), MutexRights.FullControl, AccessControlType.Allow);
						MutexSecurity mutexSecurity = new MutexSecurity();
						mutexSecurity.AddAccessRule(rule);
						mutex.SetAccessControl(mutexSecurity);
						bool flag = false;
						try
						{
							try
							{
								flag = mutex.WaitOne(5000, false);
								if (!flag)
								{
									throw new TimeoutException(string.Format(CultureInfo.InvariantCulture, "Timeout waiting for exclusive access to '{0}'.", new object[]
									{
										Scintilla.modulePath
									}));
								}
							}
							catch (AbandonedMutexException)
							{
								flag = true;
							}
							if (!File.Exists(Scintilla.modulePath))
							{
								string directoryName = Path.GetDirectoryName(Scintilla.modulePath);
								if (!Directory.Exists(directoryName))
								{
									Directory.CreateDirectory(directoryName);
								}
								string name2 = string.Format(CultureInfo.InvariantCulture, "ScintillaNET.{0}.SciLexer.dll.gz", new object[]
								{
									(IntPtr.Size == 4) ? "x86" : "x64"
								});
								using (Stream manifestResourceStream = typeof(Scintilla).Assembly.GetManifestResourceStream(name2))
								{
									using (GZipStream gzipStream = new GZipStream(manifestResourceStream, CompressionMode.Decompress))
									{
										using (FileStream fileStream = File.Create(Scintilla.modulePath))
										{
											gzipStream.CopyTo(fileStream);
										}
									}
								}
							}
						}
						finally
						{
							if (flag)
							{
								mutex.ReleaseMutex();
							}
						}
					}
				}
			}
			return Scintilla.modulePath;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000088C0 File Offset: 0x00006AC0
		public unsafe string GetProperty(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return string.Empty;
			}
			fixed (byte* bytes = Helpers.GetBytes(name, Encoding.ASCII, true))
			{
				int num = this.DirectMessage(4008, new IntPtr((void*)bytes)).ToInt32();
				if (num == 0)
				{
					return string.Empty;
				}
				fixed (byte* ptr = new byte[num + 1])
				{
					this.DirectMessage(4008, new IntPtr((void*)bytes), new IntPtr((void*)ptr));
					return Helpers.GetString(new IntPtr((void*)ptr), num, Encoding.ASCII);
				}
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00008974 File Offset: 0x00006B74
		public unsafe string GetPropertyExpanded(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return string.Empty;
			}
			fixed (byte* bytes = Helpers.GetBytes(name, Encoding.ASCII, true))
			{
				int num = this.DirectMessage(4009, new IntPtr((void*)bytes)).ToInt32();
				if (num == 0)
				{
					return string.Empty;
				}
				fixed (byte* ptr = new byte[num + 1])
				{
					this.DirectMessage(4009, new IntPtr((void*)bytes), new IntPtr((void*)ptr));
					return Helpers.GetString(new IntPtr((void*)ptr), num, Encoding.ASCII);
				}
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00008A28 File Offset: 0x00006C28
		public unsafe int GetPropertyInt(string name, int defaultValue)
		{
			if (string.IsNullOrEmpty(name))
			{
				return defaultValue;
			}
			fixed (byte* bytes = Helpers.GetBytes(name, Encoding.ASCII, true))
			{
				return this.DirectMessage(4010, new IntPtr((void*)bytes), new IntPtr(defaultValue)).ToInt32();
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00008A84 File Offset: 0x00006C84
		public int GetStyleAt(int position)
		{
			position = Helpers.Clamp(position, 0, this.TextLength);
			position = this.Lines.CharToBytePosition(position);
			return this.DirectMessage(2010, new IntPtr(position)).ToInt32();
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00008AC8 File Offset: 0x00006CC8
		public unsafe string GetTag(int tagNumber)
		{
			tagNumber = Helpers.Clamp(tagNumber, 1, 9);
			int num = this.DirectMessage(2616, new IntPtr(tagNumber), IntPtr.Zero).ToInt32();
			if (num <= 0)
			{
				return string.Empty;
			}
			fixed (byte* ptr = new byte[num + 1])
			{
				this.DirectMessage(2616, new IntPtr(tagNumber), new IntPtr((void*)ptr));
				return Helpers.GetString(new IntPtr((void*)ptr), num, this.Encoding);
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00008B54 File Offset: 0x00006D54
		public string GetTextRange(int position, int length)
		{
			int textLength = this.TextLength;
			position = Helpers.Clamp(position, 0, textLength);
			length = Helpers.Clamp(length, 0, textLength - position);
			int num = this.Lines.CharToBytePosition(position);
			int num2 = this.Lines.CharToBytePosition(position + length);
			IntPtr intPtr = this.DirectMessage(2643, new IntPtr(num), new IntPtr(num2 - num));
			if (intPtr == IntPtr.Zero)
			{
				return string.Empty;
			}
			return Helpers.GetString(intPtr, num2 - num, this.Encoding);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00008BD8 File Offset: 0x00006DD8
		public string GetTextRangeAsHtml(int position, int length)
		{
			int textLength = this.TextLength;
			position = Helpers.Clamp(position, 0, textLength);
			length = Helpers.Clamp(length, 0, textLength - position);
			int startBytePos = this.Lines.CharToBytePosition(position);
			int endBytePos = this.Lines.CharToBytePosition(position + length);
			return Helpers.GetHtml(this, startBytePos, endBytePos);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00008C26 File Offset: 0x00006E26
		public FileVersionInfo GetVersionInfo()
		{
			return FileVersionInfo.GetVersionInfo(Scintilla.GetModulePath());
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00008C34 File Offset: 0x00006E34
		public string GetWordFromPosition(int position)
		{
			int num = this.WordStartPosition(position, true);
			int num2 = this.WordEndPosition(position, true);
			return this.GetTextRange(num, num2 - num);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00008C5D File Offset: 0x00006E5D
		public void GotoPosition(int position)
		{
			position = Helpers.Clamp(position, 0, this.TextLength);
			position = this.Lines.CharToBytePosition(position);
			this.DirectMessage(2025, new IntPtr(position));
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00008C90 File Offset: 0x00006E90
		public void HideLines(int lineStart, int lineEnd)
		{
			lineStart = Helpers.Clamp(lineStart, 0, this.Lines.Count);
			lineEnd = Helpers.Clamp(lineEnd, lineStart, this.Lines.Count);
			this.DirectMessage(2227, new IntPtr(lineStart), new IntPtr(lineEnd));
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00008CE0 File Offset: 0x00006EE0
		public uint IndicatorAllOnFor(int position)
		{
			position = Helpers.Clamp(position, 0, this.TextLength);
			position = this.Lines.CharToBytePosition(position);
			return (uint)this.DirectMessage(2506, new IntPtr(position)).ToInt32();
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00008D24 File Offset: 0x00006F24
		public void IndicatorClearRange(int position, int length)
		{
			int textLength = this.TextLength;
			position = Helpers.Clamp(position, 0, textLength);
			length = Helpers.Clamp(length, 0, textLength - position);
			int num = this.Lines.CharToBytePosition(position);
			int num2 = this.Lines.CharToBytePosition(position + length);
			this.DirectMessage(2505, new IntPtr(num), new IntPtr(num2 - num));
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00008D84 File Offset: 0x00006F84
		public void IndicatorFillRange(int position, int length)
		{
			int textLength = this.TextLength;
			position = Helpers.Clamp(position, 0, textLength);
			length = Helpers.Clamp(length, 0, textLength - position);
			int num = this.Lines.CharToBytePosition(position);
			int num2 = this.Lines.CharToBytePosition(position + length);
			this.DirectMessage(2504, new IntPtr(num), new IntPtr(num2 - num));
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00008DE4 File Offset: 0x00006FE4
		private void InitDocument(Eol eolMode = Eol.CrLf, bool useTabs = false, int tabWidth = 4, int indentWidth = 0)
		{
			this.DirectMessage(2037, new IntPtr(65001));
			this.DirectMessage(2012, new IntPtr(1));
			this.DirectMessage(2031, new IntPtr((int)eolMode));
			this.DirectMessage(2124, useTabs ? new IntPtr(1) : IntPtr.Zero);
			this.DirectMessage(2036, new IntPtr(tabWidth));
			this.DirectMessage(2122, new IntPtr(indentWidth));
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00008E6C File Offset: 0x0000706C
		public unsafe void InsertText(int position, string text)
		{
			if (position < -1)
			{
				throw new ArgumentOutOfRangeException("position", "Position must be greater or equal to zero, or -1.");
			}
			if (position != -1)
			{
				int textLength = this.TextLength;
				if (position > textLength)
				{
					throw new ArgumentOutOfRangeException("position", "Position cannot exceed document length.");
				}
				position = this.Lines.CharToBytePosition(position);
			}
			fixed (byte* bytes = Helpers.GetBytes(text ?? string.Empty, this.Encoding, true))
			{
				this.DirectMessage(2003, new IntPtr(position), new IntPtr((void*)bytes));
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00008F04 File Offset: 0x00007104
		public bool IsRangeWord(int start, int end)
		{
			int textLength = this.TextLength;
			start = Helpers.Clamp(start, 0, textLength);
			end = Helpers.Clamp(end, 0, textLength);
			start = this.Lines.CharToBytePosition(start);
			end = this.Lines.CharToBytePosition(end);
			return this.DirectMessage(2691, new IntPtr(start), new IntPtr(end)) != IntPtr.Zero;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00008F69 File Offset: 0x00007169
		public int LineFromPosition(int position)
		{
			position = Helpers.Clamp(position, 0, this.TextLength);
			return this.Lines.LineFromCharPosition(position);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00008F86 File Offset: 0x00007186
		public void LineScroll(int lines, int columns)
		{
			this.DirectMessage(2168, new IntPtr(columns), new IntPtr(lines));
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00008FA0 File Offset: 0x000071A0
		public unsafe void LoadLexerLibrary(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return;
			}
			fixed (byte* bytes = Helpers.GetBytes(path, Encoding.Default, true))
			{
				this.DirectMessage(4007, IntPtr.Zero, new IntPtr((void*)bytes));
			}
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00008FF3 File Offset: 0x000071F3
		public void MarkerDeleteAll(int marker)
		{
			marker = Helpers.Clamp(marker, -1, this.Markers.Count - 1);
			this.DirectMessage(2045, new IntPtr(marker));
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000901D File Offset: 0x0000721D
		public void MarkerDeleteHandle(MarkerHandle markerHandle)
		{
			this.DirectMessage(2018, markerHandle.Value);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00009034 File Offset: 0x00007234
		public void MarkerEnableHighlight(bool enabled)
		{
			IntPtr wParam = enabled ? new IntPtr(1) : IntPtr.Zero;
			this.DirectMessage(2293, wParam);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00009060 File Offset: 0x00007260
		public int MarkerLineFromHandle(MarkerHandle markerHandle)
		{
			return this.DirectMessage(2017, markerHandle.Value).ToInt32();
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00009088 File Offset: 0x00007288
		public void MultiEdgeAddLine(int column, Color edgeColor)
		{
			column = Helpers.ClampMin(column, 0);
			int value = ColorTranslator.ToWin32(edgeColor);
			this.DirectMessage(2694, new IntPtr(column), new IntPtr(value));
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000090BD File Offset: 0x000072BD
		public void MultiEdgeClearAll()
		{
			this.DirectMessage(2695);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000090CB File Offset: 0x000072CB
		public void MultipleSelectAddEach()
		{
			this.DirectMessage(2689);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000090D9 File Offset: 0x000072D9
		public void MultipleSelectAddNext()
		{
			this.DirectMessage(2688);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000090E8 File Offset: 0x000072E8
		protected virtual void OnAutoCCancelled(EventArgs e)
		{
			EventHandler<EventArgs> eventHandler = base.Events[Scintilla.autoCCancelledEventKey] as EventHandler<EventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00009118 File Offset: 0x00007318
		protected virtual void OnAutoCCharDeleted(EventArgs e)
		{
			EventHandler<EventArgs> eventHandler = base.Events[Scintilla.autoCCharDeletedEventKey] as EventHandler<EventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00009148 File Offset: 0x00007348
		protected virtual void OnAutoCCompleted(AutoCSelectionEventArgs e)
		{
			EventHandler<AutoCSelectionEventArgs> eventHandler = base.Events[Scintilla.autoCCompletedEventKey] as EventHandler<AutoCSelectionEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00009178 File Offset: 0x00007378
		protected virtual void OnAutoCSelection(AutoCSelectionEventArgs e)
		{
			EventHandler<AutoCSelectionEventArgs> eventHandler = base.Events[Scintilla.autoCSelectionEventKey] as EventHandler<AutoCSelectionEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000091A8 File Offset: 0x000073A8
		protected virtual void OnBeforeDelete(BeforeModificationEventArgs e)
		{
			EventHandler<BeforeModificationEventArgs> eventHandler = base.Events[Scintilla.beforeDeleteEventKey] as EventHandler<BeforeModificationEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000091D8 File Offset: 0x000073D8
		protected virtual void OnBeforeInsert(BeforeModificationEventArgs e)
		{
			EventHandler<BeforeModificationEventArgs> eventHandler = base.Events[Scintilla.beforeInsertEventKey] as EventHandler<BeforeModificationEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00009208 File Offset: 0x00007408
		protected virtual void OnBorderStyleChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[Scintilla.borderStyleChangedEventKey] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00009238 File Offset: 0x00007438
		protected virtual void OnChangeAnnotation(ChangeAnnotationEventArgs e)
		{
			EventHandler<ChangeAnnotationEventArgs> eventHandler = base.Events[Scintilla.changeAnnotationEventKey] as EventHandler<ChangeAnnotationEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00009268 File Offset: 0x00007468
		protected virtual void OnCharAdded(CharAddedEventArgs e)
		{
			EventHandler<CharAddedEventArgs> eventHandler = base.Events[Scintilla.charAddedEventKey] as EventHandler<CharAddedEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00009298 File Offset: 0x00007498
		protected virtual void OnDelete(ModificationEventArgs e)
		{
			EventHandler<ModificationEventArgs> eventHandler = base.Events[Scintilla.deleteEventKey] as EventHandler<ModificationEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000092C8 File Offset: 0x000074C8
		protected virtual void OnDoubleClick(DoubleClickEventArgs e)
		{
			EventHandler<DoubleClickEventArgs> eventHandler = base.Events[Scintilla.doubleClickEventKey] as EventHandler<DoubleClickEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000092F8 File Offset: 0x000074F8
		protected virtual void OnDwellEnd(DwellEventArgs e)
		{
			EventHandler<DwellEventArgs> eventHandler = base.Events[Scintilla.dwellEndEventKey] as EventHandler<DwellEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00009328 File Offset: 0x00007528
		protected virtual void OnDwellStart(DwellEventArgs e)
		{
			EventHandler<DwellEventArgs> eventHandler = base.Events[Scintilla.dwellStartEventKey] as EventHandler<DwellEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00009358 File Offset: 0x00007558
		protected unsafe override void OnHandleCreated(EventArgs e)
		{
			this.InitDocument(Eol.CrLf, false, 4, 0);
			this.DirectMessage(2516, new IntPtr(1));
			this.DirectMessage(2212, new IntPtr(16));
			fixed (byte* bytes = Helpers.GetBytes("abcdefghijklmnopqrstuvwxyz_ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", Encoding.ASCII, true))
			{
				this.DirectMessage(2077, IntPtr.Zero, new IntPtr((void*)bytes));
			}
			NativeMethods.RevokeDragDrop(base.Handle);
			base.OnHandleCreated(e);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000093E8 File Offset: 0x000075E8
		protected virtual void OnHotspotClick(HotspotClickEventArgs e)
		{
			EventHandler<HotspotClickEventArgs> eventHandler = base.Events[Scintilla.hotspotClickEventKey] as EventHandler<HotspotClickEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00009418 File Offset: 0x00007618
		protected virtual void OnHotspotDoubleClick(HotspotClickEventArgs e)
		{
			EventHandler<HotspotClickEventArgs> eventHandler = base.Events[Scintilla.hotspotDoubleClickEventKey] as EventHandler<HotspotClickEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00009448 File Offset: 0x00007648
		protected virtual void OnHotspotReleaseClick(HotspotClickEventArgs e)
		{
			EventHandler<HotspotClickEventArgs> eventHandler = base.Events[Scintilla.hotspotReleaseClickEventKey] as EventHandler<HotspotClickEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00009478 File Offset: 0x00007678
		protected virtual void OnIndicatorClick(IndicatorClickEventArgs e)
		{
			EventHandler<IndicatorClickEventArgs> eventHandler = base.Events[Scintilla.indicatorClickEventKey] as EventHandler<IndicatorClickEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000094A8 File Offset: 0x000076A8
		protected virtual void OnIndicatorRelease(IndicatorReleaseEventArgs e)
		{
			EventHandler<IndicatorReleaseEventArgs> eventHandler = base.Events[Scintilla.indicatorReleaseEventKey] as EventHandler<IndicatorReleaseEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000094D8 File Offset: 0x000076D8
		protected virtual void OnInsert(ModificationEventArgs e)
		{
			EventHandler<ModificationEventArgs> eventHandler = base.Events[Scintilla.insertEventKey] as EventHandler<ModificationEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00009508 File Offset: 0x00007708
		protected virtual void OnInsertCheck(InsertCheckEventArgs e)
		{
			EventHandler<InsertCheckEventArgs> eventHandler = base.Events[Scintilla.insertCheckEventKey] as EventHandler<InsertCheckEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00009538 File Offset: 0x00007738
		protected virtual void OnMarginClick(MarginClickEventArgs e)
		{
			EventHandler<MarginClickEventArgs> eventHandler = base.Events[Scintilla.marginClickEventKey] as EventHandler<MarginClickEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00009568 File Offset: 0x00007768
		protected virtual void OnMarginRightClick(MarginClickEventArgs e)
		{
			EventHandler<MarginClickEventArgs> eventHandler = base.Events[Scintilla.marginRightClickEventKey] as EventHandler<MarginClickEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00009598 File Offset: 0x00007798
		protected virtual void OnModifyAttempt(EventArgs e)
		{
			EventHandler<EventArgs> eventHandler = base.Events[Scintilla.modifyAttemptEventKey] as EventHandler<EventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000095C8 File Offset: 0x000077C8
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (!this.doubleClick)
			{
				this.OnClick(e);
				this.OnMouseClick(e);
			}
			else
			{
				MouseEventArgs e2 = new MouseEventArgs(e.Button, 2, e.X, e.Y, e.Delta);
				this.OnDoubleClick(e2);
				this.OnMouseDoubleClick(e2);
				this.doubleClick = false;
			}
			base.OnMouseUp(e);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00009628 File Offset: 0x00007828
		protected virtual void OnNeedShown(NeedShownEventArgs e)
		{
			EventHandler<NeedShownEventArgs> eventHandler = base.Events[Scintilla.needShownEventKey] as EventHandler<NeedShownEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00009658 File Offset: 0x00007858
		protected virtual void OnPainted(EventArgs e)
		{
			EventHandler<EventArgs> eventHandler = base.Events[Scintilla.paintedEventKey] as EventHandler<EventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00009688 File Offset: 0x00007888
		protected virtual void OnSavePointLeft(EventArgs e)
		{
			EventHandler<EventArgs> eventHandler = base.Events[Scintilla.savePointLeftEventKey] as EventHandler<EventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000096B8 File Offset: 0x000078B8
		protected virtual void OnSavePointReached(EventArgs e)
		{
			EventHandler<EventArgs> eventHandler = base.Events[Scintilla.savePointReachedEventKey] as EventHandler<EventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000096E8 File Offset: 0x000078E8
		protected virtual void OnStyleNeeded(StyleNeededEventArgs e)
		{
			EventHandler<StyleNeededEventArgs> eventHandler = base.Events[Scintilla.styleNeededEventKey] as EventHandler<StyleNeededEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00009718 File Offset: 0x00007918
		protected virtual void OnUpdateUI(UpdateUIEventArgs e)
		{
			EventHandler<UpdateUIEventArgs> eventHandler = base.Events[Scintilla.updateUIEventKey] as EventHandler<UpdateUIEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00009748 File Offset: 0x00007948
		protected virtual void OnZoomChanged(EventArgs e)
		{
			EventHandler<EventArgs> eventHandler = base.Events[Scintilla.zoomChangedEventKey] as EventHandler<EventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00009776 File Offset: 0x00007976
		public void Paste()
		{
			this.DirectMessage(2179);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00009784 File Offset: 0x00007984
		public int PointXFromPosition(int pos)
		{
			pos = Helpers.Clamp(pos, 0, this.TextLength);
			pos = this.Lines.CharToBytePosition(pos);
			return this.DirectMessage(2164, IntPtr.Zero, new IntPtr(pos)).ToInt32();
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000097CC File Offset: 0x000079CC
		public int PointYFromPosition(int pos)
		{
			pos = Helpers.Clamp(pos, 0, this.TextLength);
			pos = this.Lines.CharToBytePosition(pos);
			return this.DirectMessage(2165, IntPtr.Zero, new IntPtr(pos)).ToInt32();
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00009814 File Offset: 0x00007A14
		public unsafe string PropertyNames()
		{
			int num = this.DirectMessage(4014).ToInt32();
			if (num == 0)
			{
				return string.Empty;
			}
			fixed (byte* ptr = new byte[num + 1])
			{
				this.DirectMessage(4014, IntPtr.Zero, new IntPtr((void*)ptr));
				return Helpers.GetString(new IntPtr((void*)ptr), num, Encoding.ASCII);
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00009888 File Offset: 0x00007A88
		public unsafe PropertyType PropertyType(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return ScintillaNET.PropertyType.Boolean;
			}
			fixed (byte* bytes = Helpers.GetBytes(name, Encoding.ASCII, true))
			{
				return (PropertyType)((int)this.DirectMessage(4015, new IntPtr((void*)bytes)));
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000098D8 File Offset: 0x00007AD8
		public void Redo()
		{
			this.DirectMessage(2011);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000098E8 File Offset: 0x00007AE8
		public unsafe void RegisterRgbaImage(int type, Bitmap image)
		{
			if (image == null)
			{
				return;
			}
			this.DirectMessage(2624, new IntPtr(image.Width));
			this.DirectMessage(2625, new IntPtr(image.Height));
			fixed (byte* ptr = Helpers.BitmapToArgb(image))
			{
				this.DirectMessage(2627, new IntPtr(type), new IntPtr((void*)ptr));
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00009960 File Offset: 0x00007B60
		public void ReleaseDocument(Document document)
		{
			IntPtr value = document.Value;
			this.DirectMessage(2377, IntPtr.Zero, value);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00009988 File Offset: 0x00007B88
		public unsafe void ReplaceSelection(string text)
		{
			fixed (byte* bytes = Helpers.GetBytes(text ?? string.Empty, this.Encoding, true))
			{
				this.DirectMessage(2170, IntPtr.Zero, new IntPtr((void*)bytes));
			}
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000099DC File Offset: 0x00007BDC
		public unsafe int ReplaceTarget(string text)
		{
			if (text == null)
			{
				text = string.Empty;
			}
			byte[] bytes = Helpers.GetBytes(text, this.Encoding, false);
			fixed (byte* ptr = bytes)
			{
				this.DirectMessage(2194, new IntPtr(bytes.Length), new IntPtr((void*)ptr));
			}
			return text.Length;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00009A3C File Offset: 0x00007C3C
		public unsafe int ReplaceTargetRe(string text)
		{
			byte[] bytes = Helpers.GetBytes(text ?? string.Empty, this.Encoding, false);
			fixed (byte* ptr = bytes)
			{
				this.DirectMessage(2195, new IntPtr(bytes.Length), new IntPtr((void*)ptr));
			}
			return Math.Abs(this.TargetEnd - this.TargetStart);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00009AA7 File Offset: 0x00007CA7
		private void ResetAdditionalCaretForeColor()
		{
			this.AdditionalCaretForeColor = Color.FromArgb(127, 127, 127);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00009ABA File Offset: 0x00007CBA
		public void RotateSelection()
		{
			this.DirectMessage(2606);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00009AC8 File Offset: 0x00007CC8
		private void ScnDoubleClick(ref NativeMethods.SCNotification scn)
		{
			Keys modifiers = (Keys)(-65536 & scn.modifiers << 16);
			DoubleClickEventArgs e = new DoubleClickEventArgs(this, modifiers, scn.position, scn.line);
			this.OnDoubleClick(e);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00009B00 File Offset: 0x00007D00
		private void ScnHotspotClick(ref NativeMethods.SCNotification scn)
		{
			Keys modifiers = (Keys)(-65536 & scn.modifiers << 16);
			HotspotClickEventArgs e = new HotspotClickEventArgs(this, modifiers, scn.position);
			int code = scn.nmhdr.code;
			if (code == 2019)
			{
				this.OnHotspotClick(e);
				return;
			}
			if (code == 2020)
			{
				this.OnHotspotDoubleClick(e);
				return;
			}
			if (code != 2027)
			{
				return;
			}
			this.OnHotspotReleaseClick(e);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00009B68 File Offset: 0x00007D68
		private void ScnIndicatorClick(ref NativeMethods.SCNotification scn)
		{
			int code = scn.nmhdr.code;
			if (code == 2023)
			{
				Keys modifiers = (Keys)(-65536 & scn.modifiers << 16);
				this.OnIndicatorClick(new IndicatorClickEventArgs(this, modifiers, scn.position));
				return;
			}
			if (code != 2024)
			{
				return;
			}
			this.OnIndicatorRelease(new IndicatorReleaseEventArgs(this, scn.position));
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00009BC8 File Offset: 0x00007DC8
		private void ScnMarginClick(ref NativeMethods.SCNotification scn)
		{
			Keys modifiers = (Keys)(-65536 & scn.modifiers << 16);
			MarginClickEventArgs e = new MarginClickEventArgs(this, modifiers, scn.position, scn.margin);
			if (scn.nmhdr.code == 2010)
			{
				this.OnMarginClick(e);
				return;
			}
			this.OnMarginRightClick(e);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00009C1C File Offset: 0x00007E1C
		private void ScnModified(ref NativeMethods.SCNotification scn)
		{
			if ((scn.modificationType & 1048576) > 0)
			{
				InsertCheckEventArgs insertCheckEventArgs = new InsertCheckEventArgs(this, scn.position, scn.length, scn.text);
				this.OnInsertCheck(insertCheckEventArgs);
				this.cachedPosition = insertCheckEventArgs.CachedPosition;
				this.cachedText = insertCheckEventArgs.CachedText;
			}
			if ((scn.modificationType & 3072) > 0)
			{
				ModificationSource source = (ModificationSource)(scn.modificationType & 112);
				BeforeModificationEventArgs beforeModificationEventArgs = new BeforeModificationEventArgs(this, source, scn.position, scn.length, scn.text);
				beforeModificationEventArgs.CachedPosition = this.cachedPosition;
				beforeModificationEventArgs.CachedText = this.cachedText;
				if ((scn.modificationType & 1024) > 0)
				{
					this.OnBeforeInsert(beforeModificationEventArgs);
				}
				else
				{
					this.OnBeforeDelete(beforeModificationEventArgs);
				}
				this.cachedPosition = beforeModificationEventArgs.CachedPosition;
				this.cachedText = beforeModificationEventArgs.CachedText;
			}
			if ((scn.modificationType & 3) > 0)
			{
				ModificationSource source2 = (ModificationSource)(scn.modificationType & 112);
				ModificationEventArgs modificationEventArgs = new ModificationEventArgs(this, source2, scn.position, scn.length, scn.text, scn.linesAdded);
				modificationEventArgs.CachedPosition = this.cachedPosition;
				modificationEventArgs.CachedText = this.cachedText;
				if ((scn.modificationType & 1) > 0)
				{
					this.OnInsert(modificationEventArgs);
				}
				else
				{
					this.OnDelete(modificationEventArgs);
				}
				this.cachedPosition = null;
				this.cachedText = null;
				this.OnTextChanged(EventArgs.Empty);
			}
			if ((scn.modificationType & 131072) > 0)
			{
				ChangeAnnotationEventArgs e = new ChangeAnnotationEventArgs(scn.line);
				this.OnChangeAnnotation(e);
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00009DA4 File Offset: 0x00007FA4
		public void ScrollCaret()
		{
			this.DirectMessage(2169);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00009DB4 File Offset: 0x00007FB4
		public void ScrollRange(int start, int end)
		{
			int textLength = this.TextLength;
			start = Helpers.Clamp(start, 0, textLength);
			end = Helpers.Clamp(end, 0, textLength);
			start = this.Lines.CharToBytePosition(start);
			end = this.Lines.CharToBytePosition(end);
			this.DirectMessage(2569, new IntPtr(start), new IntPtr(end));
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00009E10 File Offset: 0x00008010
		public unsafe int SearchInTarget(string text)
		{
			byte[] bytes = Helpers.GetBytes(text ?? string.Empty, this.Encoding, false);
			int num;
			fixed (byte* ptr = bytes)
			{
				num = this.DirectMessage(2197, new IntPtr(bytes.Length), new IntPtr((void*)ptr)).ToInt32();
			}
			if (num == -1)
			{
				return num;
			}
			return this.Lines.ByteToCharPosition(num);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00009E86 File Offset: 0x00008086
		public void SelectAll()
		{
			this.DirectMessage(2013);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00009E94 File Offset: 0x00008094
		public void SetAdditionalSelBack(Color color)
		{
			int value = ColorTranslator.ToWin32(color);
			this.DirectMessage(2601, new IntPtr(value));
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00009EBC File Offset: 0x000080BC
		public void SetAdditionalSelFore(Color color)
		{
			int value = ColorTranslator.ToWin32(color);
			this.DirectMessage(2600, new IntPtr(value));
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00009EE2 File Offset: 0x000080E2
		public void SetEmptySelection(int pos)
		{
			pos = Helpers.Clamp(pos, 0, this.TextLength);
			pos = this.Lines.CharToBytePosition(pos);
			this.DirectMessage(2556, new IntPtr(pos));
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00009F13 File Offset: 0x00008113
		public void SetFoldFlags(FoldFlags flags)
		{
			this.DirectMessage(2233, new IntPtr((int)flags));
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00009F28 File Offset: 0x00008128
		public void SetFoldMarginColor(bool use, Color color)
		{
			int value = ColorTranslator.ToWin32(color);
			IntPtr wParam = use ? new IntPtr(1) : IntPtr.Zero;
			this.DirectMessage(2290, wParam, new IntPtr(value));
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00009F60 File Offset: 0x00008160
		public void SetFoldMarginHighlightColor(bool use, Color color)
		{
			int value = ColorTranslator.ToWin32(color);
			IntPtr wParam = use ? new IntPtr(1) : IntPtr.Zero;
			this.DirectMessage(2291, wParam, new IntPtr(value));
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00009F98 File Offset: 0x00008198
		public unsafe void SetKeywords(int set, string keywords)
		{
			set = Helpers.Clamp(set, 0, 8);
			fixed (byte* bytes = Helpers.GetBytes(keywords ?? string.Empty, Encoding.ASCII, true))
			{
				this.DirectMessage(4005, new IntPtr(set), new IntPtr((void*)bytes));
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00009FF6 File Offset: 0x000081F6
		public static void SetDestroyHandleBehavior(bool reparent)
		{
			if (Scintilla.reparentAll == null)
			{
				Scintilla.reparentAll = new bool?(reparent);
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000A00F File Offset: 0x0000820F
		public static void SetModulePath(string modulePath)
		{
			if (Scintilla.modulePath == null)
			{
				Scintilla.modulePath = modulePath;
			}
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000A020 File Offset: 0x00008220
		public unsafe void SetProperty(string name, string value)
		{
			if (string.IsNullOrEmpty(name))
			{
				return;
			}
			byte[] bytes = Helpers.GetBytes(name, Encoding.ASCII, true);
			byte[] bytes2 = Helpers.GetBytes(value ?? string.Empty, Encoding.ASCII, true);
			byte[] array = bytes;
			byte* ptr;
			if (bytes == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				fixed (byte* ptr = &array[0])
				{
				}
			}
			fixed (byte* ptr2 = bytes2)
			{
				this.DirectMessage(4004, new IntPtr((void*)ptr), new IntPtr((void*)ptr2));
			}
			ptr = null;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000A0A8 File Offset: 0x000082A8
		public void SetSavePoint()
		{
			this.DirectMessage(2014);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000A0B8 File Offset: 0x000082B8
		public void SetSel(int anchorPos, int currentPos)
		{
			if (anchorPos == currentPos)
			{
				anchorPos = -1;
			}
			int textLength = this.TextLength;
			if (anchorPos >= 0)
			{
				anchorPos = Helpers.Clamp(anchorPos, 0, textLength);
				anchorPos = this.Lines.CharToBytePosition(anchorPos);
			}
			if (currentPos >= 0)
			{
				currentPos = Helpers.Clamp(currentPos, 0, textLength);
				currentPos = this.Lines.CharToBytePosition(currentPos);
			}
			this.DirectMessage(2160, new IntPtr(anchorPos), new IntPtr(currentPos));
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000A124 File Offset: 0x00008324
		public void SetSelection(int caret, int anchor)
		{
			int textLength = this.TextLength;
			caret = Helpers.Clamp(caret, 0, textLength);
			anchor = Helpers.Clamp(anchor, 0, textLength);
			caret = this.Lines.CharToBytePosition(caret);
			anchor = this.Lines.CharToBytePosition(anchor);
			this.DirectMessage(2572, new IntPtr(caret), new IntPtr(anchor));
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000A180 File Offset: 0x00008380
		public void SetSelectionBackColor(bool use, Color color)
		{
			int value = ColorTranslator.ToWin32(color);
			IntPtr wParam = use ? new IntPtr(1) : IntPtr.Zero;
			this.DirectMessage(2068, wParam, new IntPtr(value));
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000A1B8 File Offset: 0x000083B8
		public void SetSelectionForeColor(bool use, Color color)
		{
			int value = ColorTranslator.ToWin32(color);
			IntPtr wParam = use ? new IntPtr(1) : IntPtr.Zero;
			this.DirectMessage(2067, wParam, new IntPtr(value));
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000A1F0 File Offset: 0x000083F0
		public void SetStyling(int length, int style)
		{
			int textLength = this.TextLength;
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", "Length cannot be less than zero.");
			}
			if (this.stylingPosition + length > textLength)
			{
				throw new ArgumentOutOfRangeException("length", "Position and length must refer to a range within the document.");
			}
			if (style < 0 || style >= this.Styles.Count)
			{
				throw new ArgumentOutOfRangeException("style", "Style must be non-negative and less than the size of the collection.");
			}
			int pos = this.stylingPosition + length;
			int num = this.Lines.CharToBytePosition(pos);
			this.DirectMessage(2033, new IntPtr(num - this.stylingBytePosition), new IntPtr(style));
			this.stylingPosition = pos;
			this.stylingBytePosition = num;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000A298 File Offset: 0x00008498
		public void SetTargetRange(int start, int end)
		{
			int textLength = this.TextLength;
			start = Helpers.Clamp(start, 0, textLength);
			end = Helpers.Clamp(end, 0, textLength);
			start = this.Lines.CharToBytePosition(start);
			end = this.Lines.CharToBytePosition(end);
			this.DirectMessage(2686, new IntPtr(start), new IntPtr(end));
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000A2F4 File Offset: 0x000084F4
		public void SetWhitespaceBackColor(bool use, Color color)
		{
			int value = ColorTranslator.ToWin32(color);
			IntPtr wParam = use ? new IntPtr(1) : IntPtr.Zero;
			this.DirectMessage(2085, wParam, new IntPtr(value));
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000A32C File Offset: 0x0000852C
		public void SetWhitespaceForeColor(bool use, Color color)
		{
			int value = ColorTranslator.ToWin32(color);
			IntPtr wParam = use ? new IntPtr(1) : IntPtr.Zero;
			this.DirectMessage(2084, wParam, new IntPtr(value));
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000A364 File Offset: 0x00008564
		private bool ShouldSerializeAdditionalCaretForeColor()
		{
			return this.AdditionalCaretForeColor != Color.FromArgb(127, 127, 127);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000A37C File Offset: 0x0000857C
		public void ShowLines(int lineStart, int lineEnd)
		{
			lineStart = Helpers.Clamp(lineStart, 0, this.Lines.Count);
			lineEnd = Helpers.Clamp(lineEnd, lineStart, this.Lines.Count);
			this.DirectMessage(2226, new IntPtr(lineStart), new IntPtr(lineEnd));
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000A3CC File Offset: 0x000085CC
		public void StartStyling(int position)
		{
			position = Helpers.Clamp(position, 0, this.TextLength);
			int value = this.Lines.CharToBytePosition(position);
			this.DirectMessage(2032, new IntPtr(value));
			this.stylingPosition = position;
			this.stylingBytePosition = value;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000A415 File Offset: 0x00008615
		public void StyleClearAll()
		{
			this.DirectMessage(2050);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000A423 File Offset: 0x00008623
		public void StyleResetDefault()
		{
			this.DirectMessage(2058);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000A431 File Offset: 0x00008631
		public void SwapMainAnchorCaret()
		{
			this.DirectMessage(2607);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000A43F File Offset: 0x0000863F
		public void TargetFromSelection()
		{
			this.DirectMessage(2287);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000A44D File Offset: 0x0000864D
		public void TargetWholeDocument()
		{
			this.DirectMessage(2690);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000A45C File Offset: 0x0000865C
		public unsafe int TextWidth(int style, string text)
		{
			style = Helpers.Clamp(style, 0, this.Styles.Count - 1);
			fixed (byte* bytes = Helpers.GetBytes(text ?? string.Empty, this.Encoding, true))
			{
				return this.DirectMessage(2276, new IntPtr(style), new IntPtr((void*)bytes)).ToInt32();
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000A4CB File Offset: 0x000086CB
		public void Undo()
		{
			this.DirectMessage(2176);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000A4DC File Offset: 0x000086DC
		public void UsePopup(bool enablePopup)
		{
			IntPtr wParam = enablePopup ? new IntPtr(1) : IntPtr.Zero;
			this.DirectMessage(2371, wParam);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000A507 File Offset: 0x00008707
		public void UsePopup(PopupMode popupMode)
		{
			this.DirectMessage(2371, new IntPtr((int)popupMode));
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000A51B File Offset: 0x0000871B
		private void WmDestroy(ref Message m)
		{
			if (this.reparent && base.IsHandleCreated)
			{
				NativeMethods.SetParent(base.Handle, new IntPtr(-3));
				m.Result = IntPtr.Zero;
				return;
			}
			base.WndProc(ref m);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000A554 File Offset: 0x00008754
		private void WmReflectNotify(ref Message m)
		{
			NativeMethods.SCNotification scnotification = (NativeMethods.SCNotification)Marshal.PtrToStructure(m.LParam, typeof(NativeMethods.SCNotification));
			if (scnotification.nmhdr.code >= 2000 && scnotification.nmhdr.code <= 2030)
			{
				EventHandler<SCNotificationEventArgs> eventHandler = base.Events[Scintilla.scNotificationEventKey] as EventHandler<SCNotificationEventArgs>;
				if (eventHandler != null)
				{
					eventHandler(this, new SCNotificationEventArgs(scnotification));
				}
				switch (scnotification.nmhdr.code)
				{
				case 2000:
					this.OnStyleNeeded(new StyleNeededEventArgs(this, scnotification.position));
					return;
				case 2001:
					this.OnCharAdded(new CharAddedEventArgs(scnotification.ch));
					return;
				case 2002:
					this.OnSavePointReached(EventArgs.Empty);
					return;
				case 2003:
					this.OnSavePointLeft(EventArgs.Empty);
					return;
				case 2004:
					this.OnModifyAttempt(EventArgs.Empty);
					return;
				case 2006:
					this.ScnDoubleClick(ref scnotification);
					return;
				case 2007:
					this.OnUpdateUI(new UpdateUIEventArgs((UpdateChange)scnotification.updated));
					return;
				case 2008:
					this.ScnModified(ref scnotification);
					return;
				case 2010:
				case 2031:
					this.ScnMarginClick(ref scnotification);
					return;
				case 2011:
					this.OnNeedShown(new NeedShownEventArgs(this, scnotification.position, scnotification.length));
					return;
				case 2013:
					this.OnPainted(EventArgs.Empty);
					return;
				case 2016:
					this.OnDwellStart(new DwellEventArgs(this, scnotification.position, scnotification.x, scnotification.y));
					return;
				case 2017:
					this.OnDwellEnd(new DwellEventArgs(this, scnotification.position, scnotification.x, scnotification.y));
					return;
				case 2018:
					this.OnZoomChanged(EventArgs.Empty);
					return;
				case 2019:
				case 2020:
				case 2027:
					this.ScnHotspotClick(ref scnotification);
					return;
				case 2022:
					this.OnAutoCSelection(new AutoCSelectionEventArgs(this, scnotification.position, scnotification.text, scnotification.ch, (ListCompletionMethod)scnotification.listCompletionMethod));
					return;
				case 2023:
				case 2024:
					this.ScnIndicatorClick(ref scnotification);
					return;
				case 2025:
					this.OnAutoCCancelled(EventArgs.Empty);
					return;
				case 2026:
					this.OnAutoCCharDeleted(EventArgs.Empty);
					return;
				case 2030:
					this.OnAutoCCompleted(new AutoCSelectionEventArgs(this, scnotification.position, scnotification.text, scnotification.ch, (ListCompletionMethod)scnotification.listCompletionMethod));
					return;
				}
				base.WndProc(ref m);
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000A7CC File Offset: 0x000089CC
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg <= 515)
			{
				if (msg == 2)
				{
					this.WmDestroy(ref m);
					return;
				}
				if (msg == 32)
				{
					this.DefWndProc(ref m);
					return;
				}
				if (msg != 515)
				{
					goto IL_6D;
				}
			}
			else if (msg <= 521)
			{
				if (msg != 518 && msg != 521)
				{
					goto IL_6D;
				}
			}
			else if (msg != 525)
			{
				if (msg == 8270)
				{
					this.WmReflectNotify(ref m);
					return;
				}
				goto IL_6D;
			}
			this.doubleClick = true;
			IL_6D:
			base.WndProc(ref m);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000A850 File Offset: 0x00008A50
		public int WordEndPosition(int position, bool onlyWordCharacters)
		{
			IntPtr lParam = onlyWordCharacters ? new IntPtr(1) : IntPtr.Zero;
			position = Helpers.Clamp(position, 0, this.TextLength);
			position = this.Lines.CharToBytePosition(position);
			position = this.DirectMessage(2267, new IntPtr(position), lParam).ToInt32();
			return this.Lines.ByteToCharPosition(position);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000A8B4 File Offset: 0x00008AB4
		public int WordStartPosition(int position, bool onlyWordCharacters)
		{
			IntPtr lParam = onlyWordCharacters ? new IntPtr(1) : IntPtr.Zero;
			position = Helpers.Clamp(position, 0, this.TextLength);
			position = this.Lines.CharToBytePosition(position);
			position = this.DirectMessage(2266, new IntPtr(position), lParam).ToInt32();
			return this.Lines.ByteToCharPosition(position);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000A917 File Offset: 0x00008B17
		public void ZoomIn()
		{
			this.DirectMessage(2333);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000A925 File Offset: 0x00008B25
		public void ZoomOut()
		{
			this.DirectMessage(2334);
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x0000A934 File Offset: 0x00008B34
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x0000A95C File Offset: 0x00008B5C
		[Category("Multiple Selection")]
		[Description("The additional caret foreground color.")]
		public Color AdditionalCaretForeColor
		{
			get
			{
				return ColorTranslator.FromWin32(this.DirectMessage(2605).ToInt32());
			}
			set
			{
				int value2 = ColorTranslator.ToWin32(value);
				this.DirectMessage(2604, new IntPtr(value2));
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000A982 File Offset: 0x00008B82
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x0000A99C File Offset: 0x00008B9C
		[DefaultValue(true)]
		[Category("Multiple Selection")]
		[Description("Whether the carets in additional selections should blink.")]
		public bool AdditionalCaretsBlink
		{
			get
			{
				return this.DirectMessage(2568) != IntPtr.Zero;
			}
			set
			{
				IntPtr wParam = value ? new IntPtr(1) : IntPtr.Zero;
				this.DirectMessage(2567, wParam);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000A9C7 File Offset: 0x00008BC7
		// (set) Token: 0x060001EB RID: 491 RVA: 0x0000A9E0 File Offset: 0x00008BE0
		[DefaultValue(true)]
		[Category("Multiple Selection")]
		[Description("Whether the carets in additional selections are visible.")]
		public bool AdditionalCaretsVisible
		{
			get
			{
				return this.DirectMessage(2609) != IntPtr.Zero;
			}
			set
			{
				IntPtr wParam = value ? new IntPtr(1) : IntPtr.Zero;
				this.DirectMessage(2608, wParam);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000AA0C File Offset: 0x00008C0C
		// (set) Token: 0x060001ED RID: 493 RVA: 0x0000AA2C File Offset: 0x00008C2C
		[DefaultValue(256)]
		[Category("Multiple Selection")]
		[Description("The transparency of additional selections.")]
		public int AdditionalSelAlpha
		{
			get
			{
				return this.DirectMessage(2603).ToInt32();
			}
			set
			{
				value = Helpers.Clamp(value, 0, 256);
				this.DirectMessage(2602, new IntPtr(value));
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000AA4E File Offset: 0x00008C4E
		// (set) Token: 0x060001EF RID: 495 RVA: 0x0000AA68 File Offset: 0x00008C68
		[DefaultValue(false)]
		[Category("Multiple Selection")]
		[Description("Whether typing, backspace, or delete works with multiple selection simultaneously.")]
		public bool AdditionalSelectionTyping
		{
			get
			{
				return this.DirectMessage(2566) != IntPtr.Zero;
			}
			set
			{
				IntPtr wParam = value ? new IntPtr(1) : IntPtr.Zero;
				this.DirectMessage(2565, wParam);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000AA94 File Offset: 0x00008C94
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x0000AAC4 File Offset: 0x00008CC4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int AnchorPosition
		{
			get
			{
				int pos = this.DirectMessage(2009).ToInt32();
				return this.Lines.ByteToCharPosition(pos);
			}
			set
			{
				value = Helpers.Clamp(value, 0, this.TextLength);
				int value2 = this.Lines.CharToBytePosition(value);
				this.DirectMessage(2026, new IntPtr(value2));
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000AB00 File Offset: 0x00008D00
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x0000AB20 File Offset: 0x00008D20
		[DefaultValue(Annotation.Hidden)]
		[Category("Appearance")]
		[Description("Display and location of annotations.")]
		public Annotation AnnotationVisible
		{
			get
			{
				return (Annotation)this.DirectMessage(2549).ToInt32();
			}
			set
			{
				this.DirectMessage(2548, new IntPtr((int)value));
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000AB41 File Offset: 0x00008D41
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool AutoCActive
		{
			get
			{
				return this.DirectMessage(2102) != IntPtr.Zero;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000AB58 File Offset: 0x00008D58
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x0000AB70 File Offset: 0x00008D70
		[DefaultValue(true)]
		[Category("Autocompletion")]
		[Description("Whether to automatically cancel autocompletion when no match is possible.")]
		public bool AutoCAutoHide
		{
			get
			{
				return this.DirectMessage(2119) != IntPtr.Zero;
			}
			set
			{
				IntPtr wParam = value ? new IntPtr(1) : IntPtr.Zero;
				this.DirectMessage(2118, wParam);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000AB9B File Offset: 0x00008D9B
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x0000ABB4 File Offset: 0x00008DB4
		[DefaultValue(true)]
		[Category("Autocompletion")]
		[Description("Whether to cancel an autocompletion if the caret moves from its initial location, or is allowed to move to the word start.")]
		public bool AutoCCancelAtStart
		{
			get
			{
				return this.DirectMessage(2111) != IntPtr.Zero;
			}
			set
			{
				IntPtr wParam = value ? new IntPtr(1) : IntPtr.Zero;
				this.DirectMessage(2110, wParam);
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000ABE0 File Offset: 0x00008DE0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int AutoCCurrent
		{
			get
			{
				return this.DirectMessage(2445).ToInt32();
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000AC00 File Offset: 0x00008E00
		// (set) Token: 0x060001FB RID: 507 RVA: 0x0000AC18 File Offset: 0x00008E18
		[DefaultValue(false)]
		[Category("Autocompletion")]
		[Description("Whether to automatically choose an autocompletion item when it is the only one in the list.")]
		public bool AutoCChooseSingle
		{
			get
			{
				return this.DirectMessage(2114) != IntPtr.Zero;
			}
			set
			{
				IntPtr wParam = value ? new IntPtr(1) : IntPtr.Zero;
				this.DirectMessage(2113, wParam);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000AC43 File Offset: 0x00008E43
		// (set) Token: 0x060001FD RID: 509 RVA: 0x0000AC5C File Offset: 0x00008E5C
		[DefaultValue(false)]
		[Category("Autocompletion")]
		[Description("Whether to delete any existing word characters following the caret after autocompletion.")]
		public bool AutoCDropRestOfWord
		{
			get
			{
				return this.DirectMessage(2271) != IntPtr.Zero;
			}
			set
			{
				IntPtr wParam = value ? new IntPtr(1) : IntPtr.Zero;
				this.DirectMessage(2270, wParam);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000AC87 File Offset: 0x00008E87
		// (set) Token: 0x060001FF RID: 511 RVA: 0x0000ACA0 File Offset: 0x00008EA0
		[DefaultValue(false)]
		[Category("Autocompletion")]
		[Description("Whether autocompletion word matching can ignore case.")]
		public bool AutoCIgnoreCase
		{
			get
			{
				return this.DirectMessage(2116) != IntPtr.Zero;
			}
			set
			{
				IntPtr wParam = value ? new IntPtr(1) : IntPtr.Zero;
				this.DirectMessage(2115, wParam);
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000200 RID: 512 RVA: 0x0000ACCC File Offset: 0x00008ECC
		// (set) Token: 0x06000201 RID: 513 RVA: 0x0000ACEC File Offset: 0x00008EEC
		[DefaultValue(5)]
		[Category("Autocompletion")]
		[Description("The maximum number of rows to display in an autocompletion list.")]
		public int AutoCMaxHeight
		{
			get
			{
				return this.DirectMessage(2211).ToInt32();
			}
			set
			{
				value = Helpers.ClampMin(value, 0);
				this.DirectMessage(2210, new IntPtr(value));
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000202 RID: 514 RVA: 0x0000AD0C File Offset: 0x00008F0C
		// (set) Token: 0x06000203 RID: 515 RVA: 0x0000AD2C File Offset: 0x00008F2C
		[DefaultValue(0)]
		[Category("Autocompletion")]
		[Description("The width of the autocompletion list measured in characters.")]
		public int AutoCMaxWidth
		{
			get
			{
				return this.DirectMessage(2209).ToInt32();
			}
			set
			{
				value = Helpers.ClampMin(value, 0);
				this.DirectMessage(2208, new IntPtr(value));
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000204 RID: 516 RVA: 0x0000AD4C File Offset: 0x00008F4C
		// (set) Token: 0x06000205 RID: 517 RVA: 0x0000AD6C File Offset: 0x00008F6C
		[DefaultValue(Order.Presorted)]
		[Category("Autocompletion")]
		[Description("The order of words in an autocompletion list.")]
		public Order AutoCOrder
		{
			get
			{
				return (Order)this.DirectMessage(2661).ToInt32();
			}
			set
			{
				this.DirectMessage(2660, new IntPtr((int)value));
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000AD90 File Offset: 0x00008F90
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int AutoCPosStart
		{
			get
			{
				int pos = this.DirectMessage(2103).ToInt32();
				return this.Lines.ByteToCharPosition(pos);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000ADC0 File Offset: 0x00008FC0
		// (set) Token: 0x06000208 RID: 520 RVA: 0x0000ADE4 File Offset: 0x00008FE4
		[DefaultValue(' ')]
		[Category("Autocompletion")]
		[Description("The autocompletion list word delimiter. The default is a space character.")]
		public char AutoCSeparator
		{
			get
			{
				return (char)this.DirectMessage(2107).ToInt32();
			}
			set
			{
				byte value2 = (byte)value;
				this.DirectMessage(2106, new IntPtr((int)value2));
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0000AE08 File Offset: 0x00009008
		// (set) Token: 0x0600020A RID: 522 RVA: 0x0000AE2C File Offset: 0x0000902C
		[DefaultValue('?')]
		[Category("Autocompletion")]
		[Description("The autocompletion list image type delimiter.")]
		public char AutoCTypeSeparator
		{
			get
			{
				return (char)this.DirectMessage(2285).ToInt32();
			}
			set
			{
				byte value2 = (byte)value;
				this.DirectMessage(2286, new IntPtr((int)value2));
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000AE4E File Offset: 0x0000904E
		// (set) Token: 0x0600020C RID: 524 RVA: 0x0000AE60 File Offset: 0x00009060
		[DefaultValue(AutomaticFold.None)]
		[Category("Behavior")]
		[Description("Options for allowing the control to automatically handle folding.")]
		[TypeConverter(typeof(FlagsEnumConverter))]
		public AutomaticFold AutomaticFold
		{
			get
			{
				return (AutomaticFold)((int)this.DirectMessage(2664));
			}
			set
			{
				this.DirectMessage(2663, new IntPtr((int)value));
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000AE81 File Offset: 0x00009081
		// (set) Token: 0x0600020E RID: 526 RVA: 0x0000AE89 File Offset: 0x00009089
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000AE92 File Offset: 0x00009092
		// (set) Token: 0x06000210 RID: 528 RVA: 0x0000AE9A File Offset: 0x0000909A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
			set
			{
				base.BackgroundImage = value;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000211 RID: 529 RVA: 0x0000AEA3 File Offset: 0x000090A3
		// (set) Token: 0x06000212 RID: 530 RVA: 0x0000AEAB File Offset: 0x000090AB
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ImageLayout BackgroundImageLayout
		{
			get
			{
				return base.BackgroundImageLayout;
			}
			set
			{
				base.BackgroundImageLayout = value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000AEB4 File Offset: 0x000090B4
		// (set) Token: 0x06000214 RID: 532 RVA: 0x0000AEBC File Offset: 0x000090BC
		[DefaultValue(BorderStyle.Fixed3D)]
		[Category("Appearance")]
		[Description("Indicates whether the control should have a border.")]
		public BorderStyle BorderStyle
		{
			get
			{
				return this.borderStyle;
			}
			set
			{
				if (this.borderStyle != value)
				{
					if (!Enum.IsDefined(typeof(BorderStyle), value))
					{
						throw new InvalidEnumArgumentException("value", (int)value, typeof(BorderStyle));
					}
					this.borderStyle = value;
					base.UpdateStyles();
					this.OnBorderStyleChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000AF17 File Offset: 0x00009117
		// (set) Token: 0x06000216 RID: 534 RVA: 0x0000AF30 File Offset: 0x00009130
		[DefaultValue(true)]
		[Category("Misc")]
		[Description("Determines whether drawing is double-buffered.")]
		public bool BufferedDraw
		{
			get
			{
				return this.DirectMessage(2034) != IntPtr.Zero;
			}
			set
			{
				IntPtr wParam = value ? new IntPtr(1) : IntPtr.Zero;
				this.DirectMessage(2035, wParam);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000AF5B File Offset: 0x0000915B
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CallTipActive
		{
			get
			{
				return this.DirectMessage(2202) != IntPtr.Zero;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000AF72 File Offset: 0x00009172
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanPaste
		{
			get
			{
				return this.DirectMessage(2173) != IntPtr.Zero;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000AF89 File Offset: 0x00009189
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanRedo
		{
			get
			{
				return this.DirectMessage(2016) != IntPtr.Zero;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000AFA0 File Offset: 0x000091A0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanUndo
		{
			get
			{
				return this.DirectMessage(2174) != IntPtr.Zero;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0000AFB8 File Offset: 0x000091B8
		// (set) Token: 0x0600021C RID: 540 RVA: 0x0000AFE0 File Offset: 0x000091E0
		[DefaultValue(typeof(Color), "Black")]
		[Category("Caret")]
		[Description("The caret foreground color.")]
		public Color CaretForeColor
		{
			get
			{
				return ColorTranslator.FromWin32(this.DirectMessage(2138).ToInt32());
			}
			set
			{
				int value2 = ColorTranslator.ToWin32(value);
				this.DirectMessage(2069, new IntPtr(value2));
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000B008 File Offset: 0x00009208
		// (set) Token: 0x0600021E RID: 542 RVA: 0x0000B030 File Offset: 0x00009230
		[DefaultValue(typeof(Color), "Yellow")]
		[Category("Caret")]
		[Description("The background color of the current line.")]
		public Color CaretLineBackColor
		{
			get
			{
				return ColorTranslator.FromWin32(this.DirectMessage(2097).ToInt32());
			}
			set
			{
				int value2 = ColorTranslator.ToWin32(value);
				this.DirectMessage(2098, new IntPtr(value2));
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0000B058 File Offset: 0x00009258
		// (set) Token: 0x06000220 RID: 544 RVA: 0x0000B078 File Offset: 0x00009278
		[DefaultValue(256)]
		[Category("Caret")]
		[Description("The transparency of the current line background color.")]
		public int CaretLineBackColorAlpha
		{
			get
			{
				return this.DirectMessage(2471).ToInt32();
			}
			set
			{
				value = Helpers.Clamp(value, 0, 256);
				this.DirectMessage(2470, new IntPtr(value));
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000221 RID: 545 RVA: 0x0000B09A File Offset: 0x0000929A
		// (set) Token: 0x06000222 RID: 546 RVA: 0x0000B0B4 File Offset: 0x000092B4
		[DefaultValue(false)]
		[Category("Caret")]
		[Description("Determines whether to highlight the current caret line.")]
		public bool CaretLineVisible
		{
			get
			{
				return this.DirectMessage(2095) != IntPtr.Zero;
			}
			set
			{
				IntPtr wParam = value ? new IntPtr(1) : IntPtr.Zero;
				this.DirectMessage(2096, wParam);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000B0E0 File Offset: 0x000092E0
		// (set) Token: 0x06000224 RID: 548 RVA: 0x0000B100 File Offset: 0x00009300
		[DefaultValue(530)]
		[Category("Caret")]
		[Description("The caret blink rate in milliseconds.")]
		public int CaretPeriod
		{
			get
			{
				return this.DirectMessage(2075).ToInt32();
			}
			set
			{
				value = Helpers.ClampMin(value, 0);
				this.DirectMessage(2076, new IntPtr(value));
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000B120 File Offset: 0x00009320
		// (set) Token: 0x06000226 RID: 550 RVA: 0x0000B140 File Offset: 0x00009340
		[DefaultValue(CaretStyle.Line)]
		[Category("Caret")]
		[Description("The caret display style.")]
		public CaretStyle CaretStyle
		{
			get
			{
				return (CaretStyle)this.DirectMessage(2513).ToInt32();
			}
			set
			{
				this.DirectMessage(2512, new IntPtr((int)value));
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000B164 File Offset: 0x00009364
		// (set) Token: 0x06000228 RID: 552 RVA: 0x0000B184 File Offset: 0x00009384
		[DefaultValue(1)]
		[Category("Caret")]
		[Description("The width of the caret line measured in pixels (between 0 and 3).")]
		public int CaretWidth
		{
			get
			{
				return this.DirectMessage(2189).ToInt32();
			}
			set
			{
				value = Helpers.Clamp(value, 0, 3);
				this.DirectMessage(2188, new IntPtr(value));
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000B1A4 File Offset: 0x000093A4
		protected override CreateParams CreateParams
		{
			get
			{
				if (Scintilla.moduleHandle == IntPtr.Zero)
				{
					string text = Scintilla.GetModulePath();
					Scintilla.moduleHandle = NativeMethods.LoadLibrary(text);
					if (Scintilla.moduleHandle == IntPtr.Zero)
					{
						throw new Win32Exception(string.Format(CultureInfo.InvariantCulture, "Could not load the Scintilla module at the path '{0}'.", new object[]
						{
							text
						}), new Win32Exception());
					}
					IntPtr procAddress = NativeMethods.GetProcAddress(new HandleRef(this, Scintilla.moduleHandle), "Scintilla_DirectFunction");
					if (procAddress == IntPtr.Zero)
					{
						throw new Win32Exception("The Scintilla module has no export for the 'Scintilla_DirectFunction' procedure.", new Win32Exception());
					}
					Scintilla.directFunction = (NativeMethods.Scintilla_DirectFunction)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(NativeMethods.Scintilla_DirectFunction));
				}
				CreateParams createParams = base.CreateParams;
				createParams.ClassName = "Scintilla";
				createParams.ExStyle &= -513;
				createParams.Style &= -8388609;
				BorderStyle borderStyle = this.borderStyle;
				if (borderStyle != BorderStyle.FixedSingle)
				{
					if (borderStyle == BorderStyle.Fixed3D)
					{
						createParams.ExStyle |= 512;
					}
				}
				else
				{
					createParams.Style |= 8388608;
				}
				return createParams;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000B2C4 File Offset: 0x000094C4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int CurrentLine
		{
			get
			{
				int value = this.DirectMessage(2008).ToInt32();
				return this.DirectMessage(2166, new IntPtr(value)).ToInt32();
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000B300 File Offset: 0x00009500
		// (set) Token: 0x0600022C RID: 556 RVA: 0x0000B330 File Offset: 0x00009530
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int CurrentPosition
		{
			get
			{
				int pos = this.DirectMessage(2008).ToInt32();
				return this.Lines.ByteToCharPosition(pos);
			}
			set
			{
				value = Helpers.Clamp(value, 0, this.TextLength);
				int value2 = this.Lines.CharToBytePosition(value);
				this.DirectMessage(2141, new IntPtr(value2));
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000B36B File Offset: 0x0000956B
		// (set) Token: 0x0600022E RID: 558 RVA: 0x0000B373 File Offset: 0x00009573
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Cursor Cursor
		{
			get
			{
				return base.Cursor;
			}
			set
			{
				base.Cursor = value;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000B37C File Offset: 0x0000957C
		protected override Cursor DefaultCursor
		{
			get
			{
				return Cursors.IBeam;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000B383 File Offset: 0x00009583
		protected override Size DefaultSize
		{
			get
			{
				return new Size(200, 100);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000B394 File Offset: 0x00009594
		// (set) Token: 0x06000232 RID: 562 RVA: 0x0000B3C0 File Offset: 0x000095C0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Document Document
		{
			get
			{
				IntPtr value = this.DirectMessage(2357);
				return new Document
				{
					Value = value
				};
			}
			set
			{
				Eol eolMode = this.EolMode;
				bool useTabs = this.UseTabs;
				int tabWidth = this.TabWidth;
				int indentWidth = this.IndentWidth;
				IntPtr value2 = value.Value;
				this.DirectMessage(2358, IntPtr.Zero, value2);
				this.InitDocument(eolMode, useTabs, tabWidth, indentWidth);
				this.Lines.RebuildLineData();
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0000B41C File Offset: 0x0000961C
		// (set) Token: 0x06000234 RID: 564 RVA: 0x0000B444 File Offset: 0x00009644
		[DefaultValue(typeof(Color), "Silver")]
		[Category("Long Lines")]
		[Description("The background color to use when indicating long lines.")]
		public Color EdgeColor
		{
			get
			{
				return ColorTranslator.FromWin32(this.DirectMessage(2364).ToInt32());
			}
			set
			{
				int value2 = ColorTranslator.ToWin32(value);
				this.DirectMessage(2365, new IntPtr(value2));
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000235 RID: 565 RVA: 0x0000B46C File Offset: 0x0000966C
		// (set) Token: 0x06000236 RID: 566 RVA: 0x0000B48C File Offset: 0x0000968C
		[DefaultValue(0)]
		[Category("Long Lines")]
		[Description("The number of columns at which to display long line indicators.")]
		public int EdgeColumn
		{
			get
			{
				return this.DirectMessage(2360).ToInt32();
			}
			set
			{
				value = Helpers.ClampMin(value, 0);
				this.DirectMessage(2361, new IntPtr(value));
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000B4A9 File Offset: 0x000096A9
		// (set) Token: 0x06000238 RID: 568 RVA: 0x0000B4BC File Offset: 0x000096BC
		[DefaultValue(EdgeMode.None)]
		[Category("Long Lines")]
		[Description("Determines how long lines are indicated.")]
		public EdgeMode EdgeMode
		{
			get
			{
				return (EdgeMode)((int)this.DirectMessage(2362));
			}
			set
			{
				this.DirectMessage(2363, new IntPtr((int)value));
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000B4E0 File Offset: 0x000096E0
		internal Encoding Encoding
		{
			get
			{
				int num = (int)this.DirectMessage(2137);
				if (num != 0)
				{
					return Encoding.GetEncoding(num);
				}
				return Encoding.Default;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000B50D File Offset: 0x0000970D
		// (set) Token: 0x0600023B RID: 571 RVA: 0x0000B524 File Offset: 0x00009724
		[DefaultValue(true)]
		[Category("Scrolling")]
		[Description("Determines whether the maximum vertical scroll position ends at the last line or can scroll past.")]
		public bool EndAtLastLine
		{
			get
			{
				return this.DirectMessage(2278) != IntPtr.Zero;
			}
			set
			{
				IntPtr wParam = value ? new IntPtr(1) : IntPtr.Zero;
				this.DirectMessage(2277, wParam);
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000B54F File Offset: 0x0000974F
		// (set) Token: 0x0600023D RID: 573 RVA: 0x0000B564 File Offset: 0x00009764
		[DefaultValue(Eol.CrLf)]
		[Category("Line Endings")]
		[Description("Determines the characters added into the document when the user presses the Enter key.")]
		public Eol EolMode
		{
			get
			{
				return (Eol)((int)this.DirectMessage(2030));
			}
			set
			{
				this.DirectMessage(2031, new IntPtr((int)value));
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000B588 File Offset: 0x00009788
		// (set) Token: 0x0600023F RID: 575 RVA: 0x0000B5A8 File Offset: 0x000097A8
		[DefaultValue(0)]
		[Category("Whitespace")]
		[Description("Extra whitespace added to the ascent (top) of each line.")]
		public int ExtraAscent
		{
			get
			{
				return this.DirectMessage(2526).ToInt32();
			}
			set
			{
				this.DirectMessage(2525, new IntPtr(value));
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000B5BC File Offset: 0x000097BC
		// (set) Token: 0x06000241 RID: 577 RVA: 0x0000B5DC File Offset: 0x000097DC
		[DefaultValue(0)]
		[Category("Whitespace")]
		[Description("Extra whitespace added to the descent (bottom) of each line.")]
		public int ExtraDescent
		{
			get
			{
				return this.DirectMessage(2528).ToInt32();
			}
			set
			{
				this.DirectMessage(2527, new IntPtr(value));
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000B5F0 File Offset: 0x000097F0
		// (set) Token: 0x06000243 RID: 579 RVA: 0x0000B610 File Offset: 0x00009810
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int FirstVisibleLine
		{
			get
			{
				return this.DirectMessage(2152).ToInt32();
			}
			set
			{
				value = Helpers.ClampMin(value, 0);
				this.DirectMessage(2613, new IntPtr(value));
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000B62D File Offset: 0x0000982D
		// (set) Token: 0x06000245 RID: 581 RVA: 0x0000B635 File Offset: 0x00009835
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				base.Font = value;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000B63E File Offset: 0x0000983E
		// (set) Token: 0x06000247 RID: 583 RVA: 0x0000B650 File Offset: 0x00009850
		[DefaultValue(FontQuality.Default)]
		[Category("Misc")]
		[Description("Specifies the anti-aliasing method to use when rendering fonts.")]
		public FontQuality FontQuality
		{
			get
			{
				return (FontQuality)((int)this.DirectMessage(2612));
			}
			set
			{
				this.DirectMessage(2611, new IntPtr((int)value));
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000B671 File Offset: 0x00009871
		// (set) Token: 0x06000249 RID: 585 RVA: 0x0000B679 File Offset: 0x00009879
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000B684 File Offset: 0x00009884
		// (set) Token: 0x0600024B RID: 587 RVA: 0x0000B6A4 File Offset: 0x000098A4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int HighlightGuide
		{
			get
			{
				return this.DirectMessage(2135).ToInt32();
			}
			set
			{
				value = Helpers.ClampMin(value, 0);
				this.DirectMessage(2134, new IntPtr(value));
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0000B6C1 File Offset: 0x000098C1
		// (set) Token: 0x0600024D RID: 589 RVA: 0x0000B6D8 File Offset: 0x000098D8
		[DefaultValue(true)]
		[Category("Scrolling")]
		[Description("Determines whether to show the horizontal scroll bar if needed.")]
		public bool HScrollBar
		{
			get
			{
				return this.DirectMessage(2131) != IntPtr.Zero;
			}
			set
			{
				IntPtr wParam = value ? new IntPtr(1) : IntPtr.Zero;
				this.DirectMessage(2130, wParam);
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000B703 File Offset: 0x00009903
		// (set) Token: 0x0600024F RID: 591 RVA: 0x0000B718 File Offset: 0x00009918
		[DefaultValue(IdleStyling.None)]
		[Category("Misc")]
		[Description("Specifies how to use application idle time for styling.")]
		public IdleStyling IdleStyling
		{
			get
			{
				return (IdleStyling)((int)this.DirectMessage(2693));
			}
			set
			{
				this.DirectMessage(2692, new IntPtr((int)value));
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000B73C File Offset: 0x0000993C
		// (set) Token: 0x06000251 RID: 593 RVA: 0x0000B75C File Offset: 0x0000995C
		[DefaultValue(0)]
		[Category("Indentation")]
		[Description("The indentation size in characters or 0 to make it the same as the tab width.")]
		public int IndentWidth
		{
			get
			{
				return this.DirectMessage(2123).ToInt32();
			}
			set
			{
				value = Helpers.ClampMin(value, 0);
				this.DirectMessage(2122, new IntPtr(value));
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000B779 File Offset: 0x00009979
		// (set) Token: 0x06000253 RID: 595 RVA: 0x0000B78C File Offset: 0x0000998C
		[DefaultValue(IndentView.None)]
		[Category("Indentation")]
		[Description("Indicates whether indentation guides are displayed.")]
		public IndentView IndentationGuides
		{
			get
			{
				return (IndentView)((int)this.DirectMessage(2133));
			}
			set
			{
				this.DirectMessage(2132, new IntPtr((int)value));
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0000B7B0 File Offset: 0x000099B0
		// (set) Token: 0x06000255 RID: 597 RVA: 0x0000B7D0 File Offset: 0x000099D0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int IndicatorCurrent
		{
			get
			{
				return this.DirectMessage(2501).ToInt32();
			}
			set
			{
				value = Helpers.Clamp(value, 0, this.Indicators.Count - 1);
				this.DirectMessage(2500, new IntPtr(value));
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000B7FA File Offset: 0x000099FA
		// (set) Token: 0x06000257 RID: 599 RVA: 0x0000B802 File Offset: 0x00009A02
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IndicatorCollection Indicators { get; private set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000B80C File Offset: 0x00009A0C
		// (set) Token: 0x06000259 RID: 601 RVA: 0x0000B82C File Offset: 0x00009A2C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int IndicatorValue
		{
			get
			{
				return this.DirectMessage(2503).ToInt32();
			}
			set
			{
				this.DirectMessage(2502, new IntPtr(value));
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000B840 File Offset: 0x00009A40
		// (set) Token: 0x0600025B RID: 603 RVA: 0x0000B854 File Offset: 0x00009A54
		[DefaultValue(Lexer.Container)]
		[Category("Lexing")]
		[Description("The current lexer.")]
		public Lexer Lexer
		{
			get
			{
				return (Lexer)((int)this.DirectMessage(4002));
			}
			set
			{
				this.DirectMessage(4001, new IntPtr((int)value));
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000B878 File Offset: 0x00009A78
		// (set) Token: 0x0600025D RID: 605 RVA: 0x0000B8EC File Offset: 0x00009AEC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public unsafe string LexerLanguage
		{
			get
			{
				int num = this.DirectMessage(4012).ToInt32();
				if (num == 0)
				{
					return string.Empty;
				}
				fixed (byte* ptr = new byte[num + 1])
				{
					this.DirectMessage(4012, IntPtr.Zero, new IntPtr((void*)ptr));
					return Helpers.GetString(new IntPtr((void*)ptr), num, Encoding.ASCII);
				}
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.DirectMessage(4006, IntPtr.Zero, IntPtr.Zero);
					return;
				}
				fixed (byte* bytes = Helpers.GetBytes(value, Encoding.ASCII, true))
				{
					this.DirectMessage(4006, IntPtr.Zero, new IntPtr((void*)bytes));
				}
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000B955 File Offset: 0x00009B55
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public LineEndType LineEndTypesActive
		{
			get
			{
				return (LineEndType)((int)this.DirectMessage(2658));
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000B967 File Offset: 0x00009B67
		// (set) Token: 0x06000260 RID: 608 RVA: 0x0000B97C File Offset: 0x00009B7C
		[DefaultValue(LineEndType.Default)]
		[Category("Line Endings")]
		[Description("Line endings types interpreted by the control.")]
		[TypeConverter(typeof(FlagsEnumConverter))]
		public LineEndType LineEndTypesAllowed
		{
			get
			{
				return (LineEndType)((int)this.DirectMessage(2657));
			}
			set
			{
				this.DirectMessage(2656, new IntPtr((int)value));
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000B99D File Offset: 0x00009B9D
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public LineEndType LineEndTypesSupported
		{
			get
			{
				return (LineEndType)((int)this.DirectMessage(4018));
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000B9AF File Offset: 0x00009BAF
		// (set) Token: 0x06000263 RID: 611 RVA: 0x0000B9B7 File Offset: 0x00009BB7
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public LineCollection Lines { get; private set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000B9C0 File Offset: 0x00009BC0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int LinesOnScreen
		{
			get
			{
				return this.DirectMessage(2370).ToInt32();
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000B9E0 File Offset: 0x00009BE0
		// (set) Token: 0x06000266 RID: 614 RVA: 0x0000BA00 File Offset: 0x00009C00
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int MainSelection
		{
			get
			{
				return this.DirectMessage(2575).ToInt32();
			}
			set
			{
				value = Helpers.ClampMin(value, 0);
				this.DirectMessage(2574, new IntPtr(value));
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000BA1D File Offset: 0x00009C1D
		// (set) Token: 0x06000268 RID: 616 RVA: 0x0000BA25 File Offset: 0x00009C25
		[Category("Collections")]
		[Description("The margins collection.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public MarginCollection Margins { get; private set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000BA2E File Offset: 0x00009C2E
		// (set) Token: 0x0600026A RID: 618 RVA: 0x0000BA36 File Offset: 0x00009C36
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public MarkerCollection Markers { get; private set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000BA3F File Offset: 0x00009C3F
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Modified
		{
			get
			{
				return this.DirectMessage(2159) != IntPtr.Zero;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000BA58 File Offset: 0x00009C58
		// (set) Token: 0x0600026D RID: 621 RVA: 0x0000BA78 File Offset: 0x00009C78
		[DefaultValue(10000000)]
		[Category("Behavior")]
		[Description("The time in milliseconds the mouse must linger to generate a dwell start event. A value of 10000000 disables dwell events.")]
		public int MouseDwellTime
		{
			get
			{
				return this.DirectMessage(2265).ToInt32();
			}
			set
			{
				value = Helpers.ClampMin(value, 0);
				this.DirectMessage(2264, new IntPtr(value));
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000BA95 File Offset: 0x00009C95
		// (set) Token: 0x0600026F RID: 623 RVA: 0x0000BAAC File Offset: 0x00009CAC
		[DefaultValue(false)]
		[Category("Multiple Selection")]
		[Description("Enable or disable the ability to switch to rectangular selection mode while making a selection with the mouse.")]
		public bool MouseSelectionRectangularSwitch
		{
			get
			{
				return this.DirectMessage(2669) != IntPtr.Zero;
			}
			set
			{
				IntPtr wParam = value ? new IntPtr(1) : IntPtr.Zero;
				this.DirectMessage(2668, wParam);
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000BAD7 File Offset: 0x00009CD7
		// (set) Token: 0x06000271 RID: 625 RVA: 0x0000BAF0 File Offset: 0x00009CF0
		[DefaultValue(false)]
		[Category("Multiple Selection")]
		[Description("Enable or disable multiple selection with the CTRL key.")]
		public bool MultipleSelection
		{
			get
			{
				return this.DirectMessage(2564) != IntPtr.Zero;
			}
			set
			{
				IntPtr wParam = value ? new IntPtr(1) : IntPtr.Zero;
				this.DirectMessage(2563, wParam);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000BB1B File Offset: 0x00009D1B
		// (set) Token: 0x06000273 RID: 627 RVA: 0x0000BB30 File Offset: 0x00009D30
		[DefaultValue(MultiPaste.Once)]
		[Category("Multiple Selection")]
		[Description("Determines how pasted text is applied to multiple selections.")]
		public MultiPaste MultiPaste
		{
			get
			{
				return (MultiPaste)((int)this.DirectMessage(2615));
			}
			set
			{
				this.DirectMessage(2614, new IntPtr((int)value));
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000BB51 File Offset: 0x00009D51
		// (set) Token: 0x06000275 RID: 629 RVA: 0x0000BB68 File Offset: 0x00009D68
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("Puts the caret into overtype mode.")]
		public bool Overtype
		{
			get
			{
				return this.DirectMessage(2187) != IntPtr.Zero;
			}
			set
			{
				IntPtr wParam = value ? new IntPtr(1) : IntPtr.Zero;
				this.DirectMessage(2186, wParam);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000BB93 File Offset: 0x00009D93
		// (set) Token: 0x06000277 RID: 631 RVA: 0x0000BB9B File Offset: 0x00009D9B
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Padding Padding
		{
			get
			{
				return base.Padding;
			}
			set
			{
				base.Padding = value;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000BBA4 File Offset: 0x00009DA4
		// (set) Token: 0x06000279 RID: 633 RVA: 0x0000BBBC File Offset: 0x00009DBC
		[DefaultValue(true)]
		[Category("Line Endings")]
		[Description("Whether line endings in pasted text are converted to match the document end-of-line mode.")]
		public bool PasteConvertEndings
		{
			get
			{
				return this.DirectMessage(2468) != IntPtr.Zero;
			}
			set
			{
				IntPtr wParam = value ? new IntPtr(1) : IntPtr.Zero;
				this.DirectMessage(2467, wParam);
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000BBE7 File Offset: 0x00009DE7
		// (set) Token: 0x0600027B RID: 635 RVA: 0x0000BBFC File Offset: 0x00009DFC
		[DefaultValue(Phases.Two)]
		[Category("Misc")]
		[Description("Adjusts the number of phases used when drawing.")]
		public Phases PhasesDraw
		{
			get
			{
				return (Phases)((int)this.DirectMessage(2673));
			}
			set
			{
				this.DirectMessage(2674, new IntPtr((int)value));
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000BC1D File Offset: 0x00009E1D
		// (set) Token: 0x0600027D RID: 637 RVA: 0x0000BC34 File Offset: 0x00009E34
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("Controls whether the document text can be modified.")]
		public bool ReadOnly
		{
			get
			{
				return this.DirectMessage(2140) != IntPtr.Zero;
			}
			set
			{
				IntPtr wParam = value ? new IntPtr(1) : IntPtr.Zero;
				this.DirectMessage(2171, wParam);
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000BC60 File Offset: 0x00009E60
		// (set) Token: 0x0600027F RID: 639 RVA: 0x0000BC93 File Offset: 0x00009E93
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int RectangularSelectionAnchor
		{
			get
			{
				int num = this.DirectMessage(2591).ToInt32();
				if (num <= 0)
				{
					return num;
				}
				return this.Lines.ByteToCharPosition(num);
			}
			set
			{
				value = Helpers.Clamp(value, 0, this.TextLength);
				value = this.Lines.CharToBytePosition(value);
				this.DirectMessage(2590, new IntPtr(value));
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000BCC4 File Offset: 0x00009EC4
		// (set) Token: 0x06000281 RID: 641 RVA: 0x0000BCE4 File Offset: 0x00009EE4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int RectangularSelectionAnchorVirtualSpace
		{
			get
			{
				return this.DirectMessage(2595).ToInt32();
			}
			set
			{
				value = Helpers.ClampMin(value, 0);
				this.DirectMessage(2594, new IntPtr(value));
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000BD04 File Offset: 0x00009F04
		// (set) Token: 0x06000283 RID: 643 RVA: 0x0000BD37 File Offset: 0x00009F37
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int RectangularSelectionCaret
		{
			get
			{
				int num = this.DirectMessage(2589).ToInt32();
				if (num <= 0)
				{
					return 0;
				}
				return this.Lines.ByteToCharPosition(num);
			}
			set
			{
				value = Helpers.Clamp(value, 0, this.TextLength);
				value = this.Lines.CharToBytePosition(value);
				this.DirectMessage(2588, new IntPtr(value));
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000BD68 File Offset: 0x00009F68
		// (set) Token: 0x06000285 RID: 645 RVA: 0x0000BD88 File Offset: 0x00009F88
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int RectangularSelectionCaretVirtualSpace
		{
			get
			{
				return this.DirectMessage(2593).ToInt32();
			}
			set
			{
				value = Helpers.ClampMin(value, 0);
				this.DirectMessage(2592, new IntPtr(value));
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000BDA8 File Offset: 0x00009FA8
		private IntPtr SciPointer
		{
			get
			{
				if (Control.CheckForIllegalCrossThreadCalls && base.InvokeRequired)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Control '{0}' accessed from a thread other than the thread it was created on.", new object[]
					{
						base.Name
					}));
				}
				if (this.sciPtr == IntPtr.Zero)
				{
					this.sciPtr = NativeMethods.SendMessage(new HandleRef(this, base.Handle), 2185, IntPtr.Zero, IntPtr.Zero);
				}
				return this.sciPtr;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0000BE28 File Offset: 0x0000A028
		// (set) Token: 0x06000288 RID: 648 RVA: 0x0000BE48 File Offset: 0x0000A048
		[DefaultValue(2000)]
		[Category("Scrolling")]
		[Description("The range in pixels of the horizontal scroll bar.")]
		public int ScrollWidth
		{
			get
			{
				return this.DirectMessage(2275).ToInt32();
			}
			set
			{
				this.DirectMessage(2274, new IntPtr(value));
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000BE5C File Offset: 0x0000A05C
		// (set) Token: 0x0600028A RID: 650 RVA: 0x0000BE74 File Offset: 0x0000A074
		[DefaultValue(true)]
		[Category("Scrolling")]
		[Description("Determines whether to increase the horizontal scroll width as needed.")]
		public bool ScrollWidthTracking
		{
			get
			{
				return this.DirectMessage(2517) != IntPtr.Zero;
			}
			set
			{
				IntPtr wParam = value ? new IntPtr(1) : IntPtr.Zero;
				this.DirectMessage(2516, wParam);
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000BEA0 File Offset: 0x0000A0A0
		// (set) Token: 0x0600028C RID: 652 RVA: 0x0000BEC0 File Offset: 0x0000A0C0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SearchFlags SearchFlags
		{
			get
			{
				return (SearchFlags)this.DirectMessage(2199).ToInt32();
			}
			set
			{
				this.DirectMessage(2198, new IntPtr((int)value));
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000BEE4 File Offset: 0x0000A0E4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public unsafe string SelectedText
		{
			get
			{
				int num = this.DirectMessage(2161).ToInt32() - 1;
				if (num <= 0)
				{
					return string.Empty;
				}
				fixed (byte* ptr = new byte[num + 1])
				{
					this.DirectMessage(2161, IntPtr.Zero, new IntPtr((void*)ptr));
					return Helpers.GetString(new IntPtr((void*)ptr), num, this.Encoding);
				}
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600028E RID: 654 RVA: 0x0000BF5C File Offset: 0x0000A15C
		// (set) Token: 0x0600028F RID: 655 RVA: 0x0000BF89 File Offset: 0x0000A189
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int SelectionEnd
		{
			get
			{
				int pos = this.DirectMessage(2145).ToInt32();
				return this.Lines.ByteToCharPosition(pos);
			}
			set
			{
				value = Helpers.Clamp(value, 0, this.TextLength);
				value = this.Lines.CharToBytePosition(value);
				this.DirectMessage(2144, new IntPtr(value));
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000290 RID: 656 RVA: 0x0000BFBA File Offset: 0x0000A1BA
		// (set) Token: 0x06000291 RID: 657 RVA: 0x0000BFD4 File Offset: 0x0000A1D4
		[DefaultValue(false)]
		[Category("Selection")]
		[Description("Determines whether a selection should fill past the end of the line.")]
		public bool SelectionEolFilled
		{
			get
			{
				return this.DirectMessage(2479) != IntPtr.Zero;
			}
			set
			{
				IntPtr wParam = value ? new IntPtr(1) : IntPtr.Zero;
				this.DirectMessage(2480, wParam);
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0000BFFF File Offset: 0x0000A1FF
		// (set) Token: 0x06000293 RID: 659 RVA: 0x0000C007 File Offset: 0x0000A207
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SelectionCollection Selections { get; private set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000C010 File Offset: 0x0000A210
		// (set) Token: 0x06000295 RID: 661 RVA: 0x0000C03D File Offset: 0x0000A23D
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int SelectionStart
		{
			get
			{
				int pos = this.DirectMessage(2143).ToInt32();
				return this.Lines.ByteToCharPosition(pos);
			}
			set
			{
				value = Helpers.Clamp(value, 0, this.TextLength);
				value = this.Lines.CharToBytePosition(value);
				this.DirectMessage(2142, new IntPtr(value));
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000C06E File Offset: 0x0000A26E
		// (set) Token: 0x06000297 RID: 663 RVA: 0x0000C080 File Offset: 0x0000A280
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Status Status
		{
			get
			{
				return (Status)((int)this.DirectMessage(2383));
			}
			set
			{
				this.DirectMessage(2382, new IntPtr((int)value));
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000C0A1 File Offset: 0x0000A2A1
		// (set) Token: 0x06000299 RID: 665 RVA: 0x0000C0A9 File Offset: 0x0000A2A9
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public StyleCollection Styles { get; private set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000C0B2 File Offset: 0x0000A2B2
		// (set) Token: 0x0600029B RID: 667 RVA: 0x0000C0C4 File Offset: 0x0000A2C4
		[DefaultValue(TabDrawMode.LongArrow)]
		[Category("Whitespace")]
		[Description("Style of visible tab characters.")]
		public TabDrawMode TabDrawMode
		{
			get
			{
				return (TabDrawMode)((int)this.DirectMessage(2698));
			}
			set
			{
				this.DirectMessage(2699, new IntPtr((int)value));
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000C0E8 File Offset: 0x0000A2E8
		// (set) Token: 0x0600029D RID: 669 RVA: 0x0000C108 File Offset: 0x0000A308
		[DefaultValue(4)]
		[Category("Indentation")]
		[Description("The tab size in characters.")]
		public int TabWidth
		{
			get
			{
				return this.DirectMessage(2121).ToInt32();
			}
			set
			{
				this.DirectMessage(2036, new IntPtr(value));
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0000C11C File Offset: 0x0000A31C
		// (set) Token: 0x0600029F RID: 671 RVA: 0x0000C162 File Offset: 0x0000A362
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int TargetEnd
		{
			get
			{
				int pos = Helpers.Clamp(this.DirectMessage(2193).ToInt32(), 0, this.DirectMessage(2183).ToInt32());
				return this.Lines.ByteToCharPosition(pos);
			}
			set
			{
				value = Helpers.Clamp(value, 0, this.TextLength);
				value = this.Lines.CharToBytePosition(value);
				this.DirectMessage(2192, new IntPtr(value));
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000C194 File Offset: 0x0000A394
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x0000C1DA File Offset: 0x0000A3DA
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int TargetStart
		{
			get
			{
				int pos = Helpers.Clamp(this.DirectMessage(2191).ToInt32(), 0, this.DirectMessage(2183).ToInt32());
				return this.Lines.ByteToCharPosition(pos);
			}
			set
			{
				value = Helpers.Clamp(value, 0, this.TextLength);
				value = this.Lines.CharToBytePosition(value);
				this.DirectMessage(2190, new IntPtr(value));
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000C20C File Offset: 0x0000A40C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public unsafe string TargetText
		{
			get
			{
				int num = this.DirectMessage(2687).ToInt32();
				if (num == 0)
				{
					return string.Empty;
				}
				fixed (byte* ptr = new byte[num + 1])
				{
					this.DirectMessage(2687, IntPtr.Zero, new IntPtr((void*)ptr));
					return Helpers.GetString(new IntPtr((void*)ptr), num, this.Encoding);
				}
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000C27F File Offset: 0x0000A47F
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x0000C294 File Offset: 0x0000A494
		[DefaultValue(Technology.Default)]
		[Category("Misc")]
		[Description("The rendering technology used to draw text.")]
		public Technology Technology
		{
			get
			{
				return (Technology)((int)this.DirectMessage(2631));
			}
			set
			{
				this.DirectMessage(2630, new IntPtr((int)value));
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000C2B8 File Offset: 0x0000A4B8
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x0000C318 File Offset: 0x0000A518
		[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design", typeof(UITypeEditor))]
		public unsafe override string Text
		{
			get
			{
				int num = this.DirectMessage(2183).ToInt32();
				IntPtr intPtr = this.DirectMessage(2643, new IntPtr(0), new IntPtr(num));
				if (intPtr == IntPtr.Zero)
				{
					return string.Empty;
				}
				return new string((sbyte*)((void*)intPtr), 0, num, this.Encoding);
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.DirectMessage(2004);
					return;
				}
				fixed (byte* bytes = Helpers.GetBytes(value, this.Encoding, true))
				{
					this.DirectMessage(2181, IntPtr.Zero, new IntPtr((void*)bytes));
				}
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000C378 File Offset: 0x0000A578
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int TextLength
		{
			get
			{
				return this.Lines.TextLength;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000C385 File Offset: 0x0000A585
		// (set) Token: 0x060002A9 RID: 681 RVA: 0x0000C39C File Offset: 0x0000A59C
		[DefaultValue(false)]
		[Category("Indentation")]
		[Description("Determines whether indentation allows tab characters or purely space characters.")]
		public bool UseTabs
		{
			get
			{
				return this.DirectMessage(2125) != IntPtr.Zero;
			}
			set
			{
				IntPtr wParam = value ? new IntPtr(1) : IntPtr.Zero;
				this.DirectMessage(2124, wParam);
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000C3C7 File Offset: 0x0000A5C7
		// (set) Token: 0x060002AB RID: 683 RVA: 0x0000C3D0 File Offset: 0x0000A5D0
		public new bool UseWaitCursor
		{
			get
			{
				return base.UseWaitCursor;
			}
			set
			{
				base.UseWaitCursor = value;
				int value2 = value ? 4 : -1;
				this.DirectMessage(2386, new IntPtr(value2));
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000C3FE File Offset: 0x0000A5FE
		// (set) Token: 0x060002AD RID: 685 RVA: 0x0000C418 File Offset: 0x0000A618
		[DefaultValue(false)]
		[Category("Line Endings")]
		[Description("Display end-of-line characters.")]
		public bool ViewEol
		{
			get
			{
				return this.DirectMessage(2355) != IntPtr.Zero;
			}
			set
			{
				IntPtr wParam = value ? new IntPtr(1) : IntPtr.Zero;
				this.DirectMessage(2356, wParam);
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000C443 File Offset: 0x0000A643
		// (set) Token: 0x060002AF RID: 687 RVA: 0x0000C458 File Offset: 0x0000A658
		[DefaultValue(WhitespaceMode.Invisible)]
		[Category("Whitespace")]
		[Description("Options for displaying whitespace characters.")]
		public WhitespaceMode ViewWhitespace
		{
			get
			{
				return (WhitespaceMode)((int)this.DirectMessage(2020));
			}
			set
			{
				this.DirectMessage(2021, new IntPtr((int)value));
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000C479 File Offset: 0x0000A679
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x0000C48C File Offset: 0x0000A68C
		[DefaultValue(VirtualSpace.None)]
		[Category("Behavior")]
		[Description("Options for allowing the caret to move beyond the end of each line.")]
		[TypeConverter(typeof(FlagsEnumConverter))]
		public VirtualSpace VirtualSpaceOptions
		{
			get
			{
				return (VirtualSpace)((int)this.DirectMessage(2597));
			}
			set
			{
				this.DirectMessage(2596, new IntPtr((int)value));
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000C4AD File Offset: 0x0000A6AD
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x0000C4C4 File Offset: 0x0000A6C4
		[DefaultValue(true)]
		[Category("Scrolling")]
		[Description("Determines whether to show the vertical scroll bar when needed.")]
		public bool VScrollBar
		{
			get
			{
				return this.DirectMessage(2281) != IntPtr.Zero;
			}
			set
			{
				IntPtr wParam = value ? new IntPtr(1) : IntPtr.Zero;
				this.DirectMessage(2280, wParam);
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000C4F0 File Offset: 0x0000A6F0
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x0000C510 File Offset: 0x0000A710
		[DefaultValue(1)]
		[Category("Whitespace")]
		[Description("The size of whitespace dots.")]
		public int WhitespaceSize
		{
			get
			{
				return this.DirectMessage(2087).ToInt32();
			}
			set
			{
				this.DirectMessage(2086, new IntPtr(value));
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000C524 File Offset: 0x0000A724
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x0000C598 File Offset: 0x0000A798
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public unsafe string WordChars
		{
			get
			{
				int num = this.DirectMessage(2646, IntPtr.Zero, IntPtr.Zero).ToInt32();
				fixed (byte* ptr = new byte[num + 1])
				{
					this.DirectMessage(2646, IntPtr.Zero, new IntPtr((void*)ptr));
					return Helpers.GetString(new IntPtr((void*)ptr), num, Encoding.ASCII);
				}
			}
			set
			{
				if (value == null)
				{
					this.DirectMessage(2077, IntPtr.Zero, IntPtr.Zero);
					return;
				}
				fixed (byte* bytes = Helpers.GetBytes(value, Encoding.ASCII, true))
				{
					this.DirectMessage(2077, IntPtr.Zero, new IntPtr((void*)bytes));
				}
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000C5FC File Offset: 0x0000A7FC
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x0000C610 File Offset: 0x0000A810
		[DefaultValue(WrapIndentMode.Fixed)]
		[Category("Line Wrapping")]
		[Description("Determines how wrapped sublines are indented.")]
		public WrapIndentMode WrapIndentMode
		{
			get
			{
				return (WrapIndentMode)((int)this.DirectMessage(2473));
			}
			set
			{
				this.DirectMessage(2472, new IntPtr((int)value));
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000C631 File Offset: 0x0000A831
		// (set) Token: 0x060002BB RID: 699 RVA: 0x0000C644 File Offset: 0x0000A844
		[DefaultValue(WrapMode.None)]
		[Category("Line Wrapping")]
		[Description("The line wrapping strategy.")]
		public WrapMode WrapMode
		{
			get
			{
				return (WrapMode)((int)this.DirectMessage(2269));
			}
			set
			{
				this.DirectMessage(2268, new IntPtr((int)value));
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000C668 File Offset: 0x0000A868
		// (set) Token: 0x060002BD RID: 701 RVA: 0x0000C688 File Offset: 0x0000A888
		[DefaultValue(0)]
		[Category("Line Wrapping")]
		[Description("The amount of pixels to indent wrapped sublines.")]
		public int WrapStartIndent
		{
			get
			{
				return this.DirectMessage(2465).ToInt32();
			}
			set
			{
				value = Helpers.ClampMin(value, 0);
				this.DirectMessage(2464, new IntPtr(value));
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000C6A5 File Offset: 0x0000A8A5
		// (set) Token: 0x060002BF RID: 703 RVA: 0x0000C6B8 File Offset: 0x0000A8B8
		[DefaultValue(WrapVisualFlags.None)]
		[Category("Line Wrapping")]
		[Description("The visual indicator displayed on a wrapped line.")]
		[TypeConverter(typeof(FlagsEnumConverter))]
		public WrapVisualFlags WrapVisualFlags
		{
			get
			{
				return (WrapVisualFlags)((int)this.DirectMessage(2461));
			}
			set
			{
				this.DirectMessage(2460, new IntPtr((int)value));
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000C6D9 File Offset: 0x0000A8D9
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x0000C6EC File Offset: 0x0000A8EC
		[DefaultValue(WrapVisualFlagLocation.Default)]
		[Category("Line Wrapping")]
		[Description("The location of wrap visual flags in relation to the line text.")]
		public WrapVisualFlagLocation WrapVisualFlagLocation
		{
			get
			{
				return (WrapVisualFlagLocation)((int)this.DirectMessage(2463));
			}
			set
			{
				this.DirectMessage(2462, new IntPtr((int)value));
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000C710 File Offset: 0x0000A910
		// (set) Token: 0x060002C3 RID: 707 RVA: 0x0000C730 File Offset: 0x0000A930
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int XOffset
		{
			get
			{
				return this.DirectMessage(2398).ToInt32();
			}
			set
			{
				value = Helpers.ClampMin(value, 0);
				this.DirectMessage(2397, new IntPtr(value));
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000C750 File Offset: 0x0000A950
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x0000C770 File Offset: 0x0000A970
		[DefaultValue(0)]
		[Category("Appearance")]
		[Description("Zoom factor in points applied to the displayed text.")]
		public int Zoom
		{
			get
			{
				return this.DirectMessage(2374).ToInt32();
			}
			set
			{
				this.DirectMessage(2373, new IntPtr(value));
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060002C6 RID: 710 RVA: 0x0000C784 File Offset: 0x0000A984
		// (remove) Token: 0x060002C7 RID: 711 RVA: 0x0000C797 File Offset: 0x0000A997
		[Category("Notifications")]
		[Description("Occurs when an autocompletion list is cancelled.")]
		public event EventHandler<EventArgs> AutoCCancelled
		{
			add
			{
				base.Events.AddHandler(Scintilla.autoCCancelledEventKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(Scintilla.autoCCancelledEventKey, value);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060002C8 RID: 712 RVA: 0x0000C7AA File Offset: 0x0000A9AA
		// (remove) Token: 0x060002C9 RID: 713 RVA: 0x0000C7BD File Offset: 0x0000A9BD
		[Category("Notifications")]
		[Description("Occurs when the user deletes a character while an autocompletion list is active.")]
		public event EventHandler<EventArgs> AutoCCharDeleted
		{
			add
			{
				base.Events.AddHandler(Scintilla.autoCCharDeletedEventKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(Scintilla.autoCCharDeletedEventKey, value);
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060002CA RID: 714 RVA: 0x0000C7D0 File Offset: 0x0000A9D0
		// (remove) Token: 0x060002CB RID: 715 RVA: 0x0000C7E3 File Offset: 0x0000A9E3
		[Category("Notifications")]
		[Description("Occurs after autocompleted text has been inserted.")]
		public event EventHandler<AutoCSelectionEventArgs> AutoCCompleted
		{
			add
			{
				base.Events.AddHandler(Scintilla.autoCCompletedEventKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(Scintilla.autoCCompletedEventKey, value);
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060002CC RID: 716 RVA: 0x0000C7F6 File Offset: 0x0000A9F6
		// (remove) Token: 0x060002CD RID: 717 RVA: 0x0000C809 File Offset: 0x0000AA09
		[Category("Notifications")]
		[Description("Occurs when a user has selected an item in an autocompletion list.")]
		public event EventHandler<AutoCSelectionEventArgs> AutoCSelection
		{
			add
			{
				base.Events.AddHandler(Scintilla.autoCSelectionEventKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(Scintilla.autoCSelectionEventKey, value);
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060002CE RID: 718 RVA: 0x0000C81C File Offset: 0x0000AA1C
		// (remove) Token: 0x060002CF RID: 719 RVA: 0x0000C825 File Offset: 0x0000AA25
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackColorChanged
		{
			add
			{
				base.BackColorChanged += value;
			}
			remove
			{
				base.BackColorChanged -= value;
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060002D0 RID: 720 RVA: 0x0000C82E File Offset: 0x0000AA2E
		// (remove) Token: 0x060002D1 RID: 721 RVA: 0x0000C837 File Offset: 0x0000AA37
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageChanged
		{
			add
			{
				base.BackgroundImageChanged += value;
			}
			remove
			{
				base.BackgroundImageChanged -= value;
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060002D2 RID: 722 RVA: 0x0000C840 File Offset: 0x0000AA40
		// (remove) Token: 0x060002D3 RID: 723 RVA: 0x0000C849 File Offset: 0x0000AA49
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageLayoutChanged
		{
			add
			{
				base.BackgroundImageLayoutChanged += value;
			}
			remove
			{
				base.BackgroundImageLayoutChanged -= value;
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060002D4 RID: 724 RVA: 0x0000C852 File Offset: 0x0000AA52
		// (remove) Token: 0x060002D5 RID: 725 RVA: 0x0000C865 File Offset: 0x0000AA65
		[Category("Notifications")]
		[Description("Occurs before text is deleted.")]
		public event EventHandler<BeforeModificationEventArgs> BeforeDelete
		{
			add
			{
				base.Events.AddHandler(Scintilla.beforeDeleteEventKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(Scintilla.beforeDeleteEventKey, value);
			}
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060002D6 RID: 726 RVA: 0x0000C878 File Offset: 0x0000AA78
		// (remove) Token: 0x060002D7 RID: 727 RVA: 0x0000C88B File Offset: 0x0000AA8B
		[Category("Notifications")]
		[Description("Occurs before text is inserted.")]
		public event EventHandler<BeforeModificationEventArgs> BeforeInsert
		{
			add
			{
				base.Events.AddHandler(Scintilla.beforeInsertEventKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(Scintilla.beforeInsertEventKey, value);
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060002D8 RID: 728 RVA: 0x0000C89E File Offset: 0x0000AA9E
		// (remove) Token: 0x060002D9 RID: 729 RVA: 0x0000C8B1 File Offset: 0x0000AAB1
		[Category("Property Changed")]
		[Description("Occurs when the value of the BorderStyle property changes.")]
		public event EventHandler BorderStyleChanged
		{
			add
			{
				base.Events.AddHandler(Scintilla.borderStyleChangedEventKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(Scintilla.borderStyleChangedEventKey, value);
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060002DA RID: 730 RVA: 0x0000C8C4 File Offset: 0x0000AAC4
		// (remove) Token: 0x060002DB RID: 731 RVA: 0x0000C8D7 File Offset: 0x0000AAD7
		[Category("Notifications")]
		[Description("Occurs when an annotation has changed.")]
		public event EventHandler<ChangeAnnotationEventArgs> ChangeAnnotation
		{
			add
			{
				base.Events.AddHandler(Scintilla.changeAnnotationEventKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(Scintilla.changeAnnotationEventKey, value);
			}
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060002DC RID: 732 RVA: 0x0000C8EA File Offset: 0x0000AAEA
		// (remove) Token: 0x060002DD RID: 733 RVA: 0x0000C8FD File Offset: 0x0000AAFD
		[Category("Notifications")]
		[Description("Occurs when the user types a character.")]
		public event EventHandler<CharAddedEventArgs> CharAdded
		{
			add
			{
				base.Events.AddHandler(Scintilla.charAddedEventKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(Scintilla.charAddedEventKey, value);
			}
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060002DE RID: 734 RVA: 0x0000C910 File Offset: 0x0000AB10
		// (remove) Token: 0x060002DF RID: 735 RVA: 0x0000C919 File Offset: 0x0000AB19
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler CursorChanged
		{
			add
			{
				base.CursorChanged += value;
			}
			remove
			{
				base.CursorChanged -= value;
			}
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060002E0 RID: 736 RVA: 0x0000C922 File Offset: 0x0000AB22
		// (remove) Token: 0x060002E1 RID: 737 RVA: 0x0000C935 File Offset: 0x0000AB35
		[Category("Notifications")]
		[Description("Occurs when text is deleted.")]
		public event EventHandler<ModificationEventArgs> Delete
		{
			add
			{
				base.Events.AddHandler(Scintilla.deleteEventKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(Scintilla.deleteEventKey, value);
			}
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060002E2 RID: 738 RVA: 0x0000C948 File Offset: 0x0000AB48
		// (remove) Token: 0x060002E3 RID: 739 RVA: 0x0000C95B File Offset: 0x0000AB5B
		[Category("Notifications")]
		[Description("Occurs when the editor is double clicked.")]
		public new event EventHandler<DoubleClickEventArgs> DoubleClick
		{
			add
			{
				base.Events.AddHandler(Scintilla.doubleClickEventKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(Scintilla.doubleClickEventKey, value);
			}
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x060002E4 RID: 740 RVA: 0x0000C96E File Offset: 0x0000AB6E
		// (remove) Token: 0x060002E5 RID: 741 RVA: 0x0000C981 File Offset: 0x0000AB81
		[Category("Notifications")]
		[Description("Occurs when the mouse moves from its dwell start position.")]
		public event EventHandler<DwellEventArgs> DwellEnd
		{
			add
			{
				base.Events.AddHandler(Scintilla.dwellEndEventKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(Scintilla.dwellEndEventKey, value);
			}
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060002E6 RID: 742 RVA: 0x0000C994 File Offset: 0x0000AB94
		// (remove) Token: 0x060002E7 RID: 743 RVA: 0x0000C9A7 File Offset: 0x0000ABA7
		[Category("Notifications")]
		[Description("Occurs when the mouse is kept in one position (hovers) for a period of time.")]
		public event EventHandler<DwellEventArgs> DwellStart
		{
			add
			{
				base.Events.AddHandler(Scintilla.dwellStartEventKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(Scintilla.dwellStartEventKey, value);
			}
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060002E8 RID: 744 RVA: 0x0000C9BA File Offset: 0x0000ABBA
		// (remove) Token: 0x060002E9 RID: 745 RVA: 0x0000C9C3 File Offset: 0x0000ABC3
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler FontChanged
		{
			add
			{
				base.FontChanged += value;
			}
			remove
			{
				base.FontChanged -= value;
			}
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060002EA RID: 746 RVA: 0x0000C9CC File Offset: 0x0000ABCC
		// (remove) Token: 0x060002EB RID: 747 RVA: 0x0000C9D5 File Offset: 0x0000ABD5
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ForeColorChanged
		{
			add
			{
				base.ForeColorChanged += value;
			}
			remove
			{
				base.ForeColorChanged -= value;
			}
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x060002EC RID: 748 RVA: 0x0000C9DE File Offset: 0x0000ABDE
		// (remove) Token: 0x060002ED RID: 749 RVA: 0x0000C9F1 File Offset: 0x0000ABF1
		[Category("Notifications")]
		[Description("Occurs when the user clicks text styled with the hotspot flag.")]
		public event EventHandler<HotspotClickEventArgs> HotspotClick
		{
			add
			{
				base.Events.AddHandler(Scintilla.hotspotClickEventKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(Scintilla.hotspotClickEventKey, value);
			}
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x060002EE RID: 750 RVA: 0x0000CA04 File Offset: 0x0000AC04
		// (remove) Token: 0x060002EF RID: 751 RVA: 0x0000CA17 File Offset: 0x0000AC17
		[Category("Notifications")]
		[Description("Occurs when the user double clicks text styled with the hotspot flag.")]
		public event EventHandler<HotspotClickEventArgs> HotspotDoubleClick
		{
			add
			{
				base.Events.AddHandler(Scintilla.hotspotDoubleClickEventKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(Scintilla.hotspotDoubleClickEventKey, value);
			}
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x060002F0 RID: 752 RVA: 0x0000CA2A File Offset: 0x0000AC2A
		// (remove) Token: 0x060002F1 RID: 753 RVA: 0x0000CA3D File Offset: 0x0000AC3D
		[Category("Notifications")]
		[Description("Occurs when the user releases a click on text styled with the hotspot flag.")]
		public event EventHandler<HotspotClickEventArgs> HotspotReleaseClick
		{
			add
			{
				base.Events.AddHandler(Scintilla.hotspotReleaseClickEventKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(Scintilla.hotspotReleaseClickEventKey, value);
			}
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x060002F2 RID: 754 RVA: 0x0000CA50 File Offset: 0x0000AC50
		// (remove) Token: 0x060002F3 RID: 755 RVA: 0x0000CA63 File Offset: 0x0000AC63
		[Category("Notifications")]
		[Description("Occurs when the user clicks text with an indicator.")]
		public event EventHandler<IndicatorClickEventArgs> IndicatorClick
		{
			add
			{
				base.Events.AddHandler(Scintilla.indicatorClickEventKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(Scintilla.indicatorClickEventKey, value);
			}
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x060002F4 RID: 756 RVA: 0x0000CA76 File Offset: 0x0000AC76
		// (remove) Token: 0x060002F5 RID: 757 RVA: 0x0000CA89 File Offset: 0x0000AC89
		[Category("Notifications")]
		[Description("Occurs when the user releases a click on text with an indicator.")]
		public event EventHandler<IndicatorReleaseEventArgs> IndicatorRelease
		{
			add
			{
				base.Events.AddHandler(Scintilla.indicatorReleaseEventKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(Scintilla.indicatorReleaseEventKey, value);
			}
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x060002F6 RID: 758 RVA: 0x0000CA9C File Offset: 0x0000AC9C
		// (remove) Token: 0x060002F7 RID: 759 RVA: 0x0000CAAF File Offset: 0x0000ACAF
		[Category("Notifications")]
		[Description("Occurs when text is inserted.")]
		public event EventHandler<ModificationEventArgs> Insert
		{
			add
			{
				base.Events.AddHandler(Scintilla.insertEventKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(Scintilla.insertEventKey, value);
			}
		}

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x060002F8 RID: 760 RVA: 0x0000CAC2 File Offset: 0x0000ACC2
		// (remove) Token: 0x060002F9 RID: 761 RVA: 0x0000CAD5 File Offset: 0x0000ACD5
		[Category("Notifications")]
		[Description("Occurs before text is inserted. Permits changing the inserted text.")]
		public event EventHandler<InsertCheckEventArgs> InsertCheck
		{
			add
			{
				base.Events.AddHandler(Scintilla.insertCheckEventKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(Scintilla.insertCheckEventKey, value);
			}
		}

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x060002FA RID: 762 RVA: 0x0000CAE8 File Offset: 0x0000ACE8
		// (remove) Token: 0x060002FB RID: 763 RVA: 0x0000CAFB File Offset: 0x0000ACFB
		[Category("Notifications")]
		[Description("Occurs when the mouse is clicked in a sensitive margin.")]
		public event EventHandler<MarginClickEventArgs> MarginClick
		{
			add
			{
				base.Events.AddHandler(Scintilla.marginClickEventKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(Scintilla.marginClickEventKey, value);
			}
		}

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x060002FC RID: 764 RVA: 0x0000CB0E File Offset: 0x0000AD0E
		// (remove) Token: 0x060002FD RID: 765 RVA: 0x0000CB21 File Offset: 0x0000AD21
		[Category("Notifications")]
		[Description("Occurs when the mouse is right-clicked in a sensitive margin.")]
		public event EventHandler<MarginClickEventArgs> MarginRightClick
		{
			add
			{
				base.Events.AddHandler(Scintilla.marginRightClickEventKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(Scintilla.marginRightClickEventKey, value);
			}
		}

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x060002FE RID: 766 RVA: 0x0000CB34 File Offset: 0x0000AD34
		// (remove) Token: 0x060002FF RID: 767 RVA: 0x0000CB47 File Offset: 0x0000AD47
		[Category("Notifications")]
		[Description("Occurs when an attempt is made to change text in read-only mode.")]
		public event EventHandler<EventArgs> ModifyAttempt
		{
			add
			{
				base.Events.AddHandler(Scintilla.modifyAttemptEventKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(Scintilla.modifyAttemptEventKey, value);
			}
		}

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06000300 RID: 768 RVA: 0x0000CB5A File Offset: 0x0000AD5A
		// (remove) Token: 0x06000301 RID: 769 RVA: 0x0000CB6D File Offset: 0x0000AD6D
		[Category("Notifications")]
		[Description("Occurs when hidden (folded) text should be shown.")]
		public event EventHandler<NeedShownEventArgs> NeedShown
		{
			add
			{
				base.Events.AddHandler(Scintilla.needShownEventKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(Scintilla.needShownEventKey, value);
			}
		}

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06000302 RID: 770 RVA: 0x0000CB80 File Offset: 0x0000AD80
		// (remove) Token: 0x06000303 RID: 771 RVA: 0x0000CB93 File Offset: 0x0000AD93
		internal event EventHandler<SCNotificationEventArgs> SCNotification
		{
			add
			{
				base.Events.AddHandler(Scintilla.scNotificationEventKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(Scintilla.scNotificationEventKey, value);
			}
		}

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06000304 RID: 772 RVA: 0x0000CBA6 File Offset: 0x0000ADA6
		// (remove) Token: 0x06000305 RID: 773 RVA: 0x0000CBAF File Offset: 0x0000ADAF
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event PaintEventHandler Paint
		{
			add
			{
				base.Paint += value;
			}
			remove
			{
				base.Paint -= value;
			}
		}

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06000306 RID: 774 RVA: 0x0000CBB8 File Offset: 0x0000ADB8
		// (remove) Token: 0x06000307 RID: 775 RVA: 0x0000CBCB File Offset: 0x0000ADCB
		[Category("Notifications")]
		[Description("Occurs when the control is painted.")]
		public event EventHandler<EventArgs> Painted
		{
			add
			{
				base.Events.AddHandler(Scintilla.paintedEventKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(Scintilla.paintedEventKey, value);
			}
		}

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06000308 RID: 776 RVA: 0x0000CBDE File Offset: 0x0000ADDE
		// (remove) Token: 0x06000309 RID: 777 RVA: 0x0000CBF1 File Offset: 0x0000ADF1
		[Category("Notifications")]
		[Description("Occurs when a save point is left and the document becomes dirty.")]
		public event EventHandler<EventArgs> SavePointLeft
		{
			add
			{
				base.Events.AddHandler(Scintilla.savePointLeftEventKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(Scintilla.savePointLeftEventKey, value);
			}
		}

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x0600030A RID: 778 RVA: 0x0000CC04 File Offset: 0x0000AE04
		// (remove) Token: 0x0600030B RID: 779 RVA: 0x0000CC17 File Offset: 0x0000AE17
		[Category("Notifications")]
		[Description("Occurs when a save point is reached and the document is no longer dirty.")]
		public event EventHandler<EventArgs> SavePointReached
		{
			add
			{
				base.Events.AddHandler(Scintilla.savePointReachedEventKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(Scintilla.savePointReachedEventKey, value);
			}
		}

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x0600030C RID: 780 RVA: 0x0000CC2A File Offset: 0x0000AE2A
		// (remove) Token: 0x0600030D RID: 781 RVA: 0x0000CC3D File Offset: 0x0000AE3D
		[Category("Notifications")]
		[Description("Occurs when the text needs styling.")]
		public event EventHandler<StyleNeededEventArgs> StyleNeeded
		{
			add
			{
				base.Events.AddHandler(Scintilla.styleNeededEventKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(Scintilla.styleNeededEventKey, value);
			}
		}

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x0600030E RID: 782 RVA: 0x0000CC50 File Offset: 0x0000AE50
		// (remove) Token: 0x0600030F RID: 783 RVA: 0x0000CC63 File Offset: 0x0000AE63
		[Category("Notifications")]
		[Description("Occurs when the control UI is updated.")]
		public event EventHandler<UpdateUIEventArgs> UpdateUI
		{
			add
			{
				base.Events.AddHandler(Scintilla.updateUIEventKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(Scintilla.updateUIEventKey, value);
			}
		}

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06000310 RID: 784 RVA: 0x0000CC76 File Offset: 0x0000AE76
		// (remove) Token: 0x06000311 RID: 785 RVA: 0x0000CC89 File Offset: 0x0000AE89
		[Category("Notifications")]
		[Description("Occurs when the control is zoomed.")]
		public event EventHandler<EventArgs> ZoomChanged
		{
			add
			{
				base.Events.AddHandler(Scintilla.zoomChangedEventKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(Scintilla.zoomChangedEventKey, value);
			}
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000CC9C File Offset: 0x0000AE9C
		public Scintilla()
		{
			if (Scintilla.reparentAll == null || Scintilla.reparentAll.Value)
			{
				this.reparent = true;
			}
			base.SetStyle(ControlStyles.CacheText, true);
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.StandardClick | ControlStyles.StandardDoubleClick | ControlStyles.UseTextForAccessibility, false);
			this.borderStyle = BorderStyle.Fixed3D;
			this.Lines = new LineCollection(this);
			this.Styles = new StyleCollection(this);
			this.Indicators = new IndicatorCollection(this);
			this.Margins = new MarginCollection(this);
			this.Markers = new MarkerCollection(this);
			this.Selections = new SelectionCollection(this);
		}

		// Token: 0x040007A1 RID: 1953
		private static bool? reparentAll;

		// Token: 0x040007A2 RID: 1954
		private bool reparent;

		// Token: 0x040007A3 RID: 1955
		private static string modulePath;

		// Token: 0x040007A4 RID: 1956
		private static IntPtr moduleHandle;

		// Token: 0x040007A5 RID: 1957
		private static NativeMethods.Scintilla_DirectFunction directFunction;

		// Token: 0x040007A6 RID: 1958
		private static readonly object scNotificationEventKey = new object();

		// Token: 0x040007A7 RID: 1959
		private static readonly object insertCheckEventKey = new object();

		// Token: 0x040007A8 RID: 1960
		private static readonly object beforeInsertEventKey = new object();

		// Token: 0x040007A9 RID: 1961
		private static readonly object beforeDeleteEventKey = new object();

		// Token: 0x040007AA RID: 1962
		private static readonly object insertEventKey = new object();

		// Token: 0x040007AB RID: 1963
		private static readonly object deleteEventKey = new object();

		// Token: 0x040007AC RID: 1964
		private static readonly object updateUIEventKey = new object();

		// Token: 0x040007AD RID: 1965
		private static readonly object modifyAttemptEventKey = new object();

		// Token: 0x040007AE RID: 1966
		private static readonly object styleNeededEventKey = new object();

		// Token: 0x040007AF RID: 1967
		private static readonly object savePointReachedEventKey = new object();

		// Token: 0x040007B0 RID: 1968
		private static readonly object savePointLeftEventKey = new object();

		// Token: 0x040007B1 RID: 1969
		private static readonly object changeAnnotationEventKey = new object();

		// Token: 0x040007B2 RID: 1970
		private static readonly object marginClickEventKey = new object();

		// Token: 0x040007B3 RID: 1971
		private static readonly object marginRightClickEventKey = new object();

		// Token: 0x040007B4 RID: 1972
		private static readonly object charAddedEventKey = new object();

		// Token: 0x040007B5 RID: 1973
		private static readonly object autoCSelectionEventKey = new object();

		// Token: 0x040007B6 RID: 1974
		private static readonly object autoCCompletedEventKey = new object();

		// Token: 0x040007B7 RID: 1975
		private static readonly object autoCCancelledEventKey = new object();

		// Token: 0x040007B8 RID: 1976
		private static readonly object autoCCharDeletedEventKey = new object();

		// Token: 0x040007B9 RID: 1977
		private static readonly object dwellStartEventKey = new object();

		// Token: 0x040007BA RID: 1978
		private static readonly object dwellEndEventKey = new object();

		// Token: 0x040007BB RID: 1979
		private static readonly object borderStyleChangedEventKey = new object();

		// Token: 0x040007BC RID: 1980
		private static readonly object doubleClickEventKey = new object();

		// Token: 0x040007BD RID: 1981
		private static readonly object paintedEventKey = new object();

		// Token: 0x040007BE RID: 1982
		private static readonly object needShownEventKey = new object();

		// Token: 0x040007BF RID: 1983
		private static readonly object hotspotClickEventKey = new object();

		// Token: 0x040007C0 RID: 1984
		private static readonly object hotspotDoubleClickEventKey = new object();

		// Token: 0x040007C1 RID: 1985
		private static readonly object hotspotReleaseClickEventKey = new object();

		// Token: 0x040007C2 RID: 1986
		private static readonly object indicatorClickEventKey = new object();

		// Token: 0x040007C3 RID: 1987
		private static readonly object indicatorReleaseEventKey = new object();

		// Token: 0x040007C4 RID: 1988
		private static readonly object zoomChangedEventKey = new object();

		// Token: 0x040007C5 RID: 1989
		private IntPtr sciPtr;

		// Token: 0x040007C6 RID: 1990
		private BorderStyle borderStyle;

		// Token: 0x040007C7 RID: 1991
		private int stylingPosition;

		// Token: 0x040007C8 RID: 1992
		private int stylingBytePosition;

		// Token: 0x040007C9 RID: 1993
		private int? cachedPosition;

		// Token: 0x040007CA RID: 1994
		private string cachedText;

		// Token: 0x040007CB RID: 1995
		private bool doubleClick;

		// Token: 0x040007CC RID: 1996
		private IntPtr fillUpChars;

		// Token: 0x040007CD RID: 1997
		private string lastCallTip = string.Empty;

		// Token: 0x040007CE RID: 1998
		public const int TimeForever = 10000000;

		// Token: 0x040007CF RID: 1999
		public const int InvalidPosition = -1;
	}
}
