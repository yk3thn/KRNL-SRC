using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace ScintillaNET
{
	// Token: 0x02000016 RID: 22
	[DebuggerDisplay("Count = {Count}")]
	internal sealed class GapBuffer<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x0600002B RID: 43 RVA: 0x000024D8 File Offset: 0x000006D8
		public void Add(T item)
		{
			this.Insert(this.Count, item);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000024E7 File Offset: 0x000006E7
		public void AddRange(ICollection<T> collection)
		{
			this.InsertRange(this.Count, collection);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000024F8 File Offset: 0x000006F8
		private void EnsureGapCapacity(int length)
		{
			if (length > this.gapEnd - this.gapStart)
			{
				int num = this.Count + length;
				int num2 = this.buffer.Length * 2;
				if (num2 < num)
				{
					num2 = num;
				}
				T[] array = new T[num2];
				int num3 = array.Length - (this.buffer.Length - this.gapEnd);
				Array.Copy(this.buffer, 0, array, 0, this.gapStart);
				Array.Copy(this.buffer, this.gapEnd, array, num3, array.Length - num3);
				this.buffer = array;
				this.gapEnd = num3;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002584 File Offset: 0x00000784
		public IEnumerator<T> GetEnumerator()
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

		// Token: 0x0600002F RID: 47 RVA: 0x00002593 File Offset: 0x00000793
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000259B File Offset: 0x0000079B
		public void Insert(int index, T item)
		{
			this.PlaceGapStart(index);
			this.EnsureGapCapacity(1);
			this.buffer[index] = item;
			this.gapStart++;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000025C8 File Offset: 0x000007C8
		public void InsertRange(int index, ICollection<T> collection)
		{
			int count = collection.Count;
			if (count > 0)
			{
				this.PlaceGapStart(index);
				this.EnsureGapCapacity(count);
				collection.CopyTo(this.buffer, this.gapStart);
				this.gapStart += count;
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002610 File Offset: 0x00000810
		private void PlaceGapStart(int index)
		{
			if (index != this.gapStart)
			{
				if (this.gapEnd - this.gapStart == 0)
				{
					this.gapStart = index;
					this.gapEnd = index;
					return;
				}
				if (index < this.gapStart)
				{
					int num = this.gapStart - index;
					int length = (this.gapEnd - this.gapStart < num) ? (this.gapEnd - this.gapStart) : num;
					Array.Copy(this.buffer, index, this.buffer, this.gapEnd - num, num);
					this.gapStart -= num;
					this.gapEnd -= num;
					Array.Clear(this.buffer, index, length);
					return;
				}
				int num2 = index - this.gapStart;
				int num3 = (index > this.gapEnd) ? index : this.gapEnd;
				Array.Copy(this.buffer, this.gapEnd, this.buffer, this.gapStart, num2);
				this.gapStart += num2;
				this.gapEnd += num2;
				Array.Clear(this.buffer, num3, this.gapEnd - num3);
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000272C File Offset: 0x0000092C
		public void RemoveAt(int index)
		{
			this.PlaceGapStart(index);
			this.buffer[this.gapEnd] = default(T);
			this.gapEnd++;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002768 File Offset: 0x00000968
		public void RemoveRange(int index, int count)
		{
			if (count > 0)
			{
				this.PlaceGapStart(index);
				Array.Clear(this.buffer, this.gapEnd, count);
				this.gapEnd += count;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002795 File Offset: 0x00000995
		public int Count
		{
			get
			{
				return this.buffer.Length - (this.gapEnd - this.gapStart);
			}
		}

		// Token: 0x17000013 RID: 19
		public T this[int index]
		{
			get
			{
				if (index < this.gapStart)
				{
					return this.buffer[index];
				}
				return this.buffer[index + (this.gapEnd - this.gapStart)];
			}
			set
			{
				if (index >= this.gapStart)
				{
					index += this.gapEnd - this.gapStart;
				}
				this.buffer[index] = value;
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002808 File Offset: 0x00000A08
		public GapBuffer(int capacity = 0)
		{
			this.buffer = new T[capacity];
			this.gapEnd = this.buffer.Length;
		}

		// Token: 0x040000B6 RID: 182
		private T[] buffer;

		// Token: 0x040000B7 RID: 183
		private int gapStart;

		// Token: 0x040000B8 RID: 184
		private int gapEnd;
	}
}
