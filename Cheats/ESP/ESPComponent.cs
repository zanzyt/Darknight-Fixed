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
            if (MiscOptions.NoGrass) //да
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
                    // FIXED: Safely get bounds
                    Collider component = gameObject.GetComponent<Collider>();
                    if (component != null)
                    {
                        bounds = component.bounds;
                    }
                    else
                    {
                        bounds = new Bounds(gameObject.transform.position, new Vector3(1f, 2f, 1f));
                    }
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
                text += ((player.equipment.asset != null) ? ("<color=#white>" + player.equipment.asset.itemName + "</color>") : "<color=#white>None</color>");
                text2 += ((player.equipment.asset != null) ? (player.equipment.asset.itemName ?? "") : "None");
            }
            if (ESPOptions.ShowAmmo && player.gameObject != null && player.equipment.asset != null)
            {
                text += ((byte)ESPComponent.AmmoInfo.GetValue(player.equipment.asset)).ToString();
                text2 += ((byte)ESPComponent.AmmoInfo.GetValue(player.equipment.asset)).ToString();
            }
            if (ESPOptions.HitboxSize && RaycastOptions.Enabled)
            {
                Vector3 vector = T.WorldToScreen(player.transform.position);
                if (vector.z >= 0f)
                {
                    Vector3 vector2 = T.WorldToScreen(new Vector3(player.transform.position.x, player.transform.position.y + (float)AimbotOptions.HitboxSize, player.transform.position.z));
                    float radius = Vector3.Distance(vector, vector2);
                    T.DrawCircle(Color.magenta, new Vector2(vector.x, vector.y), radius);
                }
            }
            if (ESPOptions.Skeleton)
            {
                T.DrawSkeleton(espObject.GObject.transform, Color.yellow);
            }

            if (ESPOptions.showhotbar && player.equipment.asset != null)
            {
                ItemTool.getIcon(player.equipment.asset.id, 40, player.equipment.asset.getState(), (handle, texture) =>
                {
                });
            }
            if (ESPOptions.ShowPlayerVehicle)
            {
                text += ((player.movement.getVehicle() != null) ? ("<color=#white>" + player.movement.getVehicle().asset.name + "</color>") : "<color=#white>No Vehicle</color>");
                text2 += ((player.movement.getVehicle() != null) ? (player.movement.getVehicle().asset.name ?? "") : "No Vehicle");
            }
            bounds.size /= 2f;
            bounds.size = new Vector3(bounds.size.x, bounds.size.y * 1.25f, bounds.size.z);
            // Add ammo information

            if (ESPOptions.ChamsEnabled && FriendUtilities.IsFriendly(player))
            {
                color = Color.cyan;
            }
            if (ESPOptions.AdminRengi && player.channel.owner.isAdmin)
            {
                color = Color.red;
            }

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
            if (((Zombie)espObject.Object).isDead)
            {
                return;
            }

            if (espVisual.ShowName)
            {
                text += "<color=#00f742>Zombie</color>";
            }
            text2 += "Zombie";
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
            InteractableBed interactableBed = (InteractableBed)espObject.Object;
            if (espVisual.ShowName)
            {
                text += "<color=#b972cf>Bed</color>";
                text2 += "Bed";
            }
            if (ESPOptions.ShowClaimed)
            {
                if (interactableBed.isClaimed && espVisual.ShowName)
                {
                    text += "<color=#00ff00ff> - Claimed</color>";
                    text2 += "<color=#00ff00ff> - Claimed</color>";
                }
                if (!interactableBed.isClaimed && espVisual.ShowName)
                {
                    text += "<color=#ff0000ff> - No Claimed</color>";
                    text2 += "<color=#ff0000ff> - No Claimed</color>";
                }
            }
            if (ESPOptions.ClaimİDBed)
            {
                CSteamID owner = interactableBed.owner;
                if (interactableBed.owner.ToString() == "0")
                {
                    text += "\nNull";
                    text2 += "\nNull";
                }
                else
                {
                    text = text + "\n" + interactableBed.owner.ToString();
                    text2 = text2 + "\n" + interactableBed.owner.ToString();
                }
            }
            if (ESPOptions.ClaimNameBed)
            {
                SteamPlayer steamPlayer = null;
                using (List<SteamPlayer>.Enumerator enumerator = Provider.clients.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        steamPlayer = enumerator.Current;
                    }
                }
                if (steamPlayer.playerID.steamID == interactableBed.owner)
                {
                    text = text + "\n" + steamPlayer.player.name;
                    text2 = text2 + "\n" + steamPlayer.player.name;
                }
                else
                {
                    text += "\nNull";
                    text2 += "\nNull";
                }
            }
        }


        private void ProcessFlagESP(ESPVisual espVisual, ref string text, ref string text2)
        {
            if (espVisual.ShowName)
            {
                text += "<color=white>Claim Flag</color>";
            }
            text2 += "Claim Flag";
        }

        private void ProcessVehicleESP(ESPObject espObject, ESPVisual espVisual, ref string text, ref string text2)
        {
            InteractableVehicle interactableVehicle = (InteractableVehicle)espObject.Object;
            if (interactableVehicle.health == 0 || (ESPOptions.FilterVehicleLocked && interactableVehicle.isLocked))
            {
                return;
            }

            // Исправление: используем out параметры
            ushort num;
            ushort num2;
            interactableVehicle.getDisplayFuel(out num, out num2); // Добавлены ключевые слова out

            float num3 = Mathf.Round(100f * ((float)interactableVehicle.health / (float)interactableVehicle.asset.health));
            float num4 = Mathf.Round(100f * ((float)num / (float)num2));

            if (espVisual.ShowName)
            {
                text = text + "<color=yellow>" + interactableVehicle.asset.name + "</color>";
                text2 += interactableVehicle.asset.name;
            }
            if (ESPOptions.ShowVehicleHealth)
            {
                text = text + "\n" + string.Format("Health: {0}%", num3);
                text2 = text2 + "\n" + string.Format("Health: {0}%", num3);
            }
            if (ESPOptions.ShowVehicleFuel)
            {
                text += string.Format(" - Fuel: {0}%", num4);
                text2 += string.Format(" - Fuel: {0}%", num4);
            }
            if (ESPOptions.ShowVehicleLocked)
            {
                if (interactableVehicle.isLocked && espVisual.ShowName)
                {
                    text += "\n<color=#ff0000ff> - LOCKED</color>";
                    text2 += "\n- LOCKED";
                }
                if (!interactableVehicle.isLocked && espVisual.ShowName)
                {
                    text += "\n<color=#ff0000ff> - Not Locket</color>";
                    text2 += "\n - Not Locket";
                }
            }
            if (ESPOptions.FilterVehicleLocked)
            {
                CSteamID lockedOwner = interactableVehicle.lockedOwner;
                if (interactableVehicle.lockedOwner.ToString() == "0")
                {
                    text += "\nNull";
                    text2 += "\nNull";
                }
                else
                {
                    text = text + "\n" + interactableVehicle.lockedOwner.ToString();
                    text2 = text2 + "\n" + interactableVehicle.lockedOwner.ToString();
                }
            }
       
            if (ESPOptions.ClaimNameCar)
            {
                SteamPlayer steamPlayer2 = null;
                using (List<SteamPlayer>.Enumerator enumerator = Provider.clients.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        steamPlayer2 = enumerator.Current;
                    }
                }
                if (steamPlayer2.playerID.steamID == interactableVehicle.lockedOwner)
                {
                    text = text + "\n" + steamPlayer2.player.name;
                    text2 = text2 + "\n" + steamPlayer2.player.name;
                }
                else
                {
                    text += "\nNull";
                    text2 += "\nNull";
                }
            }
        }

        private void ProcessStorageESP(ESPObject espObject, ESPVisual espVisual, ref string text, ref string text2)
        {
            InteractableStorage interactableStorage = (InteractableStorage)espObject.Object;
            if (espVisual.ShowName)
            {
                text += "<color=#008fb3>Storage</color>";
            }
            text2 += "Storage";
            if (ESPOptions.ClaimİDStorage)
            {
                CSteamID owner2 = interactableStorage.owner;
                if (interactableStorage.owner.ToString() == "0")
                {
                    text += "\nNull";
                    text2 += "\nNull";
                }
                else
                {
                    text = text + "\n" + interactableStorage.owner.ToString();
                    text2 = text2 + "\n" + interactableStorage.owner.ToString();
                }
            }
            if (ESPOptions.ClaimNameStorage)
            {
                SteamPlayer steamPlayer3 = null;
                using (List<SteamPlayer>.Enumerator enumerator = Provider.clients.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        steamPlayer3 = enumerator.Current;
                    }
                }
                if (steamPlayer3.playerID.steamID == interactableStorage.owner)
                {
                    text = text + "\n" + steamPlayer3.player.name;
                    text2 = text2 + "\n" + steamPlayer3.player.name;
                }
                else
                {
                    text += "\nNull";
                    text2 += "\nNull";
                }
            }
        }

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
            foreach (SteamPlayer steamPlayer in Provider.clients)
            {
                if (steamPlayer.player == player)
                {
                    return steamPlayer;
                }
            }
            return null;
        }
       
    }
}