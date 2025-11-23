using System;
using System.Collections.Generic;
using System.Linq;
using SDG.Unturned;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x020000B5 RID: 181
	public static class SphereUtilities
	{
        // Token: 0x0600031E RID: 798 RVA: 0x000199A8 File Offset: 0x00017BA8
        public static bool GetRaycast(GameObject Target, Vector3 StartPos, out Vector3 Point)
        {
            Point = Vector3.zero;

            if (Target == null)
                return false;

            int originalLayer = Target.layer;
            Target.layer = 24; // Временное изменение слоя

            try
            {
                RaycastComponent Component = Target.GetComponent<RaycastComponent>();
                if (Component == null || Component.Sphere == null)
                {
                    Target.layer = originalLayer;
                    return false;
                }

                float distanceToTarget = Vector3.Distance(Target.transform.position, StartPos);

                // Исправленная логика для близких целей
                if (distanceToTarget <= 15.5f)
                {
                    Point = Target.transform.position; // ФИКС: позиция цели, а не игрока
                    Target.layer = originalLayer;
                    return true;
                }

                MeshCollider sphereCollider = Component.Sphere.GetComponent<MeshCollider>();
                if (sphereCollider == null || sphereCollider.sharedMesh == null)
                {
                    Target.layer = originalLayer;
                    return false;
                }

                Vector3[] vertices = sphereCollider.sharedMesh.vertices;
                Vector3 targetPos = Target.transform.position;

                // Улучшенный алгоритм проверки точек
                foreach (Vector3 vertex in vertices)
                {
                    Vector3 worldVertex = Component.Sphere.transform.TransformPoint(vertex);

                    // Проверяем, что точка находится на стороне цели от нас
                    Vector3 directionToVertex = (worldVertex - StartPos).normalized;
                    Vector3 directionToTarget = (targetPos - StartPos).normalized;

                    if (Vector3.Dot(directionToVertex, directionToTarget) < 0.3f)
                        continue; // Пропускаем точки позади цели

                    float distance = Vector3.Distance(StartPos, worldVertex);

                    // Используем более либеральную маску для проверки
                    if (!Physics.Raycast(StartPos, directionToVertex, distance, RayMasks.DAMAGE_CLIENT))
                    {
                        Point = worldVertex;
                        Target.layer = originalLayer;
                        return true;
                    }
                }

                // Дополнительная проверка центральной точки
                Vector3 centerDirection = (targetPos - StartPos).normalized;
                float centerDistance = Vector3.Distance(StartPos, targetPos);
                if (!Physics.Raycast(StartPos, centerDirection, centerDistance, RayMasks.DAMAGE_CLIENT))
                {
                    Point = targetPos;
                    Target.layer = originalLayer;
                    return true;
                }
            }
            finally
            {
                Target.layer = originalLayer; // Гарантированное восстановление слоя
            }

            return false;
        }
    }
}
