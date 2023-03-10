using System;
using System.IO;
using System.Runtime.InteropServices;

namespace ScintillaNET
{
	// Token: 0x02000036 RID: 54
	internal sealed class NativeMemoryStream : Stream
	{
		// Token: 0x060000FB RID: 251 RVA: 0x00007335 File Offset: 0x00005535
		protected override void Dispose(bool disposing)
		{
			if (this.FreeOnDispose && this.ptr != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.ptr);
				this.ptr = IntPtr.Zero;
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000736E File Offset: 0x0000556E
		public override void Flush()
		{
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00007370 File Offset: 0x00005570
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00007377 File Offset: 0x00005577
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (origin == SeekOrigin.Begin)
			{
				this.position = (int)offset;
				return (long)this.position;
			}
			throw new NotImplementedException();
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00007370 File Offset: 0x00005570
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00007394 File Offset: 0x00005594
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.position + count > this.capacity)
			{
				int num = this.position + count;
				int num2 = this.capacity * 2;
				if (num2 < num)
				{
					num2 = num;
				}
				IntPtr dest = Marshal.AllocHGlobal(num2);
				NativeMethods.MoveMemory(dest, this.ptr, this.length);
				Marshal.FreeHGlobal(this.ptr);
				this.ptr = dest;
				this.capacity = num2;
			}
			Marshal.Copy(buffer, offset, (IntPtr)((long)this.ptr + (long)this.position), count);
			this.position += count;
			this.length = Math.Max(this.length, this.position);
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00007370 File Offset: 0x00005570
		public override bool CanRead
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00007440 File Offset: 0x00005640
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00007440 File Offset: 0x00005640
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00007443 File Offset: 0x00005643
		// (set) Token: 0x06000105 RID: 261 RVA: 0x0000744B File Offset: 0x0000564B
		public bool FreeOnDispose { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00007454 File Offset: 0x00005654
		public override long Length
		{
			get
			{
				return (long)this.length;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000107 RID: 263 RVA: 0x0000745D File Offset: 0x0000565D
		public IntPtr Pointer
		{
			get
			{
				return this.ptr;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00007465 File Offset: 0x00005665
		// (set) Token: 0x06000109 RID: 265 RVA: 0x00007370 File Offset: 0x00005570
		public override long Position
		{
			get
			{
				return (long)this.position;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000746E File Offset: 0x0000566E
		public NativeMemoryStream(int capacity)
		{
			if (capacity < 4)
			{
				capacity = 4;
			}
			this.capacity = capacity;
			this.ptr = Marshal.AllocHGlobal(capacity);
			this.FreeOnDispose = true;
		}

		// Token: 0x04000174 RID: 372
		private IntPtr ptr;

		// Token: 0x04000175 RID: 373
		private int capacity;

		// Token: 0x04000176 RID: 374
		private int position;

		// Token: 0x04000177 RID: 375
		private int length;
	}
}
