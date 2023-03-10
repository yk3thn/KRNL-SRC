using System;
using System.Drawing;

namespace ScintillaNET
{
	// Token: 0x0200002F RID: 47
	public class Marker
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x00007068 File Offset: 0x00005268
		public unsafe void DefineRgbaImage(Bitmap image)
		{
			if (image == null)
			{
				return;
			}
			this.scintilla.DirectMessage(2624, new IntPtr(image.Width));
			this.scintilla.DirectMessage(2625, new IntPtr(image.Height));
			fixed (byte* ptr = Helpers.BitmapToArgb(image))
			{
				this.scintilla.DirectMessage(2626, new IntPtr(this.Index), new IntPtr((void*)ptr));
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000070F3 File Offset: 0x000052F3
		public void DeleteAll()
		{
			this.scintilla.MarkerDeleteAll(this.Index);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00007106 File Offset: 0x00005306
		public void SetAlpha(int alpha)
		{
			alpha = Helpers.Clamp(alpha, 0, 255);
			this.scintilla.DirectMessage(2476, new IntPtr(this.Index), new IntPtr(alpha));
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00007138 File Offset: 0x00005338
		public void SetBackColor(Color color)
		{
			int value = ColorTranslator.ToWin32(color);
			this.scintilla.DirectMessage(2042, new IntPtr(this.Index), new IntPtr(value));
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00007170 File Offset: 0x00005370
		public void SetForeColor(Color color)
		{
			int value = ColorTranslator.ToWin32(color);
			this.scintilla.DirectMessage(2041, new IntPtr(this.Index), new IntPtr(value));
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x000071A6 File Offset: 0x000053A6
		// (set) Token: 0x060000EA RID: 234 RVA: 0x000071AE File Offset: 0x000053AE
		public int Index { get; private set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000EB RID: 235 RVA: 0x000071B7 File Offset: 0x000053B7
		// (set) Token: 0x060000EC RID: 236 RVA: 0x000071DC File Offset: 0x000053DC
		public MarkerSymbol Symbol
		{
			get
			{
				return (MarkerSymbol)((int)this.scintilla.DirectMessage(2529, new IntPtr(this.Index)));
			}
			set
			{
				this.scintilla.DirectMessage(2040, new IntPtr(this.Index), new IntPtr((int)value));
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000720D File Offset: 0x0000540D
		public Marker(Scintilla scintilla, int index)
		{
			this.scintilla = scintilla;
			this.Index = index;
		}

		// Token: 0x0400013A RID: 314
		private readonly Scintilla scintilla;

		// Token: 0x0400013B RID: 315
		public const uint MaskAll = 4294967295U;

		// Token: 0x0400013C RID: 316
		public const uint MaskFolders = 4261412864U;

		// Token: 0x0400013D RID: 317
		public const int FolderEnd = 25;

		// Token: 0x0400013E RID: 318
		public const int FolderOpenMid = 26;

		// Token: 0x0400013F RID: 319
		public const int FolderMidTail = 27;

		// Token: 0x04000140 RID: 320
		public const int FolderTail = 28;

		// Token: 0x04000141 RID: 321
		public const int FolderSub = 29;

		// Token: 0x04000142 RID: 322
		public const int Folder = 30;

		// Token: 0x04000143 RID: 323
		public const int FolderOpen = 31;
	}
}
