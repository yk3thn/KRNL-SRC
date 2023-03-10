using System;

namespace ScintillaNET
{
	// Token: 0x02000031 RID: 49
	public struct MarkerHandle
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x00007268 File Offset: 0x00005468
		public override bool Equals(object obj)
		{
			return obj is IntPtr && this.Value == ((MarkerHandle)obj).Value;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000728A File Offset: 0x0000548A
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00007297 File Offset: 0x00005497
		public static bool operator ==(MarkerHandle a, MarkerHandle b)
		{
			return a.Value == b.Value;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000072AA File Offset: 0x000054AA
		public static bool operator !=(MarkerHandle a, MarkerHandle b)
		{
			return a.Value != b.Value;
		}

		// Token: 0x04000146 RID: 326
		internal IntPtr Value;

		// Token: 0x04000147 RID: 327
		public static readonly MarkerHandle Zero;
	}
}
