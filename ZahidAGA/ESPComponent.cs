using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HighlightingSystem;
using Newtonsoft.Json.Linq;
using SDG.Framework.Foliage;
using SDG.Unturned;
using Steamworks;
using UnityEngine;
using UnityEngine.UIElements;
using ZahidAGA;

namespace ZahidAGA
{
    [Component]
    public class ESPComponent : MonoBehaviour
    {
        public static FieldInfo AmmoInfo = typeof(UseableGun).GetField("ammo", BindingFlags.Instance | BindingFlags.NonPublic);
        public static Font ESPFont;
        public static List<Highlighter> Highlighters = new List<Highlighter>();
        public static Camera MainCamera;

        [Initializer]
        public static void Initialize()
        {
            for (int i = 0; i < ESPOptions.VisualOptions.Length; i++)
            {
                ESPTarget esptarget = (ESPTarget)i;
                Color32 color = Color.white;

                switch (esptarget)
                {
                    case ESPTarget.Player:
                        color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
                        break;
                    case ESPTarget.Zombie:
                        color = new Color32(115, byte.MaxValue, 110, byte.MaxValue);
                        break;
                    case ESPTarget.Item:
                        color = new Color32(0, byte.MaxValue, 0, byte.MaxValue);
                        break;
                    case ESPTarget.Sentry:
                        color = new Color32(220, 10, 10, byte.MaxValue);
                        break;
                    case ESPTarget.Bed:
                        color = new Color32(byte.MaxValue, 170, byte.MaxValue, byte.MaxValue);
                        break;
                    case ESPTarget.Flag:
                        color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
                        break;
                    case ESPTarget.Vehicle:
                        color = new Color32(240, 236, 0, byte.MaxValue);
                        break;
                    case ESPTarget.Storage:
                        color = new Color32(byte.MaxValue, byte.MaxValue, 90, byte.MaxValue);
                        break;
                }

                ColorUtilities.addColor(new ColorVariable(string.Format("_{0}", esptarget), string.Format("", esptarget), color, false));
                ColorUtilities.addColor(new ColorVariable(string.Format("_{0}_Glow", esptarget), string.Format("", esptarget), color, false));
                ColorUtilities.addColor(new ColorVariable("_ESPFriendly", "      ESP Friendly", new Color32(100, 0, byte.MaxValue, byte.MaxValue), true));
                ColorUtilities.addColor(new ColorVariable("_Admin", "      Admin", new Color32(byte.MaxValue, 0, 0, byte.MaxValue), true));
                ColorUtilities.addColor(new ColorVariable("_WeaponInfoColor", "", new Color32(0, byte.MaxValue, byte.MaxValue, byte.MaxValue), true));
                ColorUtilities.addColor(new ColorVariable("_WeaponInfoBorder", "", new Color32(0, 0, 0, byte.MaxValue), true));
                ColorUtilities.addColor(new ColorVariable("_Coordinates", "", new Color32(byte.MaxValue, byte.MaxValue, 0, byte.MaxValue), true));
                ColorUtilities.addColor(new ColorVariable("_CoordinatesTick", "", new Color32(0, 0, 0, byte.MaxValue), true));
                ColorUtilities.addColor(new ColorVariable("_VehicleInfoColor", "", new Color32(byte.MaxValue, 0, byte.MaxValue, byte.MaxValue), true));
                ColorUtilities.addColor(new ColorVariable("_VehicleInfoBorder", "", new Color32(0, 0, 0, byte.MaxValue), true));
                ColorUtilities.addColor(new ColorVariable("_ShowFOVAim", "", new Color32(0, byte.MaxValue, 0, byte.MaxValue), true));
                ColorUtilities.addColor(new ColorVariable("_ShowFOV", "", new Color32(byte.MaxValue, 0, 0, byte.MaxValue), true));
                ColorUtilities.addColor(new ColorVariable("_SelfColor", "      Chams Self", Color.green, false));
                ColorUtilities.addColor(new ColorVariable("_ChamsFriendVisible", "      Chams Friend - Visible", Color.green, false));
                ColorUtilities.addColor(new ColorVariable("_ChamsFriendInvisible", "      Chams Friend - Invisible", Color.blue, false));
                ColorUtilities.addColor(new ColorVariable("_ChamsEnemyVisible", "      Chams Enemy - Visible", new Color32(byte.MaxValue, 165, 0, byte.MaxValue), false));
                ColorUtilities.addColor(new ColorVariable("_ChamsEnemyInvisible", "      Chams Enemy - Invisible", Color.red, false));
                ColorUtilities.addColor(new ColorVariable("_SlientInfoColor", "      Slient - Info", new Color32(0, byte.MaxValue, byte.MaxValue, byte.MaxValue), true));
                ColorUtilities.addColor(new ColorVariable("_SlientInfoBorder", "      Slient - Info Border", new Color32(0, 0, 0, byte.MaxValue), true));
                ColorUtilities.addColor(new ColorVariable("_SlientCizgiColor", "      Slient - Tracer", new Color32(0, byte.MaxValue, byte.MaxValue, byte.MaxValue), true));
            }
        }

