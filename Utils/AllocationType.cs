using System;

namespace ZahidAGA
{
	// Token: 0x0200006E RID: 110
	[Flags]
	public enum AllocationType : uint
	{
		// Token: 0x040001F6 RID: 502
		COMMIT = 4096U,
		// Token: 0x040001F7 RID: 503
		RESERVE = 8192U,
		// Token: 0x040001F8 RID: 504
		RESET = 524288U,
		// Token: 0x040001F9 RID: 505
		LARGE_PAGES = 536870912U,
		// Token: 0x040001FA RID: 506
		PHYSICAL = 4194304U,
		// Token: 0x040001FB RID: 507
		TOP_DOWN = 1048576U,
		// Token: 0x040001FC RID: 508
		WRITE_WATCH = 2097152U
	}
}
