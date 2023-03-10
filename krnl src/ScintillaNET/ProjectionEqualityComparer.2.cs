using System;

namespace ScintillaNET
{
	// Token: 0x0200003D RID: 61
	internal static class ProjectionEqualityComparer<TSource>
	{
		// Token: 0x0600011B RID: 283 RVA: 0x00007558 File Offset: 0x00005758
		public static ProjectionEqualityComparer<TSource, TKey> Create<TKey>(Func<TSource, TKey> projection)
		{
			return new ProjectionEqualityComparer<TSource, TKey>(projection);
		}
	}
}
