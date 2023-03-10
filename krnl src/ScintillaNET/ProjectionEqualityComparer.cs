using System;

namespace ScintillaNET
{
	// Token: 0x0200003C RID: 60
	internal static class ProjectionEqualityComparer
	{
		// Token: 0x06000119 RID: 281 RVA: 0x00007548 File Offset: 0x00005748
		public static ProjectionEqualityComparer<TSource, TKey> Create<TSource, TKey>(Func<TSource, TKey> projection)
		{
			return new ProjectionEqualityComparer<TSource, TKey>(projection);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00007550 File Offset: 0x00005750
		public static ProjectionEqualityComparer<TSource, TKey> Create<TSource, TKey>(TSource ignored, Func<TSource, TKey> projection)
		{
			return new ProjectionEqualityComparer<TSource, TKey>(projection);
		}
	}
}
