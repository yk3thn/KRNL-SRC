using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ScintillaNET
{
	// Token: 0x02000028 RID: 40
	internal sealed class Loader : ILoader
	{
		// Token: 0x060000BF RID: 191 RVA: 0x00006AAC File Offset: 0x00004CAC
		public unsafe bool AddData(char[] data, int length)
		{
			if (data != null)
			{
				length = Helpers.Clamp(length, 0, data.Length);
				byte[] bytes = Helpers.GetBytes(data, length, this.encoding, false);
				fixed (byte* ptr = bytes)
				{
					if (((IntPtr.Size == 4) ? this.loader32.AddData(this.self, ptr, bytes.Length) : this.loader64.AddData(this.self, ptr, bytes.Length)) != 0)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00006B38 File Offset: 0x00004D38
		public Document ConvertToDocument()
		{
			IntPtr value = (IntPtr.Size == 4) ? this.loader32.ConvertToDocument(this.self) : this.loader64.ConvertToDocument(this.self);
			return new Document
			{
				Value = value
			};
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00006B8D File Offset: 0x00004D8D
		public int Release()
		{
			if (IntPtr.Size != 4)
			{
				return this.loader64.Release(this.self);
			}
			return this.loader32.Release(this.self);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00006BC4 File Offset: 0x00004DC4
		public unsafe Loader(IntPtr ptr, Encoding encoding)
		{
			this.self = ptr;
			this.encoding = encoding;
			IntPtr ptr2 = *(IntPtr*)((void*)ptr);
			if (IntPtr.Size == 4)
			{
				this.loader32 = (NativeMethods.ILoaderVTable32)Marshal.PtrToStructure(ptr2, typeof(NativeMethods.ILoaderVTable32));
				return;
			}
			this.loader64 = (NativeMethods.ILoaderVTable64)Marshal.PtrToStructure(ptr2, typeof(NativeMethods.ILoaderVTable64));
		}

		// Token: 0x04000120 RID: 288
		private readonly IntPtr self;

		// Token: 0x04000121 RID: 289
		private readonly NativeMethods.ILoaderVTable32 loader32;

		// Token: 0x04000122 RID: 290
		private readonly NativeMethods.ILoaderVTable64 loader64;

		// Token: 0x04000123 RID: 291
		private readonly Encoding encoding;
	}
}