        public void Start()
        {
            CoroutineComponent.ESPCoroutine = base.StartCoroutine(ESPCoroutines.UpdateObjectList());
            CoroutineComponent.ChamsCoroutine = base.StartCoroutine(ESPCoroutines.DoChams());
        }

        public void Update()
        {
            if (DrawUtilities.ShouldRun() && !G.BeingSpied)
            {
                if (MiscOptions.DeleteCharacterAnimation)
                {
                    OptimizationVariables.MainPlayer.animator.play("Move_Walk", false);
                }
                if (MiscOptions.NoRain)
                {
                    LightingManager.DisableWeather();
                }
                if (MiscOptions.NoCloud)
                {
                    RenderSettings.skybox.DisableKeyword("WITH_CLOUDS");
                }
                else
                {
                    RenderSettings.skybox.EnableKeyword("WITH_CLOUDS");
                }

                UpdateGrassSettings();
            }
        }

        private void UpdateGrassSettings()
        {
            if (MiscOptions.NoGrass)
            {
                FoliageSettings.enabled = true;
                FoliageSettings.drawDistance = 0;
                FoliageSettings.instanceDensity = 0f;
                FoliageSettings.drawFocusDistance = 0;
                FoliageSettings.focusDistance = 0f;
            }
            else
            {
                float num = (0.3f + GraphicsSettings.NormalizedFarClipDistance * 0.7f) * 2048f;
                FoliageSettings.enabled = true;
                FoliageSettings.drawDistance = 2;
                FoliageSettings.instanceDensity = 0.25f;
                FoliageSettings.drawFocusDistance = 1;
                FoliageSettings.focusDistance = num;
            }
        }

