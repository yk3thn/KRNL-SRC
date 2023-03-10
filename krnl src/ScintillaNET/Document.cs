using System;

namespace ScintillaNET
{
	// Token: 0x0200000C RID: 12
	public struct Document
	{
		// Token: 0x0600001B RID: 27 RVA: 0x0000235F File Offset: 0x0000055F
		public override bool Equals(object obj)
		{
			return obj is IntPtr && this.Value == ((Document)obj).Value;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002381 File Offset: 0x00000581
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000238E File Offset: 0x0000058E
		public static bool operator ==(Document a, Document b)
		{
			return a.Value == b.Value;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000023A1 File Offset: 0x000005A1
		public static bool operator !=(Document a, Document b)
		{
			return a.Value != b.Value;
		}

		// Token: 0x0400008A RID: 138
		internal IntPtr Value;

		// Token: 0x0400008B RID: 139
		public static readonly Document Empty;
	}
}
