using System;

namespace ScintillaNET
{
	// Token: 0x02000013 RID: 19
	[Flags]
	public enum FoldFlags
	{
		// Token: 0x040000A8 RID: 168
		LineBeforeExpanded = 2,
		// Token: 0x040000A9 RID: 169
		LineBeforeContracted = 4,
		// Token: 0x040000AA RID: 170
		LineAfterExpanded = 8,
		// Token: 0x040000AB RID: 171
		LineAfterContracted = 16,
		// Token: 0x040000AC RID: 172
		LevelNumbers = 64,
		// Token: 0x040000AD RID: 173
		LineState = 128
	}
}
