using System;
using System.IO;

namespace ScintillaNET
{
	// Token: 0x02000040 RID: 64
	public class ScintillaReader : TextReader
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00007618 File Offset: 0x00005818
		private int BufferRemaining
		{
			get
			{
				if (this._data == null)
				{
					return 0;
				}
				return this._data.Length - this._dataIndex;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00007636 File Offset: 0x00005836
		private int UnbufferedRemaining
		{
			get
			{
				return this._lastData - this._nextData;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00007645 File Offset: 0x00005845
		private int TotalRemaining
		{
			get
			{
				return this.BufferRemaining + this.UnbufferedRemaining;
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00007654 File Offset: 0x00005854
		public ScintillaReader(Scintilla scintilla) : this(scintilla, 0, scintilla.TextLength)
		{
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00007664 File Offset: 0x00005864
		public ScintillaReader(Scintilla scintilla, int bufferSize) : this(scintilla, 0, scintilla.TextLength, bufferSize)
		{
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00007675 File Offset: 0x00005875
		public ScintillaReader(Scintilla scintilla, int start, int end) : this(scintilla, start, end, 256)
		{
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00007685 File Offset: 0x00005885
		public ScintillaReader(Scintilla scintilla, int start, int end, int bufferSize)
		{
			this._scintilla = scintilla;
			this._bufferSize = ((bufferSize > 0) ? bufferSize : 256);
			this._nextData = start;
			this._lastData = end;
			this.BufferNextRegion();
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000076BC File Offset: 0x000058BC
		public override int Peek()
		{
			if (this._data == null)
			{
				return -1;
			}
			return (int)this._data[this._dataIndex];
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000076DC File Offset: 0x000058DC
		public override int Read()
		{
			if (this._data != null)
			{
				string data = this._data;
				int dataIndex = this._dataIndex;
				this._dataIndex = dataIndex + 1;
				int result = (int)data[dataIndex];
				if (this._dataIndex >= this._data.Length)
				{
					this.BufferNextRegion();
				}
				return result;
			}
			return -1;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00007728 File Offset: 0x00005928
		public override int Read(char[] buffer, int index, int count)
		{
			return this.ReadBlock(buffer, index, count);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00007734 File Offset: 0x00005934
		public override int ReadBlock(char[] buffer, int index, int count)
		{
			if (this._data == null)
			{
				return 0;
			}
			int bufferRemaining = this.BufferRemaining;
			if (count < bufferRemaining)
			{
				this._data.CopyTo(this._dataIndex, buffer, index, count);
				return count;
			}
			this._data.CopyTo(this._dataIndex, buffer, index, bufferRemaining);
			if (count > bufferRemaining)
			{
				string textRange = this._scintilla.GetTextRange(this._nextData, Math.Min(count - bufferRemaining, this.UnbufferedRemaining));
				textRange.CopyTo(0, buffer, index + bufferRemaining, textRange.Length);
				count = bufferRemaining + textRange.Length;
				this._nextData += textRange.Length;
			}
			this.BufferNextRegion();
			return count;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000077DC File Offset: 0x000059DC
		private void BufferNextRegion()
		{
			if (this._nextData < this._lastData)
			{
				int length = Math.Min(this._lastData - this._nextData, this._bufferSize);
				this._data = this._scintilla.GetTextRange(this._nextData, length);
				this._nextData += this._data.Length;
				this._dataIndex = 0;
				return;
			}
			this._data = null;
		}

		// Token: 0x04000799 RID: 1945
		private const int DefaultBufferSize = 256;

		// Token: 0x0400079A RID: 1946
		private Scintilla _scintilla;

		// Token: 0x0400079B RID: 1947
		private int _bufferSize;

		// Token: 0x0400079C RID: 1948
		private string _data;

		// Token: 0x0400079D RID: 1949
		private int _dataIndex;

		// Token: 0x0400079E RID: 1950
		private int _nextData;

		// Token: 0x0400079F RID: 1951
		private int _lastData;
	}
}
