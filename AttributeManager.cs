using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using DarkNight;

namespace ZahidAGA
{
	// Token: 0x0200005D RID: 93
	public static class AttributeManager
	{
		// Token: 0x0600022F RID: 559 RVA: 0x00013168 File Offset: 0x00011368
		public static void Init()
		{
			List<Type> list = new List<Type>();
			List<MethodInfo> list2 = new List<MethodInfo>();
			List<MethodInfo> list3 = new List<MethodInfo>();
			foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
			{
				if (type.IsDefined(typeof(ComponentAttribute), false))
				{
					Loader.oko.AddComponent(type);
				}
				if (type.IsDefined(typeof(SpyComponentAttribute), false))
				{
					list.Add(type);
				}
				MethodInfo[] methods = type.GetMethods(ReflectionVariables.Everything);
				for (int j = 0; j < methods.Length; j++)
				{
					MethodInfo M = methods[j];
					if (M.IsDefined(typeof(InitializerAttribute), false))
					{
						M.Invoke(null, null);
					}
					if (M.IsDefined(typeof(OverrideAttribute), false))
					{
						new OverrideManager().LoadOverride(M);
					}
					if (M.IsDefined(typeof(OnSpyAttribute), false))
					{
						list2.Add(M);
					}
					if (M.IsDefined(typeof(OffSpyAttribute), false))
					{
						list3.Add(M);
					}
                    if (M.IsDefined(typeof(ThreadAttribute), false))
                    {
                        Thread t = new Thread(new ThreadStart(() =>
                        {
                            try
                            {
                                M.Invoke(null, null);
                            }
                            catch { }
                        }));
                        t.Start();
                    }

                }
            }
			SpyManager.Components = list;
			SpyManager.PostSpy = list3;
			SpyManager.PreSpy = list2;
		}
	}
}
