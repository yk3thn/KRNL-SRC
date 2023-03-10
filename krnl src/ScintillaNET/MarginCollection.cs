using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace ScintillaNET
{
	// Token: 0x0200002B RID: 43
	public class MarginCollection : IEnumerable<Margin>, IEnumerable
	{
		// Token: 0x060000D8 RID: 216 RVA: 0x00006F25 File Offset: 0x00005125
		public void ClearAllText()
		{
			this.scintilla.DirectMessage(2536);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00006F38 File Offset: 0x00005138
		public IEnumerator<Margin> GetEnumerator()
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

		// Token: 0x060000DA RID: 218 RVA: 0x00006F47 File Offset: 0x00005147
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00006F50 File Offset: 0x00005150
		// (set) Token: 0x060000DC RID: 220 RVA: 0x00006F75 File Offset: 0x00005175
		[DefaultValue(5)]
		[Description("The maximum number of margins.")]
		public int Capacity
		{
			get
			{
				return this.scintilla.DirectMessage(2253).ToInt32();
			}
			set
			{
				value = Helpers.ClampMin(value, 0);
				this.scintilla.DirectMessage(2252, new IntPtr(value));
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00006F97 File Offset: 0x00005197
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Count
		{
			get
			{
				return this.Capacity;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00006FA0 File Offset: 0x000051A0
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00006FC5 File Offset: 0x000051C5
		[DefaultValue(1)]
		[Description("The left margin padding in pixels.")]
		public int Left
		{
			get
			{
				return this.scintilla.DirectMessage(2156).ToInt32();
			}
			set
			{
				value = Helpers.ClampMin(value, 0);
				this.scintilla.DirectMessage(2155, IntPtr.Zero, new IntPtr(value));
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00006FEC File Offset: 0x000051EC
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00007011 File Offset: 0x00005211
		[DefaultValue(1)]
		[Description("The right margin padding in pixels.")]
		public int Right
		{
			get
			{
				return this.scintilla.DirectMessage(2158).ToInt32();
			}
			set
			{
				value = Helpers.ClampMin(value, 0);
				this.scintilla.DirectMessage(2157, IntPtr.Zero, new IntPtr(value));
			}
		}

		// Token: 0x1700004F RID: 79
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Margin this[int index]
		{
			get
			{
				index = Helpers.Clamp(index, 0, this.Count - 1);
				return new Margin(this.scintilla, index);
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00007057 File Offset: 0x00005257
		public MarginCollection(Scintilla scintilla)
		{
			this.scintilla = scintilla;
		}

		// Token: 0x0400012B RID: 299
		private readonly Scintilla scintilla;
	}
}
