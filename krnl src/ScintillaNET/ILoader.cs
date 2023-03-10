using System;

namespace ScintillaNET
{
	// Token: 0x0200001A RID: 26
	public interface ILoader
	{
		// Token: 0x0600004D RID: 77
		bool AddData(char[] data, int length);

		// Token: 0x0600004E RID: 78
		Document ConvertToDocument();

		// Token: 0x0600004F RID: 79
		int Release();
	}
}
