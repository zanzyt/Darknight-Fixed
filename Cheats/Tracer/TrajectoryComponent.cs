using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HighlightingSystem;
using SDG.Unturned;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x02000034 RID: 52
	[Component]
	public class TrajectoryComponent : MonoBehaviour
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000BC RID: 188 RVA: 0x0000274C File Offset: 0x0000094C
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00002753 File Offset: 0x00000953
		public static Highlighter Highlighted
		{
			get
			{
				return TrajectoryComponent.highlighted;
			}
			private set
			{
				if (TrajectoryComponent.highlighted != null)
				{
					TrajectoryComponent.RemoveHighlight(TrajectoryComponent.highlighted);
				}
				TrajectoryComponent.highlighted = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00002772 File Offset: 0x00000972
		public static HashSet<GameObject> BodiesInMotion { get; } = new HashSet<GameObject>();

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00002779 File Offset: 0x00000979
		private static Color InRangeColor
		{
			get
			{
				return Color.green;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00002780 File Offset: 0x00000980
		private static Color OutOfRangeColor
		{
			get
			{
				return Color.red;
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00002787 File Offset: 0x00000987
		[Initializer]
		public static void Initialize()
		{
			ColorUtilities.addColor(new ColorVariable("_TrajectoryPredictionInRange", "B.D. Predict (In Range)", Color.cyan, true));
			ColorUtilities.addColor(new ColorVariable("_TrajectoryPredictionOutOfRange", "B.D. Predict (Out of Range)", Color.red, true));
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000027C7 File Offset: 0x000009C7
		public void Start()
		{
			CoroutineComponent.TrajectoryCoroutine = base.StartCoroutine(TrajectoryCoroutines.UpdateBodiesInMotionSet());
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000CD3C File Offset: 0x0000AF3C
		private void HighlightForTrajectory(RaycastHit hit, Color color)
		{
			if (!WeaponOptions.HighlightBulletDropPredictionTarget)
			{
				TrajectoryComponent.Highlighted = null;
				return;
			}
			if (WeaponOptions.HighlightBulletDropPredictionTarget && hit.collider != null)
			{
				Transform transform = hit.transform;
				GameObject gameObject = null;
				if (DamageTool.getPlayer(transform) != null)
				{
					gameObject = DamageTool.getPlayer(transform).gameObject;
				}
				else if (DamageTool.getZombie(transform) != null)
				{
					gameObject = DamageTool.getZombie(transform).gameObject;
				}
				else if (DamageTool.getAnimal(transform) != null)
				{
					gameObject = DamageTool.getAnimal(transform).gameObject;
				}
				else if (DamageTool.getVehicle(transform) != null)
				{
					gameObject = DamageTool.getVehicle(transform).gameObject;
				}
				if (gameObject != null)
				{
					Highlighter highlighter = gameObject.GetComponent<Highlighter>() ?? gameObject.AddComponent<Highlighter>();
					if (!highlighter.enabled)
					{
						highlighter.occluder = true;
						highlighter.overlay = true;
						highlighter.ConstantOnImmediate(color);
					}
					TrajectoryComponent.Highlighted = highlighter;
					return;
				}
				TrajectoryComponent.Highlighted = null;
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000CE30 File Offset: 0x0000B030
		public void OnGUI()
		{
			if (!DrawUtilities.ShouldRun() || TrajectoryComponent.spying || !WeaponOptions.EnableBulletDropPrediction)
			{
				TrajectoryComponent.Highlighted = null;
				return;
			}
			TrajectoryComponent.BodiesInMotion.RemoveWhere(delegate(GameObject x)
			{
				if (x == null)
				{
					return true;
				}
				StickyGrenade component2 = x.GetComponent<StickyGrenade>();
				if (component2 != null)
				{
					Rigidbody component3 = component2.GetComponent<Rigidbody>();
					if (component3 != null && !component3.useGravity)
					{
						return true;
					}
				}
				return false;
			});
			foreach (GameObject gameObject in TrajectoryComponent.BodiesInMotion)
			{
				Rigidbody component = gameObject.GetComponent<Rigidbody>();
				Vector3? vector = ((component != null) ? new Vector3?(component.velocity) : null);
				Vector3 zero = Vector3.zero;
				if (vector == null || (vector != null && vector.GetValueOrDefault() != zero))
				{
					TrajectoryComponent.DrawTrajectory(TrajectoryComponent.PlotTrajectoryRigidBodyInMotion(gameObject, 1500), TrajectoryComponent.OutOfRangeColor);
				}
			}
			Player mainPlayer = OptimizationVariables.MainPlayer;
			Useable useable;
			if (mainPlayer == null)
			{
				useable = null;
			}
			else
			{
				PlayerEquipment equipment = mainPlayer.equipment;
				useable = ((equipment != null) ? equipment.useable : null);
			}
			Useable useable2 = useable;
			if (!(useable2 == null))
			{
				UseableGun useableGun = useable2 as UseableGun;
				bool flag;
				if (useableGun == null)
				{
					flag = true;
				}
				else
				{
					ItemGunAsset equippedGunAsset = useableGun.equippedGunAsset;
					EAction? eaction = ((equippedGunAsset != null) ? new EAction?(equippedGunAsset.action) : null);
					EAction eaction2 = EAction.Rocket;
					flag = !((eaction.GetValueOrDefault() == eaction2) & (eaction != null));
				}
				if (!flag || Provider.modeConfigData.Gameplay.Ballistics)
				{
					UseableGun useableGun2 = useable2 as UseableGun;
					List<Vector3> list;
					bool flag2;
					if (useableGun2 != null)
					{
						EAction action = useableGun2.equippedGunAsset.action;
						RaycastHit raycastHit;
						list = ((action == EAction.Rocket) ? TrajectoryComponent.PlotTrajectoryRocket(useableGun2, out raycastHit, 1500) : TrajectoryComponent.PlotTrajectoryGun(useableGun2, out raycastHit, 255));
						flag2 = action != EAction.Rocket && Vector3.Distance(list.Last<Vector3>(), OptimizationVariables.MainPlayer.look.aim.position) > useableGun2.equippedGunAsset.range;
						if (action != EAction.Rocket)
						{
							this.HighlightForTrajectory(raycastHit, flag2 ? TrajectoryComponent.OutOfRangeColor : TrajectoryComponent.InRangeColor);
						}
					}
					else
					{
						UseableThrowable useableThrowable = useable2 as UseableThrowable;
						if (useableThrowable == null)
						{
							return;
						}
						flag2 = false;
						list = TrajectoryComponent.PlotTrajectoryGrenade(useableThrowable, 50 * (int)useableThrowable.equippedThrowableAsset.fuseLength);
					}
					TrajectoryComponent.DrawTrajectory(list, flag2 ? TrajectoryComponent.OutOfRangeColor : TrajectoryComponent.InRangeColor);
					return;
				}
			}
			TrajectoryComponent.Highlighted = null;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000D078 File Offset: 0x0000B278
		private static void DrawTrajectory(List<Vector3> trajectory, Color color)
		{
			T.DrawMaterial.SetPass(0);
			GL.PushMatrix();
			GL.LoadProjectionMatrix(OptimizationVariables.MainCam.projectionMatrix);
			GL.modelview = OptimizationVariables.MainCam.worldToCameraMatrix;
			GL.Begin(2);
			GL.Color(color);
			foreach (Vector3 vector in trajectory)
			{
				GL.Vertex(vector);
			}
			GL.End();
			GL.PopMatrix();
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000027D9 File Offset: 0x000009D9
		private static void RemoveHighlight(Highlighter h)
		{
			if (h == null)
			{
				return;
			}
			h.occluder = false;
			h.overlay = false;
			h.ConstantOffImmediate();
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000D108 File Offset: 0x0000B308
		public static List<Vector3> PlotTrajectoryGrenade(UseableThrowable grenade, int maxSteps)
		{
			Vector3 vector = OptimizationVariables.MainPlayer.look.aim.position;
			float num = grenade.equippedThrowableAsset.strongThrowForce;
			if (OptimizationVariables.MainPlayer.skills.boost == EPlayerBoost.OLYMPIC)
			{
				num *= grenade.equippedThrowableAsset.boostForceMultiplier;
			}
			Vector3 vector2 = OptimizationVariables.MainPlayer.look.aim.forward * num;
			float mass = grenade.equippedThrowableAsset.throwable.GetComponent<Rigidbody>().mass;
			Vector3 vector3 = vector2 / mass * Time.fixedDeltaTime;
			List<Vector3> list = new List<Vector3> { vector };
			RaycastHit raycastHit;
			if (!Physics.Raycast(new Ray(vector, OptimizationVariables.MainPlayer.look.aim.forward), out raycastHit, 1f, RayMasks.DAMAGE_SERVER, QueryTriggerInteraction.UseGlobal))
			{
				vector += OptimizationVariables.MainPlayer.look.aim.forward;
				list.Add(vector);
			}
			float fixedDeltaTime = Time.fixedDeltaTime;
			for (int i = 1; i < maxSteps; i++)
			{
				vector += vector3 * fixedDeltaTime + 0.5f * Physics.gravity * fixedDeltaTime * fixedDeltaTime;
				vector3 += Physics.gravity * fixedDeltaTime;
				RaycastHit raycastHit2;
				if (Physics.Linecast(list[i - 1], vector, out raycastHit2, RayMasks.DAMAGE_CLIENT))
				{
					list.Add(raycastHit2.point);
					break;
				}
				list.Add(vector);
			}
			return list;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000D288 File Offset: 0x0000B488
		public static List<Vector3> PlotTrajectoryRigidBodyInMotion(GameObject obj, int maxSteps)
		{
			Vector3 vector = obj.transform.position;
			Vector3 vector2 = obj.GetComponent<Rigidbody>().velocity;
			List<Vector3> list = new List<Vector3> { obj.transform.position };
			float fixedDeltaTime = Time.fixedDeltaTime;
			for (int i = 1; i < maxSteps; i++)
			{
				vector += vector2 * fixedDeltaTime + 0.5f * Physics.gravity * fixedDeltaTime * fixedDeltaTime;
				vector2 += Physics.gravity * fixedDeltaTime;
				RaycastHit raycastHit;
				if (Physics.Linecast(list[i - 1], vector, out raycastHit, RayMasks.DAMAGE_CLIENT))
				{
					list.Add(raycastHit.point);
					break;
				}
				list.Add(vector);
			}
			return list;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000D34C File Offset: 0x0000B54C
		public static List<Vector3> PlotTrajectoryRocket(UseableGun gun, out RaycastHit hit, int maxSteps)
		{
			hit = default(RaycastHit);
			Vector3 vector = OptimizationVariables.MainPlayer.look.aim.position;
			Vector3 vector2 = OptimizationVariables.MainPlayer.look.aim.forward * gun.equippedGunAsset.ballisticForce;
			float mass = gun.equippedGunAsset.projectile.GetComponent<Rigidbody>().mass;
			Vector3 vector3 = vector2 / mass * Time.fixedDeltaTime;
			List<Vector3> list = new List<Vector3> { vector };
			RaycastHit raycastHit;
			if (!Physics.Raycast(new Ray(vector, OptimizationVariables.MainPlayer.look.aim.forward), out raycastHit, 1f, RayMasks.DAMAGE_SERVER, QueryTriggerInteraction.UseGlobal))
			{
				vector += OptimizationVariables.MainPlayer.look.aim.forward;
				list.Add(vector);
			}
			float fixedDeltaTime = Time.fixedDeltaTime;
			for (int i = 1; i < maxSteps; i++)
			{
				vector += vector3 * fixedDeltaTime + 0.5f * Physics.gravity * fixedDeltaTime * fixedDeltaTime;
				vector3 += Physics.gravity * fixedDeltaTime;
				if (Physics.Linecast(list[i - 1], vector, out hit, RayMasks.DAMAGE_CLIENT))
				{
					list.Add(hit.point);
					break;
				}
				list.Add(vector);
			}
			return list;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000D4A8 File Offset: 0x0000B6A8
		public static List<Vector3> PlotTrajectoryGun(UseableGun gun, out RaycastHit hit, int maxSteps = 255)
		{
			hit = default(RaycastHit);
			Transform transform = ((OptimizationVariables.MainPlayer.look.perspective == EPlayerPerspective.FIRST) ? OptimizationVariables.MainPlayer.look.aim : OptimizationVariables.MainCam.transform);
			Vector3 vector = transform.position;
			Vector3 forward = transform.forward;
			ItemGunAsset equippedGunAsset = gun.equippedGunAsset;
            float num = equippedGunAsset.bulletGravityMultiplier;
            Attachments attachments = (Attachments)TrajectoryComponent.thirdAttachmentsField.GetValue(gun);
			List<Vector3> list = new List<Vector3> { vector };
			if (((attachments != null) ? attachments.barrelAsset : null) != null)
			{
				num *= attachments.barrelAsset.ballisticDrop;
			}
			for (int i = 1; i < maxSteps; i++)
			{
				vector += forward * equippedGunAsset.ballisticTravel;
				forward.y -= num;
				forward.Normalize();
				if (Physics.Linecast(list[i - 1], vector, out hit, RayMasks.DAMAGE_CLIENT))
				{
					list.Add(hit.point);
					break;
				}
				list.Add(vector);
			}
			return list;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000027F9 File Offset: 0x000009F9
		[OnSpy]
		public static void OnSpy()
		{
			if (TrajectoryComponent.Highlighted != null)
			{
				TrajectoryComponent.RemoveHighlight(TrajectoryComponent.Highlighted);
			}
			TrajectoryComponent.spying = true;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00002818 File Offset: 0x00000A18
		[OffSpy]
		public static void OffSpy()
		{
			TrajectoryComponent.spying = false;
		}

		// Token: 0x0400010D RID: 269
		private static readonly FieldInfo thirdAttachmentsField = typeof(UseableGun).GetField("thirdAttachments", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x0400010E RID: 270
		private static readonly FieldInfo swingModeField = typeof(UseableThrowable).GetField("swingMode", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x0400010F RID: 271
		private static Highlighter highlighted;

		// Token: 0x04000111 RID: 273
		private static bool spying;
	}
}
