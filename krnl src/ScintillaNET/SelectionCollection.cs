using System;
using System.Collections;
using System.Collections.Generic;

namespace ScintillaNET
{
	// Token: 0x02000045 RID: 69
	public class SelectionCollection : IEnumerable<Selection>, IEnumerable
	{
		// Token: 0x06000323 RID: 803 RVA: 0x0000D1E8 File Offset: 0x0000B3E8
		public IEnumerator<Selection> GetEnumerator()
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

		// Token: 0x06000324 RID: 804 RVA: 0x0000D1F7 File Offset: 0x0000B3F7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000D200 File Offset: 0x0000B400
		public int Count
		{
			get
			{
				return this.scintilla.DirectMessage(2570).ToInt32();
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000326 RID: 806 RVA: 0x0000D225 File Offset: 0x0000B425
		public bool IsEmpty
		{
			get
			{
				return this.scintilla.DirectMessage(2650) != IntPtr.Zero;
			}
		}

		// Token: 0x170000E6 RID: 230
		public Selection this[int index]
		{
			get
			{
				index = Helpers.Clamp(index, 0, this.Count - 1);
				return new Selection(this.scintilla, index);
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000D260 File Offset: 0x0000B460
		public SelectionCollection(Scintilla scintilla)
		{
			this.scintilla = scintilla;
		}

		// Token: 0x040007E0 RID: 2016
		private readonly Scintilla scintilla;
	}
}