        public void OnGUI()
        {
            if (Event.current.type != EventType.Repaint || !ESPOptions.Enabled || G.BeingSpied)
            {
                return;
            }
            if (!DrawUtilities.ShouldRun())
            {
                return;
            }
            GUI.depth = 1;
            if (ESPComponent.MainCamera == null)
            {
                ESPComponent.MainCamera = OptimizationVariables.MainCam;
            }

            Vector3 playerPosition = OptimizationVariables.MainPlayer.transform.position;

            for (int i = 0; i < ESPVariables.Objects.Count; i++)
            {
                ESPObject espObject = ESPVariables.Objects[i];
                ESPVisual espVisual = ESPOptions.VisualOptions[(int)espObject.Target];
                GameObject gameObject = espObject.GObject;

                if (!espVisual.Enabled)
                {
                    Highlighter component = gameObject.GetComponent<Highlighter>();
                    if (component != null)
                    {
                        component.ConstantOffImmediate();
                    }
                    continue;
                }

                if (espObject.Target == ESPTarget.Item && ESPOptions.FilterItems)
                {
                    if (!ItemUtilities.Whitelisted(((InteractableItem)espObject.Object).asset, ItemOptions.ItemESPOptions))
                    {
                        continue;
                    }
                }

                Color color = Color.yellow;
                Color objectColor = ColorUtilities.getColor(string.Format("_{0}", espObject.Target));
                LabelLocation location = espVisual.Location;

                if (gameObject == null) continue;

                Vector3 objectPosition = gameObject.transform.position;
                double distance = VectorUtilities.GetDistance(objectPosition, playerPosition);

                if (distance < 0.5 || (distance > espVisual.Distance && !espVisual.InfiniteDistance))
                    continue;

                Vector3 screenPosition = ESPComponent.MainCamera.WorldToScreenPoint(objectPosition);
                if (screenPosition.z <= 0f) continue;

                // Calculate bounds for the object
                Vector3 localScale = gameObject.transform.localScale;
                ESPTarget target = espObject.Target;
                Bounds bounds;

                if (target > ESPTarget.Zombie)
                {
                    if (target != ESPTarget.Vehicle)
                    {
                        bounds = gameObject.GetComponent<Collider>().bounds;
                    }
                    else
                    {
                        bounds = gameObject.transform.Find("Model_0").GetComponent<MeshRenderer>().bounds;
                        Transform transform = gameObject.transform.Find("Model_1");
                        if (transform != null)
                        {
                            bounds.Encapsulate(transform.GetComponent<MeshRenderer>().bounds);
                        }
                    }
                }
                else
                {
                    // FIXED: This was creating 2D boxes for zombies - removed the manual bounds creation
                    bounds = gameObject.GetComponent<Collider>().bounds;
                }

                int textSize = DrawUtilities.GetTextSize(espVisual, distance);
                string text = string.Format("<size={0}>", textSize);
                string text2 = string.Format("<size={0}>", textSize);

                // Add distance information
                if (espVisual.ShowDistance)
                {
                    float objectDistance = T.GetDistance(espObject.GObject.transform.position);
                    if (objectDistance >= 0f && objectDistance < 50f)
                    {
                        text += string.Format("<color=white>[</color><color=red>{0}</color><color=white>]</color> ", objectDistance);
                        text2 += string.Format("[{0}] ", objectDistance);
                    }
                    else if (objectDistance >= 50f && objectDistance < 300f)
                    {
                        text += string.Format("<color=white>[</color><color=yellow>{0}</color><color=white>]</color> ", objectDistance);
                        text2 += string.Format("[{0}] ", objectDistance);
                    }
                    else if (objectDistance >= 300f)
                    {
                        text += string.Format("<color=white>[</color><color=#00FF00>{0}</color><color=white>]</color> ", objectDistance);
                        text2 += string.Format("[{0}] ", objectDistance);
                    }
                }

                // Process different ESP target types
                switch (espObject.Target)
                {
                    case ESPTarget.Player:
                        ProcessPlayerESP(espObject, espVisual, ref text, ref text2, ref bounds, ref color);
                        break;
                    case ESPTarget.Zombie:
                        ProcessZombieESP(espObject, espVisual, ref text, ref text2);
                        break;
                    case ESPTarget.Item:
                        ProcessItemESP(espObject, espVisual, ref text, ref text2);
                        break;
                    case ESPTarget.Sentry:
                        ProcessSentryESP(espObject, espVisual, ref text, ref text2);
                        break;
                    case ESPTarget.Bed:
                        ProcessBedESP(espObject, espVisual, ref text, ref text2);
                        break;
                    case ESPTarget.Flag:
                        ProcessFlagESP(espVisual, ref text, ref text2);
                        break;
                    case ESPTarget.Vehicle:
                        ProcessVehicleESP(espObject, espVisual, ref text, ref text2);
                        break;
                    case ESPTarget.Storage:
                        ProcessStorageESP(espObject, espVisual, ref text, ref text2);
                        break;
                }

                text += "</size>";
                text2 += "</size>";

                // FIXED: Only draw boxes if explicitly enabled for the target
                if (espVisual.Boxes)
                {
                    Vector3[] boxVectors = new Vector3[]
{
    new Vector3(bounds.center.x - bounds.extents.x, bounds.center.y + bounds.extents.y, bounds.center.z - bounds.extents.z),
    new Vector3(bounds.center.x + bounds.extents.x, bounds.center.y + bounds.extents.y, bounds.center.z - bounds.extents.z),
    new Vector3(bounds.center.x - bounds.extents.x, bounds.center.y - bounds.extents.y, bounds.center.z - bounds.extents.z),
    new Vector3(bounds.center.x + bounds.extents.x, bounds.center.y - bounds.extents.y, bounds.center.z - bounds.extents.z),
    new Vector3(bounds.center.x - bounds.extents.x, bounds.center.y + bounds.extents.y, bounds.center.z + bounds.extents.z),
    new Vector3(bounds.center.x + bounds.extents.x, bounds.center.y + bounds.extents.y, bounds.center.z + bounds.extents.z),
    new Vector3(bounds.center.x - bounds.extents.x, bounds.center.y - bounds.extents.y, bounds.center.z + bounds.extents.z),
    new Vector3(bounds.center.x + bounds.extents.x, bounds.center.y - bounds.extents.y, bounds.center.z + bounds.extents.z)
};
                    Vector2[] rectangleLines = DrawUtilities.GetRectangleLines(ESPComponent.MainCamera, bounds, objectColor);
                    Vector3 labelPosition = DrawUtilities.Get2DW2SVector(ESPComponent.MainCamera, rectangleLines, location);

                    if (espVisual.TwoDimensional)
                    {
                        DrawUtilities.PrepareRectangleLines(rectangleLines, objectColor);
                    }
                    else
                    {
                        DrawUtilities.PrepareBoxLines(boxVectors, objectColor);
                        labelPosition = DrawUtilities.Get3DW2SVector(ESPComponent.MainCamera, bounds, location);
                    }

                    // Draw the label
                    if (ESPOptions.EspStyle)
                    {
                        DrawUtilities.DrawLabel(ESPComponent.ESPFont, location, labelPosition, text,
                            espVisual.CustomTextColor ? color : objectColor, Color.black,
                            espVisual.BorderStrength, text2, 12);
                    }
                }
                else
                {
                    // Just draw the label without boxes
                    Vector3 labelPosition = screenPosition;
                    labelPosition.y = Screen.height - labelPosition.y;

                    if (ESPOptions.EspStyle)
                    {
                        DrawUtilities.DrawLabel(ESPComponent.ESPFont, location, labelPosition, text,
                            espVisual.CustomTextColor ? color : objectColor, Color.black,
                            espVisual.BorderStrength, text2, 12);
                    }
                }

                // Handle custom ESP styles
                Vector2 screenPos2D = new Vector2(screenPosition.x, Screen.height - screenPosition.y);

                if (ESPOptions.EspStyle1 && Main.Espstyle1 != null)
                {
                    GUI.DrawTexture(new Rect(screenPos2D.x - 20f, screenPos2D.y - 10f, 200f, 25f), Main.Espstyle1);
                    GUI.Label(new Rect(screenPos2D.x - 10f, screenPos2D.y + 20f, 180f, 60f), text);
                }
                if (ESPOptions.EspStyle2 && Main.Espstyle2 != null)
                {
                    GUI.DrawTexture(new Rect(screenPos2D.x - 20f, screenPos2D.y - 10f, 200f, 25f), Main.Espstyle2);
                    GUI.Label(new Rect(screenPos2D.x - 10f, screenPos2D.y + 20f, 180f, 60f), text);
                }
                if (ESPOptions.EspStyle3 && Main.Espstyle3 != null)
                {
                    GUI.DrawTexture(new Rect(screenPos2D.x - 20f, screenPos2D.y - 10f, 200f, 25f), Main.Espstyle3);
                    GUI.Label(new Rect(screenPos2D.x - 10f, screenPos2D.y + 20f, 180f, 60f), text);
                }
                if (ESPOptions.EspStyle4 && Main.Espstyle4 != null)
                {
                    GUI.DrawTexture(new Rect(screenPos2D.x - 20f, screenPos2D.y - 10f, 200f, 25f), Main.Espstyle4);
                    GUI.Label(new Rect(screenPos2D.x - 10f, screenPos2D.y + 20f, 180f, 60f), text);
                }
                if (ESPOptions.EspStyle5 && Main.Espstyle5 != null)
                {
                    GUI.DrawTexture(new Rect(screenPos2D.x - 20f, screenPos2D.y - 10f, 200f, 25f), Main.Espstyle5);
                    GUI.Label(new Rect(screenPos2D.x - 10f, screenPos2D.y + 20f, 180f, 60f), text);
                }

                // Handle glow effect
                if (espVisual.Glow)
                {
                    Highlighter highlighter = espObject.GObject.GetComponent<Highlighter>() ?? espObject.GObject.AddComponent<Highlighter>();
                    highlighter.occluder = true;
                    highlighter.overlay = true;
                    highlighter.ConstantOnImmediate(objectColor);
                    ESPComponent.Highlighters.Add(highlighter);
                }
                else
                {
                    Highlighter component3 = gameObject.GetComponent<Highlighter>();
                    if (component3 != null)
                    {
                        component3.ConstantOffImmediate();
                    }
                }

                // Handle line to object
                if (espVisual.LineToObject)
                {
                    ESPVariables.DrawBuffer2.Enqueue(new ESPBox2
                    {
                        Color = objectColor,
                        Vertices = new Vector2[]
                        {
                    new Vector2((float)(Screen.width / 2), (float)Screen.height),
                    new Vector2(screenPosition.x, (float)Screen.height - screenPosition.y)
                        }
                    });
                }
            }

            // Render the collected draw buffers
            RenderDrawBuffers();
        }

