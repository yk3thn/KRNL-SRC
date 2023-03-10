using System;
using System.Collections;
using System.Collections.Generic;

namespace ScintillaNET
{
	// Token: 0x02000049 RID: 73
	public class StyleCollection : IEnumerable<Style>, IEnumerable
	{
		// Token: 0x06000347 RID: 839 RVA: 0x0000D937 File Offset: 0x0000BB37
		public IEnumerator<Style> GetEnumerator()
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

		// Token: 0x06000348 RID: 840 RVA: 0x0000D946 File Offset: 0x0000BB46
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000D94E File Offset: 0x0000BB4E
		public int Count
		{
			get
			{
				return 256;
			}
		}

		// Token: 0x170000F6 RID: 246
		public Style this[int index]
		{
			get
			{
				index = Helpers.Clamp(index, 0, this.Count - 1);
				return new Style(this.scintilla, index);
			}
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000D974 File Offset: 0x0000BB74
		public StyleCollection(Scintilla scintilla)
		{
			this.scintilla = scintilla;
		}

		// Token: 0x040007F4 RID: 2036
		private readonly Scintilla scintilla;
	}
}
