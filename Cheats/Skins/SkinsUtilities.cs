using System;
using System.Collections.Generic;
using SDG.Provider;
using SDG.Unturned;
using Steamworks;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x020000B4 RID: 180
	public static class SkinsUtilities
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000314 RID: 788 RVA: 0x000041D7 File Offset: 0x000023D7
		public static HumanClothes CharacterClothes
		{
			get
			{
				return OptimizationVariables.MainPlayer.clothing.characterClothes;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000315 RID: 789 RVA: 0x000041E8 File Offset: 0x000023E8
		public static HumanClothes FirstClothes
		{
			get
			{
				return OptimizationVariables.MainPlayer.clothing.firstClothes;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000316 RID: 790 RVA: 0x000041F9 File Offset: 0x000023F9
		public static HumanClothes ThirdClothes
		{
			get
			{
				return OptimizationVariables.MainPlayer.clothing.thirdClothes;
			}
		}

        internal static class EconReflection
        {
            private static Dictionary<int, UnturnedEconInfo> cache;

            public static Dictionary<int, UnturnedEconInfo> EconInfo
            {
                get
                {
                    if (cache != null)
                        return cache;

                    var prop = typeof(TempSteamworksEconomy)
                        .GetProperty("econInfo", System.Reflection.BindingFlags.Static |
                                                     System.Reflection.BindingFlags.NonPublic |
                                                     System.Reflection.BindingFlags.Public);

                    cache = (Dictionary<int, UnturnedEconInfo>)prop.GetValue(null);
                    return cache;
                }
            }
        }


        // Token: 0x06000317 RID: 791 RVA: 0x0001901C File Offset: 0x0001721C
        public static void Apply(Skin skin, SkinType skinType)
        {
            if (skinType == SkinType.Weapons)
            {
                Dictionary<ushort, int> itemSkins = OptimizationVariables.MainPlayer.channel.owner.itemSkins;
                if (itemSkins == null)
                {
                    return;
                }

                // Для оружия используем itemdefid из econInfo
                ushort inventoryItemID = (ushort)skin.ID;
                SkinOptions.SkinConfig.WeaponSkins.Clear();

                if (itemSkins.ContainsKey(inventoryItemID))
                {
                    itemSkins[inventoryItemID] = skin.ID;
                }
                else
                {
                    itemSkins.Add(inventoryItemID, skin.ID);
                }

                OptimizationVariables.MainPlayer.equipment.applySkinVisual();
                OptimizationVariables.MainPlayer.equipment.applyMythicVisual();

                foreach (var keyValuePair in itemSkins)
                {
                    SkinOptions.SkinConfig.WeaponSkins.Add(new WeaponSave(keyValuePair.Key, keyValuePair.Value));
                }
            }
            else
            {
                // Для одежды используем ID ассета напрямую
                SkinsUtilities.ApplyClothing(skin, skinType);
            }
        }

        // Token: 0x06000318 RID: 792 RVA: 0x0001911C File Offset: 0x0001731C
        public static void ApplyClothing(Skin skin, SkinType type)
		{
			if (!G.BeingSpied)
			{
				switch (type)
				{
				case SkinType.Shirts:
					SkinsUtilities.CharacterClothes.visualShirt = skin.ID;
					SkinsUtilities.FirstClothes.visualShirt = skin.ID;
					SkinsUtilities.ThirdClothes.visualShirt = skin.ID;
					SkinOptions.SkinConfig.ShirtID = skin.ID;
					break;
				case SkinType.Pants:
					SkinsUtilities.CharacterClothes.visualPants = skin.ID;
					SkinsUtilities.FirstClothes.visualPants = skin.ID;
					SkinsUtilities.ThirdClothes.visualPants = skin.ID;
					SkinOptions.SkinConfig.PantsID = skin.ID;
					break;
				case SkinType.Bags:
					SkinsUtilities.CharacterClothes.visualBackpack = skin.ID;
					SkinsUtilities.FirstClothes.visualBackpack = skin.ID;
					SkinsUtilities.ThirdClothes.visualBackpack = skin.ID;
					SkinOptions.SkinConfig.BackpackID = skin.ID;
					break;
				case SkinType.Vests:
					SkinsUtilities.CharacterClothes.visualVest = skin.ID;
					SkinsUtilities.FirstClothes.visualVest = skin.ID;
					SkinsUtilities.ThirdClothes.visualVest = skin.ID;
					SkinOptions.SkinConfig.VestID = skin.ID;
					break;
				case SkinType.Hats:
					SkinsUtilities.CharacterClothes.visualHat = skin.ID;
					SkinsUtilities.FirstClothes.visualHat = skin.ID;
					SkinsUtilities.ThirdClothes.visualHat = skin.ID;
					SkinOptions.SkinConfig.HatID = skin.ID;
					break;
				case SkinType.Masks:
					SkinsUtilities.CharacterClothes.visualMask = skin.ID;
					SkinsUtilities.FirstClothes.visualMask = skin.ID;
					SkinsUtilities.ThirdClothes.visualMask = skin.ID;
					SkinOptions.SkinConfig.MaskID = skin.ID;
					break;
				case SkinType.Glasses:
					SkinsUtilities.CharacterClothes.visualGlasses = skin.ID;
					SkinsUtilities.FirstClothes.visualGlasses = skin.ID;
					SkinsUtilities.ThirdClothes.visualGlasses = skin.ID;
					SkinOptions.SkinConfig.GlassesID = skin.ID;
					break;
				}
				SkinsUtilities.CharacterClothes.apply();
				SkinsUtilities.FirstClothes.apply();
				SkinsUtilities.ThirdClothes.apply();
			}
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00019358 File Offset: 0x00017558
		public static void ApplyFromConfig()
		{
			Dictionary<ushort, int> dictionary = new Dictionary<ushort, int>();
			foreach (WeaponSave weaponSave in SkinOptions.SkinConfig.WeaponSkins)
			{
				dictionary[weaponSave.WeaponID] = weaponSave.SkinID;
			}
			if (!(OptimizationVariables.MainPlayer == null))
			{
				OptimizationVariables.MainPlayer.channel.owner.itemSkins = dictionary;
				if (SkinOptions.SkinConfig.ShirtID != 0)
				{
					SkinsUtilities.CharacterClothes.visualShirt = SkinOptions.SkinConfig.ShirtID;
					SkinsUtilities.FirstClothes.visualShirt = SkinOptions.SkinConfig.ShirtID;
					SkinsUtilities.ThirdClothes.visualShirt = SkinOptions.SkinConfig.ShirtID;
				}
				if (SkinOptions.SkinConfig.PantsID != 0)
				{
					SkinsUtilities.CharacterClothes.visualPants = SkinOptions.SkinConfig.PantsID;
					SkinsUtilities.FirstClothes.visualPants = SkinOptions.SkinConfig.PantsID;
					SkinsUtilities.ThirdClothes.visualPants = SkinOptions.SkinConfig.PantsID;
				}
				if (SkinOptions.SkinConfig.BackpackID != 0)
				{
					SkinsUtilities.CharacterClothes.visualBackpack = SkinOptions.SkinConfig.BackpackID;
					SkinsUtilities.FirstClothes.visualBackpack = SkinOptions.SkinConfig.BackpackID;
					SkinsUtilities.ThirdClothes.visualBackpack = SkinOptions.SkinConfig.BackpackID;
				}
				if (SkinOptions.SkinConfig.VestID != 0)
				{
					SkinsUtilities.CharacterClothes.visualVest = SkinOptions.SkinConfig.VestID;
					SkinsUtilities.FirstClothes.visualVest = SkinOptions.SkinConfig.VestID;
					SkinsUtilities.ThirdClothes.visualVest = SkinOptions.SkinConfig.VestID;
				}
				if (SkinOptions.SkinConfig.HatID != 0)
				{
					SkinsUtilities.CharacterClothes.visualHat = SkinOptions.SkinConfig.HatID;
					SkinsUtilities.FirstClothes.visualHat = SkinOptions.SkinConfig.HatID;
					SkinsUtilities.ThirdClothes.visualHat = SkinOptions.SkinConfig.HatID;
				}
				if (SkinOptions.SkinConfig.MaskID != 0)
				{
					SkinsUtilities.CharacterClothes.visualMask = SkinOptions.SkinConfig.MaskID;
					SkinsUtilities.FirstClothes.visualMask = SkinOptions.SkinConfig.MaskID;
					SkinsUtilities.ThirdClothes.visualMask = SkinOptions.SkinConfig.MaskID;
				}
				if (SkinOptions.SkinConfig.GlassesID != 0)
				{
					SkinsUtilities.CharacterClothes.visualGlasses = SkinOptions.SkinConfig.GlassesID;
					SkinsUtilities.FirstClothes.visualGlasses = SkinOptions.SkinConfig.GlassesID;
					SkinsUtilities.ThirdClothes.visualGlasses = SkinOptions.SkinConfig.GlassesID;
				}
				SkinsUtilities.CharacterClothes.apply();
				SkinsUtilities.FirstClothes.apply();
				SkinsUtilities.ThirdClothes.apply();
			}
		}

		// Token: 0x0600031A RID: 794 RVA: 0x000195FC File Offset: 0x000177FC
		public static void DrawSkins(SkinOptionList OptionList)
		{
			SkinsUtilities.SearchString = Prefab.TextField(SkinsUtilities.SearchString, "Search: ");
			SkinsUtilities.scrollPosition1 = GUILayout.BeginScrollView(SkinsUtilities.scrollPosition1, Array.Empty<GUILayoutOption>());
			foreach (Skin skin in OptionList.Skins)
			{
				if (skin.Name.ToLower().Contains(SkinsUtilities.SearchString.ToLower()))
				{
					if (GUILayout.Button(skin.Name, "NavBox", Array.Empty<GUILayoutOption>()))
					{
						SkinsUtilities.Apply(skin, OptionList.Type);
					}
					GUILayout.Space(5f);
				}
			}
			GUILayout.EndScrollView();
		}

		// Token: 0x0600031B RID: 795 RVA: 0x000196C4 File Offset: 0x000178C4
		public static void DrawSkins2(SkinOptionList OptionList)
		{
			SkinsUtilities.SearchString = Prefab.TextField(SkinsUtilities.SearchString, "Search: ");
			SkinsUtilities.scrollPosition1 = GUILayout.BeginScrollView(SkinsUtilities.scrollPosition1, Array.Empty<GUILayoutOption>());
			foreach (Skin skin in OptionList.Skins)
			{
				if (skin.Name.ToLower().Contains(OptimizationVariables.MainPlayer.equipment.asset.itemName.ToLower()))
				{
					if (GUILayout.Button(skin.Name, "NavBox", Array.Empty<GUILayoutOption>()))
					{
						SkinsUtilities.Apply(skin, OptionList.Type);
					}
					GUILayout.Space(5f);
				}
			}
			GUILayout.EndScrollView();
		}

        // Token: 0x0600031C RID: 796 RVA: 0x0001979C File Offset: 0x0001799C
        public static void RefreshEconInfo()
        {
            var econ = EconReflection.EconInfo;

            // Очищаем списки
            SkinOptions.SkinWeapons.Skins.Clear();
            SkinOptions.SkinClothesShirts.Skins.Clear();
            SkinOptions.SkinClothesPants.Skins.Clear();
            SkinOptions.SkinClothesBackpack.Skins.Clear();
            SkinOptions.SkinClothesVest.Skins.Clear();
            SkinOptions.SkinClothesHats.Skins.Clear();
            SkinOptions.SkinClothesMask.Skins.Clear();
            SkinOptions.SkinClothesGlasses.Skins.Clear();

            // Словарь для отслеживания уже добавленных ID
            HashSet<int> addedIds = new HashSet<int>();

            // 1. Сначала добавляем скины из econInfo (Steam Economy)
            if (econ != null && econ.Count > 0)
            {
                foreach (var pair in econ)
                {
                    UnturnedEconInfo info = pair.Value;
                    string type = info.display_type?.ToLower() ?? "";

                    if (type.Contains("skin"))
                        SkinOptions.SkinWeapons.Skins.Add(new Skin(info.name, info.itemdefid));
                    else if (type.Contains("shirt"))
                        SkinOptions.SkinClothesShirts.Skins.Add(new Skin(info.name, info.itemdefid));
                    else if (type.Contains("pants"))
                        SkinOptions.SkinClothesPants.Skins.Add(new Skin(info.name, info.itemdefid));
                    else if (type.Contains("backpack"))
                        SkinOptions.SkinClothesBackpack.Skins.Add(new Skin(info.name, info.itemdefid));
                    else if (type.Contains("vest"))
                        SkinOptions.SkinClothesVest.Skins.Add(new Skin(info.name, info.itemdefid));
                    else if (type.Contains("hat"))
                        SkinOptions.SkinClothesHats.Skins.Add(new Skin(info.name, info.itemdefid));
                    else if (type.Contains("mask"))
                        SkinOptions.SkinClothesMask.Skins.Add(new Skin(info.name, info.itemdefid));
                    else if (type.Contains("glass"))
                        SkinOptions.SkinClothesGlasses.Skins.Add(new Skin(info.name, info.itemdefid));

                    addedIds.Add(info.itemdefid);
                }
            }

            // 2. Затем добавляем скины из ассетов игры (включая модовые)
            var allSkinAssets = Assets.find(EAssetType.SKIN);
            foreach (var asset in allSkinAssets)
            {
                if (asset is SkinAsset skinAsset)
                {
                    // Пропускаем если уже добавлен через econInfo
                    if (addedIds.Contains(skinAsset.id))
                        continue;

                    string skinName = skinAsset.name;
                    if (string.IsNullOrEmpty(skinName))
                        skinName = "Unnamed Skin " + skinAsset.id;

                    // Для SkinAsset добавляем в оружие
                    SkinOptions.SkinWeapons.Skins.Add(new Skin(skinName, skinAsset.id));
                }
            }

            // 3. Добавляем предметы одежды из ItemAsset
            var allItemAssets = Assets.find(EAssetType.ITEM);
            foreach (var asset in allItemAssets)
            {
                if (asset is ItemAsset itemAsset)
                {
                    // Пропускаем если уже добавлен через econInfo
                    if (addedIds.Contains(itemAsset.id))
                        continue;

                    string itemName = itemAsset.itemName;
                    if (string.IsNullOrEmpty(itemName))
                        itemName = itemAsset.name;

                    // Определяем тип одежды по типу предмета
                    switch (itemAsset.type)
                    {
                        case EItemType.SHIRT:
                            SkinOptions.SkinClothesShirts.Skins.Add(new Skin(itemName, itemAsset.id));
                            break;
                        case EItemType.PANTS:
                            SkinOptions.SkinClothesPants.Skins.Add(new Skin(itemName, itemAsset.id));
                            break;
                        case EItemType.BACKPACK:
                            SkinOptions.SkinClothesBackpack.Skins.Add(new Skin(itemName, itemAsset.id));
                            break;
                        case EItemType.VEST:
                            SkinOptions.SkinClothesVest.Skins.Add(new Skin(itemName, itemAsset.id));
                            break;
                        case EItemType.HAT:
                            SkinOptions.SkinClothesHats.Skins.Add(new Skin(itemName, itemAsset.id));
                            break;
                        case EItemType.MASK:
                            SkinOptions.SkinClothesMask.Skins.Add(new Skin(itemName, itemAsset.id));
                            break;
                        case EItemType.GLASSES:
                            SkinOptions.SkinClothesGlasses.Skins.Add(new Skin(itemName, itemAsset.id));
                            break;
                    }
                }
            }

            // 4. Сортируем списки для удобства
            SortSkinHashSet(SkinOptions.SkinWeapons.Skins);
            SortSkinHashSet(SkinOptions.SkinClothesShirts.Skins);
            SortSkinHashSet(SkinOptions.SkinClothesPants.Skins);
            SortSkinHashSet(SkinOptions.SkinClothesBackpack.Skins);
            SortSkinHashSet(SkinOptions.SkinClothesVest.Skins);
            SortSkinHashSet(SkinOptions.SkinClothesHats.Skins);
            SortSkinHashSet(SkinOptions.SkinClothesMask.Skins);
            SortSkinHashSet(SkinOptions.SkinClothesGlasses.Skins);
        }

        // Вспомогательный метод для сортировки HashSet скинов
        private static void SortSkinHashSet(HashSet<Skin> skinSet)
        {
            // Создаем временный список для сортировки
            List<Skin> tempList = new List<Skin>(skinSet);

            // Сортируем по имени
            tempList.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.OrdinalIgnoreCase));

            // Очищаем оригинальный HashSet и добавляем отсортированные элементы
            skinSet.Clear();
            foreach (var skin in tempList)
            {
                skinSet.Add(skin);
            }
        }

        // Компаратор для сортировки скинов по имени
        private class SkinComparer : System.Collections.IComparer
        {
            public int Compare(object x, object y)
            {
                if (x is Skin skinX && y is Skin skinY)
                {
                    return string.Compare(skinX.Name, skinY.Name, StringComparison.OrdinalIgnoreCase);
                }
                return 0;
            }
        }

        // Token: 0x040003BD RID: 957
        public static Vector2 ScrollPos;

		// Token: 0x040003BE RID: 958
		public static string SearchString = "";

		// Token: 0x040003BF RID: 959
		private static Vector2 scrollPosition1 = new Vector2(0f, 0f);
	}
}
