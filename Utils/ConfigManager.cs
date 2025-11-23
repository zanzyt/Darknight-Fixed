using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZahidAGA
{
	// Token: 0x0200005F RID: 95
	public class ConfigManager
	{
		// Token: 0x06000232 RID: 562 RVA: 0x000038BD File Offset: 0x00001ABD
		public static void Init()
		{
			Directory.CreateDirectory(ConfigManager.appdata + "\\DarkNight");
			ConfigManager.LoadConfig(ConfigManager.GetConfig());
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00013338 File Offset: 0x00011538
		public static Dictionary<string, object> CollectConfig()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { 
			{
				"Version",
				ConfigManager.ConfigVersion
			} };
			foreach (Type type in (from T in Assembly.GetExecutingAssembly().GetTypes()
				where T.IsClass
				select T).ToArray<Type>())
			{
				foreach (FieldInfo fieldInfo in (from F in type.GetFields()
					where F.IsDefined(typeof(SaveAttribute), false)
					select F).ToArray<FieldInfo>())
				{
					dictionary.Add(type.Name + "_" + fieldInfo.Name, fieldInfo.GetValue(null));
				}
			}
			return dictionary;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00013414 File Offset: 0x00011614
		public static Dictionary<string, object> GetConfig()
		{
			if (!File.Exists(ConfigManager.ConfigPath))
			{
				ConfigManager.SaveConfig();
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>(); 
			try
			{
				dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText(ConfigManager.ConfigPath), new JsonSerializerSettings
				{
					Formatting = Formatting.Indented
				});
			}
			catch
			{
				ConfigManager.SaveConfig();
			}
			return dictionary;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x000038DE File Offset: 0x00001ADE
		public static void SaveConfig()
		{
			ColorOptions.ColorDict = ColorOptions.ColorDict;
			File.WriteAllText(ConfigManager.ConfigPath, JsonConvert.SerializeObject(ConfigManager.CollectConfig(), Formatting.Indented));
			T.AddNotification("Save Config - " + ConfigManager.current);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00013474 File Offset: 0x00011674
		public static void LoadConfig(Dictionary<string, object> Config)
		{
			if (File.Exists(ConfigManager.ConfigPath))
			{
				foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
				{
					foreach (FieldInfo fieldInfo in from f in type.GetFields()
						where Attribute.IsDefined(f, typeof(SaveAttribute))
						select f)
					{
						string text = string.Format("{0}_{1}", type.Name, fieldInfo.Name);
						Type fieldType = fieldInfo.FieldType;
						object value = fieldInfo.GetValue(null);
						if (!Config.ContainsKey(text))
						{
							Config.Add(text, value);
						}
						try
						{
							if (Config[text].GetType() == typeof(JArray))
							{
								Config[text] = ((JArray)Config[text]).ToObject(fieldInfo.FieldType);
							}
							if (Config[text].GetType() == typeof(JObject))
							{
								Config[text] = ((JObject)Config[text]).ToObject(fieldInfo.FieldType);
							}
							fieldInfo.SetValue(null, fieldInfo.FieldType.IsEnum ? Enum.ToObject(fieldInfo.FieldType, Config[text]) : Convert.ChangeType(Config[text], fieldInfo.FieldType));
						}
						catch
						{
							Config[text] = value;
						}
					}
				}
				Main.Initialize();
				T.AddNotification("Load Config - " + ConfigManager.current);
			}
		}

		// Token: 0x040001AD RID: 429
		public static string ConfigPath = string.Format("{0}/DarkNight/DarkNightDefault.cfg", Environment.ExpandEnvironmentVariables("%appdata%"));

		// Token: 0x040001AE RID: 430
		public static string appdata = Environment.ExpandEnvironmentVariables("%appdata%/");

		// Token: 0x040001AF RID: 431
		public static string current = "DarkNightDefault";

		// Token: 0x040001B0 RID: 432
		public static string ConfigVersion = "1.0.2";
	}
}
