using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SDG.Unturned;
using UnityEngine;

namespace ZahidAGA
{
    // Token: 0x02000044 RID: 68
    public static class ESPCoroutines
    {
        // Token: 0x060001A0 RID: 416 RVA: 0x00003535 File Offset: 0x00001735
        public static IEnumerator DoChams()
        {
            for (; ; )
            { //To get turkish aura.. 
                if (!DrawUtilities.ShouldRun() || G.BeingSpied || AssetUtilities.Shaders["chamsLit"] == null)
                {
                    yield return new WaitForSeconds(1f);
                }
                else
                {
                    try
                    {
                        if (ESPOptions.ChamsEnabled)
                        {
                            ESPCoroutines.EnableChams();
                        }
                        else if (ESPOptions.Ignore)
                        {
                            ESPCoroutines.EnableXRay();
                        }
                        else
                        {
                            ESPCoroutines.DisableChams();
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.LogException(ex);
                    }
                    yield return new WaitForSeconds(1f);
                }
            }
            yield break;
        }

        // Token: 0x060001A1 RID: 417 RVA: 0x000107A4 File Offset: 0x0000E9A4
        public static void DoChamsGameObject(GameObject pgo, Color32 front, Color32 behind)
        {
            if (!(AssetUtilities.Shaders["chamsLit"] == null))
            {
                Renderer[] componentsInChildren = pgo.GetComponentsInChildren<Renderer>();
                for (int i = 0; i < componentsInChildren.Length; i++)
                {
                    if ((componentsInChildren[i].material.shader != AssetUtilities.Shaders["chamsLit"]) | AssetUtilities.Shaders["Chams"])
                    {
                        Material[] materials = componentsInChildren[i].materials;
                        for (int j = 0; j < materials.Length; j++)
                        {
                            materials[j].shader = (ESPOptions.ChamsFlat ? AssetUtilities.Shaders["Chams"] : AssetUtilities.Shaders["chamsLit"]);
                            materials[j].SetColor("_ColorVisible", new Color32(front.r, front.g, front.b, front.a));
                            materials[j].SetColor("_ColorBehind", new Color32(behind.r, behind.g, behind.b, behind.a));
                        }
                    }
                }
            }
        }

        // Token: 0x060001A2 RID: 418 RVA: 0x000108D0 File Offset: 0x0000EAD0
        public static void SeeThrough(GameObject pgo)
        {
            if (AssetUtilities.Shaders["SeethroughShader"] == null)
            {
                return;
            }
            Renderer[] componentsInChildren = pgo.GetComponentsInChildren<Renderer>();
            for (int i = 0; i < componentsInChildren.Length; i++)
            {
                if (componentsInChildren[i].material.shader != AssetUtilities.Shaders["SeethroughShader"])
                {
                    Material[] materials = componentsInChildren[i].materials;
                    for (int j = 0; j < materials.Length; j++)
                    {
                        materials[j].shader = AssetUtilities.Shaders["SeethroughShader"];
                    }
                }
            }
        }

        // Token: 0x060001A3 RID: 419 RVA: 0x0001095C File Offset: 0x0000EB5C
        [OffSpy]
        public static void EnableChams()
        {
            if (!ESPOptions.ChamsFlat)
            {
                return;
            }
            Color32 color = Color.blue;
            Color32 color2 = Color.magenta;
            Color32 color3 = Color.cyan;
            Color32 color4 = Color.green;
            foreach (SteamPlayer steamPlayer in Provider.clients.ToArray())
            {
                Color32 color5 = (FriendUtilities.IsFriendly(steamPlayer.player) ? color : color3);
                Color32 color6 = (FriendUtilities.IsFriendly(steamPlayer.player) ? color2 : color4);
                Player player = steamPlayer.player;
                if (!(player == null) && !(player == OptimizationVariables.MainPlayer) && !(player.gameObject == null) && !(player.life == null) && !player.life.isDead)
                {
                    ESPCoroutines.DoChamsGameObject(player.gameObject, color5, color6);
                }
            }
        }

        // Token: 0x060001A4 RID: 420 RVA: 0x00010A48 File Offset: 0x0000EC48
        [OffSpy]
        public static void EnableXRay()
        {
            if (!ESPOptions.Ignore)
            {
                return;
            }
            SteamPlayer[] array = Provider.clients.ToArray();
            for (int i = 0; i < array.Length; i++)
            {
                Player player = array[i].player;
                if (!(player == null) && !(player == OptimizationVariables.MainPlayer) && !(player.gameObject == null) && !(player.life == null) && !player.life.isDead)
                {
                    ESPCoroutines.SeeThrough(player.gameObject);
                }
            }
        }

        // Token: 0x060001A5 RID: 421 RVA: 0x00010ACC File Offset: 0x0000ECCC
        [OnSpy]
        public static void DisableChams()
        {
            try
            {
                if (!(Shader.Find("Standard/Clothes") == null))
                {
                    for (int i = 0; i < Provider.clients.ToArray().Length; i++)
                    {
                        Player player = Provider.clients.ToArray()[i].player;
                        if (!(player == null) && !(player == OptimizationVariables.MainPlayer) && !(player.life == null) && !player.life.isDead)
                        {
                            Renderer[] componentsInChildren = player.gameObject.GetComponentsInChildren<Renderer>();
                            for (int j = 0; j < componentsInChildren.Length; j++)
                            {
                                Material[] materials = componentsInChildren[j].materials;
                                for (int k = 0; k < materials.Length; k++)
                                {
                                    if (materials[k].shader != Shader.Find("Standard/Clothes"))
                                    {
                                        if (materials[k].name.Contains("Clothes"))
                                        {
                                            materials[k].shader = Shader.Find("Standard/Clothes");
                                        }
                                        else
                                        {
                                            materials[k].shader = Shader.Find("Standard");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Log("RetChams   " + ex.Message);
            }
        }

        // Token: 0x060001A6 RID: 422 RVA: 0x0000353D File Offset: 0x0000173D
        public static IEnumerator UpdateObjectList()
        {
            for (; ; )
            {
                if (!DrawUtilities.ShouldRun())
                {
                    yield return new WaitForSeconds(0f);
                }
                else
                {
                    List<ESPObject> objects = ESPVariables.Objects;
                    objects.Clear();
                    List<ESPTarget> list = ESPOptions.PriorityTable.Keys.OrderByDescending((ESPTarget k) => ESPOptions.PriorityTable[k]).ToList<ESPTarget>();
                    int num;
                    for (int i = 0; i < list.Count; i = num + 1)
                    {
                        ESPTarget esptarget = list[i];
                        ESPVisual espvisual = ESPOptions.VisualOptions[(int)esptarget];
                        if (espvisual.Enabled)
                        {
                            Vector2 pPos = OptimizationVariables.MainPlayer.transform.position;
                            switch (esptarget)
                            {
                                case ESPTarget.Player:
                                    {
                                        SteamPlayer[] array = Provider.clients.OrderByDescending((SteamPlayer p) => VectorUtilities.GetDistance(pPos, p.player.transform.position)).ToArray<SteamPlayer>();
                                        if (espvisual.UseObjectCap)
                                        {
                                            array = array.TakeLast(espvisual.ObjectCap).ToArray<SteamPlayer>();
                                        }
                                        for (int j = 0; j < array.Length; j = num + 1)
                                        {
                                            Player player = array[j].player;
                                            if (!player.life.isDead && !(player == OptimizationVariables.MainPlayer))
                                            {
                                                objects.Add(new ESPObject(esptarget, player, player.gameObject));
                                            }
                                            num = j;
                                        }
                                        break;
                                    }
                                case ESPTarget.Zombie:
                                    {
                                        Zombie[] array2 = (from obj in ZombieManager.regions.SelectMany((ZombieRegion r) => r.zombies)
                                                           orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
                                                           select obj).ToArray<Zombie>();
                                        if (espvisual.UseObjectCap)
                                        {
                                            array2 = array2.TakeLast(espvisual.ObjectCap).ToArray<Zombie>();
                                        }
                                        for (int k2 = 0; k2 < array2.Length; k2 = num + 1)
                                        {
                                            Zombie zombie = array2[k2];
                                            objects.Add(new ESPObject(esptarget, zombie, zombie.gameObject));
                                            num = k2;
                                        }
                                        break;
                                    }
                                case ESPTarget.Item:
                                    {
                                        InteractableItem[] array3 = (from obj in global::UnityEngine.Object.FindObjectsOfType<InteractableItem>()
                                                                     orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
                                                                     select obj).ToArray<InteractableItem>();
                                        if (espvisual.UseObjectCap)
                                        {
                                            array3 = array3.TakeLast(espvisual.ObjectCap).ToArray<InteractableItem>();
                                        }
                                        for (int l = 0; l < array3.Length; l = num + 1)
                                        {
                                            InteractableItem interactableItem = array3[l];
                                            if (ItemUtilities.Whitelisted(interactableItem.asset, ItemOptions.ItemESPOptions) || !ESPOptions.FilterItems)
                                            {
                                                objects.Add(new ESPObject(esptarget, interactableItem, interactableItem.gameObject));
                                            }
                                            num = l;
                                        }
                                        break;
                                    }
                                case ESPTarget.Sentry:
                                    {
                                        InteractableSentry[] array4 = (from obj in global::UnityEngine.Object.FindObjectsOfType<InteractableSentry>()
                                                                       orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
                                                                       select obj).ToArray<InteractableSentry>();
                                        if (espvisual.UseObjectCap)
                                        {
                                            array4 = array4.TakeLast(espvisual.ObjectCap).ToArray<InteractableSentry>();
                                        }
                                        for (int m = 0; m < array4.Length; m = num + 1)
                                        {
                                            InteractableSentry interactableSentry = array4[m];
                                            objects.Add(new ESPObject(esptarget, interactableSentry, interactableSentry.gameObject));
                                            num = m;
                                        }
                                        break;
                                    }
                                case ESPTarget.Bed:
                                    {
                                        InteractableBed[] array5 = (from obj in global::UnityEngine.Object.FindObjectsOfType<InteractableBed>()
                                                                    orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
                                                                    select obj).ToArray<InteractableBed>();
                                        if (espvisual.UseObjectCap)
                                        {
                                            array5 = array5.TakeLast(espvisual.ObjectCap).ToArray<InteractableBed>();
                                        }
                                        for (int n = 0; n < array5.Length; n = num + 1)
                                        {
                                            InteractableBed interactableBed = array5[n];
                                            objects.Add(new ESPObject(esptarget, interactableBed, interactableBed.gameObject));
                                            num = n;
                                        }
                                        break;
                                    }
                                case ESPTarget.Flag:
                                    {
                                        InteractableClaim[] array6 = (from obj in global::UnityEngine.Object.FindObjectsOfType<InteractableClaim>()
                                                                      orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
                                                                      select obj).ToArray<InteractableClaim>();
                                        if (espvisual.UseObjectCap)
                                        {
                                            array6 = array6.TakeLast(espvisual.ObjectCap).ToArray<InteractableClaim>();
                                        }
                                        for (int num2 = 0; num2 < array6.Length; num2 = num + 1)
                                        {
                                            InteractableClaim interactableClaim = array6[num2];
                                            objects.Add(new ESPObject(esptarget, interactableClaim, interactableClaim.gameObject));
                                            num = num2;
                                        }
                                        break;
                                    }
                                case ESPTarget.Vehicle:
                                    {
                                        InteractableVehicle[] array7 = (from obj in global::UnityEngine.Object.FindObjectsOfType<InteractableVehicle>()
                                                                        orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
                                                                        select obj).ToArray<InteractableVehicle>();
                                        if (espvisual.UseObjectCap)
                                        {
                                            array7 = array7.TakeLast(espvisual.ObjectCap).ToArray<InteractableVehicle>();
                                        }
                                        for (int num3 = 0; num3 < array7.Length; num3 = num + 1)
                                        {
                                            InteractableVehicle interactableVehicle = array7[num3];
                                            if (!interactableVehicle.isDead)
                                            {
                                                objects.Add(new ESPObject(esptarget, interactableVehicle, interactableVehicle.gameObject));
                                            }
                                            num = num3;
                                        }
                                        break;
                                    }
                                case ESPTarget.Storage:
                                    {
                                        InteractableStorage[] array8 = (from obj in global::UnityEngine.Object.FindObjectsOfType<InteractableStorage>()
                                                                        orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
                                                                        select obj).ToArray<InteractableStorage>();
                                        if (espvisual.UseObjectCap)
                                        {
                                            array8 = array8.TakeLast(espvisual.ObjectCap).ToArray<InteractableStorage>();
                                        }
                                        for (int num4 = 0; num4 < array8.Length; num4 = num + 1)
                                        {
                                            InteractableStorage interactableStorage = array8[num4];
                                            objects.Add(new ESPObject(esptarget, interactableStorage, interactableStorage.gameObject));
                                            num = num4;
                                        }
                                        break;
                                    }
                                case ESPTarget.Airdrop:
                                    {
                                        Carepackage[] array9 = (from obj in global::UnityEngine.Object.FindObjectsOfType<Carepackage>()
                                                                orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
                                                                select obj).ToArray<Carepackage>();
                                        if (espvisual.UseObjectCap)
                                        {
                                            array9 = array9.TakeLast(espvisual.ObjectCap).ToArray<Carepackage>();
                                        }
                                        for (int num5 = 0; num5 < array9.Length; num5 = num + 1)
                                        {
                                            Carepackage carepackage = array9[num5];
                                            objects.Add(new ESPObject(esptarget, carepackage, carepackage.gameObject));
                                            num = num5;
                                        }
                                        break;
                                    }
                            }
                        }
                        num = i;
                    }
                    yield return new WaitForSeconds(0f);
                }
            }
            yield break;
        }
    }
}
