using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace ScintillaNET
{
	// Token: 0x0200001F RID: 31
	public class IndicatorCollection : IEnumerable<Indicator>, IEnumerable
	{
		// Token: 0x0600006B RID: 107 RVA: 0x0000535C File Offset: 0x0000355C
		public IEnumerator<Indicator> GetEnumerator()
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

		// Token: 0x0600006C RID: 108 RVA: 0x0000536B File Offset: 0x0000356B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00005373 File Offset: 0x00003573
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Count
		{
			get
			{
				return 32;
			}
		}

		// Token: 0x17000022 RID: 34
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Indicator this[int index]
		{
			get
			{
				index = Helpers.Clamp(index, 0, this.Count - 1);
				return new Indicator(this.scintilla, index);
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00005396 File Offset: 0x00003596
		public IndicatorCollection(Scintilla scintilla)
		{
			this.scintilla = scintilla;
		}

		// Token: 0x040000D4 RID: 212
		private readonly Scintilla scintilla;
	}
}