        private void RenderDrawBuffers()
        {
            T.DrawMaterial.SetPass(0);
            GL.PushMatrix();
            GL.LoadProjectionMatrix(ESPComponent.MainCamera.projectionMatrix);
            GL.modelview = ESPComponent.MainCamera.worldToCameraMatrix;
            GL.Begin(1);

            while (ESPVariables.DrawBuffer.Count > 0)
            {
                ESPBox espBox = ESPVariables.DrawBuffer.Dequeue();
                GL.Color(espBox.Color);
                foreach (Vector3 vertex in espBox.Vertices)
                {
                    GL.Vertex(vertex);
                }
            }

            GL.End();
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Begin(1);

            while (ESPVariables.DrawBuffer2.Count > 0)
            {
                ESPBox2 espBox2 = ESPVariables.DrawBuffer2.Dequeue();
                GL.Color(espBox2.Color);
                Vector2[] vertices = espBox2.Vertices;
                bool validLine = true;

                for (int i = 0; i < vertices.Length - 1; i++)
                {
                    if (Vector2.Distance(vertices[i + 1], vertices[i]) > Screen.width / 2)
                    {
                        validLine = false;
                        break;
                    }
                }

                if (validLine)
                {
                    foreach (Vector2 vertex in vertices)
                    {
                        GL.Vertex3(vertex.x, vertex.y, 0f);
                    }
                }
            }

            GL.End();
            GL.PopMatrix();
        }
       

