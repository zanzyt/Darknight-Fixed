using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ZahidAGA
{
	// Token: 0x02000061 RID: 97
	public class OverrideManager
	{
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000398A File Offset: 0x00001B8A
		public Dictionary<OverrideAttribute, OverrideWrapper> Overrides
		{
			get
			{
				return OverrideManager._overrides;
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00013668 File Offset: 0x00011868
		public void OffHook()
		{
			foreach (OverrideWrapper overrideWrapper in this.Overrides.Values)
			{
				overrideWrapper.Revert();
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000136C0 File Offset: 0x000118C0
		public void LoadOverride(MethodInfo method)
		{
			try
			{
				OverrideAttribute attribute = (OverrideAttribute)Attribute.GetCustomAttribute(method, typeof(OverrideAttribute));
				if (this.Overrides.Count((KeyValuePair<OverrideAttribute, OverrideWrapper> a) => a.Key.Method == attribute.Method) <= 0)
				{
					OverrideWrapper overrideWrapper = new OverrideWrapper(attribute.Method, method, attribute, null);
					overrideWrapper.Override();
					this.Overrides.Add(attribute, overrideWrapper);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00013754 File Offset: 0x00011954
		public void InitHook()
		{
			Type[] types = Assembly.GetExecutingAssembly().GetTypes();
			for (int i = 0; i < types.Length; i++)
			{
				foreach (MethodInfo methodInfo in types[i].GetMethods())
				{
					if (methodInfo.Name == "OV_GetKey" && methodInfo.IsDefined(typeof(OverrideAttribute), false))
					{
						this.LoadOverride(methodInfo);
					}
				}
			}
		}

		// Token: 0x040001B5 RID: 437
		public static Dictionary<OverrideAttribute, OverrideWrapper> _overrides = new Dictionary<OverrideAttribute, OverrideWrapper>();
	}
}
