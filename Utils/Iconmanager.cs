using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDG.Unturned;
using UnityEngine;

namespace ZahidAGA
{



    //pasted from MINE EGGUWARE
    /// <summary>
    /// Менеджер безопасной загрузки и кэширования иконок предметов.
    /// Исправляет баг Unturned, при котором ItemTool.getIcon ломает инвентарь.
    /// </summary>
    internal static class IconManager
    {
        // Кэш иконок по ID предмета
        private static readonly Dictionary<ushort, Texture2D> _iconCache = new();

        /// <summary>
        /// Получает иконку предмета безопасно. Если уже кэширована — возвращает сразу.
        /// </summary>
        public static Texture2D GetIcon(ushort id, byte quality, byte[] state, ItemAsset asset)
        {
            if (asset == null || id == 0)
                return null;

            // Если иконка уже есть в кэше
            if (_iconCache.TryGetValue(id, out Texture2D tex) && tex != null)
                return tex;

            // Callback сохранит иконку, когда она будет готова
            ItemIconReady callback = (int handle, Texture2D iconTex) =>
            {
                if (iconTex != null)
                {
                    _iconCache[id] = iconTex;
                    Debug.Log($"[IconManager] Cached icon for {id}: {asset.itemName}");
                }
            };

            try
            {
                // Безопасный вызов (Unturned сам загрузит текстуру)
                ItemTool.getIcon(id, quality, state, asset, callback);
            }
            catch
            {
                // Игнорируем, если Unturned занят или камера не инициализирована
            }

            // Если ещё не загрузилась — вернуть null (вызовется повторно через секунду)
            _iconCache.TryGetValue(id, out tex);
            return tex;
        }

        /// <summary>
        /// Принудительно очищает кэш иконок (например, при дисконнекте).
        /// </summary>
        public static void ClearCache()
        {
            foreach (var tex in _iconCache.Values)
            {
                if (tex != null)
                    UnityEngine.Object.Destroy(tex);
            }
            _iconCache.Clear();
        }

        /// <summary>
        /// Проверяет, есть ли уже иконка в кэше. 
        /// </summary>
        public static bool HasIcon(ushort id)
        {
            return _iconCache.ContainsKey(id) && _iconCache[id] != null;
        }
    }
}
