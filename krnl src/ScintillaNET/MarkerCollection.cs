using System;
using System.Collections;
using System.Collections.Generic;

namespace ScintillaNET
{
	// Token: 0x02000030 RID: 48
	public class MarkerCollection : IEnumerable<Marker>, IEnumerable
	{
		// Token: 0x060000EE RID: 238 RVA: 0x00007223 File Offset: 0x00005423
		public IEnumerator<Marker> GetEnumerator()
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

		// Token: 0x060000EF RID: 239 RVA: 0x00007232 File Offset: 0x00005432
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00005373 File Offset: 0x00003573
		public int Count
		{
			get
			{
				return 32;
			}
		}

		// Token: 0x17000053 RID: 83
		public Marker this[int index]
		{
			get
			{
				index = Helpers.Clamp(index, 0, this.Count - 1);
				return new Marker(this.scintilla, index);
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00007259 File Offset: 0x00005459
		public MarkerCollection(Scintilla scintilla)
		{
			this.scintilla = scintilla;
		}

		// Token: 0x04000145 RID: 325
		private readonly Scintilla scintilla;
	}
}