        private void ProcessPlayerESP(ESPObject espObject, ESPVisual espVisual, ref string text, ref string text2, ref Bounds bounds, ref Color color)
        {
            Player player = (Player)espObject.Object;

            if (player.life.isDead)
                return;

            SteamPlayer steamPlayer = GetSteamPlayer(player);

            if (espVisual.ShowName && steamPlayer != null)
            {
                text += steamPlayer.playerID.characterName + "\n";
                text2 += steamPlayer.playerID.characterName + "\n";
            }

            // Add weapon information
            if (ESPOptions.ShowPlayerWeapon)
            {
                string weaponName = (player.equipment.asset != null) ? player.equipment.asset.itemName : "None";
                text += "<color=white>" + weaponName + "</color>";
                text2 += weaponName;
            }

            // Add ammo information
            if (ESPOptions.ShowAmmo && player.equipment.asset != null)
            {
                byte ammo = (byte)AmmoInfo.GetValue(player.equipment.asset);
                text += ammo.ToString();
                text2 += ammo.ToString();
            }

            // Adjust bounds for players
            bounds.size /= 2f;
            bounds.size = new Vector3(bounds.size.x, bounds.size.y * 1.25f, bounds.size.z);
        }

        private void ProcessZombieESP(ESPObject espObject, ESPVisual espVisual, ref string text, ref string text2)
        {
            Zombie zombie = (Zombie)espObject.Object;

            if (zombie.isDead)
                return;

            if (text.Contains("<size="))
            {
                text += "<color=#00f742>Zombie</color>";
                text2 += "Zombie";
            }
        }

        private void ProcessItemESP(ESPObject espObject, ESPVisual espVisual, ref string text, ref string text2)
        {
            InteractableItem item = (InteractableItem)espObject.Object;

            if (text.Contains("<size="))
            {
                text += "<color=#00f742>" + item.asset.itemName + "</color>";
                text2 += item.asset.itemName;
            }
        }

        private void ProcessSentryESP(ESPObject espObject, ESPVisual espVisual, ref string text, ref string text2)
        {
            InteractableSentry sentry = (InteractableSentry)espObject.Object;

            if (text.Contains("<size="))
            {
                text += "<color=#0212eb>Sentry</color>";
                text2 += "Sentry";
            }

            if (ESPOptions.ShowSentryItem)
            {
                text += SentryName(sentry.displayItem, true);
                text2 += SentryName(sentry.displayItem, false);
            }
        }

