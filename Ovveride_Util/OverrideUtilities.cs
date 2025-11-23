using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace ZahidAGA
{
	// Token: 0x020000AD RID: 173
	public static class OverrideUtilities
	{
		// Token: 0x060002FD RID: 765 RVA: 0x00018680 File Offset: 0x00016880
		public static object CallOriginalFunc(MethodInfo method, object instance = null, params object[] args)
		{
			OverrideManager overrideManager = new OverrideManager();
			if (overrideManager.Overrides.All((KeyValuePair<OverrideAttribute, OverrideWrapper> o) => o.Value.Original != method))
			{
				throw new Exception("The Override specified was not found!");
			}
			return overrideManager.Overrides.First((KeyValuePair<OverrideAttribute, OverrideWrapper> a) => a.Value.Original == method).Value.CallOriginal(args, instance);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x000186E8 File Offset: 0x000168E8
		public static object CallOriginal(object instance = null, params object[] args)
		{
			OverrideManager overrideManager = new OverrideManager();
			StackTrace stackTrace = new StackTrace(false);
			if (stackTrace.FrameCount < 1)
			{
				throw new Exception("Invalid trace back to the original method! Please provide the methodinfo instead!");
			}
			MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();
			MethodInfo original = null;
			if (!Attribute.IsDefined(methodBase, typeof(OverrideAttribute)))
			{
				methodBase = stackTrace.GetFrame(2).GetMethod();
			}
			OverrideAttribute overrideAttribute = (OverrideAttribute)Attribute.GetCustomAttribute(methodBase, typeof(OverrideAttribute));
			if (overrideAttribute == null)
			{
				throw new Exception("This method can only be called from an overwritten method!");
			}
			if (!overrideAttribute.MethodFound)
			{
				throw new Exception("The original method was never found!");
			}
			original = overrideAttribute.Method;
			if (overrideManager.Overrides.All((KeyValuePair<OverrideAttribute, OverrideWrapper> o) => o.Value.Original != original))
			{
				throw new Exception("The Override specified was not found!");
			}
			return overrideManager.Overrides.First((KeyValuePair<OverrideAttribute, OverrideWrapper> a) => a.Value.Original == original).Value.CallOriginal(args, instance);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x000187E8 File Offset: 0x000169E8
		public static bool EnableOverride(MethodInfo method)
		{
			OverrideWrapper value = new OverrideManager().Overrides.First((KeyValuePair<OverrideAttribute, OverrideWrapper> a) => a.Value.Original == method).Value;
			return value != null && value.Override();
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00018834 File Offset: 0x00016A34
		public static bool DisableOverride(MethodInfo method)
		{
			OverrideWrapper value = new OverrideManager().Overrides.First((KeyValuePair<OverrideAttribute, OverrideWrapper> a) => a.Value.Original == method).Value;
			return value != null && value.Revert();
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00018880 File Offset: 0x00016A80
		public unsafe static bool OverrideFunction(IntPtr ptrOriginal, IntPtr ptrModified)
		{
			bool flag;
			try
			{
				int size = IntPtr.Size;
				if (size != 4)
				{
					if (size != 8)
					{
						return false;
					}
					byte* ptr = (byte*)ptrOriginal.ToPointer();
					*ptr = 72;
					ptr[1] = 184;
					*(long*)(ptr + 2) = ptrModified.ToInt64();
					ptr[10] = byte.MaxValue;
					ptr[11] = 224;
				}
				else
				{
					byte* ptr2 = (byte*)ptrOriginal.ToPointer();
					*ptr2 = 104;
					*(int*)(ptr2 + 1) = ptrModified.ToInt32();
					ptr2[5] = 195;
				}
				flag = true;
			}
			catch (Exception)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00018918 File Offset: 0x00016B18
		public unsafe static bool RevertOverride(OverrideUtilities.OffsetBackup backup)
		{
			bool flag;
			try
			{
				byte* ptr = (byte*)backup.Method.ToPointer();
				*ptr = backup.A;
				ptr[1] = backup.B;
				ptr[10] = backup.C;
				ptr[11] = backup.D;
				ptr[12] = backup.E;
				if (IntPtr.Size == 4)
				{
					*(int*)(ptr + 1) = (int)backup.F32;
					ptr[5] = backup.G;
				}
				else
				{
					*(long*)(ptr + 2) = (long)backup.F64;
				}
				flag = true;
			}
			catch (Exception)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x020000AE RID: 174
		public class OffsetBackup
		{
			// Token: 0x06000303 RID: 771 RVA: 0x000189A8 File Offset: 0x00016BA8
			public unsafe OffsetBackup(IntPtr method)
			{
				this.Method = method;
				byte* ptr = (byte*)method.ToPointer();
				this.A = *ptr;
				this.B = ptr[1];
				this.C = ptr[10];
				this.D = ptr[11];
				this.E = ptr[12];
				if (IntPtr.Size == 4)
				{
					this.F32 = *(uint*)(ptr + 1);
					this.G = ptr[5];
					return;
				}
				this.F64 = (ulong)(*(long*)(ptr + 2));
			}

			// Token: 0x040003AC RID: 940
			public IntPtr Method;

			// Token: 0x040003AD RID: 941
			public byte A;

			// Token: 0x040003AE RID: 942
			public byte B;

			// Token: 0x040003AF RID: 943
			public byte C;

			// Token: 0x040003B0 RID: 944
			public byte D;

			// Token: 0x040003B1 RID: 945
			public byte E;

			// Token: 0x040003B2 RID: 946
			public byte G;

			// Token: 0x040003B3 RID: 947
			public ulong F64;

			// Token: 0x040003B4 RID: 948
			public uint F32;
		}
	}
}
