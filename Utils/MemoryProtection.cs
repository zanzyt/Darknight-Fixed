using System;

namespace ZahidAGA
{
	// Token: 0x02000071 RID: 113
	[Flags]
	public enum MemoryProtection : uint
	{
		// Token: 0x04000212 RID: 530
		EXECUTE = 16U,
		// Token: 0x04000213 RID: 531
		EXECUTE_READ = 32U,
		// Token: 0x04000214 RID: 532
		EXECUTE_READWRITE = 64U,
		// Token: 0x04000215 RID: 533
		EXECUTE_WRITECOPY = 128U,
		// Token: 0x04000216 RID: 534
		NOACCESS = 1U,
		// Token: 0x04000217 RID: 535
		READONLY = 2U,
		// Token: 0x04000218 RID: 536
		READWRITE = 4U,
		// Token: 0x04000219 RID: 537
		WRITECOPY = 8U,
		// Token: 0x0400021A RID: 538
		GUARD_Modifierflag = 256U,
		// Token: 0x0400021B RID: 539
		NOCACHE_Modifierflag = 512U,
		// Token: 0x0400021C RID: 540
		WRITECOMBINE_Modifierflag = 1024U
	}
}
