using System;
using System.Linq;
using System.Reflection;

namespace ZahidAGA
{
	// Token: 0x02000022 RID: 34
	[AttributeUsage(AttributeTargets.Method)]
	public class OverrideAttribute : Attribute
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002319 File Offset: 0x00000519
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002321 File Offset: 0x00000521
		public Type Class { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600005E RID: 94 RVA: 0x0000232A File Offset: 0x0000052A
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00002332 File Offset: 0x00000532
		public string MethodName { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000060 RID: 96 RVA: 0x0000233B File Offset: 0x0000053B
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00002343 File Offset: 0x00000543
		public MethodInfo Method { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000062 RID: 98 RVA: 0x0000234C File Offset: 0x0000054C
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00002354 File Offset: 0x00000554
		public BindingFlags Flags { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000064 RID: 100 RVA: 0x0000235D File Offset: 0x0000055D
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00002365 File Offset: 0x00000565
		public bool MethodFound { get; private set; }

		// Token: 0x06000066 RID: 102 RVA: 0x0000A924 File Offset: 0x00008B24
		public OverrideAttribute(Type tClass, string method, BindingFlags flags, int index = 0)
		{
			this.Class = tClass;
			this.MethodName = method;
			this.Flags = flags;
			try
			{
				this.Method = (from a in this.Class.GetMethods(flags)
					where a.Name == method
					select a).ToArray<MethodInfo>()[index];
				this.MethodFound = true;
			}
			catch (Exception)
			{
				this.MethodFound = false;
			}
		}
	}
}
