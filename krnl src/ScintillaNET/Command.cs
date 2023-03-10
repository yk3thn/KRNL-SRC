using System;

namespace ScintillaNET
{
	// Token: 0x0200000A RID: 10
	public enum Command
	{
		// Token: 0x04000020 RID: 32
		Default,
		// Token: 0x04000021 RID: 33
		Null = 2172,
		// Token: 0x04000022 RID: 34
		LineDown = 2300,
		// Token: 0x04000023 RID: 35
		LineDownExtend,
		// Token: 0x04000024 RID: 36
		LineDownRectExtend = 2426,
		// Token: 0x04000025 RID: 37
		LineScrollDown = 2342,
		// Token: 0x04000026 RID: 38
		LineUp = 2302,
		// Token: 0x04000027 RID: 39
		LineUpExtend,
		// Token: 0x04000028 RID: 40
		LineUpRectExtend = 2427,
		// Token: 0x04000029 RID: 41
		LineScrollUp = 2343,
		// Token: 0x0400002A RID: 42
		ParaDown = 2413,
		// Token: 0x0400002B RID: 43
		ParaDownExtend,
		// Token: 0x0400002C RID: 44
		ParaUp,
		// Token: 0x0400002D RID: 45
		ParaUpExtend,
		// Token: 0x0400002E RID: 46
		CharLeft = 2304,
		// Token: 0x0400002F RID: 47
		CharLeftExtend,
		// Token: 0x04000030 RID: 48
		CharLeftRectExtend = 2428,
		// Token: 0x04000031 RID: 49
		CharRight = 2306,
		// Token: 0x04000032 RID: 50
		CharRightExtend,
		// Token: 0x04000033 RID: 51
		CharRightRectExtend = 2429,
		// Token: 0x04000034 RID: 52
		WordLeft = 2308,
		// Token: 0x04000035 RID: 53
		WordLeftExtend,
		// Token: 0x04000036 RID: 54
		WordRight,
		// Token: 0x04000037 RID: 55
		WordRightExtend,
		// Token: 0x04000038 RID: 56
		WordLeftEnd = 2439,
		// Token: 0x04000039 RID: 57
		WordLeftEndExtend,
		// Token: 0x0400003A RID: 58
		WordRightEnd,
		// Token: 0x0400003B RID: 59
		WordRightEndExtend,
		// Token: 0x0400003C RID: 60
		WordPartLeft = 2390,
		// Token: 0x0400003D RID: 61
		WordPartLeftExtend,
		// Token: 0x0400003E RID: 62
		WordPartRight,
		// Token: 0x0400003F RID: 63
		WordPartRightExtend,
		// Token: 0x04000040 RID: 64
		Home = 2312,
		// Token: 0x04000041 RID: 65
		HomeExtend,
		// Token: 0x04000042 RID: 66
		HomeRectExtend = 2430,
		// Token: 0x04000043 RID: 67
		HomeDisplay = 2345,
		// Token: 0x04000044 RID: 68
		HomeDisplayExtend,
		// Token: 0x04000045 RID: 69
		HomeWrap = 2349,
		// Token: 0x04000046 RID: 70
		HomeWrapExtend = 2450,
		// Token: 0x04000047 RID: 71
		VcHome = 2331,
		// Token: 0x04000048 RID: 72
		VcHomeExtend,
		// Token: 0x04000049 RID: 73
		VcHomeRectExtend = 2431,
		// Token: 0x0400004A RID: 74
		VcHomeWrap = 2453,
		// Token: 0x0400004B RID: 75
		VcHomeWrapExtend,
		// Token: 0x0400004C RID: 76
		VcHomeDisplay = 2652,
		// Token: 0x0400004D RID: 77
		VcHomeDisplayExtend,
		// Token: 0x0400004E RID: 78
		LineEnd = 2314,
		// Token: 0x0400004F RID: 79
		LineEndExtend,
		// Token: 0x04000050 RID: 80
		LineEndRectExtend = 2432,
		// Token: 0x04000051 RID: 81
		LineEndDisplay = 2347,
		// Token: 0x04000052 RID: 82
		LineEndDisplayExtend,
		// Token: 0x04000053 RID: 83
		LineEndWrap = 2451,
		// Token: 0x04000054 RID: 84
		LineEndWrapExtend,
		// Token: 0x04000055 RID: 85
		DocumentStart = 2316,
		// Token: 0x04000056 RID: 86
		DocumentStartExtend,
		// Token: 0x04000057 RID: 87
		DocumentEnd,
		// Token: 0x04000058 RID: 88
		DocumentEndExtend,
		// Token: 0x04000059 RID: 89
		PageUp,
		// Token: 0x0400005A RID: 90
		PageUpExtend,
		// Token: 0x0400005B RID: 91
		PageUpRectExtend = 2433,
		// Token: 0x0400005C RID: 92
		PageDown = 2322,
		// Token: 0x0400005D RID: 93
		PageDownExtend,
		// Token: 0x0400005E RID: 94
		PageDownRectExtend = 2434,
		// Token: 0x0400005F RID: 95
		StutteredPageUp,
		// Token: 0x04000060 RID: 96
		StutteredPageUpExtend,
		// Token: 0x04000061 RID: 97
		StutteredPageDown,
		// Token: 0x04000062 RID: 98
		StutteredPageDownExtend,
		// Token: 0x04000063 RID: 99
		DeleteBack = 2326,
		// Token: 0x04000064 RID: 100
		DeleteBackNotLine = 2344,
		// Token: 0x04000065 RID: 101
		DelWordLeft = 2335,
		// Token: 0x04000066 RID: 102
		DelWordRight,
		// Token: 0x04000067 RID: 103
		DelWordRightEnd = 2518,
		// Token: 0x04000068 RID: 104
		DelLineLeft = 2395,
		// Token: 0x04000069 RID: 105
		DelLineRight,
		// Token: 0x0400006A RID: 106
		LineDelete = 2338,
		// Token: 0x0400006B RID: 107
		LineCut = 2337,
		// Token: 0x0400006C RID: 108
		LineCopy = 2455,
		// Token: 0x0400006D RID: 109
		LineTranspose = 2339,
		// Token: 0x0400006E RID: 110
		LineDuplicate = 2404,
		// Token: 0x0400006F RID: 111
		Lowercase = 2340,
		// Token: 0x04000070 RID: 112
		Uppercase,
		// Token: 0x04000071 RID: 113
		Cancel = 2325,
		// Token: 0x04000072 RID: 114
		EditToggleOvertype = 2324,
		// Token: 0x04000073 RID: 115
		NewLine = 2329,
		// Token: 0x04000074 RID: 116
		FormFeed,
		// Token: 0x04000075 RID: 117
		Tab = 2327,
		// Token: 0x04000076 RID: 118
		BackTab,
		// Token: 0x04000077 RID: 119
		SelectionDuplicate = 2469,
		// Token: 0x04000078 RID: 120
		VerticalCenterCaret = 2619,
		// Token: 0x04000079 RID: 121
		MoveSelectedLinesUp,
		// Token: 0x0400007A RID: 122
		MoveSelectedLinesDown,
		// Token: 0x0400007B RID: 123
		ScrollToStart = 2628,
		// Token: 0x0400007C RID: 124
		ScrollToEnd,
		// Token: 0x0400007D RID: 125
		ZoomIn = 2333,
		// Token: 0x0400007E RID: 126
		ZoomOut,
		// Token: 0x0400007F RID: 127
		Undo = 2176,
		// Token: 0x04000080 RID: 128
		Redo = 2011,
		// Token: 0x04000081 RID: 129
		SwapMainAnchorCaret = 2607,
		// Token: 0x04000082 RID: 130
		RotateSelection = 2606,
		// Token: 0x04000083 RID: 131
		MultipleSelectAddNext = 2688,
		// Token: 0x04000084 RID: 132
		MultipleSelectAddEach,
		// Token: 0x04000085 RID: 133
		SelectAll = 2013
	}
}
