using System;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x020000AB RID: 171
	public class VertTriList
	{
		// Token: 0x170000C0 RID: 192
		public int[] this[int index]
		{
			get
			{
				return this.list[index];
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00004101 File Offset: 0x00002301
		public VertTriList(int[] tri, int numVerts)
		{
			this.Init(tri, numVerts);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00004111 File Offset: 0x00002311
		public VertTriList(Mesh mesh)
		{
			this.Init(mesh.triangles, mesh.vertexCount);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x000183F4 File Offset: 0x000165F4
		public void Init(int[] tri, int numVerts)
		{
			int[] array = new int[numVerts];
			for (int i = 0; i < tri.Length; i++)
			{
				array[tri[i]]++;
			}
			this.list = new int[numVerts][];
			for (int j = 0; j < array.Length; j++)
			{
				this.list[j] = new int[array[j]];
			}
			for (int k = 0; k < tri.Length; k++)
			{
				int num = tri[k];
				int[] array2 = this.list[num];
				int[] array3 = array;
				int num2 = num;
				int num3 = array3[num2] - 1;
				array3[num2] = num3;
				array2[num3] = k / 3;
			}
		}

		// Token: 0x040003AB RID: 939
		public int[][] list;
	}
}
