using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

namespace ScintillaNET
{
	// Token: 0x02000017 RID: 23
	internal static class Helpers
	{
		// Token: 0x06000039 RID: 57 RVA: 0x0000282C File Offset: 0x00000A2C
		public static long CopyTo(this Stream source, Stream destination)
		{
			byte[] array = new byte[2048];
			long num = 0L;
			int num2;
			while ((num2 = source.Read(array, 0, array.Length)) > 0)
			{
				destination.Write(array, 0, num2);
				num += (long)num2;
			}
			return num;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002868 File Offset: 0x00000A68
		public static byte[] BitmapToArgb(Bitmap image)
		{
			byte[] array = new byte[4 * image.Width * image.Height];
			int num = 0;
			for (int i = 0; i < image.Height; i++)
			{
				for (int j = 0; j < image.Width; j++)
				{
					Color pixel = image.GetPixel(j, i);
					array[num++] = pixel.R;
					array[num++] = pixel.G;
					array[num++] = pixel.B;
					array[num++] = pixel.A;
				}
			}
			return array;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000028F4 File Offset: 0x00000AF4
		public unsafe static byte[] ByteToCharStyles(byte* styles, byte* text, int length, Encoding encoding)
		{
			int i = 0;
			int num = 0;
			Decoder decoder = encoding.GetDecoder();
			byte[] array = new byte[encoding.GetCharCount(text, length)];
			while (i < length)
			{
				if (decoder.GetCharCount(text + i, 1, false) > 0)
				{
					array[num++] = styles[i];
				}
				i++;
			}
			return array;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002940 File Offset: 0x00000B40
		public unsafe static byte[] CharToByteStyles(byte[] styles, byte* text, int length, Encoding encoding)
		{
			int num = 0;
			int num2 = 0;
			Decoder decoder = encoding.GetDecoder();
			byte[] array = new byte[length];
			while (num < length && num2 < styles.Length)
			{
				array[num] = styles[num2];
				if (decoder.GetCharCount(text + num, 1, false) > 0)
				{
					num2++;
				}
				num++;
			}
			return array;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002988 File Offset: 0x00000B88
		public static int Clamp(int value, int min, int max)
		{
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002997 File Offset: 0x00000B97
		public static int ClampMin(int value, int min)
		{
			if (value < min)
			{
				return min;
			}
			return value;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000029A0 File Offset: 0x00000BA0
		public static void Copy(Scintilla scintilla, CopyFormat format, bool useSelection, bool allowLine, int startBytePos, int endBytePos)
		{
			if ((format & CopyFormat.Text) > (CopyFormat)0)
			{
				if (useSelection)
				{
					if (allowLine)
					{
						scintilla.DirectMessage(2519);
					}
					else
					{
						scintilla.DirectMessage(2178);
					}
				}
				else
				{
					scintilla.DirectMessage(2419, new IntPtr(startBytePos), new IntPtr(endBytePos));
				}
			}
			if ((format & (CopyFormat.Rtf | CopyFormat.Html)) > (CopyFormat)0)
			{
				if (!Helpers.registeredFormats)
				{
					Helpers.CF_LINESELECT = NativeMethods.RegisterClipboardFormat("MSDEVLineSelect");
					Helpers.CF_VSLINETAG = NativeMethods.RegisterClipboardFormat("VisualStudioEditorOperationsLineCutCopyClipboardTag");
					Helpers.CF_HTML = NativeMethods.RegisterClipboardFormat("HTML Format");
					Helpers.CF_RTF = NativeMethods.RegisterClipboardFormat("Rich Text Format");
					Helpers.registeredFormats = true;
				}
				bool flag = false;
				Helpers.StyleData[] styles = null;
				List<ArraySegment<byte>> list = null;
				if (useSelection)
				{
					if (scintilla.DirectMessage(2650) != IntPtr.Zero)
					{
						if (allowLine)
						{
							list = Helpers.GetStyledSegments(scintilla, false, true, 0, 0, out styles);
							flag = true;
						}
					}
					else
					{
						list = Helpers.GetStyledSegments(scintilla, true, false, 0, 0, out styles);
					}
				}
				else if (startBytePos != endBytePos)
				{
					list = Helpers.GetStyledSegments(scintilla, false, false, startBytePos, endBytePos, out styles);
				}
				if (list != null && list.Count > 0 && NativeMethods.OpenClipboard(scintilla.Handle))
				{
					if ((format & CopyFormat.Text) == (CopyFormat)0)
					{
						NativeMethods.EmptyClipboard();
						if (flag)
						{
							NativeMethods.SetClipboardData(Helpers.CF_LINESELECT, IntPtr.Zero);
							NativeMethods.SetClipboardData(Helpers.CF_VSLINETAG, IntPtr.Zero);
						}
					}
					if ((format & CopyFormat.Rtf) > (CopyFormat)0)
					{
						Helpers.CopyRtf(scintilla, styles, list);
					}
					if ((format & CopyFormat.Html) > (CopyFormat)0)
					{
						Helpers.CopyHtml(scintilla, styles, list);
					}
					NativeMethods.CloseClipboard();
				}
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002B04 File Offset: 0x00000D04
		private static void CopyHtml(Scintilla scintilla, Helpers.StyleData[] styles, List<ArraySegment<byte>> styledSegments)
		{
			try
			{
				using (NativeMemoryStream nativeMemoryStream = new NativeMemoryStream(styledSegments.Sum((ArraySegment<byte> s) => s.Count)))
				{
					using (StreamWriter streamWriter = new StreamWriter(nativeMemoryStream, new UTF8Encoding(false)))
					{
						streamWriter.WriteLine("Version:0.9");
						streamWriter.WriteLine("StartHTML:00000000");
						streamWriter.WriteLine("EndHTML:00000000");
						streamWriter.WriteLine("StartFragment:00000000");
						streamWriter.WriteLine("EndFragment:00000000");
						streamWriter.Flush();
						long position = nativeMemoryStream.Position;
						nativeMemoryStream.Seek(23L, SeekOrigin.Begin);
						byte[] bytes;
						nativeMemoryStream.Write(bytes = Encoding.ASCII.GetBytes(nativeMemoryStream.Length.ToString("D8")), 0, bytes.Length);
						nativeMemoryStream.Seek(position, SeekOrigin.Begin);
						streamWriter.WriteLine("<html>");
						streamWriter.WriteLine("<head>");
						streamWriter.WriteLine("<meta charset=\"utf-8\" />");
						streamWriter.WriteLine("<title>ScintillaNET v{0}</title>", scintilla.GetType().Assembly.GetName().Version.ToString(3));
						streamWriter.WriteLine("</head>");
						streamWriter.WriteLine("<body>");
						streamWriter.Flush();
						position = nativeMemoryStream.Position;
						nativeMemoryStream.Seek(65L, SeekOrigin.Begin);
						nativeMemoryStream.Write(bytes = Encoding.ASCII.GetBytes(nativeMemoryStream.Length.ToString("D8")), 0, bytes.Length);
						nativeMemoryStream.Seek(position, SeekOrigin.Begin);
						streamWriter.WriteLine("<!--StartFragment -->");
						streamWriter.WriteLine("<style type=\"text/css\" scoped=\"\">");
						streamWriter.Write("div#segments {");
						streamWriter.Write(" float: left;");
						streamWriter.Write(" white-space: pre;");
						streamWriter.Write(" line-height: {0}px;", scintilla.DirectMessage(2279, new IntPtr(0)).ToInt32());
						streamWriter.Write(" background-color: #{0:X2}{1:X2}{2:X2};", styles[32].BackColor & 255, styles[32].BackColor >> 8 & 255, styles[32].BackColor >> 16 & 255);
						streamWriter.WriteLine(" }");
						for (int i = 0; i < styles.Length; i++)
						{
							if (styles[i].Used)
							{
								streamWriter.Write("span.s{0} {{", i);
								streamWriter.Write(" font-family: \"{0}\";", styles[i].FontName);
								streamWriter.Write(" font-size: {0}pt;", styles[i].SizeF);
								streamWriter.Write(" font-weight: {0};", styles[i].Weight);
								if (styles[i].Italic != 0)
								{
									streamWriter.Write(" font-style: italic;");
								}
								if (styles[i].Underline != 0)
								{
									streamWriter.Write(" text-decoration: underline;");
								}
								streamWriter.Write(" background-color: #{0:X2}{1:X2}{2:X2};", styles[i].BackColor & 255, styles[i].BackColor >> 8 & 255, styles[i].BackColor >> 16 & 255);
								streamWriter.Write(" color: #{0:X2}{1:X2}{2:X2};", styles[i].ForeColor & 255, styles[i].ForeColor >> 8 & 255, styles[i].ForeColor >> 16 & 255);
								StyleCase @case = (StyleCase)styles[i].Case;
								if (@case != StyleCase.Upper)
								{
									if (@case == StyleCase.Lower)
									{
										streamWriter.Write(" text-transform: lowercase;");
									}
								}
								else
								{
									streamWriter.Write(" text-transform: uppercase;");
								}
								if (styles[i].Visible == 0)
								{
									streamWriter.Write(" visibility: hidden;");
								}
								streamWriter.WriteLine(" }");
							}
						}
						streamWriter.WriteLine("</style>");
						streamWriter.Write("<div id=\"segments\"><span class=\"s{0}\">", 32);
						streamWriter.Flush();
						int count = scintilla.DirectMessage(2121).ToInt32();
						string value = new string(' ', count);
						streamWriter.AutoFlush = true;
						int num = 32;
						bool flag = (scintilla.DirectMessage(2658).ToInt32() & 1) > 0;
						foreach (ArraySegment<byte> arraySegment in styledSegments)
						{
							int num2 = arraySegment.Offset + arraySegment.Count;
							int j = arraySegment.Offset;
							while (j < num2)
							{
								byte b = arraySegment.Array[j];
								byte b2 = arraySegment.Array[j + 1];
								if (num != (int)b2)
								{
									streamWriter.Write("</span><span class=\"s{0}\">", b2);
									num = (int)b2;
								}
								if (b <= 60)
								{
									switch (b)
									{
									case 9:
										streamWriter.Write(value);
										break;
									case 10:
										goto IL_616;
									case 11:
									case 12:
										goto IL_623;
									case 13:
										if (j + 2 < num2 && arraySegment.Array[j + 2] == 10)
										{
											j += 2;
											goto IL_616;
										}
										goto IL_616;
									default:
										if (b != 38)
										{
											if (b != 60)
											{
												goto IL_623;
											}
											streamWriter.Write("&lt;");
										}
										else
										{
											streamWriter.Write("&amp;");
										}
										break;
									}
								}
								else if (b != 62)
								{
									if (b != 194)
									{
										if (b != 226)
										{
											goto IL_623;
										}
										if (!flag || j + 4 >= num2)
										{
											goto IL_623;
										}
										if (arraySegment.Array[j + 2] == 128 && arraySegment.Array[j + 4] == 168)
										{
											j += 4;
											goto IL_616;
										}
										if (arraySegment.Array[j + 2] == 128 && arraySegment.Array[j + 4] == 169)
										{
											j += 4;
											goto IL_616;
										}
										goto IL_623;
									}
									else
									{
										if (flag && j + 2 < num2 && arraySegment.Array[j + 2] == 133)
										{
											j += 2;
											goto IL_616;
										}
										goto IL_623;
									}
								}
								else
								{
									streamWriter.Write("&gt;");
								}
								IL_63C:
								j += 2;
								continue;
								IL_616:
								streamWriter.Write("\r\n");
								goto IL_63C;
								IL_623:
								if (b == 0)
								{
									streamWriter.Write(" ");
									goto IL_63C;
								}
								nativeMemoryStream.WriteByte(b);
								goto IL_63C;
							}
						}
						streamWriter.AutoFlush = false;
						streamWriter.WriteLine("</span></div>");
						streamWriter.Flush();
						position = nativeMemoryStream.Position;
						nativeMemoryStream.Seek(87L, SeekOrigin.Begin);
						nativeMemoryStream.Write(bytes = Encoding.ASCII.GetBytes(nativeMemoryStream.Length.ToString("D8")), 0, bytes.Length);
						nativeMemoryStream.Seek(position, SeekOrigin.Begin);
						streamWriter.WriteLine("<!--EndFragment-->");
						streamWriter.WriteLine("</body>");
						streamWriter.WriteLine("</html>");
						streamWriter.Flush();
						position = nativeMemoryStream.Position;
						nativeMemoryStream.Seek(41L, SeekOrigin.Begin);
						nativeMemoryStream.Write(bytes = Encoding.ASCII.GetBytes(nativeMemoryStream.Length.ToString("D8")), 0, bytes.Length);
						nativeMemoryStream.Seek(position, SeekOrigin.Begin);
						nativeMemoryStream.WriteByte(0);
						Helpers.GetString(nativeMemoryStream.Pointer, (int)nativeMemoryStream.Length, Encoding.UTF8);
						if (NativeMethods.SetClipboardData(Helpers.CF_HTML, nativeMemoryStream.Pointer) != IntPtr.Zero)
						{
							nativeMemoryStream.FreeOnDispose = false;
						}
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003304 File Offset: 0x00001504
		private static void CopyRtf(Scintilla scintilla, Helpers.StyleData[] styles, List<ArraySegment<byte>> styledSegments)
		{
			try
			{
				FontStyle fontStyle = FontStyle.Regular;
				if (styles[32].Weight >= 700)
				{
					fontStyle |= FontStyle.Bold;
				}
				if (styles[32].Italic != 0)
				{
					fontStyle |= FontStyle.Italic;
				}
				if (styles[32].Underline != 0)
				{
					fontStyle |= FontStyle.Underline;
				}
				int num;
				using (Graphics graphics = scintilla.CreateGraphics())
				{
					using (Font font = new Font(styles[32].FontName, styles[32].SizeF, fontStyle))
					{
						num = (int)(graphics.MeasureString(" ", font).Width / graphics.DpiX * 1440f);
					}
				}
				using (NativeMemoryStream nativeMemoryStream = new NativeMemoryStream(styledSegments.Sum((ArraySegment<byte> s) => s.Count)))
				{
					using (StreamWriter streamWriter = new StreamWriter(nativeMemoryStream, Encoding.ASCII))
					{
						int num2 = scintilla.DirectMessage(2121).ToInt32() * num;
						streamWriter.WriteLine("{{\\rtf1\\ansi\\deff0\\deftab{0}", num2);
						streamWriter.Flush();
						streamWriter.Write("{\\fonttbl");
						streamWriter.Write("{{\\f0 {0};}}", styles[32].FontName);
						int num3 = 1;
						for (int i = 0; i < styles.Length; i++)
						{
							if (styles[i].Used && i != 32 && styles[i].FontName != styles[32].FontName)
							{
								styles[i].FontIndex = num3++;
								streamWriter.Write("{{\\f{0} {1};}}", styles[i].FontIndex, styles[i].FontName);
							}
						}
						streamWriter.WriteLine("}");
						streamWriter.Flush();
						streamWriter.Write("{\\colortbl");
						streamWriter.Write("\\red{0}\\green{1}\\blue{2};", styles[32].ForeColor & 255, styles[32].ForeColor >> 8 & 255, styles[32].ForeColor >> 16 & 255);
						streamWriter.Write("\\red{0}\\green{1}\\blue{2};", styles[32].BackColor & 255, styles[32].BackColor >> 8 & 255, styles[32].BackColor >> 16 & 255);
						styles[32].ForeColorIndex = 0;
						styles[32].BackColorIndex = 1;
						int num4 = 2;
						for (int j = 0; j < styles.Length; j++)
						{
							if (styles[j].Used && j != 32)
							{
								if (styles[j].ForeColor != styles[32].ForeColor)
								{
									styles[j].ForeColorIndex = num4++;
									streamWriter.Write("\\red{0}\\green{1}\\blue{2};", styles[j].ForeColor & 255, styles[j].ForeColor >> 8 & 255, styles[j].ForeColor >> 16 & 255);
								}
								else
								{
									styles[j].ForeColorIndex = styles[32].ForeColorIndex;
								}
								if (styles[j].BackColor != styles[32].BackColor)
								{
									styles[j].BackColorIndex = num4++;
									streamWriter.Write("\\red{0}\\green{1}\\blue{2};", styles[j].BackColor & 255, styles[j].BackColor >> 8 & 255, styles[j].BackColor >> 16 & 255);
								}
								else
								{
									styles[j].BackColorIndex = styles[32].BackColorIndex;
								}
							}
						}
						streamWriter.WriteLine("}");
						streamWriter.Flush();
						streamWriter.Write("\\f{0}\\fs{1}\\cf{2}\\chshdng0\\chcbpat{3}\\cb{3} ", new object[]
						{
							styles[32].FontIndex,
							(int)(styles[32].SizeF * 2f),
							styles[32].ForeColorIndex,
							styles[32].BackColorIndex
						});
						if (styles[32].Italic != 0)
						{
							streamWriter.Write("\\i");
						}
						if (styles[32].Underline != 0)
						{
							streamWriter.Write("\\ul");
						}
						if (styles[32].Weight >= 700)
						{
							streamWriter.Write("\\b");
						}
						streamWriter.AutoFlush = true;
						int num5 = 32;
						bool flag = (scintilla.DirectMessage(2658).ToInt32() & 1) > 0;
						foreach (ArraySegment<byte> arraySegment in styledSegments)
						{
							int num6 = arraySegment.Offset + arraySegment.Count;
							int k = arraySegment.Offset;
							while (k < num6)
							{
								byte b = arraySegment.Array[k];
								byte b2 = arraySegment.Array[k + 1];
								if (num5 != (int)b2)
								{
									if (styles[num5].FontIndex != styles[(int)b2].FontIndex)
									{
										streamWriter.Write("\\f{0}", styles[(int)b2].FontIndex);
									}
									if (styles[num5].SizeF != styles[(int)b2].SizeF)
									{
										streamWriter.Write("\\fs{0}", (int)(styles[(int)b2].SizeF * 2f));
									}
									if (styles[num5].ForeColorIndex != styles[(int)b2].ForeColorIndex)
									{
										streamWriter.Write("\\cf{0}", styles[(int)b2].ForeColorIndex);
									}
									if (styles[num5].BackColorIndex != styles[(int)b2].BackColorIndex)
									{
										streamWriter.Write("\\chshdng0\\chcbpat{0}\\cb{0}", styles[(int)b2].BackColorIndex);
									}
									if (styles[num5].Italic != styles[(int)b2].Italic)
									{
										streamWriter.Write("\\i{0}", (styles[(int)b2].Italic != 0) ? "" : "0");
									}
									if (styles[num5].Underline != styles[(int)b2].Underline)
									{
										streamWriter.Write("\\ul{0}", (styles[(int)b2].Underline != 0) ? "" : "0");
									}
									if (styles[num5].Weight != styles[(int)b2].Weight)
									{
										if (styles[(int)b2].Weight >= 700 && styles[num5].Weight < 700)
										{
											streamWriter.Write("\\b");
										}
										else if (styles[(int)b2].Weight < 700 && styles[num5].Weight >= 700)
										{
											streamWriter.Write("\\b0");
										}
									}
									num5 = (int)b2;
									streamWriter.Write(" ");
								}
								if (b <= 123)
								{
									switch (b)
									{
									case 9:
										streamWriter.Write("\\tab ");
										break;
									case 10:
										goto IL_8E4;
									case 11:
									case 12:
										goto IL_8F5;
									case 13:
										if (k + 2 < num6 && arraySegment.Array[k + 2] == 10)
										{
											k += 2;
											goto IL_8E4;
										}
										goto IL_8E4;
									default:
										if (b != 92)
										{
											if (b != 123)
											{
												goto IL_8F5;
											}
											streamWriter.Write("\\{");
										}
										else
										{
											streamWriter.Write("\\\\");
										}
										break;
									}
								}
								else if (b != 125)
								{
									if (b != 194)
									{
										if (b != 226)
										{
											goto IL_8F5;
										}
										if (!flag || k + 4 >= num6)
										{
											goto IL_8F5;
										}
										if (arraySegment.Array[k + 2] == 128 && arraySegment.Array[k + 4] == 168)
										{
											k += 4;
											goto IL_8E4;
										}
										if (arraySegment.Array[k + 2] == 128 && arraySegment.Array[k + 4] == 169)
										{
											k += 4;
											goto IL_8E4;
										}
										goto IL_8F5;
									}
									else
									{
										if (flag && k + 2 < num6 && arraySegment.Array[k + 2] == 133)
										{
											k += 2;
											goto IL_8E4;
										}
										goto IL_8F5;
									}
								}
								else
								{
									streamWriter.Write("\\}");
								}
								IL_A4D:
								k += 2;
								continue;
								IL_8E4:
								streamWriter.WriteLine("\\par");
								goto IL_A4D;
								IL_8F5:
								if (b == 0)
								{
									streamWriter.Write(" ");
									goto IL_A4D;
								}
								if (b > 127)
								{
									int num7 = 0;
									if (b < 224 && k + 2 < num6)
									{
										num7 |= (int)(31 & b) << 6;
										num7 |= (int)(63 & arraySegment.Array[k + 2]);
										streamWriter.Write("\\u{0}?", num7);
										k += 2;
										goto IL_A4D;
									}
									if (b < 240 && k + 4 < num6)
									{
										num7 |= (int)(15 & b) << 12;
										num7 |= (int)(63 & arraySegment.Array[k + 2]) << 6;
										num7 |= (int)(63 & arraySegment.Array[k + 4]);
										streamWriter.Write("\\u{0}?", num7);
										k += 4;
										goto IL_A4D;
									}
									if (b < 248 && k + 6 < num6)
									{
										num7 |= (int)(7 & b) << 18;
										num7 |= (int)(63 & arraySegment.Array[k + 2]) << 12;
										num7 |= (int)(63 & arraySegment.Array[k + 4]) << 6;
										num7 |= (int)(63 & arraySegment.Array[k + 6]);
										streamWriter.Write("\\u{0}?", num7);
										k += 6;
										goto IL_A4D;
									}
								}
								nativeMemoryStream.WriteByte(b);
								goto IL_A4D;
							}
						}
						streamWriter.AutoFlush = false;
						streamWriter.WriteLine("}");
						streamWriter.Flush();
						nativeMemoryStream.WriteByte(0);
						if (NativeMethods.SetClipboardData(Helpers.CF_RTF, nativeMemoryStream.Pointer) != IntPtr.Zero)
						{
							nativeMemoryStream.FreeOnDispose = false;
						}
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003E88 File Offset: 0x00002088
		public unsafe static byte[] GetBytes(string text, Encoding encoding, bool zeroTerminated)
		{
			if (!string.IsNullOrEmpty(text))
			{
				int byteCount = encoding.GetByteCount(text);
				byte[] array = new byte[byteCount + (zeroTerminated ? 1 : 0)];
				fixed (byte* ptr = array)
				{
					fixed (string text2 = text)
					{
						char* ptr2 = text2;
						if (ptr2 != null)
						{
							ptr2 += RuntimeHelpers.OffsetToStringData / 2;
						}
						encoding.GetBytes(ptr2, text.Length, ptr, byteCount);
					}
				}
				if (zeroTerminated)
				{
					array[array.Length - 1] = 0;
				}
				return array;
			}
			if (!zeroTerminated)
			{
				return new byte[0];
			}
			return new byte[1];
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003F18 File Offset: 0x00002118
		public unsafe static byte[] GetBytes(char[] text, int length, Encoding encoding, bool zeroTerminated)
		{
			fixed (char* ptr = text)
			{
				byte[] array = new byte[encoding.GetByteCount(ptr, length) + (zeroTerminated ? 1 : 0)];
				fixed (byte* ptr2 = array)
				{
					encoding.GetBytes(ptr, length, ptr2, array.Length);
				}
				if (zeroTerminated)
				{
					array[array.Length - 1] = 0;
				}
				return array;
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003F8C File Offset: 0x0000218C
		public static string GetHtml(Scintilla scintilla, int startBytePos, int endBytePos)
		{
			if (startBytePos == endBytePos)
			{
				return string.Empty;
			}
			Helpers.StyleData[] array = null;
			List<ArraySegment<byte>> styledSegments = Helpers.GetStyledSegments(scintilla, false, false, startBytePos, endBytePos, out array);
			string @string;
			using (NativeMemoryStream nativeMemoryStream = new NativeMemoryStream(styledSegments.Sum((ArraySegment<byte> s) => s.Count)))
			{
				using (StreamWriter streamWriter = new StreamWriter(nativeMemoryStream, new UTF8Encoding(false)))
				{
					streamWriter.WriteLine("<style type=\"text/css\" scoped=\"\">");
					streamWriter.Write("div#segments {");
					streamWriter.Write(" float: left;");
					streamWriter.Write(" white-space: pre;");
					streamWriter.Write(" line-height: {0}px;", scintilla.DirectMessage(2279, new IntPtr(0)).ToInt32());
					streamWriter.Write(" background-color: #{0:X2}{1:X2}{2:X2};", array[32].BackColor & 255, array[32].BackColor >> 8 & 255, array[32].BackColor >> 16 & 255);
					streamWriter.WriteLine(" }");
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i].Used)
						{
							streamWriter.Write("span.s{0} {{", i);
							streamWriter.Write(" font-family: \"{0}\";", array[i].FontName);
							streamWriter.Write(" font-size: {0}pt;", array[i].SizeF);
							streamWriter.Write(" font-weight: {0};", array[i].Weight);
							if (array[i].Italic != 0)
							{
								streamWriter.Write(" font-style: italic;");
							}
							if (array[i].Underline != 0)
							{
								streamWriter.Write(" text-decoration: underline;");
							}
							streamWriter.Write(" background-color: #{0:X2}{1:X2}{2:X2};", array[i].BackColor & 255, array[i].BackColor >> 8 & 255, array[i].BackColor >> 16 & 255);
							streamWriter.Write(" color: #{0:X2}{1:X2}{2:X2};", array[i].ForeColor & 255, array[i].ForeColor >> 8 & 255, array[i].ForeColor >> 16 & 255);
							StyleCase @case = (StyleCase)array[i].Case;
							if (@case != StyleCase.Upper)
							{
								if (@case == StyleCase.Lower)
								{
									streamWriter.Write(" text-transform: lowercase;");
								}
							}
							else
							{
								streamWriter.Write(" text-transform: uppercase;");
							}
							if (array[i].Visible == 0)
							{
								streamWriter.Write(" visibility: hidden;");
							}
							streamWriter.WriteLine(" }");
						}
					}
					streamWriter.WriteLine("</style>");
					bool flag = (scintilla.DirectMessage(2658).ToInt32() & 1) > 0;
					int count = scintilla.DirectMessage(2121).ToInt32();
					string value = new string(' ', count);
					int num = 32;
					streamWriter.Write("<div id=\"segments\"><span class=\"s{0}\">", 32);
					streamWriter.Flush();
					streamWriter.AutoFlush = true;
					foreach (ArraySegment<byte> arraySegment in styledSegments)
					{
						int num2 = arraySegment.Offset + arraySegment.Count;
						int j = arraySegment.Offset;
						while (j < num2)
						{
							byte b = arraySegment.Array[j];
							byte b2 = arraySegment.Array[j + 1];
							if (num != (int)b2)
							{
								streamWriter.Write("</span><span class=\"s{0}\">", b2);
								num = (int)b2;
							}
							if (b <= 60)
							{
								switch (b)
								{
								case 9:
									streamWriter.Write(value);
									break;
								case 10:
									goto IL_4F7;
								case 11:
								case 12:
									goto IL_504;
								case 13:
									if (j + 2 < num2 && arraySegment.Array[j + 2] == 10)
									{
										j += 2;
										goto IL_4F7;
									}
									goto IL_4F7;
								default:
									if (b != 38)
									{
										if (b != 60)
										{
											goto IL_504;
										}
										streamWriter.Write("&lt;");
									}
									else
									{
										streamWriter.Write("&amp;");
									}
									break;
								}
							}
							else if (b != 62)
							{
								if (b != 194)
								{
									if (b != 226)
									{
										goto IL_504;
									}
									if (!flag || j + 4 >= num2)
									{
										goto IL_504;
									}
									if (arraySegment.Array[j + 2] == 128 && arraySegment.Array[j + 4] == 168)
									{
										j += 4;
										goto IL_4F7;
									}
									if (arraySegment.Array[j + 2] == 128 && arraySegment.Array[j + 4] == 169)
									{
										j += 4;
										goto IL_4F7;
									}
									goto IL_504;
								}
								else
								{
									if (flag && j + 2 < num2 && arraySegment.Array[j + 2] == 133)
									{
										j += 2;
										goto IL_4F7;
									}
									goto IL_504;
								}
							}
							else
							{
								streamWriter.Write("&gt;");
							}
							IL_51D:
							j += 2;
							continue;
							IL_4F7:
							streamWriter.Write("\r\n");
							goto IL_51D;
							IL_504:
							if (b == 0)
							{
								streamWriter.Write(" ");
								goto IL_51D;
							}
							nativeMemoryStream.WriteByte(b);
							goto IL_51D;
						}
					}
					streamWriter.AutoFlush = false;
					streamWriter.WriteLine("</span></div>");
					streamWriter.Flush();
					@string = Helpers.GetString(nativeMemoryStream.Pointer, (int)nativeMemoryStream.Length, Encoding.UTF8);
				}
			}
			return @string;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00004578 File Offset: 0x00002778
		public unsafe static string GetString(IntPtr bytes, int length, Encoding encoding)
		{
			sbyte* value = (sbyte*)((void*)bytes);
			return new string(value, 0, length, encoding);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00004598 File Offset: 0x00002798
		private static List<ArraySegment<byte>> GetStyledSegments(Scintilla scintilla, bool currentSelection, bool currentLine, int startBytePos, int endBytePos, out Helpers.StyleData[] styles)
		{
			List<ArraySegment<byte>> list = new List<ArraySegment<byte>>();
			if (currentSelection)
			{
				List<Tuple<int, int>> list2 = new List<Tuple<int, int>>();
				int num = scintilla.DirectMessage(2570).ToInt32();
				for (int i = 0; i < num; i++)
				{
					int item = scintilla.DirectMessage(2585, new IntPtr(i)).ToInt32();
					int item2 = scintilla.DirectMessage(2587, new IntPtr(i)).ToInt32();
					list2.Add(Tuple.Create<int, int>(item, item2));
				}
				bool flag = scintilla.DirectMessage(2372) != IntPtr.Zero;
				if (flag)
				{
					from r in list2
					orderby r.Item1
					select r;
				}
				using (List<Tuple<int, int>>.Enumerator enumerator = list2.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Tuple<int, int> tuple = enumerator.Current;
						ArraySegment<byte> styledText = Helpers.GetStyledText(scintilla, tuple.Item1, tuple.Item2, flag);
						list.Add(styledText);
					}
					goto IL_1CB;
				}
			}
			if (currentLine)
			{
				int value = scintilla.DirectMessage(2575).ToInt32();
				int value2 = scintilla.DirectMessage(2577, new IntPtr(value)).ToInt32();
				int value3 = scintilla.DirectMessage(2166, new IntPtr(value2)).ToInt32();
				int num2 = scintilla.DirectMessage(2167, new IntPtr(value3)).ToInt32();
				int num3 = scintilla.DirectMessage(2167, new IntPtr(value3)).ToInt32();
				ArraySegment<byte> styledText2 = Helpers.GetStyledText(scintilla, num2, num2 + num3, false);
				list.Add(styledText2);
			}
			else
			{
				ArraySegment<byte> styledText3 = Helpers.GetStyledText(scintilla, startBytePos, endBytePos, false);
				list.Add(styledText3);
			}
			IL_1CB:
			styles = new Helpers.StyleData[256];
			styles[32].Used = true;
			styles[32].FontName = scintilla.Styles[32].Font;
			styles[32].SizeF = scintilla.Styles[32].SizeF;
			styles[32].Weight = scintilla.DirectMessage(2064, new IntPtr(32), IntPtr.Zero).ToInt32();
			styles[32].Italic = scintilla.DirectMessage(2484, new IntPtr(32), IntPtr.Zero).ToInt32();
			styles[32].Underline = scintilla.DirectMessage(2488, new IntPtr(32), IntPtr.Zero).ToInt32();
			styles[32].BackColor = scintilla.DirectMessage(2482, new IntPtr(32), IntPtr.Zero).ToInt32();
			styles[32].ForeColor = scintilla.DirectMessage(2481, new IntPtr(32), IntPtr.Zero).ToInt32();
			styles[32].Case = scintilla.DirectMessage(2489, new IntPtr(32), IntPtr.Zero).ToInt32();
			styles[32].Visible = scintilla.DirectMessage(2491, new IntPtr(32), IntPtr.Zero).ToInt32();
			foreach (ArraySegment<byte> arraySegment in list)
			{
				for (int j = 0; j < arraySegment.Count; j += 2)
				{
					byte b = arraySegment.Array[j + 1];
					if (!styles[(int)b].Used)
					{
						styles[(int)b].Used = true;
						styles[(int)b].FontName = scintilla.Styles[(int)b].Font;
						styles[(int)b].SizeF = scintilla.Styles[(int)b].SizeF;
						styles[(int)b].Weight = scintilla.DirectMessage(2064, new IntPtr((int)b), IntPtr.Zero).ToInt32();
						styles[(int)b].Italic = scintilla.DirectMessage(2484, new IntPtr((int)b), IntPtr.Zero).ToInt32();
						styles[(int)b].Underline = scintilla.DirectMessage(2488, new IntPtr((int)b), IntPtr.Zero).ToInt32();
						styles[(int)b].BackColor = scintilla.DirectMessage(2482, new IntPtr((int)b), IntPtr.Zero).ToInt32();
						styles[(int)b].ForeColor = scintilla.DirectMessage(2481, new IntPtr((int)b), IntPtr.Zero).ToInt32();
						styles[(int)b].Case = scintilla.DirectMessage(2489, new IntPtr((int)b), IntPtr.Zero).ToInt32();
						styles[(int)b].Visible = scintilla.DirectMessage(2491, new IntPtr((int)b), IntPtr.Zero).ToInt32();
					}
				}
			}
			return list;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00004B58 File Offset: 0x00002D58
		private unsafe static ArraySegment<byte> GetStyledText(Scintilla scintilla, int startBytePos, int endBytePos, bool addLineBreak)
		{
			scintilla.DirectMessage(4003, new IntPtr(startBytePos), new IntPtr(endBytePos));
			int num = endBytePos - startBytePos;
			byte[] array = new byte[num * 2 + (addLineBreak ? 4 : 0) + 2];
			fixed (byte* ptr = array)
			{
				NativeMethods.Sci_TextRange* ptr2 = stackalloc NativeMethods.Sci_TextRange[checked(unchecked((UIntPtr)1) * (UIntPtr)sizeof(NativeMethods.Sci_TextRange))];
				ptr2->chrg.cpMin = startBytePos;
				ptr2->chrg.cpMax = endBytePos;
				ptr2->lpstrText = new IntPtr((void*)ptr);
				scintilla.DirectMessage(2015, IntPtr.Zero, new IntPtr((void*)ptr2));
				num *= 2;
			}
			if (addLineBreak)
			{
				byte b = array[num - 1];
				array[num++] = 13;
				array[num++] = b;
				array[num++] = 10;
				array[num++] = b;
				array[num] = 0;
				array[num + 1] = 0;
			}
			return new ArraySegment<byte>(array, 0, num);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00004C40 File Offset: 0x00002E40
		public static int TranslateKeys(Keys keys)
		{
			Keys keys2 = keys & Keys.KeyCode;
			int num;
			if (keys2 <= Keys.Delete)
			{
				if (keys2 <= Keys.Tab)
				{
					if (keys2 == Keys.Back)
					{
						num = 8;
						goto IL_1C0;
					}
					if (keys2 == Keys.Tab)
					{
						num = 9;
						goto IL_1C0;
					}
				}
				else
				{
					if (keys2 == Keys.Return)
					{
						num = 13;
						goto IL_1C0;
					}
					switch (keys2)
					{
					case Keys.Escape:
						num = 7;
						goto IL_1C0;
					case Keys.Prior:
						num = 306;
						goto IL_1C0;
					case Keys.Next:
						num = 307;
						goto IL_1C0;
					case Keys.End:
						num = 305;
						goto IL_1C0;
					case Keys.Home:
						num = 304;
						goto IL_1C0;
					case Keys.Left:
						num = 302;
						goto IL_1C0;
					case Keys.Up:
						num = 301;
						goto IL_1C0;
					case Keys.Right:
						num = 303;
						goto IL_1C0;
					case Keys.Down:
						num = 300;
						goto IL_1C0;
					case Keys.Insert:
						num = 309;
						goto IL_1C0;
					case Keys.Delete:
						num = 308;
						goto IL_1C0;
					}
				}
			}
			else if (keys2 <= Keys.Divide)
			{
				switch (keys2)
				{
				case Keys.LWin:
					num = 313;
					goto IL_1C0;
				case Keys.RWin:
					num = 314;
					goto IL_1C0;
				case Keys.Apps:
					num = 315;
					goto IL_1C0;
				default:
					switch (keys2)
					{
					case Keys.Add:
						num = 310;
						goto IL_1C0;
					case Keys.Subtract:
						num = 311;
						goto IL_1C0;
					case Keys.Divide:
						num = 312;
						goto IL_1C0;
					}
					break;
				}
			}
			else
			{
				if (keys2 == Keys.OemQuestion)
				{
					num = 47;
					goto IL_1C0;
				}
				if (keys2 == Keys.Oemtilde)
				{
					num = 96;
					goto IL_1C0;
				}
				switch (keys2)
				{
				case Keys.OemOpenBrackets:
					num = 91;
					goto IL_1C0;
				case Keys.OemPipe:
					num = 92;
					goto IL_1C0;
				case Keys.OemCloseBrackets:
					num = 93;
					goto IL_1C0;
				}
			}
			num = (int)(keys & Keys.KeyCode);
			IL_1C0:
			return num | (int)(keys & Keys.Modifiers);
		}

		// Token: 0x040000B9 RID: 185
		private static bool registeredFormats;

		// Token: 0x040000BA RID: 186
		private static uint CF_HTML;

		// Token: 0x040000BB RID: 187
		private static uint CF_RTF;

		// Token: 0x040000BC RID: 188
		private static uint CF_LINESELECT;

		// Token: 0x040000BD RID: 189
		private static uint CF_VSLINETAG;

		// Token: 0x02000059 RID: 89
		private struct StyleData
		{
			// Token: 0x0400082A RID: 2090
			public bool Used;

			// Token: 0x0400082B RID: 2091
			public string FontName;

			// Token: 0x0400082C RID: 2092
			public int FontIndex;

			// Token: 0x0400082D RID: 2093
			public float SizeF;

			// Token: 0x0400082E RID: 2094
			public int Weight;

			// Token: 0x0400082F RID: 2095
			public int Italic;

			// Token: 0x04000830 RID: 2096
			public int Underline;

			// Token: 0x04000831 RID: 2097
			public int BackColor;

			// Token: 0x04000832 RID: 2098
			public int BackColorIndex;

			// Token: 0x04000833 RID: 2099
			public int ForeColor;

			// Token: 0x04000834 RID: 2100
			public int ForeColorIndex;

			// Token: 0x04000835 RID: 2101
			public int Case;

			// Token: 0x04000836 RID: 2102
			public int Visible;
		}
	}
}
