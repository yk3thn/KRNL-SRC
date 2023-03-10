using System;
using System.Runtime.InteropServices;

namespace ScintillaNET
{
	// Token: 0x02000037 RID: 55
	internal static class NativeMethods
	{
		// Token: 0x0600010B RID: 267
		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CloseClipboard();

		// Token: 0x0600010C RID: 268
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr GetProcAddress(HandleRef hModule, string lpProcName);

		// Token: 0x0600010D RID: 269
		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool EmptyClipboard();

		// Token: 0x0600010E RID: 270
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "LoadLibraryW", SetLastError = true)]
		public static extern IntPtr LoadLibrary(string lpFileName);

		// Token: 0x0600010F RID: 271
		[DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory", SetLastError = true)]
		public static extern void MoveMemory(IntPtr dest, IntPtr src, int length);

		// Token: 0x06000110 RID: 272
		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool OpenClipboard(IntPtr hWndNewOwner);

		// Token: 0x06000111 RID: 273
		[DllImport("user32.dll", SetLastError = true)]
		public static extern uint RegisterClipboardFormat(string lpszFormat);

		// Token: 0x06000112 RID: 274
		[DllImport("ole32.dll", ExactSpelling = true)]
		public static extern int RevokeDragDrop(IntPtr hwnd);

		// Token: 0x06000113 RID: 275
		[DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "SendMessageW", SetLastError = true)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x06000114 RID: 276
		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr SetClipboardData(uint uFormat, IntPtr hMem);

		// Token: 0x06000115 RID: 277
		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

		// Token: 0x04000179 RID: 377
		private const string DLL_NAME_KERNEL32 = "kernel32.dll";

		// Token: 0x0400017A RID: 378
		private const string DLL_NAME_OLE32 = "ole32.dll";

		// Token: 0x0400017B RID: 379
		private const string DLL_NAME_USER32 = "user32.dll";

		// Token: 0x0400017C RID: 380
		public const int INVALID_POSITION = -1;

		// Token: 0x0400017D RID: 381
		public const int SC_AC_FILLUP = 1;

		// Token: 0x0400017E RID: 382
		public const int SC_AC_DOUBLECLICK = 2;

		// Token: 0x0400017F RID: 383
		public const int SC_AC_TAB = 3;

		// Token: 0x04000180 RID: 384
		public const int SC_AC_NEWLINE = 4;

		// Token: 0x04000181 RID: 385
		public const int SC_AC_COMMAND = 5;

		// Token: 0x04000182 RID: 386
		public const int ANNOTATION_HIDDEN = 0;

		// Token: 0x04000183 RID: 387
		public const int ANNOTATION_STANDARD = 1;

		// Token: 0x04000184 RID: 388
		public const int ANNOTATION_BOXED = 2;

		// Token: 0x04000185 RID: 389
		public const int ANNOTATION_INDENTED = 3;

		// Token: 0x04000186 RID: 390
		public const string CF_HTML = "HTML Format";

		// Token: 0x04000187 RID: 391
		public const int SC_IDLESTYLING_NONE = 0;

		// Token: 0x04000188 RID: 392
		public const int SC_IDLESTYLING_TOVISIBLE = 1;

		// Token: 0x04000189 RID: 393
		public const int SC_IDLESTYLING_AFTERVISIBLE = 2;

		// Token: 0x0400018A RID: 394
		public const int SC_IDLESTYLING_ALL = 3;

		// Token: 0x0400018B RID: 395
		public const int SC_IV_NONE = 0;

		// Token: 0x0400018C RID: 396
		public const int SC_IV_REAL = 1;

		// Token: 0x0400018D RID: 397
		public const int SC_IV_LOOKFORWARD = 2;

		// Token: 0x0400018E RID: 398
		public const int SC_IV_LOOKBOTH = 3;

		// Token: 0x0400018F RID: 399
		public const int SCMOD_NORM = 0;

		// Token: 0x04000190 RID: 400
		public const int SCMOD_SHIFT = 1;

		// Token: 0x04000191 RID: 401
		public const int SCMOD_CTRL = 2;

		// Token: 0x04000192 RID: 402
		public const int SCMOD_ALT = 4;

		// Token: 0x04000193 RID: 403
		public const int SCMOD_SUPER = 8;

		// Token: 0x04000194 RID: 404
		public const int SCMOD_META = 16;

		// Token: 0x04000195 RID: 405
		public const int SCI_NORM = 0;

		// Token: 0x04000196 RID: 406
		public const int SCI_SHIFT = 1;

		// Token: 0x04000197 RID: 407
		public const int SCI_CTRL = 2;

		// Token: 0x04000198 RID: 408
		public const int SCI_ALT = 4;

		// Token: 0x04000199 RID: 409
		public const int SCI_META = 16;

		// Token: 0x0400019A RID: 410
		public const int SCI_CSHIFT = 3;

		// Token: 0x0400019B RID: 411
		public const int SCI_ASHIFT = 5;

		// Token: 0x0400019C RID: 412
		public const int CARETSTYLE_INVISIBLE = 0;

		// Token: 0x0400019D RID: 413
		public const int CARETSTYLE_LINE = 1;

		// Token: 0x0400019E RID: 414
		public const int CARETSTYLE_BLOCK = 2;

		// Token: 0x0400019F RID: 415
		public const int EDGE_NONE = 0;

		// Token: 0x040001A0 RID: 416
		public const int EDGE_LINE = 1;

		// Token: 0x040001A1 RID: 417
		public const int EDGE_BACKGROUND = 2;

		// Token: 0x040001A2 RID: 418
		public const int EDGE_MULTILINE = 3;

		// Token: 0x040001A3 RID: 419
		public const int HWND_MESSAGE = -3;

		// Token: 0x040001A4 RID: 420
		public const int INDIC_PLAIN = 0;

		// Token: 0x040001A5 RID: 421
		public const int INDIC_SQUIGGLE = 1;

		// Token: 0x040001A6 RID: 422
		public const int INDIC_TT = 2;

		// Token: 0x040001A7 RID: 423
		public const int INDIC_DIAGONAL = 3;

		// Token: 0x040001A8 RID: 424
		public const int INDIC_STRIKE = 4;

		// Token: 0x040001A9 RID: 425
		public const int INDIC_HIDDEN = 5;

		// Token: 0x040001AA RID: 426
		public const int INDIC_BOX = 6;

		// Token: 0x040001AB RID: 427
		public const int INDIC_ROUNDBOX = 7;

		// Token: 0x040001AC RID: 428
		public const int INDIC_STRAIGHTBOX = 8;

		// Token: 0x040001AD RID: 429
		public const int INDIC_DASH = 9;

		// Token: 0x040001AE RID: 430
		public const int INDIC_DOTS = 10;

		// Token: 0x040001AF RID: 431
		public const int INDIC_SQUIGGLELOW = 11;

		// Token: 0x040001B0 RID: 432
		public const int INDIC_DOTBOX = 12;

		// Token: 0x040001B1 RID: 433
		public const int INDIC_SQUIGGLEPIXMAP = 13;

		// Token: 0x040001B2 RID: 434
		public const int INDIC_COMPOSITIONTHICK = 14;

		// Token: 0x040001B3 RID: 435
		public const int INDIC_COMPOSITIONTHIN = 15;

		// Token: 0x040001B4 RID: 436
		public const int INDIC_FULLBOX = 16;

		// Token: 0x040001B5 RID: 437
		public const int INDIC_TEXTFORE = 17;

		// Token: 0x040001B6 RID: 438
		public const int INDIC_POINT = 18;

		// Token: 0x040001B7 RID: 439
		public const int INDIC_POINTCHARACTER = 19;

		// Token: 0x040001B8 RID: 440
		public const int INDIC_MAX = 31;

		// Token: 0x040001B9 RID: 441
		public const int INDIC_CONTAINER = 8;

		// Token: 0x040001BA RID: 442
		public const int SC_PHASES_ONE = 0;

		// Token: 0x040001BB RID: 443
		public const int SC_PHASES_TWO = 1;

		// Token: 0x040001BC RID: 444
		public const int SC_PHASES_MULTIPLE = 2;

		// Token: 0x040001BD RID: 445
		public const int SC_INDICFLAG_VALUEFORE = 1;

		// Token: 0x040001BE RID: 446
		public const int SC_INDICVALUEBIT = 16777216;

		// Token: 0x040001BF RID: 447
		public const int SC_INDICVALUEMASK = 16777215;

		// Token: 0x040001C0 RID: 448
		public const int KEYWORDSET_MAX = 8;

		// Token: 0x040001C1 RID: 449
		public const int SC_ALPHA_TRANSPARENT = 0;

		// Token: 0x040001C2 RID: 450
		public const int SC_ALPHA_OPAQUE = 255;

		// Token: 0x040001C3 RID: 451
		public const int SC_ALPHA_NOALPHA = 256;

		// Token: 0x040001C4 RID: 452
		public const int SC_AUTOMATICFOLD_SHOW = 1;

		// Token: 0x040001C5 RID: 453
		public const int SC_AUTOMATICFOLD_CLICK = 2;

		// Token: 0x040001C6 RID: 454
		public const int SC_AUTOMATICFOLD_CHANGE = 4;

		// Token: 0x040001C7 RID: 455
		public const int SC_CARETSTICKY_OFF = 0;

		// Token: 0x040001C8 RID: 456
		public const int SC_CARETSTICKY_ON = 1;

		// Token: 0x040001C9 RID: 457
		public const int SC_CARETSTICKY_WHITESPACE = 2;

		// Token: 0x040001CA RID: 458
		public const int SC_CP_UTF8 = 65001;

		// Token: 0x040001CB RID: 459
		public const int SC_CURSORNORMAL = -1;

		// Token: 0x040001CC RID: 460
		public const int SC_CURSORARROW = 2;

		// Token: 0x040001CD RID: 461
		public const int SC_CURSORWAIT = 4;

		// Token: 0x040001CE RID: 462
		public const int SC_CURSORREVERSEARROW = 7;

		// Token: 0x040001CF RID: 463
		public const int SC_EFF_QUALITY_DEFAULT = 0;

		// Token: 0x040001D0 RID: 464
		public const int SC_EFF_QUALITY_NON_ANTIALIASED = 1;

		// Token: 0x040001D1 RID: 465
		public const int SC_EFF_QUALITY_ANTIALIASED = 2;

		// Token: 0x040001D2 RID: 466
		public const int SC_EFF_QUALITY_LCD_OPTIMIZED = 3;

		// Token: 0x040001D3 RID: 467
		public const int SC_EOL_CRLF = 0;

		// Token: 0x040001D4 RID: 468
		public const int SC_EOL_CR = 1;

		// Token: 0x040001D5 RID: 469
		public const int SC_EOL_LF = 2;

		// Token: 0x040001D6 RID: 470
		public const int SC_FOLDACTION_CONTRACT = 0;

		// Token: 0x040001D7 RID: 471
		public const int SC_FOLDACTION_EXPAND = 1;

		// Token: 0x040001D8 RID: 472
		public const int SC_FOLDACTION_TOGGLE = 2;

		// Token: 0x040001D9 RID: 473
		public const int SC_FOLDLEVELBASE = 1024;

		// Token: 0x040001DA RID: 474
		public const int SC_FOLDLEVELWHITEFLAG = 4096;

		// Token: 0x040001DB RID: 475
		public const int SC_FOLDLEVELHEADERFLAG = 8192;

		// Token: 0x040001DC RID: 476
		public const int SC_FOLDLEVELNUMBERMASK = 4095;

		// Token: 0x040001DD RID: 477
		public const int SC_FOLDFLAG_LINEBEFORE_EXPANDED = 2;

		// Token: 0x040001DE RID: 478
		public const int SC_FOLDFLAG_LINEBEFORE_CONTRACTED = 4;

		// Token: 0x040001DF RID: 479
		public const int SC_FOLDFLAG_LINEAFTER_EXPANDED = 8;

		// Token: 0x040001E0 RID: 480
		public const int SC_FOLDFLAG_LINEAFTER_CONTRACTED = 16;

		// Token: 0x040001E1 RID: 481
		public const int SC_FOLDFLAG_LEVELNUMBERS = 64;

		// Token: 0x040001E2 RID: 482
		public const int SC_FOLDFLAG_LINESTATE = 128;

		// Token: 0x040001E3 RID: 483
		public const int SC_FOLDDISPLAYTEXT_HIDDEN = 0;

		// Token: 0x040001E4 RID: 484
		public const int SC_FOLDDISPLAYTEXT_STANDARD = 1;

		// Token: 0x040001E5 RID: 485
		public const int SC_FOLDDISPLAYTEXT_BOXED = 2;

		// Token: 0x040001E6 RID: 486
		public const int SC_LINE_END_TYPE_DEFAULT = 0;

		// Token: 0x040001E7 RID: 487
		public const int SC_LINE_END_TYPE_UNICODE = 1;

		// Token: 0x040001E8 RID: 488
		public const int SC_MAX_MARGIN = 4;

		// Token: 0x040001E9 RID: 489
		public const int SC_MARGIN_SYMBOL = 0;

		// Token: 0x040001EA RID: 490
		public const int SC_MARGIN_NUMBER = 1;

		// Token: 0x040001EB RID: 491
		public const int SC_MARGIN_BACK = 2;

		// Token: 0x040001EC RID: 492
		public const int SC_MARGIN_FORE = 3;

		// Token: 0x040001ED RID: 493
		public const int SC_MARGIN_TEXT = 4;

		// Token: 0x040001EE RID: 494
		public const int SC_MARGIN_RTEXT = 5;

		// Token: 0x040001EF RID: 495
		public const int SC_MARGIN_COLOUR = 6;

		// Token: 0x040001F0 RID: 496
		public const int SC_MARGINOPTION_NONE = 0;

		// Token: 0x040001F1 RID: 497
		public const int SC_MARGINOPTION_SUBLINESELECT = 1;

		// Token: 0x040001F2 RID: 498
		public const int MARKER_MAX = 31;

		// Token: 0x040001F3 RID: 499
		public const int SC_MARK_CIRCLE = 0;

		// Token: 0x040001F4 RID: 500
		public const int SC_MARK_ROUNDRECT = 1;

		// Token: 0x040001F5 RID: 501
		public const int SC_MARK_ARROW = 2;

		// Token: 0x040001F6 RID: 502
		public const int SC_MARK_SMALLRECT = 3;

		// Token: 0x040001F7 RID: 503
		public const int SC_MARK_SHORTARROW = 4;

		// Token: 0x040001F8 RID: 504
		public const int SC_MARK_EMPTY = 5;

		// Token: 0x040001F9 RID: 505
		public const int SC_MARK_ARROWDOWN = 6;

		// Token: 0x040001FA RID: 506
		public const int SC_MARK_MINUS = 7;

		// Token: 0x040001FB RID: 507
		public const int SC_MARK_PLUS = 8;

		// Token: 0x040001FC RID: 508
		public const int SC_MARK_VLINE = 9;

		// Token: 0x040001FD RID: 509
		public const int SC_MARK_LCORNER = 10;

		// Token: 0x040001FE RID: 510
		public const int SC_MARK_TCORNER = 11;

		// Token: 0x040001FF RID: 511
		public const int SC_MARK_BOXPLUS = 12;

		// Token: 0x04000200 RID: 512
		public const int SC_MARK_BOXPLUSCONNECTED = 13;

		// Token: 0x04000201 RID: 513
		public const int SC_MARK_BOXMINUS = 14;

		// Token: 0x04000202 RID: 514
		public const int SC_MARK_BOXMINUSCONNECTED = 15;

		// Token: 0x04000203 RID: 515
		public const int SC_MARK_LCORNERCURVE = 16;

		// Token: 0x04000204 RID: 516
		public const int SC_MARK_TCORNERCURVE = 17;

		// Token: 0x04000205 RID: 517
		public const int SC_MARK_CIRCLEPLUS = 18;

		// Token: 0x04000206 RID: 518
		public const int SC_MARK_CIRCLEPLUSCONNECTED = 19;

		// Token: 0x04000207 RID: 519
		public const int SC_MARK_CIRCLEMINUS = 20;

		// Token: 0x04000208 RID: 520
		public const int SC_MARK_CIRCLEMINUSCONNECTED = 21;

		// Token: 0x04000209 RID: 521
		public const int SC_MARK_BACKGROUND = 22;

		// Token: 0x0400020A RID: 522
		public const int SC_MARK_DOTDOTDOT = 23;

		// Token: 0x0400020B RID: 523
		public const int SC_MARK_ARROWS = 24;

		// Token: 0x0400020C RID: 524
		public const int SC_MARK_PIXMAP = 25;

		// Token: 0x0400020D RID: 525
		public const int SC_MARK_FULLRECT = 26;

		// Token: 0x0400020E RID: 526
		public const int SC_MARK_LEFTRECT = 27;

		// Token: 0x0400020F RID: 527
		public const int SC_MARK_AVAILABLE = 28;

		// Token: 0x04000210 RID: 528
		public const int SC_MARK_UNDERLINE = 29;

		// Token: 0x04000211 RID: 529
		public const int SC_MARK_RGBAIMAGE = 30;

		// Token: 0x04000212 RID: 530
		public const int SC_MARK_BOOKMARK = 31;

		// Token: 0x04000213 RID: 531
		public const int SC_MARK_CHARACTER = 10000;

		// Token: 0x04000214 RID: 532
		public const int SC_MARKNUM_FOLDEREND = 25;

		// Token: 0x04000215 RID: 533
		public const int SC_MARKNUM_FOLDEROPENMID = 26;

		// Token: 0x04000216 RID: 534
		public const int SC_MARKNUM_FOLDERMIDTAIL = 27;

		// Token: 0x04000217 RID: 535
		public const int SC_MARKNUM_FOLDERTAIL = 28;

		// Token: 0x04000218 RID: 536
		public const int SC_MARKNUM_FOLDERSUB = 29;

		// Token: 0x04000219 RID: 537
		public const int SC_MARKNUM_FOLDER = 30;

		// Token: 0x0400021A RID: 538
		public const int SC_MARKNUM_FOLDEROPEN = 31;

		// Token: 0x0400021B RID: 539
		public const uint SC_MASK_FOLDERS = 4261412864U;

		// Token: 0x0400021C RID: 540
		public const int SC_MULTIPASTE_ONCE = 0;

		// Token: 0x0400021D RID: 541
		public const int SC_MULTIPASTE_EACH = 1;

		// Token: 0x0400021E RID: 542
		public const int SC_ORDER_PRESORTED = 0;

		// Token: 0x0400021F RID: 543
		public const int SC_ORDER_PERFORMSORT = 1;

		// Token: 0x04000220 RID: 544
		public const int SC_ORDER_CUSTOM = 2;

		// Token: 0x04000221 RID: 545
		public const int SC_UPDATE_CONTENT = 1;

		// Token: 0x04000222 RID: 546
		public const int SC_UPDATE_SELECTION = 2;

		// Token: 0x04000223 RID: 547
		public const int SC_UPDATE_V_SCROLL = 4;

		// Token: 0x04000224 RID: 548
		public const int SC_UPDATE_H_SCROLL = 8;

		// Token: 0x04000225 RID: 549
		public const int SC_MOD_INSERTTEXT = 1;

		// Token: 0x04000226 RID: 550
		public const int SC_MOD_DELETETEXT = 2;

		// Token: 0x04000227 RID: 551
		public const int SC_MOD_BEFOREINSERT = 1024;

		// Token: 0x04000228 RID: 552
		public const int SC_MOD_BEFOREDELETE = 2048;

		// Token: 0x04000229 RID: 553
		public const int SC_MOD_CHANGEANNOTATION = 131072;

		// Token: 0x0400022A RID: 554
		public const int SC_MOD_INSERTCHECK = 1048576;

		// Token: 0x0400022B RID: 555
		public const int SC_PERFORMED_USER = 16;

		// Token: 0x0400022C RID: 556
		public const int SC_PERFORMED_UNDO = 32;

		// Token: 0x0400022D RID: 557
		public const int SC_PERFORMED_REDO = 64;

		// Token: 0x0400022E RID: 558
		public const int SC_STATUS_OK = 0;

		// Token: 0x0400022F RID: 559
		public const int SC_STATUS_FAILURE = 1;

		// Token: 0x04000230 RID: 560
		public const int SC_STATUS_BADALLOC = 2;

		// Token: 0x04000231 RID: 561
		public const int SC_STATUS_WARN_START = 1000;

		// Token: 0x04000232 RID: 562
		public const int SC_STATUS_WARN_REGEX = 1001;

		// Token: 0x04000233 RID: 563
		public const int SC_TIME_FOREVER = 10000000;

		// Token: 0x04000234 RID: 564
		public const int SC_TYPE_BOOLEAN = 0;

		// Token: 0x04000235 RID: 565
		public const int SC_TYPE_INTEGER = 1;

		// Token: 0x04000236 RID: 566
		public const int SC_TYPE_STRING = 2;

		// Token: 0x04000237 RID: 567
		public const int SCFIND_WHOLEWORD = 2;

		// Token: 0x04000238 RID: 568
		public const int SCFIND_MATCHCASE = 4;

		// Token: 0x04000239 RID: 569
		public const int SCFIND_WORDSTART = 1048576;

		// Token: 0x0400023A RID: 570
		public const int SCFIND_REGEXP = 2097152;

		// Token: 0x0400023B RID: 571
		public const int SCFIND_POSIX = 4194304;

		// Token: 0x0400023C RID: 572
		public const int SCFIND_CXX11REGEX = 8388608;

		// Token: 0x0400023D RID: 573
		public const int SCI_START = 2000;

		// Token: 0x0400023E RID: 574
		public const int SCI_OPTIONAL_START = 3000;

		// Token: 0x0400023F RID: 575
		public const int SCI_LEXER_START = 4000;

		// Token: 0x04000240 RID: 576
		public const int SCI_ADDTEXT = 2001;

		// Token: 0x04000241 RID: 577
		public const int SCI_ADDSTYLEDTEXT = 2002;

		// Token: 0x04000242 RID: 578
		public const int SCI_INSERTTEXT = 2003;

		// Token: 0x04000243 RID: 579
		public const int SCI_CHANGEINSERTION = 2672;

		// Token: 0x04000244 RID: 580
		public const int SCI_CLEARALL = 2004;

		// Token: 0x04000245 RID: 581
		public const int SCI_DELETERANGE = 2645;

		// Token: 0x04000246 RID: 582
		public const int SCI_CLEARDOCUMENTSTYLE = 2005;

		// Token: 0x04000247 RID: 583
		public const int SCI_GETLENGTH = 2006;

		// Token: 0x04000248 RID: 584
		public const int SCI_GETCHARAT = 2007;

		// Token: 0x04000249 RID: 585
		public const int SCI_GETCURRENTPOS = 2008;

		// Token: 0x0400024A RID: 586
		public const int SCI_GETANCHOR = 2009;

		// Token: 0x0400024B RID: 587
		public const int SCI_GETSTYLEAT = 2010;

		// Token: 0x0400024C RID: 588
		public const int SCI_REDO = 2011;

		// Token: 0x0400024D RID: 589
		public const int SCI_SETUNDOCOLLECTION = 2012;

		// Token: 0x0400024E RID: 590
		public const int SCI_SELECTALL = 2013;

		// Token: 0x0400024F RID: 591
		public const int SCI_SETSAVEPOINT = 2014;

		// Token: 0x04000250 RID: 592
		public const int SCI_GETSTYLEDTEXT = 2015;

		// Token: 0x04000251 RID: 593
		public const int SCI_CANREDO = 2016;

		// Token: 0x04000252 RID: 594
		public const int SCI_MARKERLINEFROMHANDLE = 2017;

		// Token: 0x04000253 RID: 595
		public const int SCI_MARKERDELETEHANDLE = 2018;

		// Token: 0x04000254 RID: 596
		public const int SCI_GETUNDOCOLLECTION = 2019;

		// Token: 0x04000255 RID: 597
		public const int SCI_GETVIEWWS = 2020;

		// Token: 0x04000256 RID: 598
		public const int SCI_SETVIEWWS = 2021;

		// Token: 0x04000257 RID: 599
		public const int SCI_POSITIONFROMPOINT = 2022;

		// Token: 0x04000258 RID: 600
		public const int SCI_POSITIONFROMPOINTCLOSE = 2023;

		// Token: 0x04000259 RID: 601
		public const int SCI_GOTOLINE = 2024;

		// Token: 0x0400025A RID: 602
		public const int SCI_GOTOPOS = 2025;

		// Token: 0x0400025B RID: 603
		public const int SCI_SETANCHOR = 2026;

		// Token: 0x0400025C RID: 604
		public const int SCI_GETCURLINE = 2027;

		// Token: 0x0400025D RID: 605
		public const int SCI_GETENDSTYLED = 2028;

		// Token: 0x0400025E RID: 606
		public const int SCI_CONVERTEOLS = 2029;

		// Token: 0x0400025F RID: 607
		public const int SCI_GETEOLMODE = 2030;

		// Token: 0x04000260 RID: 608
		public const int SCI_SETEOLMODE = 2031;

		// Token: 0x04000261 RID: 609
		public const int SCI_STARTSTYLING = 2032;

		// Token: 0x04000262 RID: 610
		public const int SCI_SETSTYLING = 2033;

		// Token: 0x04000263 RID: 611
		public const int SCI_GETBUFFEREDDRAW = 2034;

		// Token: 0x04000264 RID: 612
		public const int SCI_SETBUFFEREDDRAW = 2035;

		// Token: 0x04000265 RID: 613
		public const int SCI_SETTABWIDTH = 2036;

		// Token: 0x04000266 RID: 614
		public const int SCI_GETTABWIDTH = 2121;

		// Token: 0x04000267 RID: 615
		public const int SCI_CLEARTABSTOPS = 2675;

		// Token: 0x04000268 RID: 616
		public const int SCI_ADDTABSTOP = 2676;

		// Token: 0x04000269 RID: 617
		public const int SCI_GETNEXTTABSTOP = 2677;

		// Token: 0x0400026A RID: 618
		public const int SCI_SETCODEPAGE = 2037;

		// Token: 0x0400026B RID: 619
		public const int SCI_MARKERDEFINE = 2040;

		// Token: 0x0400026C RID: 620
		public const int SCI_MARKERSETFORE = 2041;

		// Token: 0x0400026D RID: 621
		public const int SCI_MARKERSETBACK = 2042;

		// Token: 0x0400026E RID: 622
		public const int SCI_MARKERSETBACKSELECTED = 2292;

		// Token: 0x0400026F RID: 623
		public const int SCI_MARKERENABLEHIGHLIGHT = 2293;

		// Token: 0x04000270 RID: 624
		public const int SCI_MARKERADD = 2043;

		// Token: 0x04000271 RID: 625
		public const int SCI_MARKERDELETE = 2044;

		// Token: 0x04000272 RID: 626
		public const int SCI_MARKERDELETEALL = 2045;

		// Token: 0x04000273 RID: 627
		public const int SCI_MARKERGET = 2046;

		// Token: 0x04000274 RID: 628
		public const int SCI_MARKERNEXT = 2047;

		// Token: 0x04000275 RID: 629
		public const int SCI_MARKERPREVIOUS = 2048;

		// Token: 0x04000276 RID: 630
		public const int SCI_MARKERDEFINEPIXMAP = 2049;

		// Token: 0x04000277 RID: 631
		public const int SCI_MARKERADDSET = 2466;

		// Token: 0x04000278 RID: 632
		public const int SCI_MARKERSETALPHA = 2476;

		// Token: 0x04000279 RID: 633
		public const int SCI_SETMARGINTYPEN = 2240;

		// Token: 0x0400027A RID: 634
		public const int SCI_GETMARGINTYPEN = 2241;

		// Token: 0x0400027B RID: 635
		public const int SCI_SETMARGINWIDTHN = 2242;

		// Token: 0x0400027C RID: 636
		public const int SCI_GETMARGINWIDTHN = 2243;

		// Token: 0x0400027D RID: 637
		public const int SCI_SETMARGINMASKN = 2244;

		// Token: 0x0400027E RID: 638
		public const int SCI_GETMARGINMASKN = 2245;

		// Token: 0x0400027F RID: 639
		public const int SCI_SETMARGINSENSITIVEN = 2246;

		// Token: 0x04000280 RID: 640
		public const int SCI_GETMARGINSENSITIVEN = 2247;

		// Token: 0x04000281 RID: 641
		public const int SCI_SETMARGINCURSORN = 2248;

		// Token: 0x04000282 RID: 642
		public const int SCI_GETMARGINCURSORN = 2249;

		// Token: 0x04000283 RID: 643
		public const int SCI_SETMARGINBACKN = 2250;

		// Token: 0x04000284 RID: 644
		public const int SCI_GETMARGINBACKN = 2251;

		// Token: 0x04000285 RID: 645
		public const int SCI_SETMARGINS = 2252;

		// Token: 0x04000286 RID: 646
		public const int SCI_GETMARGINS = 2253;

		// Token: 0x04000287 RID: 647
		public const int SCI_STYLECLEARALL = 2050;

		// Token: 0x04000288 RID: 648
		public const int SCI_STYLESETFORE = 2051;

		// Token: 0x04000289 RID: 649
		public const int SCI_STYLESETBACK = 2052;

		// Token: 0x0400028A RID: 650
		public const int SCI_STYLESETBOLD = 2053;

		// Token: 0x0400028B RID: 651
		public const int SCI_STYLESETITALIC = 2054;

		// Token: 0x0400028C RID: 652
		public const int SCI_STYLESETSIZE = 2055;

		// Token: 0x0400028D RID: 653
		public const int SCI_STYLESETFONT = 2056;

		// Token: 0x0400028E RID: 654
		public const int SCI_STYLESETEOLFILLED = 2057;

		// Token: 0x0400028F RID: 655
		public const int SCI_STYLERESETDEFAULT = 2058;

		// Token: 0x04000290 RID: 656
		public const int SCI_STYLESETUNDERLINE = 2059;

		// Token: 0x04000291 RID: 657
		public const int SCI_STYLEGETFORE = 2481;

		// Token: 0x04000292 RID: 658
		public const int SCI_STYLEGETBACK = 2482;

		// Token: 0x04000293 RID: 659
		public const int SCI_STYLEGETBOLD = 2483;

		// Token: 0x04000294 RID: 660
		public const int SCI_STYLEGETITALIC = 2484;

		// Token: 0x04000295 RID: 661
		public const int SCI_STYLEGETSIZE = 2485;

		// Token: 0x04000296 RID: 662
		public const int SCI_STYLEGETFONT = 2486;

		// Token: 0x04000297 RID: 663
		public const int SCI_STYLEGETEOLFILLED = 2487;

		// Token: 0x04000298 RID: 664
		public const int SCI_STYLEGETUNDERLINE = 2488;

		// Token: 0x04000299 RID: 665
		public const int SCI_STYLEGETCASE = 2489;

		// Token: 0x0400029A RID: 666
		public const int SCI_STYLEGETCHARACTERSET = 2490;

		// Token: 0x0400029B RID: 667
		public const int SCI_STYLEGETVISIBLE = 2491;

		// Token: 0x0400029C RID: 668
		public const int SCI_STYLEGETCHANGEABLE = 2492;

		// Token: 0x0400029D RID: 669
		public const int SCI_STYLEGETHOTSPOT = 2493;

		// Token: 0x0400029E RID: 670
		public const int SCI_STYLESETCASE = 2060;

		// Token: 0x0400029F RID: 671
		public const int SCI_STYLESETSIZEFRACTIONAL = 2061;

		// Token: 0x040002A0 RID: 672
		public const int SCI_STYLEGETSIZEFRACTIONAL = 2062;

		// Token: 0x040002A1 RID: 673
		public const int SCI_STYLESETWEIGHT = 2063;

		// Token: 0x040002A2 RID: 674
		public const int SCI_STYLEGETWEIGHT = 2064;

		// Token: 0x040002A3 RID: 675
		public const int SCI_STYLESETCHARACTERSET = 2066;

		// Token: 0x040002A4 RID: 676
		public const int SCI_STYLESETHOTSPOT = 2409;

		// Token: 0x040002A5 RID: 677
		public const int SCI_SETSELFORE = 2067;

		// Token: 0x040002A6 RID: 678
		public const int SCI_SETSELBACK = 2068;

		// Token: 0x040002A7 RID: 679
		public const int SCI_GETSELALPHA = 2477;

		// Token: 0x040002A8 RID: 680
		public const int SCI_SETSELALPHA = 2478;

		// Token: 0x040002A9 RID: 681
		public const int SCI_GETSELEOLFILLED = 2479;

		// Token: 0x040002AA RID: 682
		public const int SCI_SETSELEOLFILLED = 2480;

		// Token: 0x040002AB RID: 683
		public const int SCI_SETCARETFORE = 2069;

		// Token: 0x040002AC RID: 684
		public const int SCI_ASSIGNCMDKEY = 2070;

		// Token: 0x040002AD RID: 685
		public const int SCI_CLEARCMDKEY = 2071;

		// Token: 0x040002AE RID: 686
		public const int SCI_CLEARALLCMDKEYS = 2072;

		// Token: 0x040002AF RID: 687
		public const int SCI_SETSTYLINGEX = 2073;

		// Token: 0x040002B0 RID: 688
		public const int SCI_STYLESETVISIBLE = 2074;

		// Token: 0x040002B1 RID: 689
		public const int SCI_GETCARETPERIOD = 2075;

		// Token: 0x040002B2 RID: 690
		public const int SCI_SETCARETPERIOD = 2076;

		// Token: 0x040002B3 RID: 691
		public const int SCI_SETWORDCHARS = 2077;

		// Token: 0x040002B4 RID: 692
		public const int SCI_GETWORDCHARS = 2646;

		// Token: 0x040002B5 RID: 693
		public const int SCI_BEGINUNDOACTION = 2078;

		// Token: 0x040002B6 RID: 694
		public const int SCI_ENDUNDOACTION = 2079;

		// Token: 0x040002B7 RID: 695
		public const int SCI_INDICSETSTYLE = 2080;

		// Token: 0x040002B8 RID: 696
		public const int SCI_INDICGETSTYLE = 2081;

		// Token: 0x040002B9 RID: 697
		public const int SCI_INDICSETFORE = 2082;

		// Token: 0x040002BA RID: 698
		public const int SCI_INDICGETFORE = 2083;

		// Token: 0x040002BB RID: 699
		public const int SCI_INDICSETUNDER = 2510;

		// Token: 0x040002BC RID: 700
		public const int SCI_INDICGETUNDER = 2511;

		// Token: 0x040002BD RID: 701
		public const int SCI_INDICSETHOVERSTYLE = 2680;

		// Token: 0x040002BE RID: 702
		public const int SCI_INDICGETHOVERSTYLE = 2681;

		// Token: 0x040002BF RID: 703
		public const int SCI_INDICSETHOVERFORE = 2682;

		// Token: 0x040002C0 RID: 704
		public const int SCI_INDICGETHOVERFORE = 2683;

		// Token: 0x040002C1 RID: 705
		public const int SCI_INDICSETFLAGS = 2684;

		// Token: 0x040002C2 RID: 706
		public const int SCI_INDICGETFLAGS = 2685;

		// Token: 0x040002C3 RID: 707
		public const int SCI_SETWHITESPACEFORE = 2084;

		// Token: 0x040002C4 RID: 708
		public const int SCI_SETWHITESPACEBACK = 2085;

		// Token: 0x040002C5 RID: 709
		public const int SCI_SETWHITESPACESIZE = 2086;

		// Token: 0x040002C6 RID: 710
		public const int SCI_GETWHITESPACESIZE = 2087;

		// Token: 0x040002C7 RID: 711
		public const int SCI_SETLINESTATE = 2092;

		// Token: 0x040002C8 RID: 712
		public const int SCI_GETLINESTATE = 2093;

		// Token: 0x040002C9 RID: 713
		public const int SCI_GETMAXLINESTATE = 2094;

		// Token: 0x040002CA RID: 714
		public const int SCI_GETCARETLINEVISIBLE = 2095;

		// Token: 0x040002CB RID: 715
		public const int SCI_SETCARETLINEVISIBLE = 2096;

		// Token: 0x040002CC RID: 716
		public const int SCI_GETCARETLINEBACK = 2097;

		// Token: 0x040002CD RID: 717
		public const int SCI_SETCARETLINEBACK = 2098;

		// Token: 0x040002CE RID: 718
		public const int SCI_STYLESETCHANGEABLE = 2099;

		// Token: 0x040002CF RID: 719
		public const int SCI_AUTOCSHOW = 2100;

		// Token: 0x040002D0 RID: 720
		public const int SCI_AUTOCCANCEL = 2101;

		// Token: 0x040002D1 RID: 721
		public const int SCI_AUTOCACTIVE = 2102;

		// Token: 0x040002D2 RID: 722
		public const int SCI_AUTOCPOSSTART = 2103;

		// Token: 0x040002D3 RID: 723
		public const int SCI_AUTOCCOMPLETE = 2104;

		// Token: 0x040002D4 RID: 724
		public const int SCI_AUTOCSTOPS = 2105;

		// Token: 0x040002D5 RID: 725
		public const int SCI_AUTOCSETSEPARATOR = 2106;

		// Token: 0x040002D6 RID: 726
		public const int SCI_AUTOCGETSEPARATOR = 2107;

		// Token: 0x040002D7 RID: 727
		public const int SCI_AUTOCSELECT = 2108;

		// Token: 0x040002D8 RID: 728
		public const int SCI_AUTOCSETCANCELATSTART = 2110;

		// Token: 0x040002D9 RID: 729
		public const int SCI_AUTOCGETCANCELATSTART = 2111;

		// Token: 0x040002DA RID: 730
		public const int SCI_AUTOCSETFILLUPS = 2112;

		// Token: 0x040002DB RID: 731
		public const int SCI_AUTOCSETCHOOSESINGLE = 2113;

		// Token: 0x040002DC RID: 732
		public const int SCI_AUTOCGETCHOOSESINGLE = 2114;

		// Token: 0x040002DD RID: 733
		public const int SCI_AUTOCSETIGNORECASE = 2115;

		// Token: 0x040002DE RID: 734
		public const int SCI_AUTOCGETIGNORECASE = 2116;

		// Token: 0x040002DF RID: 735
		public const int SCI_USERLISTSHOW = 2117;

		// Token: 0x040002E0 RID: 736
		public const int SCI_AUTOCSETAUTOHIDE = 2118;

		// Token: 0x040002E1 RID: 737
		public const int SCI_AUTOCGETAUTOHIDE = 2119;

		// Token: 0x040002E2 RID: 738
		public const int SCI_AUTOCSETDROPRESTOFWORD = 2270;

		// Token: 0x040002E3 RID: 739
		public const int SCI_AUTOCGETDROPRESTOFWORD = 2271;

		// Token: 0x040002E4 RID: 740
		public const int SCI_REGISTERIMAGE = 2405;

		// Token: 0x040002E5 RID: 741
		public const int SCI_CLEARREGISTEREDIMAGES = 2408;

		// Token: 0x040002E6 RID: 742
		public const int SCI_AUTOCGETTYPESEPARATOR = 2285;

		// Token: 0x040002E7 RID: 743
		public const int SCI_AUTOCSETTYPESEPARATOR = 2286;

		// Token: 0x040002E8 RID: 744
		public const int SCI_AUTOCSETMAXWIDTH = 2208;

		// Token: 0x040002E9 RID: 745
		public const int SCI_AUTOCGETMAXWIDTH = 2209;

		// Token: 0x040002EA RID: 746
		public const int SCI_AUTOCSETMAXHEIGHT = 2210;

		// Token: 0x040002EB RID: 747
		public const int SCI_AUTOCGETMAXHEIGHT = 2211;

		// Token: 0x040002EC RID: 748
		public const int SCI_SETINDENT = 2122;

		// Token: 0x040002ED RID: 749
		public const int SCI_GETINDENT = 2123;

		// Token: 0x040002EE RID: 750
		public const int SCI_SETUSETABS = 2124;

		// Token: 0x040002EF RID: 751
		public const int SCI_GETUSETABS = 2125;

		// Token: 0x040002F0 RID: 752
		public const int SCI_SETLINEINDENTATION = 2126;

		// Token: 0x040002F1 RID: 753
		public const int SCI_GETLINEINDENTATION = 2127;

		// Token: 0x040002F2 RID: 754
		public const int SCI_GETLINEINDENTPOSITION = 2128;

		// Token: 0x040002F3 RID: 755
		public const int SCI_GETCOLUMN = 2129;

		// Token: 0x040002F4 RID: 756
		public const int SCI_COUNTCHARACTERS = 2633;

		// Token: 0x040002F5 RID: 757
		public const int SCI_SETHSCROLLBAR = 2130;

		// Token: 0x040002F6 RID: 758
		public const int SCI_GETHSCROLLBAR = 2131;

		// Token: 0x040002F7 RID: 759
		public const int SCI_SETINDENTATIONGUIDES = 2132;

		// Token: 0x040002F8 RID: 760
		public const int SCI_GETINDENTATIONGUIDES = 2133;

		// Token: 0x040002F9 RID: 761
		public const int SCI_SETHIGHLIGHTGUIDE = 2134;

		// Token: 0x040002FA RID: 762
		public const int SCI_GETHIGHLIGHTGUIDE = 2135;

		// Token: 0x040002FB RID: 763
		public const int SCI_GETLINEENDPOSITION = 2136;

		// Token: 0x040002FC RID: 764
		public const int SCI_GETCODEPAGE = 2137;

		// Token: 0x040002FD RID: 765
		public const int SCI_GETCARETFORE = 2138;

		// Token: 0x040002FE RID: 766
		public const int SCI_GETREADONLY = 2140;

		// Token: 0x040002FF RID: 767
		public const int SCI_SETCURRENTPOS = 2141;

		// Token: 0x04000300 RID: 768
		public const int SCI_SETSELECTIONSTART = 2142;

		// Token: 0x04000301 RID: 769
		public const int SCI_GETSELECTIONSTART = 2143;

		// Token: 0x04000302 RID: 770
		public const int SCI_SETSELECTIONEND = 2144;

		// Token: 0x04000303 RID: 771
		public const int SCI_GETSELECTIONEND = 2145;

		// Token: 0x04000304 RID: 772
		public const int SCI_SETEMPTYSELECTION = 2556;

		// Token: 0x04000305 RID: 773
		public const int SCI_SETPRINTMAGNIFICATION = 2146;

		// Token: 0x04000306 RID: 774
		public const int SCI_GETPRINTMAGNIFICATION = 2147;

		// Token: 0x04000307 RID: 775
		public const int SCI_SETPRINTCOLOURMODE = 2148;

		// Token: 0x04000308 RID: 776
		public const int SCI_GETPRINTCOLOURMODE = 2149;

		// Token: 0x04000309 RID: 777
		public const int SCI_FINDTEXT = 2150;

		// Token: 0x0400030A RID: 778
		public const int SCI_FORMATRANGE = 2151;

		// Token: 0x0400030B RID: 779
		public const int SCI_GETFIRSTVISIBLELINE = 2152;

		// Token: 0x0400030C RID: 780
		public const int SCI_GETLINE = 2153;

		// Token: 0x0400030D RID: 781
		public const int SCI_GETLINECOUNT = 2154;

		// Token: 0x0400030E RID: 782
		public const int SCI_SETMARGINLEFT = 2155;

		// Token: 0x0400030F RID: 783
		public const int SCI_GETMARGINLEFT = 2156;

		// Token: 0x04000310 RID: 784
		public const int SCI_SETMARGINRIGHT = 2157;

		// Token: 0x04000311 RID: 785
		public const int SCI_GETMARGINRIGHT = 2158;

		// Token: 0x04000312 RID: 786
		public const int SCI_GETMODIFY = 2159;

		// Token: 0x04000313 RID: 787
		public const int SCI_SETSEL = 2160;

		// Token: 0x04000314 RID: 788
		public const int SCI_GETSELTEXT = 2161;

		// Token: 0x04000315 RID: 789
		public const int SCI_GETTEXTRANGE = 2162;

		// Token: 0x04000316 RID: 790
		public const int SCI_HIDESELECTION = 2163;

		// Token: 0x04000317 RID: 791
		public const int SCI_POINTXFROMPOSITION = 2164;

		// Token: 0x04000318 RID: 792
		public const int SCI_POINTYFROMPOSITION = 2165;

		// Token: 0x04000319 RID: 793
		public const int SCI_LINEFROMPOSITION = 2166;

		// Token: 0x0400031A RID: 794
		public const int SCI_POSITIONFROMLINE = 2167;

		// Token: 0x0400031B RID: 795
		public const int SCI_LINESCROLL = 2168;

		// Token: 0x0400031C RID: 796
		public const int SCI_SCROLLCARET = 2169;

		// Token: 0x0400031D RID: 797
		public const int SCI_SCROLLRANGE = 2569;

		// Token: 0x0400031E RID: 798
		public const int SCI_REPLACESEL = 2170;

		// Token: 0x0400031F RID: 799
		public const int SCI_SETREADONLY = 2171;

		// Token: 0x04000320 RID: 800
		public const int SCI_NULL = 2172;

		// Token: 0x04000321 RID: 801
		public const int SCI_CANPASTE = 2173;

		// Token: 0x04000322 RID: 802
		public const int SCI_CANUNDO = 2174;

		// Token: 0x04000323 RID: 803
		public const int SCI_EMPTYUNDOBUFFER = 2175;

		// Token: 0x04000324 RID: 804
		public const int SCI_UNDO = 2176;

		// Token: 0x04000325 RID: 805
		public const int SCI_CUT = 2177;

		// Token: 0x04000326 RID: 806
		public const int SCI_COPY = 2178;

		// Token: 0x04000327 RID: 807
		public const int SCI_PASTE = 2179;

		// Token: 0x04000328 RID: 808
		public const int SCI_CLEAR = 2180;

		// Token: 0x04000329 RID: 809
		public const int SCI_SETTEXT = 2181;

		// Token: 0x0400032A RID: 810
		public const int SCI_GETTEXT = 2182;

		// Token: 0x0400032B RID: 811
		public const int SCI_GETTEXTLENGTH = 2183;

		// Token: 0x0400032C RID: 812
		public const int SCI_GETDIRECTFUNCTION = 2184;

		// Token: 0x0400032D RID: 813
		public const int SCI_GETDIRECTPOINTER = 2185;

		// Token: 0x0400032E RID: 814
		public const int SCI_SETOVERTYPE = 2186;

		// Token: 0x0400032F RID: 815
		public const int SCI_GETOVERTYPE = 2187;

		// Token: 0x04000330 RID: 816
		public const int SCI_SETCARETWIDTH = 2188;

		// Token: 0x04000331 RID: 817
		public const int SCI_GETCARETWIDTH = 2189;

		// Token: 0x04000332 RID: 818
		public const int SCI_SETTARGETSTART = 2190;

		// Token: 0x04000333 RID: 819
		public const int SCI_GETTARGETSTART = 2191;

		// Token: 0x04000334 RID: 820
		public const int SCI_SETTARGETEND = 2192;

		// Token: 0x04000335 RID: 821
		public const int SCI_GETTARGETEND = 2193;

		// Token: 0x04000336 RID: 822
		public const int SCI_REPLACETARGET = 2194;

		// Token: 0x04000337 RID: 823
		public const int SCI_REPLACETARGETRE = 2195;

		// Token: 0x04000338 RID: 824
		public const int SCI_SEARCHINTARGET = 2197;

		// Token: 0x04000339 RID: 825
		public const int SCI_SETSEARCHFLAGS = 2198;

		// Token: 0x0400033A RID: 826
		public const int SCI_GETSEARCHFLAGS = 2199;

		// Token: 0x0400033B RID: 827
		public const int SCI_CALLTIPSHOW = 2200;

		// Token: 0x0400033C RID: 828
		public const int SCI_CALLTIPCANCEL = 2201;

		// Token: 0x0400033D RID: 829
		public const int SCI_CALLTIPACTIVE = 2202;

		// Token: 0x0400033E RID: 830
		public const int SCI_CALLTIPPOSSTART = 2203;

		// Token: 0x0400033F RID: 831
		public const int SCI_CALLTIPSETPOSSTART = 2214;

		// Token: 0x04000340 RID: 832
		public const int SCI_CALLTIPSETHLT = 2204;

		// Token: 0x04000341 RID: 833
		public const int SCI_CALLTIPSETBACK = 2205;

		// Token: 0x04000342 RID: 834
		public const int SCI_CALLTIPSETFORE = 2206;

		// Token: 0x04000343 RID: 835
		public const int SCI_CALLTIPSETFOREHLT = 2207;

		// Token: 0x04000344 RID: 836
		public const int SCI_CALLTIPUSESTYLE = 2212;

		// Token: 0x04000345 RID: 837
		public const int SCI_CALLTIPSETPOSITION = 2213;

		// Token: 0x04000346 RID: 838
		public const int SCI_VISIBLEFROMDOCLINE = 2220;

		// Token: 0x04000347 RID: 839
		public const int SCI_DOCLINEFROMVISIBLE = 2221;

		// Token: 0x04000348 RID: 840
		public const int SCI_WRAPCOUNT = 2235;

		// Token: 0x04000349 RID: 841
		public const int SCI_SETFOLDLEVEL = 2222;

		// Token: 0x0400034A RID: 842
		public const int SCI_GETFOLDLEVEL = 2223;

		// Token: 0x0400034B RID: 843
		public const int SCI_GETLASTCHILD = 2224;

		// Token: 0x0400034C RID: 844
		public const int SCI_GETFOLDPARENT = 2225;

		// Token: 0x0400034D RID: 845
		public const int SCI_SHOWLINES = 2226;

		// Token: 0x0400034E RID: 846
		public const int SCI_HIDELINES = 2227;

		// Token: 0x0400034F RID: 847
		public const int SCI_GETLINEVISIBLE = 2228;

		// Token: 0x04000350 RID: 848
		public const int SCI_GETALLLINESVISIBLE = 2236;

		// Token: 0x04000351 RID: 849
		public const int SCI_SETFOLDEXPANDED = 2229;

		// Token: 0x04000352 RID: 850
		public const int SCI_GETFOLDEXPANDED = 2230;

		// Token: 0x04000353 RID: 851
		public const int SCI_TOGGLEFOLD = 2231;

		// Token: 0x04000354 RID: 852
		public const int SCI_FOLDLINE = 2237;

		// Token: 0x04000355 RID: 853
		public const int SCI_FOLDCHILDREN = 2238;

		// Token: 0x04000356 RID: 854
		public const int SCI_EXPANDCHILDREN = 2239;

		// Token: 0x04000357 RID: 855
		public const int SCI_FOLDALL = 2662;

		// Token: 0x04000358 RID: 856
		public const int SCI_ENSUREVISIBLE = 2232;

		// Token: 0x04000359 RID: 857
		public const int SCI_SETAUTOMATICFOLD = 2663;

		// Token: 0x0400035A RID: 858
		public const int SCI_GETAUTOMATICFOLD = 2664;

		// Token: 0x0400035B RID: 859
		public const int SCI_SETFOLDFLAGS = 2233;

		// Token: 0x0400035C RID: 860
		public const int SCI_ENSUREVISIBLEENFORCEPOLICY = 2234;

		// Token: 0x0400035D RID: 861
		public const int SCI_SETTABINDENTS = 2260;

		// Token: 0x0400035E RID: 862
		public const int SCI_GETTABINDENTS = 2261;

		// Token: 0x0400035F RID: 863
		public const int SCI_SETBACKSPACEUNINDENTS = 2262;

		// Token: 0x04000360 RID: 864
		public const int SCI_GETBACKSPACEUNINDENTS = 2263;

		// Token: 0x04000361 RID: 865
		public const int SCI_SETMOUSEDWELLTIME = 2264;

		// Token: 0x04000362 RID: 866
		public const int SCI_GETMOUSEDWELLTIME = 2265;

		// Token: 0x04000363 RID: 867
		public const int SCI_WORDSTARTPOSITION = 2266;

		// Token: 0x04000364 RID: 868
		public const int SCI_WORDENDPOSITION = 2267;

		// Token: 0x04000365 RID: 869
		public const int SCI_ISRANGEWORD = 2691;

		// Token: 0x04000366 RID: 870
		public const int SCI_SETWRAPMODE = 2268;

		// Token: 0x04000367 RID: 871
		public const int SCI_GETWRAPMODE = 2269;

		// Token: 0x04000368 RID: 872
		public const int SCI_SETWRAPVISUALFLAGS = 2460;

		// Token: 0x04000369 RID: 873
		public const int SCI_GETWRAPVISUALFLAGS = 2461;

		// Token: 0x0400036A RID: 874
		public const int SCI_SETWRAPVISUALFLAGSLOCATION = 2462;

		// Token: 0x0400036B RID: 875
		public const int SCI_GETWRAPVISUALFLAGSLOCATION = 2463;

		// Token: 0x0400036C RID: 876
		public const int SCI_SETWRAPSTARTINDENT = 2464;

		// Token: 0x0400036D RID: 877
		public const int SCI_GETWRAPSTARTINDENT = 2465;

		// Token: 0x0400036E RID: 878
		public const int SCI_SETWRAPINDENTMODE = 2472;

		// Token: 0x0400036F RID: 879
		public const int SCI_GETWRAPINDENTMODE = 2473;

		// Token: 0x04000370 RID: 880
		public const int SCI_SETLAYOUTCACHE = 2272;

		// Token: 0x04000371 RID: 881
		public const int SCI_GETLAYOUTCACHE = 2273;

		// Token: 0x04000372 RID: 882
		public const int SCI_SETSCROLLWIDTH = 2274;

		// Token: 0x04000373 RID: 883
		public const int SCI_GETSCROLLWIDTH = 2275;

		// Token: 0x04000374 RID: 884
		public const int SCI_SETSCROLLWIDTHTRACKING = 2516;

		// Token: 0x04000375 RID: 885
		public const int SCI_GETSCROLLWIDTHTRACKING = 2517;

		// Token: 0x04000376 RID: 886
		public const int SCI_TEXTWIDTH = 2276;

		// Token: 0x04000377 RID: 887
		public const int SCI_SETENDATLASTLINE = 2277;

		// Token: 0x04000378 RID: 888
		public const int SCI_GETENDATLASTLINE = 2278;

		// Token: 0x04000379 RID: 889
		public const int SCI_TEXTHEIGHT = 2279;

		// Token: 0x0400037A RID: 890
		public const int SCI_SETVSCROLLBAR = 2280;

		// Token: 0x0400037B RID: 891
		public const int SCI_GETVSCROLLBAR = 2281;

		// Token: 0x0400037C RID: 892
		public const int SCI_APPENDTEXT = 2282;

		// Token: 0x0400037D RID: 893
		public const int SCI_GETTWOPHASEDRAW = 2283;

		// Token: 0x0400037E RID: 894
		public const int SCI_SETTWOPHASEDRAW = 2284;

		// Token: 0x0400037F RID: 895
		public const int SCI_GETPHASESDRAW = 2673;

		// Token: 0x04000380 RID: 896
		public const int SCI_SETPHASESDRAW = 2674;

		// Token: 0x04000381 RID: 897
		public const int SCI_SETFONTQUALITY = 2611;

		// Token: 0x04000382 RID: 898
		public const int SCI_GETFONTQUALITY = 2612;

		// Token: 0x04000383 RID: 899
		public const int SCI_SETFIRSTVISIBLELINE = 2613;

		// Token: 0x04000384 RID: 900
		public const int SCI_SETMULTIPASTE = 2614;

		// Token: 0x04000385 RID: 901
		public const int SCI_GETMULTIPASTE = 2615;

		// Token: 0x04000386 RID: 902
		public const int SCI_GETTAG = 2616;

		// Token: 0x04000387 RID: 903
		public const int SCI_TARGETFROMSELECTION = 2287;

		// Token: 0x04000388 RID: 904
		public const int SCI_TARGETWHOLEDOCUMENT = 2690;

		// Token: 0x04000389 RID: 905
		public const int SCI_LINESJOIN = 2288;

		// Token: 0x0400038A RID: 906
		public const int SCI_LINESSPLIT = 2289;

		// Token: 0x0400038B RID: 907
		public const int SCI_SETFOLDMARGINCOLOUR = 2290;

		// Token: 0x0400038C RID: 908
		public const int SCI_SETFOLDMARGINHICOLOUR = 2291;

		// Token: 0x0400038D RID: 909
		public const int SCI_LINEDOWN = 2300;

		// Token: 0x0400038E RID: 910
		public const int SCI_LINEDOWNEXTEND = 2301;

		// Token: 0x0400038F RID: 911
		public const int SCI_LINEUP = 2302;

		// Token: 0x04000390 RID: 912
		public const int SCI_LINEUPEXTEND = 2303;

		// Token: 0x04000391 RID: 913
		public const int SCI_CHARLEFT = 2304;

		// Token: 0x04000392 RID: 914
		public const int SCI_CHARLEFTEXTEND = 2305;

		// Token: 0x04000393 RID: 915
		public const int SCI_CHARRIGHT = 2306;

		// Token: 0x04000394 RID: 916
		public const int SCI_CHARRIGHTEXTEND = 2307;

		// Token: 0x04000395 RID: 917
		public const int SCI_WORDLEFT = 2308;

		// Token: 0x04000396 RID: 918
		public const int SCI_WORDLEFTEXTEND = 2309;

		// Token: 0x04000397 RID: 919
		public const int SCI_WORDRIGHT = 2310;

		// Token: 0x04000398 RID: 920
		public const int SCI_WORDRIGHTEXTEND = 2311;

		// Token: 0x04000399 RID: 921
		public const int SCI_HOME = 2312;

		// Token: 0x0400039A RID: 922
		public const int SCI_HOMEEXTEND = 2313;

		// Token: 0x0400039B RID: 923
		public const int SCI_LINEEND = 2314;

		// Token: 0x0400039C RID: 924
		public const int SCI_LINEENDEXTEND = 2315;

		// Token: 0x0400039D RID: 925
		public const int SCI_DOCUMENTSTART = 2316;

		// Token: 0x0400039E RID: 926
		public const int SCI_DOCUMENTSTARTEXTEND = 2317;

		// Token: 0x0400039F RID: 927
		public const int SCI_DOCUMENTEND = 2318;

		// Token: 0x040003A0 RID: 928
		public const int SCI_DOCUMENTENDEXTEND = 2319;

		// Token: 0x040003A1 RID: 929
		public const int SCI_PAGEUP = 2320;

		// Token: 0x040003A2 RID: 930
		public const int SCI_PAGEUPEXTEND = 2321;

		// Token: 0x040003A3 RID: 931
		public const int SCI_PAGEDOWN = 2322;

		// Token: 0x040003A4 RID: 932
		public const int SCI_PAGEDOWNEXTEND = 2323;

		// Token: 0x040003A5 RID: 933
		public const int SCI_EDITTOGGLEOVERTYPE = 2324;

		// Token: 0x040003A6 RID: 934
		public const int SCI_CANCEL = 2325;

		// Token: 0x040003A7 RID: 935
		public const int SCI_DELETEBACK = 2326;

		// Token: 0x040003A8 RID: 936
		public const int SCI_TAB = 2327;

		// Token: 0x040003A9 RID: 937
		public const int SCI_BACKTAB = 2328;

		// Token: 0x040003AA RID: 938
		public const int SCI_NEWLINE = 2329;

		// Token: 0x040003AB RID: 939
		public const int SCI_FORMFEED = 2330;

		// Token: 0x040003AC RID: 940
		public const int SCI_VCHOME = 2331;

		// Token: 0x040003AD RID: 941
		public const int SCI_VCHOMEEXTEND = 2332;

		// Token: 0x040003AE RID: 942
		public const int SCI_ZOOMIN = 2333;

		// Token: 0x040003AF RID: 943
		public const int SCI_ZOOMOUT = 2334;

		// Token: 0x040003B0 RID: 944
		public const int SCI_DELWORDLEFT = 2335;

		// Token: 0x040003B1 RID: 945
		public const int SCI_DELWORDRIGHT = 2336;

		// Token: 0x040003B2 RID: 946
		public const int SCI_DELWORDRIGHTEND = 2518;

		// Token: 0x040003B3 RID: 947
		public const int SCI_LINECUT = 2337;

		// Token: 0x040003B4 RID: 948
		public const int SCI_LINEDELETE = 2338;

		// Token: 0x040003B5 RID: 949
		public const int SCI_LINETRANSPOSE = 2339;

		// Token: 0x040003B6 RID: 950
		public const int SCI_LINEDUPLICATE = 2404;

		// Token: 0x040003B7 RID: 951
		public const int SCI_LOWERCASE = 2340;

		// Token: 0x040003B8 RID: 952
		public const int SCI_UPPERCASE = 2341;

		// Token: 0x040003B9 RID: 953
		public const int SCI_LINESCROLLDOWN = 2342;

		// Token: 0x040003BA RID: 954
		public const int SCI_LINESCROLLUP = 2343;

		// Token: 0x040003BB RID: 955
		public const int SCI_DELETEBACKNOTLINE = 2344;

		// Token: 0x040003BC RID: 956
		public const int SCI_HOMEDISPLAY = 2345;

		// Token: 0x040003BD RID: 957
		public const int SCI_HOMEDISPLAYEXTEND = 2346;

		// Token: 0x040003BE RID: 958
		public const int SCI_LINEENDDISPLAY = 2347;

		// Token: 0x040003BF RID: 959
		public const int SCI_LINEENDDISPLAYEXTEND = 2348;

		// Token: 0x040003C0 RID: 960
		public const int SCI_HOMEWRAP = 2349;

		// Token: 0x040003C1 RID: 961
		public const int SCI_HOMEWRAPEXTEND = 2450;

		// Token: 0x040003C2 RID: 962
		public const int SCI_LINEENDWRAP = 2451;

		// Token: 0x040003C3 RID: 963
		public const int SCI_LINEENDWRAPEXTEND = 2452;

		// Token: 0x040003C4 RID: 964
		public const int SCI_VCHOMEWRAP = 2453;

		// Token: 0x040003C5 RID: 965
		public const int SCI_VCHOMEWRAPEXTEND = 2454;

		// Token: 0x040003C6 RID: 966
		public const int SCI_LINECOPY = 2455;

		// Token: 0x040003C7 RID: 967
		public const int SCI_MOVECARETINSIDEVIEW = 2401;

		// Token: 0x040003C8 RID: 968
		public const int SCI_LINELENGTH = 2350;

		// Token: 0x040003C9 RID: 969
		public const int SCI_BRACEHIGHLIGHT = 2351;

		// Token: 0x040003CA RID: 970
		public const int SCI_BRACEHIGHLIGHTINDICATOR = 2498;

		// Token: 0x040003CB RID: 971
		public const int SCI_BRACEBADLIGHT = 2352;

		// Token: 0x040003CC RID: 972
		public const int SCI_BRACEBADLIGHTINDICATOR = 2499;

		// Token: 0x040003CD RID: 973
		public const int SCI_BRACEMATCH = 2353;

		// Token: 0x040003CE RID: 974
		public const int SCI_GETVIEWEOL = 2355;

		// Token: 0x040003CF RID: 975
		public const int SCI_SETVIEWEOL = 2356;

		// Token: 0x040003D0 RID: 976
		public const int SCI_GETDOCPOINTER = 2357;

		// Token: 0x040003D1 RID: 977
		public const int SCI_SETDOCPOINTER = 2358;

		// Token: 0x040003D2 RID: 978
		public const int SCI_SETMODEVENTMASK = 2359;

		// Token: 0x040003D3 RID: 979
		public const int SCI_GETEDGECOLUMN = 2360;

		// Token: 0x040003D4 RID: 980
		public const int SCI_SETEDGECOLUMN = 2361;

		// Token: 0x040003D5 RID: 981
		public const int SCI_GETEDGEMODE = 2362;

		// Token: 0x040003D6 RID: 982
		public const int SCI_SETEDGEMODE = 2363;

		// Token: 0x040003D7 RID: 983
		public const int SCI_GETEDGECOLOUR = 2364;

		// Token: 0x040003D8 RID: 984
		public const int SCI_SETEDGECOLOUR = 2365;

		// Token: 0x040003D9 RID: 985
		public const int SCI_SEARCHANCHOR = 2366;

		// Token: 0x040003DA RID: 986
		public const int SCI_SEARCHNEXT = 2367;

		// Token: 0x040003DB RID: 987
		public const int SCI_SEARCHPREV = 2368;

		// Token: 0x040003DC RID: 988
		public const int SCI_LINESONSCREEN = 2370;

		// Token: 0x040003DD RID: 989
		public const int SCI_USEPOPUP = 2371;

		// Token: 0x040003DE RID: 990
		public const int SCI_SELECTIONISRECTANGLE = 2372;

		// Token: 0x040003DF RID: 991
		public const int SCI_SETZOOM = 2373;

		// Token: 0x040003E0 RID: 992
		public const int SCI_GETZOOM = 2374;

		// Token: 0x040003E1 RID: 993
		public const int SCI_CREATEDOCUMENT = 2375;

		// Token: 0x040003E2 RID: 994
		public const int SCI_ADDREFDOCUMENT = 2376;

		// Token: 0x040003E3 RID: 995
		public const int SCI_RELEASEDOCUMENT = 2377;

		// Token: 0x040003E4 RID: 996
		public const int SCI_GETMODEVENTMASK = 2378;

		// Token: 0x040003E5 RID: 997
		public const int SCI_SETFOCUS = 2380;

		// Token: 0x040003E6 RID: 998
		public const int SCI_GETFOCUS = 2381;

		// Token: 0x040003E7 RID: 999
		public const int SCI_SETSTATUS = 2382;

		// Token: 0x040003E8 RID: 1000
		public const int SCI_GETSTATUS = 2383;

		// Token: 0x040003E9 RID: 1001
		public const int SCI_SETMOUSEDOWNCAPTURES = 2384;

		// Token: 0x040003EA RID: 1002
		public const int SCI_GETMOUSEDOWNCAPTURES = 2385;

		// Token: 0x040003EB RID: 1003
		public const int SCI_SETCURSOR = 2386;

		// Token: 0x040003EC RID: 1004
		public const int SCI_GETCURSOR = 2387;

		// Token: 0x040003ED RID: 1005
		public const int SCI_SETCONTROLCHARSYMBOL = 2388;

		// Token: 0x040003EE RID: 1006
		public const int SCI_GETCONTROLCHARSYMBOL = 2389;

		// Token: 0x040003EF RID: 1007
		public const int SCI_WORDPARTLEFT = 2390;

		// Token: 0x040003F0 RID: 1008
		public const int SCI_WORDPARTLEFTEXTEND = 2391;

		// Token: 0x040003F1 RID: 1009
		public const int SCI_WORDPARTRIGHT = 2392;

		// Token: 0x040003F2 RID: 1010
		public const int SCI_WORDPARTRIGHTEXTEND = 2393;

		// Token: 0x040003F3 RID: 1011
		public const int SCI_SETVISIBLEPOLICY = 2394;

		// Token: 0x040003F4 RID: 1012
		public const int SCI_DELLINELEFT = 2395;

		// Token: 0x040003F5 RID: 1013
		public const int SCI_DELLINERIGHT = 2396;

		// Token: 0x040003F6 RID: 1014
		public const int SCI_SETXOFFSET = 2397;

		// Token: 0x040003F7 RID: 1015
		public const int SCI_GETXOFFSET = 2398;

		// Token: 0x040003F8 RID: 1016
		public const int SCI_CHOOSECARETX = 2399;

		// Token: 0x040003F9 RID: 1017
		public const int SCI_GRABFOCUS = 2400;

		// Token: 0x040003FA RID: 1018
		public const int SCI_SETXCARETPOLICY = 2402;

		// Token: 0x040003FB RID: 1019
		public const int SCI_SETYCARETPOLICY = 2403;

		// Token: 0x040003FC RID: 1020
		public const int SCI_SETPRINTWRAPMODE = 2406;

		// Token: 0x040003FD RID: 1021
		public const int SCI_GETPRINTWRAPMODE = 2407;

		// Token: 0x040003FE RID: 1022
		public const int SCI_SETHOTSPOTACTIVEFORE = 2410;

		// Token: 0x040003FF RID: 1023
		public const int SCI_GETHOTSPOTACTIVEFORE = 2494;

		// Token: 0x04000400 RID: 1024
		public const int SCI_SETHOTSPOTACTIVEBACK = 2411;

		// Token: 0x04000401 RID: 1025
		public const int SCI_GETHOTSPOTACTIVEBACK = 2495;

		// Token: 0x04000402 RID: 1026
		public const int SCI_SETHOTSPOTACTIVEUNDERLINE = 2412;

		// Token: 0x04000403 RID: 1027
		public const int SCI_GETHOTSPOTACTIVEUNDERLINE = 2496;

		// Token: 0x04000404 RID: 1028
		public const int SCI_SETHOTSPOTSINGLELINE = 2421;

		// Token: 0x04000405 RID: 1029
		public const int SCI_GETHOTSPOTSINGLELINE = 2497;

		// Token: 0x04000406 RID: 1030
		public const int SCI_PARADOWN = 2413;

		// Token: 0x04000407 RID: 1031
		public const int SCI_PARADOWNEXTEND = 2414;

		// Token: 0x04000408 RID: 1032
		public const int SCI_PARAUP = 2415;

		// Token: 0x04000409 RID: 1033
		public const int SCI_PARAUPEXTEND = 2416;

		// Token: 0x0400040A RID: 1034
		public const int SCI_POSITIONRELATIVE = 2670;

		// Token: 0x0400040B RID: 1035
		public const int SCI_COPYRANGE = 2419;

		// Token: 0x0400040C RID: 1036
		public const int SCI_COPYTEXT = 2420;

		// Token: 0x0400040D RID: 1037
		public const int SCI_SETSELECTIONMODE = 2422;

		// Token: 0x0400040E RID: 1038
		public const int SCI_GETSELECTIONMODE = 2423;

		// Token: 0x0400040F RID: 1039
		public const int SCI_GETLINESELSTARTPOSITION = 2424;

		// Token: 0x04000410 RID: 1040
		public const int SCI_GETLINESELENDPOSITION = 2425;

		// Token: 0x04000411 RID: 1041
		public const int SCI_LINEDOWNRECTEXTEND = 2426;

		// Token: 0x04000412 RID: 1042
		public const int SCI_LINEUPRECTEXTEND = 2427;

		// Token: 0x04000413 RID: 1043
		public const int SCI_CHARLEFTRECTEXTEND = 2428;

		// Token: 0x04000414 RID: 1044
		public const int SCI_CHARRIGHTRECTEXTEND = 2429;

		// Token: 0x04000415 RID: 1045
		public const int SCI_HOMERECTEXTEND = 2430;

		// Token: 0x04000416 RID: 1046
		public const int SCI_VCHOMERECTEXTEND = 2431;

		// Token: 0x04000417 RID: 1047
		public const int SCI_LINEENDRECTEXTEND = 2432;

		// Token: 0x04000418 RID: 1048
		public const int SCI_PAGEUPRECTEXTEND = 2433;

		// Token: 0x04000419 RID: 1049
		public const int SCI_PAGEDOWNRECTEXTEND = 2434;

		// Token: 0x0400041A RID: 1050
		public const int SCI_STUTTEREDPAGEUP = 2435;

		// Token: 0x0400041B RID: 1051
		public const int SCI_STUTTEREDPAGEUPEXTEND = 2436;

		// Token: 0x0400041C RID: 1052
		public const int SCI_STUTTEREDPAGEDOWN = 2437;

		// Token: 0x0400041D RID: 1053
		public const int SCI_STUTTEREDPAGEDOWNEXTEND = 2438;

		// Token: 0x0400041E RID: 1054
		public const int SCI_WORDLEFTEND = 2439;

		// Token: 0x0400041F RID: 1055
		public const int SCI_WORDLEFTENDEXTEND = 2440;

		// Token: 0x04000420 RID: 1056
		public const int SCI_WORDRIGHTEND = 2441;

		// Token: 0x04000421 RID: 1057
		public const int SCI_WORDRIGHTENDEXTEND = 2442;

		// Token: 0x04000422 RID: 1058
		public const int SCI_SETWHITESPACECHARS = 2443;

		// Token: 0x04000423 RID: 1059
		public const int SCI_GETWHITESPACECHARS = 2647;

		// Token: 0x04000424 RID: 1060
		public const int SCI_SETPUNCTUATIONCHARS = 2648;

		// Token: 0x04000425 RID: 1061
		public const int SCI_GETPUNCTUATIONCHARS = 2649;

		// Token: 0x04000426 RID: 1062
		public const int SCI_SETCHARSDEFAULT = 2444;

		// Token: 0x04000427 RID: 1063
		public const int SCI_AUTOCGETCURRENT = 2445;

		// Token: 0x04000428 RID: 1064
		public const int SCI_AUTOCGETCURRENTTEXT = 2610;

		// Token: 0x04000429 RID: 1065
		public const int SCI_AUTOCSETCASEINSENSITIVEBEHAVIOUR = 2634;

		// Token: 0x0400042A RID: 1066
		public const int SCI_AUTOCGETCASEINSENSITIVEBEHAVIOUR = 2635;

		// Token: 0x0400042B RID: 1067
		public const int SCI_AUTOCSETMULTI = 2636;

		// Token: 0x0400042C RID: 1068
		public const int SCI_AUTOCGETMULTI = 2637;

		// Token: 0x0400042D RID: 1069
		public const int SCI_AUTOCSETORDER = 2660;

		// Token: 0x0400042E RID: 1070
		public const int SCI_AUTOCGETORDER = 2661;

		// Token: 0x0400042F RID: 1071
		public const int SCI_ALLOCATE = 2446;

		// Token: 0x04000430 RID: 1072
		public const int SCI_TARGETASUTF8 = 2447;

		// Token: 0x04000431 RID: 1073
		public const int SCI_SETLENGTHFORENCODE = 2448;

		// Token: 0x04000432 RID: 1074
		public const int SCI_ENCODEDFROMUTF8 = 2449;

		// Token: 0x04000433 RID: 1075
		public const int SCI_FINDCOLUMN = 2456;

		// Token: 0x04000434 RID: 1076
		public const int SCI_GETCARETSTICKY = 2457;

		// Token: 0x04000435 RID: 1077
		public const int SCI_SETCARETSTICKY = 2458;

		// Token: 0x04000436 RID: 1078
		public const int SCI_TOGGLECARETSTICKY = 2459;

		// Token: 0x04000437 RID: 1079
		public const int SCI_SETPASTECONVERTENDINGS = 2467;

		// Token: 0x04000438 RID: 1080
		public const int SCI_GETPASTECONVERTENDINGS = 2468;

		// Token: 0x04000439 RID: 1081
		public const int SCI_SELECTIONDUPLICATE = 2469;

		// Token: 0x0400043A RID: 1082
		public const int SCI_SETCARETLINEBACKALPHA = 2470;

		// Token: 0x0400043B RID: 1083
		public const int SCI_GETCARETLINEBACKALPHA = 2471;

		// Token: 0x0400043C RID: 1084
		public const int SCI_SETCARETSTYLE = 2512;

		// Token: 0x0400043D RID: 1085
		public const int SCI_GETCARETSTYLE = 2513;

		// Token: 0x0400043E RID: 1086
		public const int SCI_SETINDICATORCURRENT = 2500;

		// Token: 0x0400043F RID: 1087
		public const int SCI_GETINDICATORCURRENT = 2501;

		// Token: 0x04000440 RID: 1088
		public const int SCI_SETINDICATORVALUE = 2502;

		// Token: 0x04000441 RID: 1089
		public const int SCI_GETINDICATORVALUE = 2503;

		// Token: 0x04000442 RID: 1090
		public const int SCI_INDICATORFILLRANGE = 2504;

		// Token: 0x04000443 RID: 1091
		public const int SCI_INDICATORCLEARRANGE = 2505;

		// Token: 0x04000444 RID: 1092
		public const int SCI_INDICATORALLONFOR = 2506;

		// Token: 0x04000445 RID: 1093
		public const int SCI_INDICATORVALUEAT = 2507;

		// Token: 0x04000446 RID: 1094
		public const int SCI_INDICATORSTART = 2508;

		// Token: 0x04000447 RID: 1095
		public const int SCI_INDICATOREND = 2509;

		// Token: 0x04000448 RID: 1096
		public const int SCI_SETPOSITIONCACHE = 2514;

		// Token: 0x04000449 RID: 1097
		public const int SCI_GETPOSITIONCACHE = 2515;

		// Token: 0x0400044A RID: 1098
		public const int SCI_COPYALLOWLINE = 2519;

		// Token: 0x0400044B RID: 1099
		public const int SCI_GETCHARACTERPOINTER = 2520;

		// Token: 0x0400044C RID: 1100
		public const int SCI_GETRANGEPOINTER = 2643;

		// Token: 0x0400044D RID: 1101
		public const int SCI_GETGAPPOSITION = 2644;

		// Token: 0x0400044E RID: 1102
		public const int SCI_INDICSETALPHA = 2523;

		// Token: 0x0400044F RID: 1103
		public const int SCI_INDICGETALPHA = 2524;

		// Token: 0x04000450 RID: 1104
		public const int SCI_INDICSETOUTLINEALPHA = 2558;

		// Token: 0x04000451 RID: 1105
		public const int SCI_INDICGETOUTLINEALPHA = 2559;

		// Token: 0x04000452 RID: 1106
		public const int SCI_SETEXTRAASCENT = 2525;

		// Token: 0x04000453 RID: 1107
		public const int SCI_GETEXTRAASCENT = 2526;

		// Token: 0x04000454 RID: 1108
		public const int SCI_SETEXTRADESCENT = 2527;

		// Token: 0x04000455 RID: 1109
		public const int SCI_GETEXTRADESCENT = 2528;

		// Token: 0x04000456 RID: 1110
		public const int SCI_MARKERSYMBOLDEFINED = 2529;

		// Token: 0x04000457 RID: 1111
		public const int SCI_MARGINSETTEXT = 2530;

		// Token: 0x04000458 RID: 1112
		public const int SCI_MARGINGETTEXT = 2531;

		// Token: 0x04000459 RID: 1113
		public const int SCI_MARGINSETSTYLE = 2532;

		// Token: 0x0400045A RID: 1114
		public const int SCI_MARGINGETSTYLE = 2533;

		// Token: 0x0400045B RID: 1115
		public const int SCI_MARGINSETSTYLES = 2534;

		// Token: 0x0400045C RID: 1116
		public const int SCI_MARGINGETSTYLES = 2535;

		// Token: 0x0400045D RID: 1117
		public const int SCI_MARGINTEXTCLEARALL = 2536;

		// Token: 0x0400045E RID: 1118
		public const int SCI_MARGINSETSTYLEOFFSET = 2537;

		// Token: 0x0400045F RID: 1119
		public const int SCI_MARGINGETSTYLEOFFSET = 2538;

		// Token: 0x04000460 RID: 1120
		public const int SCI_SETMARGINOPTIONS = 2539;

		// Token: 0x04000461 RID: 1121
		public const int SCI_GETMARGINOPTIONS = 2557;

		// Token: 0x04000462 RID: 1122
		public const int SCI_ANNOTATIONSETTEXT = 2540;

		// Token: 0x04000463 RID: 1123
		public const int SCI_ANNOTATIONGETTEXT = 2541;

		// Token: 0x04000464 RID: 1124
		public const int SCI_ANNOTATIONSETSTYLE = 2542;

		// Token: 0x04000465 RID: 1125
		public const int SCI_ANNOTATIONGETSTYLE = 2543;

		// Token: 0x04000466 RID: 1126
		public const int SCI_ANNOTATIONSETSTYLES = 2544;

		// Token: 0x04000467 RID: 1127
		public const int SCI_ANNOTATIONGETSTYLES = 2545;

		// Token: 0x04000468 RID: 1128
		public const int SCI_ANNOTATIONGETLINES = 2546;

		// Token: 0x04000469 RID: 1129
		public const int SCI_ANNOTATIONCLEARALL = 2547;

		// Token: 0x0400046A RID: 1130
		public const int SCI_ANNOTATIONSETVISIBLE = 2548;

		// Token: 0x0400046B RID: 1131
		public const int SCI_ANNOTATIONGETVISIBLE = 2549;

		// Token: 0x0400046C RID: 1132
		public const int SCI_ANNOTATIONSETSTYLEOFFSET = 2550;

		// Token: 0x0400046D RID: 1133
		public const int SCI_ANNOTATIONGETSTYLEOFFSET = 2551;

		// Token: 0x0400046E RID: 1134
		public const int SCI_RELEASEALLEXTENDEDSTYLES = 2552;

		// Token: 0x0400046F RID: 1135
		public const int SCI_ALLOCATEEXTENDEDSTYLES = 2553;

		// Token: 0x04000470 RID: 1136
		public const int SCI_ADDUNDOACTION = 2560;

		// Token: 0x04000471 RID: 1137
		public const int SCI_CHARPOSITIONFROMPOINT = 2561;

		// Token: 0x04000472 RID: 1138
		public const int SCI_CHARPOSITIONFROMPOINTCLOSE = 2562;

		// Token: 0x04000473 RID: 1139
		public const int SCI_SETMOUSESELECTIONRECTANGULARSWITCH = 2668;

		// Token: 0x04000474 RID: 1140
		public const int SCI_GETMOUSESELECTIONRECTANGULARSWITCH = 2669;

		// Token: 0x04000475 RID: 1141
		public const int SCI_SETMULTIPLESELECTION = 2563;

		// Token: 0x04000476 RID: 1142
		public const int SCI_GETMULTIPLESELECTION = 2564;

		// Token: 0x04000477 RID: 1143
		public const int SCI_SETADDITIONALSELECTIONTYPING = 2565;

		// Token: 0x04000478 RID: 1144
		public const int SCI_GETADDITIONALSELECTIONTYPING = 2566;

		// Token: 0x04000479 RID: 1145
		public const int SCI_SETADDITIONALCARETSBLINK = 2567;

		// Token: 0x0400047A RID: 1146
		public const int SCI_GETADDITIONALCARETSBLINK = 2568;

		// Token: 0x0400047B RID: 1147
		public const int SCI_SETADDITIONALCARETSVISIBLE = 2608;

		// Token: 0x0400047C RID: 1148
		public const int SCI_GETADDITIONALCARETSVISIBLE = 2609;

		// Token: 0x0400047D RID: 1149
		public const int SCI_GETTABDRAWMODE = 2698;

		// Token: 0x0400047E RID: 1150
		public const int SCI_SETTABDRAWMODE = 2699;

		// Token: 0x0400047F RID: 1151
		public const int SCI_GETSELECTIONS = 2570;

		// Token: 0x04000480 RID: 1152
		public const int SCI_GETSELECTIONEMPTY = 2650;

		// Token: 0x04000481 RID: 1153
		public const int SCI_CLEARSELECTIONS = 2571;

		// Token: 0x04000482 RID: 1154
		public const int SCI_SETSELECTION = 2572;

		// Token: 0x04000483 RID: 1155
		public const int SCI_ADDSELECTION = 2573;

		// Token: 0x04000484 RID: 1156
		public const int SCI_DROPSELECTIONN = 2671;

		// Token: 0x04000485 RID: 1157
		public const int SCI_SETMAINSELECTION = 2574;

		// Token: 0x04000486 RID: 1158
		public const int SCI_GETMAINSELECTION = 2575;

		// Token: 0x04000487 RID: 1159
		public const int SCI_SETSELECTIONNCARET = 2576;

		// Token: 0x04000488 RID: 1160
		public const int SCI_GETSELECTIONNCARET = 2577;

		// Token: 0x04000489 RID: 1161
		public const int SCI_SETSELECTIONNANCHOR = 2578;

		// Token: 0x0400048A RID: 1162
		public const int SCI_GETSELECTIONNANCHOR = 2579;

		// Token: 0x0400048B RID: 1163
		public const int SCI_SETSELECTIONNCARETVIRTUALSPACE = 2580;

		// Token: 0x0400048C RID: 1164
		public const int SCI_GETSELECTIONNCARETVIRTUALSPACE = 2581;

		// Token: 0x0400048D RID: 1165
		public const int SCI_SETSELECTIONNANCHORVIRTUALSPACE = 2582;

		// Token: 0x0400048E RID: 1166
		public const int SCI_GETSELECTIONNANCHORVIRTUALSPACE = 2583;

		// Token: 0x0400048F RID: 1167
		public const int SCI_SETSELECTIONNSTART = 2584;

		// Token: 0x04000490 RID: 1168
		public const int SCI_GETSELECTIONNSTART = 2585;

		// Token: 0x04000491 RID: 1169
		public const int SCI_SETSELECTIONNEND = 2586;

		// Token: 0x04000492 RID: 1170
		public const int SCI_GETSELECTIONNEND = 2587;

		// Token: 0x04000493 RID: 1171
		public const int SCI_SETRECTANGULARSELECTIONCARET = 2588;

		// Token: 0x04000494 RID: 1172
		public const int SCI_GETRECTANGULARSELECTIONCARET = 2589;

		// Token: 0x04000495 RID: 1173
		public const int SCI_SETRECTANGULARSELECTIONANCHOR = 2590;

		// Token: 0x04000496 RID: 1174
		public const int SCI_GETRECTANGULARSELECTIONANCHOR = 2591;

		// Token: 0x04000497 RID: 1175
		public const int SCI_SETRECTANGULARSELECTIONCARETVIRTUALSPACE = 2592;

		// Token: 0x04000498 RID: 1176
		public const int SCI_GETRECTANGULARSELECTIONCARETVIRTUALSPACE = 2593;

		// Token: 0x04000499 RID: 1177
		public const int SCI_SETRECTANGULARSELECTIONANCHORVIRTUALSPACE = 2594;

		// Token: 0x0400049A RID: 1178
		public const int SCI_GETRECTANGULARSELECTIONANCHORVIRTUALSPACE = 2595;

		// Token: 0x0400049B RID: 1179
		public const int SCI_SETVIRTUALSPACEOPTIONS = 2596;

		// Token: 0x0400049C RID: 1180
		public const int SCI_GETVIRTUALSPACEOPTIONS = 2597;

		// Token: 0x0400049D RID: 1181
		public const int SCI_SETRECTANGULARSELECTIONMODIFIER = 2598;

		// Token: 0x0400049E RID: 1182
		public const int SCI_GETRECTANGULARSELECTIONMODIFIER = 2599;

		// Token: 0x0400049F RID: 1183
		public const int SCI_SETADDITIONALSELFORE = 2600;

		// Token: 0x040004A0 RID: 1184
		public const int SCI_SETADDITIONALSELBACK = 2601;

		// Token: 0x040004A1 RID: 1185
		public const int SCI_SETADDITIONALSELALPHA = 2602;

		// Token: 0x040004A2 RID: 1186
		public const int SCI_GETADDITIONALSELALPHA = 2603;

		// Token: 0x040004A3 RID: 1187
		public const int SCI_SETADDITIONALCARETFORE = 2604;

		// Token: 0x040004A4 RID: 1188
		public const int SCI_GETADDITIONALCARETFORE = 2605;

		// Token: 0x040004A5 RID: 1189
		public const int SCI_ROTATESELECTION = 2606;

		// Token: 0x040004A6 RID: 1190
		public const int SCI_SWAPMAINANCHORCARET = 2607;

		// Token: 0x040004A7 RID: 1191
		public const int SCI_MULTIPLESELECTADDNEXT = 2688;

		// Token: 0x040004A8 RID: 1192
		public const int SCI_MULTIPLESELECTADDEACH = 2689;

		// Token: 0x040004A9 RID: 1193
		public const int SCI_CHANGELEXERSTATE = 2617;

		// Token: 0x040004AA RID: 1194
		public const int SCI_CONTRACTEDFOLDNEXT = 2618;

		// Token: 0x040004AB RID: 1195
		public const int SCI_VERTICALCENTRECARET = 2619;

		// Token: 0x040004AC RID: 1196
		public const int SCI_MOVESELECTEDLINESUP = 2620;

		// Token: 0x040004AD RID: 1197
		public const int SCI_MOVESELECTEDLINESDOWN = 2621;

		// Token: 0x040004AE RID: 1198
		public const int SCI_SETIDENTIFIER = 2622;

		// Token: 0x040004AF RID: 1199
		public const int SCI_GETIDENTIFIER = 2623;

		// Token: 0x040004B0 RID: 1200
		public const int SCI_RGBAIMAGESETWIDTH = 2624;

		// Token: 0x040004B1 RID: 1201
		public const int SCI_RGBAIMAGESETHEIGHT = 2625;

		// Token: 0x040004B2 RID: 1202
		public const int SCI_RGBAIMAGESETSCALE = 2651;

		// Token: 0x040004B3 RID: 1203
		public const int SCI_MARKERDEFINERGBAIMAGE = 2626;

		// Token: 0x040004B4 RID: 1204
		public const int SCI_REGISTERRGBAIMAGE = 2627;

		// Token: 0x040004B5 RID: 1205
		public const int SCI_SCROLLTOSTART = 2628;

		// Token: 0x040004B6 RID: 1206
		public const int SCI_SCROLLTOEND = 2629;

		// Token: 0x040004B7 RID: 1207
		public const int SCI_SETTECHNOLOGY = 2630;

		// Token: 0x040004B8 RID: 1208
		public const int SCI_GETTECHNOLOGY = 2631;

		// Token: 0x040004B9 RID: 1209
		public const int SCI_CREATELOADER = 2632;

		// Token: 0x040004BA RID: 1210
		public const int SCI_FINDINDICATORSHOW = 2640;

		// Token: 0x040004BB RID: 1211
		public const int SCI_FINDINDICATORFLASH = 2641;

		// Token: 0x040004BC RID: 1212
		public const int SCI_FINDINDICATORHIDE = 2642;

		// Token: 0x040004BD RID: 1213
		public const int SCI_VCHOMEDISPLAY = 2652;

		// Token: 0x040004BE RID: 1214
		public const int SCI_VCHOMEDISPLAYEXTEND = 2653;

		// Token: 0x040004BF RID: 1215
		public const int SCI_GETCARETLINEVISIBLEALWAYS = 2654;

		// Token: 0x040004C0 RID: 1216
		public const int SCI_SETCARETLINEVISIBLEALWAYS = 2655;

		// Token: 0x040004C1 RID: 1217
		public const int SCI_SETLINEENDTYPESALLOWED = 2656;

		// Token: 0x040004C2 RID: 1218
		public const int SCI_GETLINEENDTYPESALLOWED = 2657;

		// Token: 0x040004C3 RID: 1219
		public const int SCI_GETLINEENDTYPESACTIVE = 2658;

		// Token: 0x040004C4 RID: 1220
		public const int SCI_SETREPRESENTATION = 2665;

		// Token: 0x040004C5 RID: 1221
		public const int SCI_GETREPRESENTATION = 2666;

		// Token: 0x040004C6 RID: 1222
		public const int SCI_CLEARREPRESENTATION = 2667;

		// Token: 0x040004C7 RID: 1223
		public const int SCI_SETTARGETRANGE = 2686;

		// Token: 0x040004C8 RID: 1224
		public const int SCI_GETTARGETTEXT = 2687;

		// Token: 0x040004C9 RID: 1225
		public const int SCI_SETIDLESTYLING = 2692;

		// Token: 0x040004CA RID: 1226
		public const int SCI_GETIDLESTYLING = 2693;

		// Token: 0x040004CB RID: 1227
		public const int SCI_MULTIEDGEADDLINE = 2694;

		// Token: 0x040004CC RID: 1228
		public const int SCI_MULTIEDGECLEARALL = 2695;

		// Token: 0x040004CD RID: 1229
		public const int SCI_SETMOUSEWHEELCAPTURES = 2696;

		// Token: 0x040004CE RID: 1230
		public const int SCI_GETMOUSEWHEELCAPTURES = 2697;

		// Token: 0x040004CF RID: 1231
		public const int SCI_TOGGLEFOLDSHOWTEXT = 2700;

		// Token: 0x040004D0 RID: 1232
		public const int SCI_FOLDDISPLAYTEXTSETSTYLE = 2701;

		// Token: 0x040004D1 RID: 1233
		public const int SCI_STARTRECORD = 3001;

		// Token: 0x040004D2 RID: 1234
		public const int SCI_STOPRECORD = 3002;

		// Token: 0x040004D3 RID: 1235
		public const int SCI_SETLEXER = 4001;

		// Token: 0x040004D4 RID: 1236
		public const int SCI_GETLEXER = 4002;

		// Token: 0x040004D5 RID: 1237
		public const int SCI_COLOURISE = 4003;

		// Token: 0x040004D6 RID: 1238
		public const int SCI_SETPROPERTY = 4004;

		// Token: 0x040004D7 RID: 1239
		public const int SCI_SETKEYWORDS = 4005;

		// Token: 0x040004D8 RID: 1240
		public const int SCI_SETLEXERLANGUAGE = 4006;

		// Token: 0x040004D9 RID: 1241
		public const int SCI_LOADLEXERLIBRARY = 4007;

		// Token: 0x040004DA RID: 1242
		public const int SCI_GETPROPERTY = 4008;

		// Token: 0x040004DB RID: 1243
		public const int SCI_GETPROPERTYEXPANDED = 4009;

		// Token: 0x040004DC RID: 1244
		public const int SCI_GETPROPERTYINT = 4010;

		// Token: 0x040004DD RID: 1245
		public const int SCI_GETLEXERLANGUAGE = 4012;

		// Token: 0x040004DE RID: 1246
		public const int SCI_PRIVATELEXERCALL = 4013;

		// Token: 0x040004DF RID: 1247
		public const int SCI_PROPERTYNAMES = 4014;

		// Token: 0x040004E0 RID: 1248
		public const int SCI_PROPERTYTYPE = 4015;

		// Token: 0x040004E1 RID: 1249
		public const int SCI_DESCRIBEPROPERTY = 4016;

		// Token: 0x040004E2 RID: 1250
		public const int SCI_DESCRIBEKEYWORDSETS = 4017;

		// Token: 0x040004E3 RID: 1251
		public const int SCI_GETLINEENDTYPESSUPPORTED = 4018;

		// Token: 0x040004E4 RID: 1252
		public const int SCI_ALLOCATESUBSTYLES = 4020;

		// Token: 0x040004E5 RID: 1253
		public const int SCI_GETSUBSTYLESSTART = 4021;

		// Token: 0x040004E6 RID: 1254
		public const int SCI_GETSUBSTYLESLENGTH = 4022;

		// Token: 0x040004E7 RID: 1255
		public const int SCI_GETSTYLEFROMSUBSTYLE = 4027;

		// Token: 0x040004E8 RID: 1256
		public const int SCI_GETPRIMARYSTYLEFROMSTYLE = 4028;

		// Token: 0x040004E9 RID: 1257
		public const int SCI_FREESUBSTYLES = 4023;

		// Token: 0x040004EA RID: 1258
		public const int SCI_SETIDENTIFIERS = 4024;

		// Token: 0x040004EB RID: 1259
		public const int SCI_DISTANCETOSECONDARYSTYLES = 4025;

		// Token: 0x040004EC RID: 1260
		public const int SCI_GETSUBSTYLEBASES = 4026;

		// Token: 0x040004ED RID: 1261
		public const int SCK_DOWN = 300;

		// Token: 0x040004EE RID: 1262
		public const int SCK_UP = 301;

		// Token: 0x040004EF RID: 1263
		public const int SCK_LEFT = 302;

		// Token: 0x040004F0 RID: 1264
		public const int SCK_RIGHT = 303;

		// Token: 0x040004F1 RID: 1265
		public const int SCK_HOME = 304;

		// Token: 0x040004F2 RID: 1266
		public const int SCK_END = 305;

		// Token: 0x040004F3 RID: 1267
		public const int SCK_PRIOR = 306;

		// Token: 0x040004F4 RID: 1268
		public const int SCK_NEXT = 307;

		// Token: 0x040004F5 RID: 1269
		public const int SCK_DELETE = 308;

		// Token: 0x040004F6 RID: 1270
		public const int SCK_INSERT = 309;

		// Token: 0x040004F7 RID: 1271
		public const int SCK_ESCAPE = 7;

		// Token: 0x040004F8 RID: 1272
		public const int SCK_BACK = 8;

		// Token: 0x040004F9 RID: 1273
		public const int SCK_TAB = 9;

		// Token: 0x040004FA RID: 1274
		public const int SCK_RETURN = 13;

		// Token: 0x040004FB RID: 1275
		public const int SCK_ADD = 310;

		// Token: 0x040004FC RID: 1276
		public const int SCK_SUBTRACT = 311;

		// Token: 0x040004FD RID: 1277
		public const int SCK_DIVIDE = 312;

		// Token: 0x040004FE RID: 1278
		public const int SCK_WIN = 313;

		// Token: 0x040004FF RID: 1279
		public const int SCK_RWIN = 314;

		// Token: 0x04000500 RID: 1280
		public const int SCK_MENU = 315;

		// Token: 0x04000501 RID: 1281
		public const int SCN_STYLENEEDED = 2000;

		// Token: 0x04000502 RID: 1282
		public const int SCN_CHARADDED = 2001;

		// Token: 0x04000503 RID: 1283
		public const int SCN_SAVEPOINTREACHED = 2002;

		// Token: 0x04000504 RID: 1284
		public const int SCN_SAVEPOINTLEFT = 2003;

		// Token: 0x04000505 RID: 1285
		public const int SCN_MODIFYATTEMPTRO = 2004;

		// Token: 0x04000506 RID: 1286
		public const int SCN_KEY = 2005;

		// Token: 0x04000507 RID: 1287
		public const int SCN_DOUBLECLICK = 2006;

		// Token: 0x04000508 RID: 1288
		public const int SCN_UPDATEUI = 2007;

		// Token: 0x04000509 RID: 1289
		public const int SCN_MODIFIED = 2008;

		// Token: 0x0400050A RID: 1290
		public const int SCN_MACRORECORD = 2009;

		// Token: 0x0400050B RID: 1291
		public const int SCN_MARGINCLICK = 2010;

		// Token: 0x0400050C RID: 1292
		public const int SCN_NEEDSHOWN = 2011;

		// Token: 0x0400050D RID: 1293
		public const int SCN_PAINTED = 2013;

		// Token: 0x0400050E RID: 1294
		public const int SCN_USERLISTSELECTION = 2014;

		// Token: 0x0400050F RID: 1295
		public const int SCN_URIDROPPED = 2015;

		// Token: 0x04000510 RID: 1296
		public const int SCN_DWELLSTART = 2016;

		// Token: 0x04000511 RID: 1297
		public const int SCN_DWELLEND = 2017;

		// Token: 0x04000512 RID: 1298
		public const int SCN_ZOOM = 2018;

		// Token: 0x04000513 RID: 1299
		public const int SCN_HOTSPOTCLICK = 2019;

		// Token: 0x04000514 RID: 1300
		public const int SCN_HOTSPOTDOUBLECLICK = 2020;

		// Token: 0x04000515 RID: 1301
		public const int SCN_CALLTIPCLICK = 2021;

		// Token: 0x04000516 RID: 1302
		public const int SCN_AUTOCSELECTION = 2022;

		// Token: 0x04000517 RID: 1303
		public const int SCN_INDICATORCLICK = 2023;

		// Token: 0x04000518 RID: 1304
		public const int SCN_INDICATORRELEASE = 2024;

		// Token: 0x04000519 RID: 1305
		public const int SCN_AUTOCCANCELLED = 2025;

		// Token: 0x0400051A RID: 1306
		public const int SCN_AUTOCCHARDELETED = 2026;

		// Token: 0x0400051B RID: 1307
		public const int SCN_HOTSPOTRELEASECLICK = 2027;

		// Token: 0x0400051C RID: 1308
		public const int SCN_FOCUSIN = 2028;

		// Token: 0x0400051D RID: 1309
		public const int SCN_FOCUSOUT = 2029;

		// Token: 0x0400051E RID: 1310
		public const int SCN_AUTOCCOMPLETED = 2030;

		// Token: 0x0400051F RID: 1311
		public const int SCN_MARGINRIGHTCLICK = 2031;

		// Token: 0x04000520 RID: 1312
		public const int SC_POPUP_NEVER = 0;

		// Token: 0x04000521 RID: 1313
		public const int SC_POPUP_ALL = 1;

		// Token: 0x04000522 RID: 1314
		public const int SC_POPUP_TEXT = 2;

		// Token: 0x04000523 RID: 1315
		public const int SC_WRAP_NONE = 0;

		// Token: 0x04000524 RID: 1316
		public const int SC_WRAP_WORD = 1;

		// Token: 0x04000525 RID: 1317
		public const int SC_WRAP_CHAR = 2;

		// Token: 0x04000526 RID: 1318
		public const int SC_WRAP_WHITESPACE = 3;

		// Token: 0x04000527 RID: 1319
		public const int SC_WRAPVISUALFLAG_NONE = 0;

		// Token: 0x04000528 RID: 1320
		public const int SC_WRAPVISUALFLAG_END = 1;

		// Token: 0x04000529 RID: 1321
		public const int SC_WRAPVISUALFLAG_START = 2;

		// Token: 0x0400052A RID: 1322
		public const int SC_WRAPVISUALFLAG_MARGIN = 4;

		// Token: 0x0400052B RID: 1323
		public const int SC_WRAPVISUALFLAGLOC_DEFAULT = 0;

		// Token: 0x0400052C RID: 1324
		public const int SC_WRAPVISUALFLAGLOC_END_BY_TEXT = 1;

		// Token: 0x0400052D RID: 1325
		public const int SC_WRAPVISUALFLAGLOC_START_BY_TEXT = 2;

		// Token: 0x0400052E RID: 1326
		public const int SC_WRAPINDENT_FIXED = 0;

		// Token: 0x0400052F RID: 1327
		public const int SC_WRAPINDENT_SAME = 1;

		// Token: 0x04000530 RID: 1328
		public const int SC_WRAPINDENT_INDENT = 2;

		// Token: 0x04000531 RID: 1329
		public const int SCVS_NONE = 0;

		// Token: 0x04000532 RID: 1330
		public const int SCVS_RECTANGULARSELECTION = 1;

		// Token: 0x04000533 RID: 1331
		public const int SCVS_USERACCESSIBLE = 2;

		// Token: 0x04000534 RID: 1332
		public const int SCVS_NOWRAPLINESTART = 4;

		// Token: 0x04000535 RID: 1333
		public const int STYLE_DEFAULT = 32;

		// Token: 0x04000536 RID: 1334
		public const int STYLE_LINENUMBER = 33;

		// Token: 0x04000537 RID: 1335
		public const int STYLE_BRACELIGHT = 34;

		// Token: 0x04000538 RID: 1336
		public const int STYLE_BRACEBAD = 35;

		// Token: 0x04000539 RID: 1337
		public const int STYLE_CONTROLCHAR = 36;

		// Token: 0x0400053A RID: 1338
		public const int STYLE_INDENTGUIDE = 37;

		// Token: 0x0400053B RID: 1339
		public const int STYLE_CALLTIP = 38;

		// Token: 0x0400053C RID: 1340
		public const int STYLE_FOLDDISPLAYTEXT = 39;

		// Token: 0x0400053D RID: 1341
		public const int STYLE_LASTPREDEFINED = 39;

		// Token: 0x0400053E RID: 1342
		public const int STYLE_MAX = 255;

		// Token: 0x0400053F RID: 1343
		public const int SC_FONT_SIZE_MULTIPLIER = 100;

		// Token: 0x04000540 RID: 1344
		public const int SC_CASE_MIXED = 0;

		// Token: 0x04000541 RID: 1345
		public const int SC_CASE_UPPER = 1;

		// Token: 0x04000542 RID: 1346
		public const int SC_CASE_LOWER = 2;

		// Token: 0x04000543 RID: 1347
		public const int SC_CASE_CAMEL = 3;

		// Token: 0x04000544 RID: 1348
		public const int SC_TECHNOLOGY_DEFAULT = 0;

		// Token: 0x04000545 RID: 1349
		public const int SC_TECHNOLOGY_DIRECTWRITE = 1;

		// Token: 0x04000546 RID: 1350
		public const int SC_TECHNOLOGY_DIRECTWRITERETAIN = 2;

		// Token: 0x04000547 RID: 1351
		public const int SC_TECHNOLOGY_DIRECTWRITEDC = 3;

		// Token: 0x04000548 RID: 1352
		public const int SCTD_LONGARROW = 0;

		// Token: 0x04000549 RID: 1353
		public const int SCTD_STRIKEOUT = 1;

		// Token: 0x0400054A RID: 1354
		public const int UNDO_MAY_COALESCE = 1;

		// Token: 0x0400054B RID: 1355
		public const int SCWS_INVISIBLE = 0;

		// Token: 0x0400054C RID: 1356
		public const int SCWS_VISIBLEALWAYS = 1;

		// Token: 0x0400054D RID: 1357
		public const int SCWS_VISIBLEAFTERINDENT = 2;

		// Token: 0x0400054E RID: 1358
		public const int SCWS_VISIBLEONLYININDENT = 3;

		// Token: 0x0400054F RID: 1359
		public const int WM_CREATE = 1;

		// Token: 0x04000550 RID: 1360
		public const int WM_DESTROY = 2;

		// Token: 0x04000551 RID: 1361
		public const int WM_SETCURSOR = 32;

		// Token: 0x04000552 RID: 1362
		public const int WM_NOTIFY = 78;

		// Token: 0x04000553 RID: 1363
		public const int WM_LBUTTONDBLCLK = 515;

		// Token: 0x04000554 RID: 1364
		public const int WM_RBUTTONDBLCLK = 518;

		// Token: 0x04000555 RID: 1365
		public const int WM_MBUTTONDBLCLK = 521;

		// Token: 0x04000556 RID: 1366
		public const int WM_XBUTTONDBLCLK = 525;

		// Token: 0x04000557 RID: 1367
		public const int WM_USER = 1024;

		// Token: 0x04000558 RID: 1368
		public const int WM_REFLECT = 8192;

		// Token: 0x04000559 RID: 1369
		public const int WS_BORDER = 8388608;

		// Token: 0x0400055A RID: 1370
		public const int WS_EX_CLIENTEDGE = 512;

		// Token: 0x0400055B RID: 1371
		public const int SCLEX_CONTAINER = 0;

		// Token: 0x0400055C RID: 1372
		public const int SCLEX_NULL = 1;

		// Token: 0x0400055D RID: 1373
		public const int SCLEX_PYTHON = 2;

		// Token: 0x0400055E RID: 1374
		public const int SCLEX_CPP = 3;

		// Token: 0x0400055F RID: 1375
		public const int SCLEX_HTML = 4;

		// Token: 0x04000560 RID: 1376
		public const int SCLEX_XML = 5;

		// Token: 0x04000561 RID: 1377
		public const int SCLEX_PERL = 6;

		// Token: 0x04000562 RID: 1378
		public const int SCLEX_SQL = 7;

		// Token: 0x04000563 RID: 1379
		public const int SCLEX_VB = 8;

		// Token: 0x04000564 RID: 1380
		public const int SCLEX_PROPERTIES = 9;

		// Token: 0x04000565 RID: 1381
		public const int SCLEX_ERRORLIST = 10;

		// Token: 0x04000566 RID: 1382
		public const int SCLEX_MAKEFILE = 11;

		// Token: 0x04000567 RID: 1383
		public const int SCLEX_BATCH = 12;

		// Token: 0x04000568 RID: 1384
		public const int SCLEX_XCODE = 13;

		// Token: 0x04000569 RID: 1385
		public const int SCLEX_LATEX = 14;

		// Token: 0x0400056A RID: 1386
		public const int SCLEX_LUA = 15;

		// Token: 0x0400056B RID: 1387
		public const int SCLEX_DIFF = 16;

		// Token: 0x0400056C RID: 1388
		public const int SCLEX_CONF = 17;

		// Token: 0x0400056D RID: 1389
		public const int SCLEX_PASCAL = 18;

		// Token: 0x0400056E RID: 1390
		public const int SCLEX_AVE = 19;

		// Token: 0x0400056F RID: 1391
		public const int SCLEX_ADA = 20;

		// Token: 0x04000570 RID: 1392
		public const int SCLEX_LISP = 21;

		// Token: 0x04000571 RID: 1393
		public const int SCLEX_RUBY = 22;

		// Token: 0x04000572 RID: 1394
		public const int SCLEX_EIFFEL = 23;

		// Token: 0x04000573 RID: 1395
		public const int SCLEX_EIFFELKW = 24;

		// Token: 0x04000574 RID: 1396
		public const int SCLEX_TCL = 25;

		// Token: 0x04000575 RID: 1397
		public const int SCLEX_NNCRONTAB = 26;

		// Token: 0x04000576 RID: 1398
		public const int SCLEX_BULLANT = 27;

		// Token: 0x04000577 RID: 1399
		public const int SCLEX_VBSCRIPT = 28;

		// Token: 0x04000578 RID: 1400
		public const int SCLEX_BAAN = 31;

		// Token: 0x04000579 RID: 1401
		public const int SCLEX_MATLAB = 32;

		// Token: 0x0400057A RID: 1402
		public const int SCLEX_SCRIPTOL = 33;

		// Token: 0x0400057B RID: 1403
		public const int SCLEX_ASM = 34;

		// Token: 0x0400057C RID: 1404
		public const int SCLEX_CPPNOCASE = 35;

		// Token: 0x0400057D RID: 1405
		public const int SCLEX_FORTRAN = 36;

		// Token: 0x0400057E RID: 1406
		public const int SCLEX_F77 = 37;

		// Token: 0x0400057F RID: 1407
		public const int SCLEX_CSS = 38;

		// Token: 0x04000580 RID: 1408
		public const int SCLEX_POV = 39;

		// Token: 0x04000581 RID: 1409
		public const int SCLEX_LOUT = 40;

		// Token: 0x04000582 RID: 1410
		public const int SCLEX_ESCRIPT = 41;

		// Token: 0x04000583 RID: 1411
		public const int SCLEX_PS = 42;

		// Token: 0x04000584 RID: 1412
		public const int SCLEX_NSIS = 43;

		// Token: 0x04000585 RID: 1413
		public const int SCLEX_MMIXAL = 44;

		// Token: 0x04000586 RID: 1414
		public const int SCLEX_CLW = 45;

		// Token: 0x04000587 RID: 1415
		public const int SCLEX_CLWNOCASE = 46;

		// Token: 0x04000588 RID: 1416
		public const int SCLEX_LOT = 47;

		// Token: 0x04000589 RID: 1417
		public const int SCLEX_YAML = 48;

		// Token: 0x0400058A RID: 1418
		public const int SCLEX_TEX = 49;

		// Token: 0x0400058B RID: 1419
		public const int SCLEX_METAPOST = 50;

		// Token: 0x0400058C RID: 1420
		public const int SCLEX_POWERBASIC = 51;

		// Token: 0x0400058D RID: 1421
		public const int SCLEX_FORTH = 52;

		// Token: 0x0400058E RID: 1422
		public const int SCLEX_ERLANG = 53;

		// Token: 0x0400058F RID: 1423
		public const int SCLEX_OCTAVE = 54;

		// Token: 0x04000590 RID: 1424
		public const int SCLEX_MSSQL = 55;

		// Token: 0x04000591 RID: 1425
		public const int SCLEX_VERILOG = 56;

		// Token: 0x04000592 RID: 1426
		public const int SCLEX_KIX = 57;

		// Token: 0x04000593 RID: 1427
		public const int SCLEX_GUI4CLI = 58;

		// Token: 0x04000594 RID: 1428
		public const int SCLEX_SPECMAN = 59;

		// Token: 0x04000595 RID: 1429
		public const int SCLEX_AU3 = 60;

		// Token: 0x04000596 RID: 1430
		public const int SCLEX_APDL = 61;

		// Token: 0x04000597 RID: 1431
		public const int SCLEX_BASH = 62;

		// Token: 0x04000598 RID: 1432
		public const int SCLEX_ASN1 = 63;

		// Token: 0x04000599 RID: 1433
		public const int SCLEX_VHDL = 64;

		// Token: 0x0400059A RID: 1434
		public const int SCLEX_CAML = 65;

		// Token: 0x0400059B RID: 1435
		public const int SCLEX_BLITZBASIC = 66;

		// Token: 0x0400059C RID: 1436
		public const int SCLEX_PUREBASIC = 67;

		// Token: 0x0400059D RID: 1437
		public const int SCLEX_HASKELL = 68;

		// Token: 0x0400059E RID: 1438
		public const int SCLEX_PHPSCRIPT = 69;

		// Token: 0x0400059F RID: 1439
		public const int SCLEX_TADS3 = 70;

		// Token: 0x040005A0 RID: 1440
		public const int SCLEX_REBOL = 71;

		// Token: 0x040005A1 RID: 1441
		public const int SCLEX_SMALLTALK = 72;

		// Token: 0x040005A2 RID: 1442
		public const int SCLEX_FLAGSHIP = 73;

		// Token: 0x040005A3 RID: 1443
		public const int SCLEX_CSOUND = 74;

		// Token: 0x040005A4 RID: 1444
		public const int SCLEX_FREEBASIC = 75;

		// Token: 0x040005A5 RID: 1445
		public const int SCLEX_INNOSETUP = 76;

		// Token: 0x040005A6 RID: 1446
		public const int SCLEX_OPAL = 77;

		// Token: 0x040005A7 RID: 1447
		public const int SCLEX_SPICE = 78;

		// Token: 0x040005A8 RID: 1448
		public const int SCLEX_D = 79;

		// Token: 0x040005A9 RID: 1449
		public const int SCLEX_CMAKE = 80;

		// Token: 0x040005AA RID: 1450
		public const int SCLEX_GAP = 81;

		// Token: 0x040005AB RID: 1451
		public const int SCLEX_PLM = 82;

		// Token: 0x040005AC RID: 1452
		public const int SCLEX_PROGRESS = 83;

		// Token: 0x040005AD RID: 1453
		public const int SCLEX_ABAQUS = 84;

		// Token: 0x040005AE RID: 1454
		public const int SCLEX_ASYMPTOTE = 85;

		// Token: 0x040005AF RID: 1455
		public const int SCLEX_R = 86;

		// Token: 0x040005B0 RID: 1456
		public const int SCLEX_MAGIK = 87;

		// Token: 0x040005B1 RID: 1457
		public const int SCLEX_POWERSHELL = 88;

		// Token: 0x040005B2 RID: 1458
		public const int SCLEX_MYSQL = 89;

		// Token: 0x040005B3 RID: 1459
		public const int SCLEX_PO = 90;

		// Token: 0x040005B4 RID: 1460
		public const int SCLEX_TAL = 91;

		// Token: 0x040005B5 RID: 1461
		public const int SCLEX_COBOL = 92;

		// Token: 0x040005B6 RID: 1462
		public const int SCLEX_TACL = 93;

		// Token: 0x040005B7 RID: 1463
		public const int SCLEX_SORCUS = 94;

		// Token: 0x040005B8 RID: 1464
		public const int SCLEX_POWERPRO = 95;

		// Token: 0x040005B9 RID: 1465
		public const int SCLEX_NIMROD = 96;

		// Token: 0x040005BA RID: 1466
		public const int SCLEX_SML = 97;

		// Token: 0x040005BB RID: 1467
		public const int SCLEX_MARKDOWN = 98;

		// Token: 0x040005BC RID: 1468
		public const int SCLEX_TXT2TAGS = 99;

		// Token: 0x040005BD RID: 1469
		public const int SCLEX_A68K = 100;

		// Token: 0x040005BE RID: 1470
		public const int SCLEX_MODULA = 101;

		// Token: 0x040005BF RID: 1471
		public const int SCLEX_COFFEESCRIPT = 102;

		// Token: 0x040005C0 RID: 1472
		public const int SCLEX_TCMD = 103;

		// Token: 0x040005C1 RID: 1473
		public const int SCLEX_AVS = 104;

		// Token: 0x040005C2 RID: 1474
		public const int SCLEX_ECL = 105;

		// Token: 0x040005C3 RID: 1475
		public const int SCLEX_OSCRIPT = 106;

		// Token: 0x040005C4 RID: 1476
		public const int SCLEX_VISUALPROLOG = 107;

		// Token: 0x040005C5 RID: 1477
		public const int SCLEX_LITERATEHASKELL = 108;

		// Token: 0x040005C6 RID: 1478
		public const int SCLEX_STTXT = 109;

		// Token: 0x040005C7 RID: 1479
		public const int SCLEX_KVIRC = 110;

		// Token: 0x040005C8 RID: 1480
		public const int SCLEX_RUST = 111;

		// Token: 0x040005C9 RID: 1481
		public const int SCLEX_DMAP = 112;

		// Token: 0x040005CA RID: 1482
		public const int SCLEX_AS = 113;

		// Token: 0x040005CB RID: 1483
		public const int SCLEX_DMIS = 114;

		// Token: 0x040005CC RID: 1484
		public const int SCLEX_REGISTRY = 115;

		// Token: 0x040005CD RID: 1485
		public const int SCLEX_BIBTEX = 116;

		// Token: 0x040005CE RID: 1486
		public const int SCLEX_SREC = 117;

		// Token: 0x040005CF RID: 1487
		public const int SCLEX_IHEX = 118;

		// Token: 0x040005D0 RID: 1488
		public const int SCLEX_TEHEX = 119;

		// Token: 0x040005D1 RID: 1489
		public const int SCLEX_JSON = 120;

		// Token: 0x040005D2 RID: 1490
		public const int SCLEX_AUTOMATIC = 1000;

		// Token: 0x040005D3 RID: 1491
		public const int SCE_ADA_DEFAULT = 0;

		// Token: 0x040005D4 RID: 1492
		public const int SCE_ADA_WORD = 1;

		// Token: 0x040005D5 RID: 1493
		public const int SCE_ADA_IDENTIFIER = 2;

		// Token: 0x040005D6 RID: 1494
		public const int SCE_ADA_NUMBER = 3;

		// Token: 0x040005D7 RID: 1495
		public const int SCE_ADA_DELIMITER = 4;

		// Token: 0x040005D8 RID: 1496
		public const int SCE_ADA_CHARACTER = 5;

		// Token: 0x040005D9 RID: 1497
		public const int SCE_ADA_CHARACTEREOL = 6;

		// Token: 0x040005DA RID: 1498
		public const int SCE_ADA_STRING = 7;

		// Token: 0x040005DB RID: 1499
		public const int SCE_ADA_STRINGEOL = 8;

		// Token: 0x040005DC RID: 1500
		public const int SCE_ADA_LABEL = 9;

		// Token: 0x040005DD RID: 1501
		public const int SCE_ADA_COMMENTLINE = 10;

		// Token: 0x040005DE RID: 1502
		public const int SCE_ADA_ILLEGAL = 11;

		// Token: 0x040005DF RID: 1503
		public const int SCE_ASM_DEFAULT = 0;

		// Token: 0x040005E0 RID: 1504
		public const int SCE_ASM_COMMENT = 1;

		// Token: 0x040005E1 RID: 1505
		public const int SCE_ASM_NUMBER = 2;

		// Token: 0x040005E2 RID: 1506
		public const int SCE_ASM_STRING = 3;

		// Token: 0x040005E3 RID: 1507
		public const int SCE_ASM_OPERATOR = 4;

		// Token: 0x040005E4 RID: 1508
		public const int SCE_ASM_IDENTIFIER = 5;

		// Token: 0x040005E5 RID: 1509
		public const int SCE_ASM_CPUINSTRUCTION = 6;

		// Token: 0x040005E6 RID: 1510
		public const int SCE_ASM_MATHINSTRUCTION = 7;

		// Token: 0x040005E7 RID: 1511
		public const int SCE_ASM_REGISTER = 8;

		// Token: 0x040005E8 RID: 1512
		public const int SCE_ASM_DIRECTIVE = 9;

		// Token: 0x040005E9 RID: 1513
		public const int SCE_ASM_DIRECTIVEOPERAND = 10;

		// Token: 0x040005EA RID: 1514
		public const int SCE_ASM_COMMENTBLOCK = 11;

		// Token: 0x040005EB RID: 1515
		public const int SCE_ASM_CHARACTER = 12;

		// Token: 0x040005EC RID: 1516
		public const int SCE_ASM_STRINGEOL = 13;

		// Token: 0x040005ED RID: 1517
		public const int SCE_ASM_EXTINSTRUCTION = 14;

		// Token: 0x040005EE RID: 1518
		public const int SCE_ASM_COMMENTDIRECTIVE = 15;

		// Token: 0x040005EF RID: 1519
		public const int SCE_BAT_DEFAULT = 0;

		// Token: 0x040005F0 RID: 1520
		public const int SCE_BAT_COMMENT = 1;

		// Token: 0x040005F1 RID: 1521
		public const int SCE_BAT_WORD = 2;

		// Token: 0x040005F2 RID: 1522
		public const int SCE_BAT_LABEL = 3;

		// Token: 0x040005F3 RID: 1523
		public const int SCE_BAT_HIDE = 4;

		// Token: 0x040005F4 RID: 1524
		public const int SCE_BAT_COMMAND = 5;

		// Token: 0x040005F5 RID: 1525
		public const int SCE_BAT_IDENTIFIER = 6;

		// Token: 0x040005F6 RID: 1526
		public const int SCE_BAT_OPERATOR = 7;

		// Token: 0x040005F7 RID: 1527
		public const int SCE_C_DEFAULT = 0;

		// Token: 0x040005F8 RID: 1528
		public const int SCE_C_COMMENT = 1;

		// Token: 0x040005F9 RID: 1529
		public const int SCE_C_COMMENTLINE = 2;

		// Token: 0x040005FA RID: 1530
		public const int SCE_C_COMMENTDOC = 3;

		// Token: 0x040005FB RID: 1531
		public const int SCE_C_NUMBER = 4;

		// Token: 0x040005FC RID: 1532
		public const int SCE_C_WORD = 5;

		// Token: 0x040005FD RID: 1533
		public const int SCE_C_STRING = 6;

		// Token: 0x040005FE RID: 1534
		public const int SCE_C_CHARACTER = 7;

		// Token: 0x040005FF RID: 1535
		public const int SCE_C_UUID = 8;

		// Token: 0x04000600 RID: 1536
		public const int SCE_C_PREPROCESSOR = 9;

		// Token: 0x04000601 RID: 1537
		public const int SCE_C_OPERATOR = 10;

		// Token: 0x04000602 RID: 1538
		public const int SCE_C_IDENTIFIER = 11;

		// Token: 0x04000603 RID: 1539
		public const int SCE_C_STRINGEOL = 12;

		// Token: 0x04000604 RID: 1540
		public const int SCE_C_VERBATIM = 13;

		// Token: 0x04000605 RID: 1541
		public const int SCE_C_REGEX = 14;

		// Token: 0x04000606 RID: 1542
		public const int SCE_C_COMMENTLINEDOC = 15;

		// Token: 0x04000607 RID: 1543
		public const int SCE_C_WORD2 = 16;

		// Token: 0x04000608 RID: 1544
		public const int SCE_C_COMMENTDOCKEYWORD = 17;

		// Token: 0x04000609 RID: 1545
		public const int SCE_C_COMMENTDOCKEYWORDERROR = 18;

		// Token: 0x0400060A RID: 1546
		public const int SCE_C_GLOBALCLASS = 19;

		// Token: 0x0400060B RID: 1547
		public const int SCE_C_STRINGRAW = 20;

		// Token: 0x0400060C RID: 1548
		public const int SCE_C_TRIPLEVERBATIM = 21;

		// Token: 0x0400060D RID: 1549
		public const int SCE_C_HASHQUOTEDSTRING = 22;

		// Token: 0x0400060E RID: 1550
		public const int SCE_C_PREPROCESSORCOMMENT = 23;

		// Token: 0x0400060F RID: 1551
		public const int SCE_C_PREPROCESSORCOMMENTDOC = 24;

		// Token: 0x04000610 RID: 1552
		public const int SCE_C_USERLITERAL = 25;

		// Token: 0x04000611 RID: 1553
		public const int SCE_C_TASKMARKER = 26;

		// Token: 0x04000612 RID: 1554
		public const int SCE_C_ESCAPESEQUENCE = 27;

		// Token: 0x04000613 RID: 1555
		public const int SCE_CSS_DEFAULT = 0;

		// Token: 0x04000614 RID: 1556
		public const int SCE_CSS_TAG = 1;

		// Token: 0x04000615 RID: 1557
		public const int SCE_CSS_CLASS = 2;

		// Token: 0x04000616 RID: 1558
		public const int SCE_CSS_PSEUDOCLASS = 3;

		// Token: 0x04000617 RID: 1559
		public const int SCE_CSS_UNKNOWN_PSEUDOCLASS = 4;

		// Token: 0x04000618 RID: 1560
		public const int SCE_CSS_OPERATOR = 5;

		// Token: 0x04000619 RID: 1561
		public const int SCE_CSS_IDENTIFIER = 6;

		// Token: 0x0400061A RID: 1562
		public const int SCE_CSS_UNKNOWN_IDENTIFIER = 7;

		// Token: 0x0400061B RID: 1563
		public const int SCE_CSS_VALUE = 8;

		// Token: 0x0400061C RID: 1564
		public const int SCE_CSS_COMMENT = 9;

		// Token: 0x0400061D RID: 1565
		public const int SCE_CSS_ID = 10;

		// Token: 0x0400061E RID: 1566
		public const int SCE_CSS_IMPORTANT = 11;

		// Token: 0x0400061F RID: 1567
		public const int SCE_CSS_DIRECTIVE = 12;

		// Token: 0x04000620 RID: 1568
		public const int SCE_CSS_DOUBLESTRING = 13;

		// Token: 0x04000621 RID: 1569
		public const int SCE_CSS_SINGLESTRING = 14;

		// Token: 0x04000622 RID: 1570
		public const int SCE_CSS_IDENTIFIER2 = 15;

		// Token: 0x04000623 RID: 1571
		public const int SCE_CSS_ATTRIBUTE = 16;

		// Token: 0x04000624 RID: 1572
		public const int SCE_CSS_IDENTIFIER3 = 17;

		// Token: 0x04000625 RID: 1573
		public const int SCE_CSS_PSEUDOELEMENT = 18;

		// Token: 0x04000626 RID: 1574
		public const int SCE_CSS_EXTENDED_IDENTIFIER = 19;

		// Token: 0x04000627 RID: 1575
		public const int SCE_CSS_EXTENDED_PSEUDOCLASS = 20;

		// Token: 0x04000628 RID: 1576
		public const int SCE_CSS_EXTENDED_PSEUDOELEMENT = 21;

		// Token: 0x04000629 RID: 1577
		public const int SCE_CSS_MEDIA = 22;

		// Token: 0x0400062A RID: 1578
		public const int SCE_CSS_VARIABLE = 23;

		// Token: 0x0400062B RID: 1579
		public const int SCE_F_DEFAULT = 0;

		// Token: 0x0400062C RID: 1580
		public const int SCE_F_COMMENT = 1;

		// Token: 0x0400062D RID: 1581
		public const int SCE_F_NUMBER = 2;

		// Token: 0x0400062E RID: 1582
		public const int SCE_F_STRING1 = 3;

		// Token: 0x0400062F RID: 1583
		public const int SCE_F_STRING2 = 4;

		// Token: 0x04000630 RID: 1584
		public const int SCE_F_STRINGEOL = 5;

		// Token: 0x04000631 RID: 1585
		public const int SCE_F_OPERATOR = 6;

		// Token: 0x04000632 RID: 1586
		public const int SCE_F_IDENTIFIER = 7;

		// Token: 0x04000633 RID: 1587
		public const int SCE_F_WORD = 8;

		// Token: 0x04000634 RID: 1588
		public const int SCE_F_WORD2 = 9;

		// Token: 0x04000635 RID: 1589
		public const int SCE_F_WORD3 = 10;

		// Token: 0x04000636 RID: 1590
		public const int SCE_F_PREPROCESSOR = 11;

		// Token: 0x04000637 RID: 1591
		public const int SCE_F_OPERATOR2 = 12;

		// Token: 0x04000638 RID: 1592
		public const int SCE_F_LABEL = 13;

		// Token: 0x04000639 RID: 1593
		public const int SCE_F_CONTINUATION = 14;

		// Token: 0x0400063A RID: 1594
		public const int SCE_H_DEFAULT = 0;

		// Token: 0x0400063B RID: 1595
		public const int SCE_H_TAG = 1;

		// Token: 0x0400063C RID: 1596
		public const int SCE_H_TAGUNKNOWN = 2;

		// Token: 0x0400063D RID: 1597
		public const int SCE_H_ATTRIBUTE = 3;

		// Token: 0x0400063E RID: 1598
		public const int SCE_H_ATTRIBUTEUNKNOWN = 4;

		// Token: 0x0400063F RID: 1599
		public const int SCE_H_NUMBER = 5;

		// Token: 0x04000640 RID: 1600
		public const int SCE_H_DOUBLESTRING = 6;

		// Token: 0x04000641 RID: 1601
		public const int SCE_H_SINGLESTRING = 7;

		// Token: 0x04000642 RID: 1602
		public const int SCE_H_OTHER = 8;

		// Token: 0x04000643 RID: 1603
		public const int SCE_H_COMMENT = 9;

		// Token: 0x04000644 RID: 1604
		public const int SCE_H_ENTITY = 10;

		// Token: 0x04000645 RID: 1605
		public const int SCE_H_TAGEND = 11;

		// Token: 0x04000646 RID: 1606
		public const int SCE_H_XMLSTART = 12;

		// Token: 0x04000647 RID: 1607
		public const int SCE_H_XMLEND = 13;

		// Token: 0x04000648 RID: 1608
		public const int SCE_H_SCRIPT = 14;

		// Token: 0x04000649 RID: 1609
		public const int SCE_H_ASP = 15;

		// Token: 0x0400064A RID: 1610
		public const int SCE_H_ASPAT = 16;

		// Token: 0x0400064B RID: 1611
		public const int SCE_H_CDATA = 17;

		// Token: 0x0400064C RID: 1612
		public const int SCE_H_QUESTION = 18;

		// Token: 0x0400064D RID: 1613
		public const int SCE_H_VALUE = 19;

		// Token: 0x0400064E RID: 1614
		public const int SCE_H_XCCOMMENT = 20;

		// Token: 0x0400064F RID: 1615
		public const int SCE_JSON_DEFAULT = 0;

		// Token: 0x04000650 RID: 1616
		public const int SCE_JSON_NUMBER = 1;

		// Token: 0x04000651 RID: 1617
		public const int SCE_JSON_STRING = 2;

		// Token: 0x04000652 RID: 1618
		public const int SCE_JSON_STRINGEOL = 3;

		// Token: 0x04000653 RID: 1619
		public const int SCE_JSON_PROPERTYNAME = 4;

		// Token: 0x04000654 RID: 1620
		public const int SCE_JSON_ESCAPESEQUENCE = 5;

		// Token: 0x04000655 RID: 1621
		public const int SCE_JSON_LINECOMMENT = 6;

		// Token: 0x04000656 RID: 1622
		public const int SCE_JSON_BLOCKCOMMENT = 7;

		// Token: 0x04000657 RID: 1623
		public const int SCE_JSON_OPERATOR = 8;

		// Token: 0x04000658 RID: 1624
		public const int SCE_JSON_URI = 9;

		// Token: 0x04000659 RID: 1625
		public const int SCE_JSON_COMPACTIRI = 10;

		// Token: 0x0400065A RID: 1626
		public const int SCE_JSON_KEYWORD = 11;

		// Token: 0x0400065B RID: 1627
		public const int SCE_JSON_LDKEYWORD = 12;

		// Token: 0x0400065C RID: 1628
		public const int SCE_JSON_ERROR = 13;

		// Token: 0x0400065D RID: 1629
		public const int SCE_LISP_DEFAULT = 0;

		// Token: 0x0400065E RID: 1630
		public const int SCE_LISP_COMMENT = 1;

		// Token: 0x0400065F RID: 1631
		public const int SCE_LISP_NUMBER = 2;

		// Token: 0x04000660 RID: 1632
		public const int SCE_LISP_KEYWORD = 3;

		// Token: 0x04000661 RID: 1633
		public const int SCE_LISP_KEYWORD_KW = 4;

		// Token: 0x04000662 RID: 1634
		public const int SCE_LISP_SYMBOL = 5;

		// Token: 0x04000663 RID: 1635
		public const int SCE_LISP_STRING = 6;

		// Token: 0x04000664 RID: 1636
		public const int SCE_LISP_STRINGEOL = 8;

		// Token: 0x04000665 RID: 1637
		public const int SCE_LISP_IDENTIFIER = 9;

		// Token: 0x04000666 RID: 1638
		public const int SCE_LISP_OPERATOR = 10;

		// Token: 0x04000667 RID: 1639
		public const int SCE_LISP_SPECIAL = 11;

		// Token: 0x04000668 RID: 1640
		public const int SCE_LISP_MULTI_COMMENT = 12;

		// Token: 0x04000669 RID: 1641
		public const int SCE_LUA_DEFAULT = 0;

		// Token: 0x0400066A RID: 1642
		public const int SCE_LUA_COMMENT = 1;

		// Token: 0x0400066B RID: 1643
		public const int SCE_LUA_COMMENTLINE = 2;

		// Token: 0x0400066C RID: 1644
		public const int SCE_LUA_COMMENTDOC = 3;

		// Token: 0x0400066D RID: 1645
		public const int SCE_LUA_NUMBER = 4;

		// Token: 0x0400066E RID: 1646
		public const int SCE_LUA_WORD = 5;

		// Token: 0x0400066F RID: 1647
		public const int SCE_LUA_STRING = 6;

		// Token: 0x04000670 RID: 1648
		public const int SCE_LUA_CHARACTER = 7;

		// Token: 0x04000671 RID: 1649
		public const int SCE_LUA_LITERALSTRING = 8;

		// Token: 0x04000672 RID: 1650
		public const int SCE_LUA_PREPROCESSOR = 9;

		// Token: 0x04000673 RID: 1651
		public const int SCE_LUA_OPERATOR = 10;

		// Token: 0x04000674 RID: 1652
		public const int SCE_LUA_IDENTIFIER = 11;

		// Token: 0x04000675 RID: 1653
		public const int SCE_LUA_STRINGEOL = 12;

		// Token: 0x04000676 RID: 1654
		public const int SCE_LUA_WORD2 = 13;

		// Token: 0x04000677 RID: 1655
		public const int SCE_LUA_WORD3 = 14;

		// Token: 0x04000678 RID: 1656
		public const int SCE_LUA_WORD4 = 15;

		// Token: 0x04000679 RID: 1657
		public const int SCE_LUA_WORD5 = 16;

		// Token: 0x0400067A RID: 1658
		public const int SCE_LUA_WORD6 = 17;

		// Token: 0x0400067B RID: 1659
		public const int SCE_LUA_WORD7 = 18;

		// Token: 0x0400067C RID: 1660
		public const int SCE_LUA_WORD8 = 19;

		// Token: 0x0400067D RID: 1661
		public const int SCE_LUA_LABEL = 20;

		// Token: 0x0400067E RID: 1662
		public const int SCE_PAS_DEFAULT = 0;

		// Token: 0x0400067F RID: 1663
		public const int SCE_PAS_IDENTIFIER = 1;

		// Token: 0x04000680 RID: 1664
		public const int SCE_PAS_COMMENT = 2;

		// Token: 0x04000681 RID: 1665
		public const int SCE_PAS_COMMENT2 = 3;

		// Token: 0x04000682 RID: 1666
		public const int SCE_PAS_COMMENTLINE = 4;

		// Token: 0x04000683 RID: 1667
		public const int SCE_PAS_PREPROCESSOR = 5;

		// Token: 0x04000684 RID: 1668
		public const int SCE_PAS_PREPROCESSOR2 = 6;

		// Token: 0x04000685 RID: 1669
		public const int SCE_PAS_NUMBER = 7;

		// Token: 0x04000686 RID: 1670
		public const int SCE_PAS_HEXNUMBER = 8;

		// Token: 0x04000687 RID: 1671
		public const int SCE_PAS_WORD = 9;

		// Token: 0x04000688 RID: 1672
		public const int SCE_PAS_STRING = 10;

		// Token: 0x04000689 RID: 1673
		public const int SCE_PAS_STRINGEOL = 11;

		// Token: 0x0400068A RID: 1674
		public const int SCE_PAS_CHARACTER = 12;

		// Token: 0x0400068B RID: 1675
		public const int SCE_PAS_OPERATOR = 13;

		// Token: 0x0400068C RID: 1676
		public const int SCE_PAS_ASM = 14;

		// Token: 0x0400068D RID: 1677
		public const int SCE_PL_DEFAULT = 0;

		// Token: 0x0400068E RID: 1678
		public const int SCE_PL_ERROR = 1;

		// Token: 0x0400068F RID: 1679
		public const int SCE_PL_COMMENTLINE = 2;

		// Token: 0x04000690 RID: 1680
		public const int SCE_PL_POD = 3;

		// Token: 0x04000691 RID: 1681
		public const int SCE_PL_NUMBER = 4;

		// Token: 0x04000692 RID: 1682
		public const int SCE_PL_WORD = 5;

		// Token: 0x04000693 RID: 1683
		public const int SCE_PL_STRING = 6;

		// Token: 0x04000694 RID: 1684
		public const int SCE_PL_CHARACTER = 7;

		// Token: 0x04000695 RID: 1685
		public const int SCE_PL_PUNCTUATION = 8;

		// Token: 0x04000696 RID: 1686
		public const int SCE_PL_PREPROCESSOR = 9;

		// Token: 0x04000697 RID: 1687
		public const int SCE_PL_OPERATOR = 10;

		// Token: 0x04000698 RID: 1688
		public const int SCE_PL_IDENTIFIER = 11;

		// Token: 0x04000699 RID: 1689
		public const int SCE_PL_SCALAR = 12;

		// Token: 0x0400069A RID: 1690
		public const int SCE_PL_ARRAY = 13;

		// Token: 0x0400069B RID: 1691
		public const int SCE_PL_HASH = 14;

		// Token: 0x0400069C RID: 1692
		public const int SCE_PL_SYMBOLTABLE = 15;

		// Token: 0x0400069D RID: 1693
		public const int SCE_PL_VARIABLE_INDEXER = 16;

		// Token: 0x0400069E RID: 1694
		public const int SCE_PL_REGEX = 17;

		// Token: 0x0400069F RID: 1695
		public const int SCE_PL_REGSUBST = 18;

		// Token: 0x040006A0 RID: 1696
		public const int SCE_PL_LONGQUOTE = 19;

		// Token: 0x040006A1 RID: 1697
		public const int SCE_PL_BACKTICKS = 20;

		// Token: 0x040006A2 RID: 1698
		public const int SCE_PL_DATASECTION = 21;

		// Token: 0x040006A3 RID: 1699
		public const int SCE_PL_HERE_DELIM = 22;

		// Token: 0x040006A4 RID: 1700
		public const int SCE_PL_HERE_Q = 23;

		// Token: 0x040006A5 RID: 1701
		public const int SCE_PL_HERE_QQ = 24;

		// Token: 0x040006A6 RID: 1702
		public const int SCE_PL_HERE_QX = 25;

		// Token: 0x040006A7 RID: 1703
		public const int SCE_PL_STRING_Q = 26;

		// Token: 0x040006A8 RID: 1704
		public const int SCE_PL_STRING_QQ = 27;

		// Token: 0x040006A9 RID: 1705
		public const int SCE_PL_STRING_QX = 28;

		// Token: 0x040006AA RID: 1706
		public const int SCE_PL_STRING_QR = 29;

		// Token: 0x040006AB RID: 1707
		public const int SCE_PL_STRING_QW = 30;

		// Token: 0x040006AC RID: 1708
		public const int SCE_PL_POD_VERB = 31;

		// Token: 0x040006AD RID: 1709
		public const int SCE_PL_SUB_PROTOTYPE = 40;

		// Token: 0x040006AE RID: 1710
		public const int SCE_PL_FORMAT_IDENT = 41;

		// Token: 0x040006AF RID: 1711
		public const int SCE_PL_FORMAT = 42;

		// Token: 0x040006B0 RID: 1712
		public const int SCE_PL_STRING_VAR = 43;

		// Token: 0x040006B1 RID: 1713
		public const int SCE_PL_XLAT = 44;

		// Token: 0x040006B2 RID: 1714
		public const int SCE_PL_REGEX_VAR = 54;

		// Token: 0x040006B3 RID: 1715
		public const int SCE_PL_REGSUBST_VAR = 55;

		// Token: 0x040006B4 RID: 1716
		public const int SCE_PL_BACKTICKS_VAR = 57;

		// Token: 0x040006B5 RID: 1717
		public const int SCE_PL_HERE_QQ_VAR = 61;

		// Token: 0x040006B6 RID: 1718
		public const int SCE_PL_HERE_QX_VAR = 62;

		// Token: 0x040006B7 RID: 1719
		public const int SCE_PL_STRING_QQ_VAR = 64;

		// Token: 0x040006B8 RID: 1720
		public const int SCE_PL_STRING_QX_VAR = 65;

		// Token: 0x040006B9 RID: 1721
		public const int SCE_PL_STRING_QR_VAR = 66;

		// Token: 0x040006BA RID: 1722
		public const int SCE_POWERSHELL_DEFAULT = 0;

		// Token: 0x040006BB RID: 1723
		public const int SCE_POWERSHELL_COMMENT = 1;

		// Token: 0x040006BC RID: 1724
		public const int SCE_POWERSHELL_STRING = 2;

		// Token: 0x040006BD RID: 1725
		public const int SCE_POWERSHELL_CHARACTER = 3;

		// Token: 0x040006BE RID: 1726
		public const int SCE_POWERSHELL_NUMBER = 4;

		// Token: 0x040006BF RID: 1727
		public const int SCE_POWERSHELL_VARIABLE = 5;

		// Token: 0x040006C0 RID: 1728
		public const int SCE_POWERSHELL_OPERATOR = 6;

		// Token: 0x040006C1 RID: 1729
		public const int SCE_POWERSHELL_IDENTIFIER = 7;

		// Token: 0x040006C2 RID: 1730
		public const int SCE_POWERSHELL_KEYWORD = 8;

		// Token: 0x040006C3 RID: 1731
		public const int SCE_POWERSHELL_CMDLET = 9;

		// Token: 0x040006C4 RID: 1732
		public const int SCE_POWERSHELL_ALIAS = 10;

		// Token: 0x040006C5 RID: 1733
		public const int SCE_POWERSHELL_FUNCTION = 11;

		// Token: 0x040006C6 RID: 1734
		public const int SCE_POWERSHELL_USER1 = 12;

		// Token: 0x040006C7 RID: 1735
		public const int SCE_POWERSHELL_COMMENTSTREAM = 13;

		// Token: 0x040006C8 RID: 1736
		public const int SCE_POWERSHELL_HERE_STRING = 14;

		// Token: 0x040006C9 RID: 1737
		public const int SCE_POWERSHELL_HERE_CHARACTER = 15;

		// Token: 0x040006CA RID: 1738
		public const int SCE_POWERSHELL_COMMENTDOCKEYWORD = 16;

		// Token: 0x040006CB RID: 1739
		public const int SCE_PROPS_DEFAULT = 0;

		// Token: 0x040006CC RID: 1740
		public const int SCE_PROPS_COMMENT = 1;

		// Token: 0x040006CD RID: 1741
		public const int SCE_PROPS_SECTION = 2;

		// Token: 0x040006CE RID: 1742
		public const int SCE_PROPS_ASSIGNMENT = 3;

		// Token: 0x040006CF RID: 1743
		public const int SCE_PROPS_DEFVAL = 4;

		// Token: 0x040006D0 RID: 1744
		public const int SCE_PROPS_KEY = 5;

		// Token: 0x040006D1 RID: 1745
		public const int SCE_HPHP_COMPLEX_VARIABLE = 104;

		// Token: 0x040006D2 RID: 1746
		public const int SCE_HPHP_DEFAULT = 118;

		// Token: 0x040006D3 RID: 1747
		public const int SCE_HPHP_HSTRING = 119;

		// Token: 0x040006D4 RID: 1748
		public const int SCE_HPHP_SIMPLESTRING = 120;

		// Token: 0x040006D5 RID: 1749
		public const int SCE_HPHP_WORD = 121;

		// Token: 0x040006D6 RID: 1750
		public const int SCE_HPHP_NUMBER = 122;

		// Token: 0x040006D7 RID: 1751
		public const int SCE_HPHP_VARIABLE = 123;

		// Token: 0x040006D8 RID: 1752
		public const int SCE_HPHP_COMMENT = 124;

		// Token: 0x040006D9 RID: 1753
		public const int SCE_HPHP_COMMENTLINE = 125;

		// Token: 0x040006DA RID: 1754
		public const int SCE_HPHP_HSTRING_VARIABLE = 126;

		// Token: 0x040006DB RID: 1755
		public const int SCE_HPHP_OPERATOR = 127;

		// Token: 0x040006DC RID: 1756
		public const int SCE_SQL_DEFAULT = 0;

		// Token: 0x040006DD RID: 1757
		public const int SCE_SQL_COMMENT = 1;

		// Token: 0x040006DE RID: 1758
		public const int SCE_SQL_COMMENTLINE = 2;

		// Token: 0x040006DF RID: 1759
		public const int SCE_SQL_COMMENTDOC = 3;

		// Token: 0x040006E0 RID: 1760
		public const int SCE_SQL_NUMBER = 4;

		// Token: 0x040006E1 RID: 1761
		public const int SCE_SQL_WORD = 5;

		// Token: 0x040006E2 RID: 1762
		public const int SCE_SQL_STRING = 6;

		// Token: 0x040006E3 RID: 1763
		public const int SCE_SQL_CHARACTER = 7;

		// Token: 0x040006E4 RID: 1764
		public const int SCE_SQL_SQLPLUS = 8;

		// Token: 0x040006E5 RID: 1765
		public const int SCE_SQL_SQLPLUS_PROMPT = 9;

		// Token: 0x040006E6 RID: 1766
		public const int SCE_SQL_OPERATOR = 10;

		// Token: 0x040006E7 RID: 1767
		public const int SCE_SQL_IDENTIFIER = 11;

		// Token: 0x040006E8 RID: 1768
		public const int SCE_SQL_SQLPLUS_COMMENT = 13;

		// Token: 0x040006E9 RID: 1769
		public const int SCE_SQL_COMMENTLINEDOC = 15;

		// Token: 0x040006EA RID: 1770
		public const int SCE_SQL_WORD2 = 16;

		// Token: 0x040006EB RID: 1771
		public const int SCE_SQL_COMMENTDOCKEYWORD = 17;

		// Token: 0x040006EC RID: 1772
		public const int SCE_SQL_COMMENTDOCKEYWORDERROR = 18;

		// Token: 0x040006ED RID: 1773
		public const int SCE_SQL_USER1 = 19;

		// Token: 0x040006EE RID: 1774
		public const int SCE_SQL_USER2 = 20;

		// Token: 0x040006EF RID: 1775
		public const int SCE_SQL_USER3 = 21;

		// Token: 0x040006F0 RID: 1776
		public const int SCE_SQL_USER4 = 22;

		// Token: 0x040006F1 RID: 1777
		public const int SCE_SQL_QUOTEDIDENTIFIER = 23;

		// Token: 0x040006F2 RID: 1778
		public const int SCE_SQL_QOPERATOR = 24;

		// Token: 0x040006F3 RID: 1779
		public const int SCE_P_DEFAULT = 0;

		// Token: 0x040006F4 RID: 1780
		public const int SCE_P_COMMENTLINE = 1;

		// Token: 0x040006F5 RID: 1781
		public const int SCE_P_NUMBER = 2;

		// Token: 0x040006F6 RID: 1782
		public const int SCE_P_STRING = 3;

		// Token: 0x040006F7 RID: 1783
		public const int SCE_P_CHARACTER = 4;

		// Token: 0x040006F8 RID: 1784
		public const int SCE_P_WORD = 5;

		// Token: 0x040006F9 RID: 1785
		public const int SCE_P_TRIPLE = 6;

		// Token: 0x040006FA RID: 1786
		public const int SCE_P_TRIPLEDOUBLE = 7;

		// Token: 0x040006FB RID: 1787
		public const int SCE_P_CLASSNAME = 8;

		// Token: 0x040006FC RID: 1788
		public const int SCE_P_DEFNAME = 9;

		// Token: 0x040006FD RID: 1789
		public const int SCE_P_OPERATOR = 10;

		// Token: 0x040006FE RID: 1790
		public const int SCE_P_IDENTIFIER = 11;

		// Token: 0x040006FF RID: 1791
		public const int SCE_P_COMMENTBLOCK = 12;

		// Token: 0x04000700 RID: 1792
		public const int SCE_P_STRINGEOL = 13;

		// Token: 0x04000701 RID: 1793
		public const int SCE_P_WORD2 = 14;

		// Token: 0x04000702 RID: 1794
		public const int SCE_P_DECORATOR = 15;

		// Token: 0x04000703 RID: 1795
		public const int SCE_RB_DEFAULT = 0;

		// Token: 0x04000704 RID: 1796
		public const int SCE_RB_ERROR = 1;

		// Token: 0x04000705 RID: 1797
		public const int SCE_RB_COMMENTLINE = 2;

		// Token: 0x04000706 RID: 1798
		public const int SCE_RB_POD = 3;

		// Token: 0x04000707 RID: 1799
		public const int SCE_RB_NUMBER = 4;

		// Token: 0x04000708 RID: 1800
		public const int SCE_RB_WORD = 5;

		// Token: 0x04000709 RID: 1801
		public const int SCE_RB_STRING = 6;

		// Token: 0x0400070A RID: 1802
		public const int SCE_RB_CHARACTER = 7;

		// Token: 0x0400070B RID: 1803
		public const int SCE_RB_CLASSNAME = 8;

		// Token: 0x0400070C RID: 1804
		public const int SCE_RB_DEFNAME = 9;

		// Token: 0x0400070D RID: 1805
		public const int SCE_RB_OPERATOR = 10;

		// Token: 0x0400070E RID: 1806
		public const int SCE_RB_IDENTIFIER = 11;

		// Token: 0x0400070F RID: 1807
		public const int SCE_RB_REGEX = 12;

		// Token: 0x04000710 RID: 1808
		public const int SCE_RB_GLOBAL = 13;

		// Token: 0x04000711 RID: 1809
		public const int SCE_RB_SYMBOL = 14;

		// Token: 0x04000712 RID: 1810
		public const int SCE_RB_MODULE_NAME = 15;

		// Token: 0x04000713 RID: 1811
		public const int SCE_RB_INSTANCE_VAR = 16;

		// Token: 0x04000714 RID: 1812
		public const int SCE_RB_CLASS_VAR = 17;

		// Token: 0x04000715 RID: 1813
		public const int SCE_RB_BACKTICKS = 18;

		// Token: 0x04000716 RID: 1814
		public const int SCE_RB_DATASECTION = 19;

		// Token: 0x04000717 RID: 1815
		public const int SCE_RB_HERE_DELIM = 20;

		// Token: 0x04000718 RID: 1816
		public const int SCE_RB_HERE_Q = 21;

		// Token: 0x04000719 RID: 1817
		public const int SCE_RB_HERE_QQ = 22;

		// Token: 0x0400071A RID: 1818
		public const int SCE_RB_HERE_QX = 23;

		// Token: 0x0400071B RID: 1819
		public const int SCE_RB_STRING_Q = 24;

		// Token: 0x0400071C RID: 1820
		public const int SCE_RB_STRING_QQ = 25;

		// Token: 0x0400071D RID: 1821
		public const int SCE_RB_STRING_QX = 26;

		// Token: 0x0400071E RID: 1822
		public const int SCE_RB_STRING_QR = 27;

		// Token: 0x0400071F RID: 1823
		public const int SCE_RB_STRING_QW = 28;

		// Token: 0x04000720 RID: 1824
		public const int SCE_RB_WORD_DEMOTED = 29;

		// Token: 0x04000721 RID: 1825
		public const int SCE_RB_STDIN = 30;

		// Token: 0x04000722 RID: 1826
		public const int SCE_RB_STDOUT = 31;

		// Token: 0x04000723 RID: 1827
		public const int SCE_RB_STDERR = 40;

		// Token: 0x04000724 RID: 1828
		public const int SCE_RB_UPPER_BOUND = 41;

		// Token: 0x04000725 RID: 1829
		public const int SCE_ST_DEFAULT = 0;

		// Token: 0x04000726 RID: 1830
		public const int SCE_ST_STRING = 1;

		// Token: 0x04000727 RID: 1831
		public const int SCE_ST_NUMBER = 2;

		// Token: 0x04000728 RID: 1832
		public const int SCE_ST_COMMENT = 3;

		// Token: 0x04000729 RID: 1833
		public const int SCE_ST_SYMBOL = 4;

		// Token: 0x0400072A RID: 1834
		public const int SCE_ST_BINARY = 5;

		// Token: 0x0400072B RID: 1835
		public const int SCE_ST_BOOL = 6;

		// Token: 0x0400072C RID: 1836
		public const int SCE_ST_SELF = 7;

		// Token: 0x0400072D RID: 1837
		public const int SCE_ST_SUPER = 8;

		// Token: 0x0400072E RID: 1838
		public const int SCE_ST_NIL = 9;

		// Token: 0x0400072F RID: 1839
		public const int SCE_ST_GLOBAL = 10;

		// Token: 0x04000730 RID: 1840
		public const int SCE_ST_RETURN = 11;

		// Token: 0x04000731 RID: 1841
		public const int SCE_ST_SPECIAL = 12;

		// Token: 0x04000732 RID: 1842
		public const int SCE_ST_KWSEND = 13;

		// Token: 0x04000733 RID: 1843
		public const int SCE_ST_ASSIGN = 14;

		// Token: 0x04000734 RID: 1844
		public const int SCE_ST_CHARACTER = 15;

		// Token: 0x04000735 RID: 1845
		public const int SCE_ST_SPEC_SEL = 16;

		// Token: 0x04000736 RID: 1846
		public const int SCE_B_DEFAULT = 0;

		// Token: 0x04000737 RID: 1847
		public const int SCE_B_COMMENT = 1;

		// Token: 0x04000738 RID: 1848
		public const int SCE_B_NUMBER = 2;

		// Token: 0x04000739 RID: 1849
		public const int SCE_B_KEYWORD = 3;

		// Token: 0x0400073A RID: 1850
		public const int SCE_B_STRING = 4;

		// Token: 0x0400073B RID: 1851
		public const int SCE_B_PREPROCESSOR = 5;

		// Token: 0x0400073C RID: 1852
		public const int SCE_B_OPERATOR = 6;

		// Token: 0x0400073D RID: 1853
		public const int SCE_B_IDENTIFIER = 7;

		// Token: 0x0400073E RID: 1854
		public const int SCE_B_DATE = 8;

		// Token: 0x0400073F RID: 1855
		public const int SCE_B_STRINGEOL = 9;

		// Token: 0x04000740 RID: 1856
		public const int SCE_B_KEYWORD2 = 10;

		// Token: 0x04000741 RID: 1857
		public const int SCE_B_KEYWORD3 = 11;

		// Token: 0x04000742 RID: 1858
		public const int SCE_B_KEYWORD4 = 12;

		// Token: 0x04000743 RID: 1859
		public const int SCE_B_CONSTANT = 13;

		// Token: 0x04000744 RID: 1860
		public const int SCE_B_ASM = 14;

		// Token: 0x04000745 RID: 1861
		public const int SCE_B_LABEL = 15;

		// Token: 0x04000746 RID: 1862
		public const int SCE_B_ERROR = 16;

		// Token: 0x04000747 RID: 1863
		public const int SCE_B_HEXNUMBER = 17;

		// Token: 0x04000748 RID: 1864
		public const int SCE_B_BINNUMBER = 18;

		// Token: 0x04000749 RID: 1865
		public const int SCE_B_COMMENTBLOCK = 19;

		// Token: 0x0400074A RID: 1866
		public const int SCE_B_DOCLINE = 20;

		// Token: 0x0400074B RID: 1867
		public const int SCE_B_DOCBLOCK = 21;

		// Token: 0x0400074C RID: 1868
		public const int SCE_B_DOCKEYWORD = 22;

		// Token: 0x0400074D RID: 1869
		public const int SCE_MARKDOWN_DEFAULT = 0;

		// Token: 0x0400074E RID: 1870
		public const int SCE_MARKDOWN_LINE_BEGIN = 1;

		// Token: 0x0400074F RID: 1871
		public const int SCE_MARKDOWN_STRONG1 = 2;

		// Token: 0x04000750 RID: 1872
		public const int SCE_MARKDOWN_STRONG2 = 3;

		// Token: 0x04000751 RID: 1873
		public const int SCE_MARKDOWN_EM1 = 4;

		// Token: 0x04000752 RID: 1874
		public const int SCE_MARKDOWN_EM2 = 5;

		// Token: 0x04000753 RID: 1875
		public const int SCE_MARKDOWN_HEADER1 = 6;

		// Token: 0x04000754 RID: 1876
		public const int SCE_MARKDOWN_HEADER2 = 7;

		// Token: 0x04000755 RID: 1877
		public const int SCE_MARKDOWN_HEADER3 = 8;

		// Token: 0x04000756 RID: 1878
		public const int SCE_MARKDOWN_HEADER4 = 9;

		// Token: 0x04000757 RID: 1879
		public const int SCE_MARKDOWN_HEADER5 = 10;

		// Token: 0x04000758 RID: 1880
		public const int SCE_MARKDOWN_HEADER6 = 11;

		// Token: 0x04000759 RID: 1881
		public const int SCE_MARKDOWN_PRECHAR = 12;

		// Token: 0x0400075A RID: 1882
		public const int SCE_MARKDOWN_ULIST_ITEM = 13;

		// Token: 0x0400075B RID: 1883
		public const int SCE_MARKDOWN_OLIST_ITEM = 14;

		// Token: 0x0400075C RID: 1884
		public const int SCE_MARKDOWN_BLOCKQUOTE = 15;

		// Token: 0x0400075D RID: 1885
		public const int SCE_MARKDOWN_STRIKEOUT = 16;

		// Token: 0x0400075E RID: 1886
		public const int SCE_MARKDOWN_HRULE = 17;

		// Token: 0x0400075F RID: 1887
		public const int SCE_MARKDOWN_LINK = 18;

		// Token: 0x04000760 RID: 1888
		public const int SCE_MARKDOWN_CODE = 19;

		// Token: 0x04000761 RID: 1889
		public const int SCE_MARKDOWN_CODE2 = 20;

		// Token: 0x04000762 RID: 1890
		public const int SCE_MARKDOWN_CODEBK = 21;

		// Token: 0x04000763 RID: 1891
		public const int SCE_R_DEFAULT = 0;

		// Token: 0x04000764 RID: 1892
		public const int SCE_R_COMMENT = 1;

		// Token: 0x04000765 RID: 1893
		public const int SCE_R_KWORD = 2;

		// Token: 0x04000766 RID: 1894
		public const int SCE_R_BASEKWORD = 3;

		// Token: 0x04000767 RID: 1895
		public const int SCE_R_OTHERKWORD = 4;

		// Token: 0x04000768 RID: 1896
		public const int SCE_R_NUMBER = 5;

		// Token: 0x04000769 RID: 1897
		public const int SCE_R_STRING = 6;

		// Token: 0x0400076A RID: 1898
		public const int SCE_R_STRING2 = 7;

		// Token: 0x0400076B RID: 1899
		public const int SCE_R_OPERATOR = 8;

		// Token: 0x0400076C RID: 1900
		public const int SCE_R_IDENTIFIER = 9;

		// Token: 0x0400076D RID: 1901
		public const int SCE_R_INFIX = 10;

		// Token: 0x0400076E RID: 1902
		public const int SCE_R_INFIXEOL = 11;

		// Token: 0x0400076F RID: 1903
		public const int SCE_V_DEFAULT = 0;

		// Token: 0x04000770 RID: 1904
		public const int SCE_V_COMMENT = 1;

		// Token: 0x04000771 RID: 1905
		public const int SCE_V_COMMENTLINE = 2;

		// Token: 0x04000772 RID: 1906
		public const int SCE_V_COMMENTLINEBANG = 3;

		// Token: 0x04000773 RID: 1907
		public const int SCE_V_NUMBER = 4;

		// Token: 0x04000774 RID: 1908
		public const int SCE_V_WORD = 5;

		// Token: 0x04000775 RID: 1909
		public const int SCE_V_STRING = 6;

		// Token: 0x04000776 RID: 1910
		public const int SCE_V_WORD2 = 7;

		// Token: 0x04000777 RID: 1911
		public const int SCE_V_WORD3 = 8;

		// Token: 0x04000778 RID: 1912
		public const int SCE_V_PREPROCESSOR = 9;

		// Token: 0x04000779 RID: 1913
		public const int SCE_V_OPERATOR = 10;

		// Token: 0x0400077A RID: 1914
		public const int SCE_V_IDENTIFIER = 11;

		// Token: 0x0400077B RID: 1915
		public const int SCE_V_STRINGEOL = 12;

		// Token: 0x0400077C RID: 1916
		public const int SCE_V_USER = 19;

		// Token: 0x0400077D RID: 1917
		public const int SCE_V_COMMENT_WORD = 20;

		// Token: 0x0400077E RID: 1918
		public const int SCE_V_INPUT = 21;

		// Token: 0x0400077F RID: 1919
		public const int SCE_V_OUTPUT = 22;

		// Token: 0x04000780 RID: 1920
		public const int SCE_V_INOUT = 23;

		// Token: 0x04000781 RID: 1921
		public const int SCE_V_PORT_CONNECT = 24;

		// Token: 0x02000061 RID: 97
		// (Invoke) Token: 0x06000389 RID: 905
		public delegate IntPtr Scintilla_DirectFunction(IntPtr ptr, int iMessage, IntPtr wParam, IntPtr lParam);

		// Token: 0x02000062 RID: 98
		public struct ILoaderVTable32
		{
			// Token: 0x04000856 RID: 2134
			public NativeMethods.ILoaderVTable32.ReleaseDelegate Release;

			// Token: 0x04000857 RID: 2135
			public NativeMethods.ILoaderVTable32.AddDataDelegate AddData;

			// Token: 0x04000858 RID: 2136
			public NativeMethods.ILoaderVTable32.ConvertToDocumentDelegate ConvertToDocument;

			// Token: 0x02000086 RID: 134
			// (Invoke) Token: 0x06000399 RID: 921
			[UnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate int ReleaseDelegate(IntPtr self);

			// Token: 0x02000087 RID: 135
			// (Invoke) Token: 0x0600039D RID: 925
			[UnmanagedFunctionPointer(CallingConvention.StdCall)]
			public unsafe delegate int AddDataDelegate(IntPtr self, byte* data, int length);

			// Token: 0x02000088 RID: 136
			// (Invoke) Token: 0x060003A1 RID: 929
			[UnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate IntPtr ConvertToDocumentDelegate(IntPtr self);
		}

		// Token: 0x02000063 RID: 99
		public struct ILoaderVTable64
		{
			// Token: 0x04000859 RID: 2137
			public NativeMethods.ILoaderVTable64.ReleaseDelegate Release;

			// Token: 0x0400085A RID: 2138
			public NativeMethods.ILoaderVTable64.AddDataDelegate AddData;

			// Token: 0x0400085B RID: 2139
			public NativeMethods.ILoaderVTable64.ConvertToDocumentDelegate ConvertToDocument;

			// Token: 0x02000089 RID: 137
			// (Invoke) Token: 0x060003A5 RID: 933
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate int ReleaseDelegate(IntPtr self);

			// Token: 0x0200008A RID: 138
			// (Invoke) Token: 0x060003A9 RID: 937
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate int AddDataDelegate(IntPtr self, byte* data, int length);

			// Token: 0x0200008B RID: 139
			// (Invoke) Token: 0x060003AD RID: 941
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate IntPtr ConvertToDocumentDelegate(IntPtr self);
		}

		// Token: 0x02000064 RID: 100
		public struct Sci_CharacterRange
		{
			// Token: 0x0400085C RID: 2140
			public int cpMin;

			// Token: 0x0400085D RID: 2141
			public int cpMax;
		}

		// Token: 0x02000065 RID: 101
		public struct Sci_NotifyHeader
		{
			// Token: 0x0400085E RID: 2142
			public IntPtr hwndFrom;

			// Token: 0x0400085F RID: 2143
			public IntPtr idFrom;

			// Token: 0x04000860 RID: 2144
			public int code;
		}

		// Token: 0x02000066 RID: 102
		public struct Sci_TextRange
		{
			// Token: 0x04000861 RID: 2145
			public NativeMethods.Sci_CharacterRange chrg;

			// Token: 0x04000862 RID: 2146
			public IntPtr lpstrText;
		}

		// Token: 0x02000067 RID: 103
		public struct SCNotification
		{
			// Token: 0x04000863 RID: 2147
			public NativeMethods.Sci_NotifyHeader nmhdr;

			// Token: 0x04000864 RID: 2148
			public int position;

			// Token: 0x04000865 RID: 2149
			public int ch;

			// Token: 0x04000866 RID: 2150
			public int modifiers;

			// Token: 0x04000867 RID: 2151
			public int modificationType;

			// Token: 0x04000868 RID: 2152
			public IntPtr text;

			// Token: 0x04000869 RID: 2153
			public int length;

			// Token: 0x0400086A RID: 2154
			public int linesAdded;

			// Token: 0x0400086B RID: 2155
			public int message;

			// Token: 0x0400086C RID: 2156
			public IntPtr wParam;

			// Token: 0x0400086D RID: 2157
			public IntPtr lParam;

			// Token: 0x0400086E RID: 2158
			public int line;

			// Token: 0x0400086F RID: 2159
			public int foldLevelNow;

			// Token: 0x04000870 RID: 2160
			public int foldLevelPrev;

			// Token: 0x04000871 RID: 2161
			public int margin;

			// Token: 0x04000872 RID: 2162
			public int listType;

			// Token: 0x04000873 RID: 2163
			public int x;

			// Token: 0x04000874 RID: 2164
			public int y;

			// Token: 0x04000875 RID: 2165
			public int token;

			// Token: 0x04000876 RID: 2166
			public int annotationLinesAdded;

			// Token: 0x04000877 RID: 2167
			public int updated;

			// Token: 0x04000878 RID: 2168
			public int listCompletionMethod;
		}
	}
}