        private void ProcessBedESP(ESPObject espObject, ESPVisual espVisual, ref string text, ref string text2)
        {
            InteractableBed bed = (InteractableBed)espObject.Object;

            if (text.Contains("<size="))
            {
                text += "<color=#b972cf>Bed</color>";
                text2 += "Bed";
            }

            // Add bed claim information
            AddClaimInformation(bed.owner, bed.isClaimed, ref text, ref text2, ESPOptions.ClaimİDBed, ESPOptions.ClaimNameBed);
        }

        private void ProcessFlagESP(ESPVisual espVisual, ref string text, ref string text2)
        {
            if (text.Contains("<size="))
            {
                text += "<color=white>Claim Flag</color>";
                text2 += "Claim Flag";
            }
        }

        private void ProcessVehicleESP(ESPObject espObject, ESPVisual espVisual, ref string text, ref string text2)
        {
            InteractableVehicle vehicle = (InteractableVehicle)espObject.Object;

            if (vehicle.health == 0 || (ESPOptions.FilterVehicleLocked && vehicle.isLocked))
                return;

            if (text.Contains("<size="))
            {
                text += "<color=yellow>" + vehicle.asset.name + "</color>";
                text2 += vehicle.asset.name;
            }

            // Add vehicle health and fuel information
            if (ESPOptions.ShowVehicleHealth)
            {
                float healthPercent = Mathf.Round(100f * ((float)vehicle.health / (float)vehicle.asset.health));
                text += "\n" + string.Format("Health: {0}%", healthPercent);
                text2 += "\n" + string.Format("Health: {0}%", healthPercent);
            }

            // Add claim information
            AddClaimInformation(vehicle.lockedOwner, vehicle.isLocked, ref text, ref text2, ESPOptions.ClaimİDCar, ESPOptions.ClaimNameCar);
        }

        private void ProcessStorageESP(ESPObject espObject, ESPVisual espVisual, ref string text, ref string text2)
        {
            InteractableStorage storage = (InteractableStorage)espObject.Object;

            if (text.Contains("<size="))
            {
                text += "<color=#008fb3>Storage</color>";
                text2 += "Storage";
            }

            AddClaimInformation(storage.owner, false, ref text, ref text2, ESPOptions.ClaimİDStorage, ESPOptions.ClaimNameStorage);
        }

        private void AddClaimInformation(CSteamID owner, bool isClaimed, ref string text, ref string text2, bool showID, bool showName)
        {
            if (showID)
            {
                string ownerID = (owner.ToString() == "0") ? "Null" : owner.ToString();
                text += "\n" + ownerID;
                text2 += "\n" + ownerID;
            }

            if (showName)
            {
                SteamPlayer ownerPlayer = FindPlayerBySteamID(owner);
                string ownerName = (ownerPlayer != null) ? ownerPlayer.player.name : "Null";
                text += "\n" + ownerName;
                text2 += "\n" + ownerName;
            }

            if (isClaimed)
            {
                text += "\n<color=#00ff00ff> - Claimed</color>";
                text2 += "\n - Claimed";
            }
        }

        //public static void DisableHighlighters()
        //{
        //    foreach (Highlighter highlighter in Highlighters)
        //    {
        //        highlighter.occluder = false;
        //        highlighter.overlay = false;
        //        highlighter.ConstantOffImmediate();
        //    }
        //    Highlighters.Clear();
        //}
        public static string SentryName(Item displayItem, bool useColor)
        {
            if (displayItem != null)
            {
                Asset asset = Assets.find(EAssetType.ITEM, displayItem.id);
                return asset?.name ?? "Unknown";
            }

            return useColor ? "<color=#ff0000>No Item</color>" : "-No Item";
        }

        public static SteamPlayer GetSteamPlayer(Player player)
        {
            return Provider.clients.FirstOrDefault(steamPlayer => steamPlayer.player == player);
        }

        private SteamPlayer FindPlayerBySteamID(CSteamID steamID)
        {
            return Provider.clients.FirstOrDefault(player => player.playerID.steamID == steamID);
        }
    }
}