using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ScintillaNET
{
	// Token: 0x02000025 RID: 37
	public class LineCollection : IEnumerable<Line>, IEnumerable
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x0000621C File Offset: 0x0000441C
		private void AdjustLineLength(int index, int delta)
		{
			this.MoveStep(index);
			this.stepLength += delta;
			LineCollection.PerLine value = this.perLineData[index];
			value.ContainsMultibyte = LineCollection.ContainsMultibyte.Unkown;
			this.perLineData[index] = value;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00006260 File Offset: 0x00004460
		internal int ByteToCharPosition(int pos)
		{
			int num = this.scintilla.DirectMessage(2166, new IntPtr(pos)).ToInt32();
			int num2 = this.scintilla.DirectMessage(2167, new IntPtr(num)).ToInt32();
			return this.CharPositionFromLine(num) + this.GetCharCount(num2, pos - num2);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000062C0 File Offset: 0x000044C0
		internal int CharLineLength(int index)
		{
			if (index + 1 <= this.stepLine)
			{
				return this.perLineData[index + 1].Start - this.perLineData[index].Start;
			}
			if (index <= this.stepLine)
			{
				return this.perLineData[index + 1].Start + this.stepLength - this.perLineData[index].Start;
			}
			return this.perLineData[index + 1].Start + this.stepLength - (this.perLineData[index].Start + this.stepLength);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00006368 File Offset: 0x00004568
		internal int CharPositionFromLine(int index)
		{
			int num = this.perLineData[index].Start;
			if (index > this.stepLine)
			{
				num += this.stepLength;
			}
			return num;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000639C File Offset: 0x0000459C
		internal int CharToBytePosition(int pos)
		{
			int num = this.LineFromCharPosition(pos);
			int num2 = this.scintilla.DirectMessage(2167, new IntPtr(num)).ToInt32();
			pos -= this.CharPositionFromLine(num);
			if (!this.LineContainsMultibyteChar(num))
			{
				return num2 + pos;
			}
			while (pos > 0)
			{
				num2 = this.scintilla.DirectMessage(2670, new IntPtr(num2), new IntPtr(1)).ToInt32();
				pos--;
			}
			return num2;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00006417 File Offset: 0x00004617
		private void DeletePerLine(int index)
		{
			this.MoveStep(index);
			this.stepLength -= this.CharLineLength(index);
			this.perLineData.RemoveAt(index);
			this.stepLine--;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000644E File Offset: 0x0000464E
		private int GetCharCount(int pos, int length)
		{
			return LineCollection.GetCharCount(this.scintilla.DirectMessage(2643, new IntPtr(pos), new IntPtr(length)), length, this.scintilla.Encoding);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000647D File Offset: 0x0000467D
		private unsafe static int GetCharCount(IntPtr text, int length, Encoding encoding)
		{
			if (text == IntPtr.Zero || length == 0)
			{
				return 0;
			}
			return encoding.GetCharCount((byte*)((void*)text), length);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000649E File Offset: 0x0000469E
		public IEnumerator<Line> GetEnumerator()
		{
			int count = this.Count;
			int num;
			for (int i = 0; i < count; i = num + 1)
			{
				yield return this[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000064AD File Offset: 0x000046AD
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000064B8 File Offset: 0x000046B8
		private bool LineContainsMultibyteChar(int index)
		{
			LineCollection.PerLine perLine = this.perLineData[index];
			if (perLine.ContainsMultibyte == LineCollection.ContainsMultibyte.Unkown)
			{
				perLine.ContainsMultibyte = ((this.scintilla.DirectMessage(2350, new IntPtr(index)).ToInt32() == this.CharLineLength(index)) ? LineCollection.ContainsMultibyte.No : LineCollection.ContainsMultibyte.Yes);
				this.perLineData[index] = perLine;
			}
			return perLine.ContainsMultibyte == LineCollection.ContainsMultibyte.Yes;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00006524 File Offset: 0x00004724
		internal int LineFromCharPosition(int pos)
		{
			int i = 0;
			int num = this.Count - 1;
			while (i <= num)
			{
				int num2 = i + (num - i) / 2;
				int num3 = this.CharPositionFromLine(num2);
				if (pos == num3)
				{
					return num2;
				}
				if (num3 < pos)
				{
					i = num2 + 1;
				}
				else
				{
					num = num2 - 1;
				}
			}
			return i - 1;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000656C File Offset: 0x0000476C
		private void InsertPerLine(int index, int length = 0)
		{
			this.MoveStep(index);
			LineCollection.PerLine perLine = this.perLineData[index];
			int start = perLine.Start;
			perLine.Start += length;
			this.perLineData[index] = perLine;
			perLine = new LineCollection.PerLine
			{
				Start = start
			};
			this.perLineData.Insert(index, perLine);
			this.stepLength += length;
			this.stepLine++;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000065EC File Offset: 0x000047EC
		private void MoveStep(int line)
		{
			if (this.stepLength == 0)
			{
				this.stepLine = line;
				return;
			}
			if (this.stepLine < line)
			{
				while (this.stepLine < line)
				{
					this.stepLine++;
					LineCollection.PerLine value = this.perLineData[this.stepLine];
					value.Start += this.stepLength;
					this.perLineData[this.stepLine] = value;
				}
				return;
			}
			if (this.stepLine > line)
			{
				while (this.stepLine > line)
				{
					LineCollection.PerLine value2 = this.perLineData[this.stepLine];
					value2.Start -= this.stepLength;
					this.perLineData[this.stepLine] = value2;
					this.stepLine--;
				}
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000066B8 File Offset: 0x000048B8
		internal void RebuildLineData()
		{
			this.stepLine = 0;
			this.stepLength = 0;
			this.perLineData = new GapBuffer<LineCollection.PerLine>(0);
			this.perLineData.Add(new LineCollection.PerLine
			{
				Start = 0
			});
			this.perLineData.Add(new LineCollection.PerLine
			{
				Start = 0
			});
			NativeMethods.SCNotification scnotification = default(NativeMethods.SCNotification);
			scnotification.linesAdded = this.scintilla.DirectMessage(2154).ToInt32() - 1;
			scnotification.position = 0;
			scnotification.length = this.scintilla.DirectMessage(2006).ToInt32();
			scnotification.text = this.scintilla.DirectMessage(2643, new IntPtr(scnotification.position), new IntPtr(scnotification.length));
			this.TrackInsertText(scnotification);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000679C File Offset: 0x0000499C
		private void scintilla_SCNotification(object sender, SCNotificationEventArgs e)
		{
			NativeMethods.SCNotification scnotification = e.SCNotification;
			int code = scnotification.nmhdr.code;
			if (code == 2008)
			{
				this.ScnModified(scnotification);
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000067CB File Offset: 0x000049CB
		private void ScnModified(NativeMethods.SCNotification scn)
		{
			if ((scn.modificationType & 2) > 0)
			{
				this.TrackDeleteText(scn);
			}
			if ((scn.modificationType & 1) > 0)
			{
				this.TrackInsertText(scn);
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000067F4 File Offset: 0x000049F4
		private void TrackDeleteText(NativeMethods.SCNotification scn)
		{
			int num = this.scintilla.DirectMessage(2166, new IntPtr(scn.position)).ToInt32();
			if (scn.linesAdded == 0)
			{
				int charCount = LineCollection.GetCharCount(scn.text, scn.length, this.scintilla.Encoding);
				this.AdjustLineLength(num, charCount * -1);
				return;
			}
			int pos = this.scintilla.DirectMessage(2167, new IntPtr(num)).ToInt32();
			int length = this.scintilla.DirectMessage(2350, new IntPtr(num)).ToInt32();
			this.AdjustLineLength(num, this.GetCharCount(pos, length) - this.CharLineLength(num));
			int num2 = scn.linesAdded * -1;
			for (int i = 0; i < num2; i++)
			{
				this.DeletePerLine(num + 1);
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000068D0 File Offset: 0x00004AD0
		private void TrackInsertText(NativeMethods.SCNotification scn)
		{
			int num = this.scintilla.DirectMessage(2166, new IntPtr(scn.position)).ToInt32();
			if (scn.linesAdded == 0)
			{
				int charCount = this.GetCharCount(scn.position, scn.length);
				this.AdjustLineLength(num, charCount);
				return;
			}
			int num2 = this.scintilla.DirectMessage(2167, new IntPtr(num)).ToInt32();
			int num3 = this.scintilla.DirectMessage(2350, new IntPtr(num)).ToInt32();
			this.AdjustLineLength(num, this.GetCharCount(num2, num3) - this.CharLineLength(num));
			for (int i = 1; i <= scn.linesAdded; i++)
			{
				int num4 = num + i;
				num2 += num3;
				num3 = this.scintilla.DirectMessage(2350, new IntPtr(num4)).ToInt32();
				this.InsertPerLine(num4, this.GetCharCount(num2, num3));
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000BA RID: 186 RVA: 0x000069D3 File Offset: 0x00004BD3
		public bool AllLinesVisible
		{
			get
			{
				return this.scintilla.DirectMessage(2236) != IntPtr.Zero;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000069EF File Offset: 0x00004BEF
		public int Count
		{
			get
			{
				return this.perLineData.Count - 1;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000BC RID: 188 RVA: 0x000069FE File Offset: 0x00004BFE
		internal int TextLength
		{
			get
			{
				return this.CharPositionFromLine(this.perLineData.Count - 1);
			}
		}

		// Token: 0x17000040 RID: 64
		public Line this[int index]
		{
			get
			{
				index = Helpers.Clamp(index, 0, this.Count - 1);
				return new Line(this.scintilla, index);
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00006A34 File Offset: 0x00004C34
		public LineCollection(Scintilla scintilla)
		{
			this.scintilla = scintilla;
			this.scintilla.SCNotification += this.scintilla_SCNotification;
			this.perLineData = new GapBuffer<LineCollection.PerLine>(0);
			this.perLineData.Add(new LineCollection.PerLine
			{
				Start = 0
			});
			this.perLineData.Add(new LineCollection.PerLine
			{
				Start = 0
			});
		}

		// Token: 0x04000113 RID: 275
		private readonly Scintilla scintilla;

		// Token: 0x04000114 RID: 276
		private GapBuffer<LineCollection.PerLine> perLineData;

		// Token: 0x04000115 RID: 277
		private int stepLine;

		// Token: 0x04000116 RID: 278
		private int stepLength;

		// Token: 0x0200005C RID: 92
		private struct PerLine
		{
			// Token: 0x04000841 RID: 2113
			public int Start;

			// Token: 0x04000842 RID: 2114
			public LineCollection.ContainsMultibyte ContainsMultibyte;
		}

		// Token: 0x0200005D RID: 93
		private enum ContainsMultibyte
		{
			// Token: 0x04000844 RID: 2116
			No = -1,
			// Token: 0x04000845 RID: 2117
			Unkown,
			// Token: 0x04000846 RID: 2118
			Yes
		}
	}
}
