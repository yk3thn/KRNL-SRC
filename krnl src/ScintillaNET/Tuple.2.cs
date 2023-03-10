using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace ScintillaNET
{
	// Token: 0x0200004E RID: 78
	[DebuggerDisplay("Item1={Item1};Item2={Item2}")]
	internal class Tuple<T1, T2> : IFormattable
	{
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600034F RID: 847 RVA: 0x0000D9DD File Offset: 0x0000BBDD
		// (set) Token: 0x06000350 RID: 848 RVA: 0x0000D9E5 File Offset: 0x0000BBE5
		public T1 Item1 { get; private set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000351 RID: 849 RVA: 0x0000D9EE File Offset: 0x0000BBEE
		// (set) Token: 0x06000352 RID: 850 RVA: 0x0000D9F6 File Offset: 0x0000BBF6
		public T2 Item2 { get; private set; }

		// Token: 0x06000353 RID: 851 RVA: 0x0000D9FF File Offset: 0x0000BBFF
		public Tuple(T1 item1, T2 item2)
		{
			this.Item1 = item1;
			this.Item2 = item2;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000DA18 File Offset: 0x0000BC18
		public override int GetHashCode()
		{
			int num = 0;
			if (this.Item1 != null)
			{
				num = Tuple<T1, T2>.Item1Comparer.GetHashCode(this.Item1);
			}
			if (this.Item2 != null)
			{
				num = (num << 3 ^ Tuple<T1, T2>.Item2Comparer.GetHashCode(this.Item2));
			}
			return num;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000DA68 File Offset: 0x0000BC68
		public override bool Equals(object obj)
		{
			Tuple<T1, T2> tuple = obj as Tuple<T1, T2>;
			return tuple != null && Tuple<T1, T2>.Item1Comparer.Equals(this.Item1, tuple.Item1) && Tuple<T1, T2>.Item2Comparer.Equals(this.Item2, tuple.Item2);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000DAB1 File Offset: 0x0000BCB1
		public override string ToString()
		{
			return this.ToString(null, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000DABF File Offset: 0x0000BCBF
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format(formatProvider, format ?? "{0},{1}", new object[]
			{
				this.Item1,
				this.Item2
			});
		}

		// Token: 0x04000800 RID: 2048
		private static readonly IEqualityComparer<T1> Item1Comparer = EqualityComparer<T1>.Default;

		// Token: 0x04000801 RID: 2049
		private static readonly IEqualityComparer<T2> Item2Comparer = EqualityComparer<T2>.Default;
	}
}
