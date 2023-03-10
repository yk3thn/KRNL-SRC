using System;
using System.Collections.Generic;

namespace ScintillaNET
{
	// Token: 0x0200003E RID: 62
	internal class ProjectionEqualityComparer<TSource, TKey> : IEqualityComparer<TSource>
	{
		// Token: 0x0600011C RID: 284 RVA: 0x00007560 File Offset: 0x00005760
		public ProjectionEqualityComparer(Func<TSource, TKey> projection) : this(projection, null)
		{
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000756A File Offset: 0x0000576A
		public ProjectionEqualityComparer(Func<TSource, TKey> projection, IEqualityComparer<TKey> comparer)
		{
			if (projection == null)
			{
				throw new ArgumentNullException("projection");
			}
			this.comparer = (comparer ?? EqualityComparer<TKey>.Default);
			this.projection = projection;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00007598 File Offset: 0x00005798
		public bool Equals(TSource x, TSource y)
		{
			return (x == null && y == null) || (x != null && y != null && this.comparer.Equals(this.projection(x), this.projection(y)));
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000075EC File Offset: 0x000057EC
		public int GetHashCode(TSource obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			return this.comparer.GetHashCode(this.projection(obj));
		}

		// Token: 0x04000793 RID: 1939
		private readonly Func<TSource, TKey> projection;

		// Token: 0x04000794 RID: 1940
		private readonly IEqualityComparer<TKey> comparer;
	}
}
