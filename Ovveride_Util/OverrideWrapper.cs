using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ZahidAGA
{
	// Token: 0x020000C2 RID: 194
	public class OverrideWrapper
	{
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000440E File Offset: 0x0000260E
		// (set) Token: 0x0600033E RID: 830 RVA: 0x00004416 File Offset: 0x00002616
		public MethodInfo Original { get; set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000441F File Offset: 0x0000261F
		// (set) Token: 0x06000340 RID: 832 RVA: 0x00004427 File Offset: 0x00002627
		public MethodInfo Modified { get; set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000341 RID: 833 RVA: 0x00004430 File Offset: 0x00002630
		// (set) Token: 0x06000342 RID: 834 RVA: 0x00004438 File Offset: 0x00002638
		public IntPtr PtrOriginal { get; private set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00004441 File Offset: 0x00002641
		// (set) Token: 0x06000344 RID: 836 RVA: 0x00004449 File Offset: 0x00002649
		public IntPtr PtrModified { get; private set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000345 RID: 837 RVA: 0x00004452 File Offset: 0x00002652
		// (set) Token: 0x06000346 RID: 838 RVA: 0x0000445A File Offset: 0x0000265A
		public OverrideUtilities.OffsetBackup OffsetBackup { get; private set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00004463 File Offset: 0x00002663
		// (set) Token: 0x06000348 RID: 840 RVA: 0x0000446B File Offset: 0x0000266B
		public OverrideAttribute Attribute { get; set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000349 RID: 841 RVA: 0x00004474 File Offset: 0x00002674
		// (set) Token: 0x0600034A RID: 842 RVA: 0x0000447C File Offset: 0x0000267C
		public bool Detoured { get; private set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600034B RID: 843 RVA: 0x00004485 File Offset: 0x00002685
		public object Instance { get; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0000448D File Offset: 0x0000268D
		// (set) Token: 0x0600034D RID: 845 RVA: 0x00004495 File Offset: 0x00002695
		public bool Local { get; private set; }

		// Token: 0x0600034E RID: 846 RVA: 0x00019CCC File Offset: 0x00017ECC
		public OverrideWrapper(MethodInfo original, MethodInfo modified, OverrideAttribute attribute, object instance = null)
		{
			try
			{
				this.Original = original;
				this.Modified = modified;
				this.Instance = instance;
				this.Attribute = attribute;
				this.Local = this.Modified.DeclaringType.Assembly == Assembly.GetExecutingAssembly();
				RuntimeHelpers.PrepareMethod(original.MethodHandle);
				RuntimeHelpers.PrepareMethod(modified.MethodHandle);
				this.PtrOriginal = this.Original.MethodHandle.GetFunctionPointer();
				this.PtrModified = this.Modified.MethodHandle.GetFunctionPointer();
				this.OffsetBackup = new OverrideUtilities.OffsetBackup(this.PtrOriginal);
				this.Detoured = false;
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00019D94 File Offset: 0x00017F94
		public bool Override()
		{
			bool flag;
			if (this.Detoured)
			{
				flag = true;
			}
			else
			{
				bool flag2 = OverrideUtilities.OverrideFunction(this.PtrOriginal, this.PtrModified);
				if (flag2)
				{
					this.Detoured = true;
				}
				flag = flag2;
			}
			return flag;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00019DCC File Offset: 0x00017FCC
		public bool Revert()
		{
			bool flag;
			if (!this.Detoured)
			{
				flag = false;
			}
			else
			{
				bool flag2 = OverrideUtilities.RevertOverride(this.OffsetBackup);
				if (flag2)
				{
					this.Detoured = false;
				}
				flag = flag2;
			}
			return flag;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000449E File Offset: 0x0000269E
		public object CallOriginal(object[] args, object instance = null)
		{
			this.Revert();
			object obj = this.Original.Invoke(instance ?? this.Instance, args);
			this.Override();
			return obj;
		}
	}
}
