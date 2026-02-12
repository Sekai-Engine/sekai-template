using Godot;
using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public partial class Tools : Node
{
	/// <summary>
	/// Why avoid `expand_mode` for image scaling:
	/// Godot lacks native support for dynamically selecting the stretching axis. Using built-in modes may cause 
	/// Aspect ratio distortion
	/// or
	/// Imbalanced scaling
	/// </summary> 
	public static Texture2D LoadImage(string absolutePath)
	{
		// 始终使用 ResourceLoader 加载资源，确保在编辑器和导出后都能正常工作
		Texture2D texture = ResourceLoader.Load<Texture2D>(absolutePath);
		if (texture == null)
		{
			GD.PrintErr($"加载失败: {absolutePath}");
		}
		return texture;
	}


	/// <summary>
	/// Set Audio.
	/// </summary>
	public static AudioStream LoadAudio(string absolutePath)
	{
		// 始终使用 ResourceLoader 加载资源，确保在编辑器和导出后都能正常工作
		AudioStream audioStream = ResourceLoader.Load<AudioStream>(absolutePath);
		if (audioStream == null)
		{
			GD.PrintErr($"加载失败: {absolutePath}");
		}
		return audioStream;
	}

	public static void SetTexture(Sprite2D sprite, string path)
	{
		// path = "background/start_texture", sprite = start_texture
		var texture = Tools.LoadImage($"res://image/{path}.png") as Texture2D
			?? Tools.LoadImage($"res://image/{path}.jpg") as Texture2D;
		if (texture == null)
		{
			GD.PrintErr($"`res://image/` Failed to load `{path}.png` or `{path}.jpg`.");
		}
		else
		{
			sprite.Texture = texture;
			float width = (float)Global.window_width / (float)texture.GetWidth();
			float height = (float)Global.window_height / (float)texture.GetHeight();
			if (height > width)
			{
				sprite.Scale = new Vector2(height, height);
			}
			else
			{
				sprite.Scale = new Vector2(width, width);
			}
		}
	}

	/*
	   public static void SetTexture(TextureRect textureRect, string path)
	   {
	   var texture = Tools.LoadImage($"res://image/{path}.png") as Texture2D
	   ?? Tools.LoadImage($"res://image/{path}.jpg") as Texture2D;

	   if (texture == null)
	   {
	   GD.PrintErr($"`res://image/` Failed to load `{path}.png` or `{path}.jpg`.");
	   }
	   else
	   {
	   textureRect.Texture = texture;
	   }
	   }
	   */

	// todo: only remove like "[url]" & "[/url]", while [example] will not remove.
	public static string RemoveBBCode(string input)
	{
		return Regex.Replace(input, @"\[\/?[^\]]+\]", "");
	}
}

public partial class ToolsInit : Node
{
	public static T FindInitValue<T>(string scene, string node, string key, T defaultValue)
	{
		string jsonString = FlowData.jsonString;
		if (!FlowData.IsBuild)
		{
			string filePath = "./script/.init.json";
			try
			{
				using FileAccess file = FileAccess.Open(filePath, FileAccess.ModeFlags.Read);
				if (file == null)
				{
					return defaultValue;
				}
				jsonString = file.GetAsText();
			}
			catch
			{
				return defaultValue;
			}
		}
		using JsonDocument doc = JsonDocument.Parse(jsonString);
		JsonElement rootElement = doc.RootElement;
		if (!rootElement.TryGetProperty(scene, out JsonElement sceneElement))
			return defaultValue;
		if (!sceneElement.TryGetProperty(node, out JsonElement nodeElement))
			return defaultValue;
		if (!nodeElement.TryGetProperty(key, out JsonElement keyElement))
			return defaultValue;
		return keyElement.Deserialize<T>();
	}

	public static Color FindInitColor(string scene, string node, string key, Color defaultColor)
	{
		string keyElement = FindInitValue<string>(scene, node, key, null);
		try
		{
			Color result = StringToColor(keyElement);
			return result;
		}
		catch
		{
			return defaultColor;
		}
	}

	private static Color StringToColor(string rgbaString)
	{
		Regex regex = new Regex(@"rgba\(([^,]+),\s*([^,]+),\s*([^,]+),\s*([^)]+)\)");
		Match match = regex.Match(rgbaString);
		if (!match.Success)
		{
			throw new ArgumentException("Invalid rgba string format");
		}
		float r = float.Parse(match.Groups[1].Value.Trim());
		float g = float.Parse(match.Groups[2].Value.Trim());
		float b = float.Parse(match.Groups[3].Value.Trim());
		float a = float.Parse(match.Groups[4].Value.Trim());
		return new Color(r, g, b, a);
	}
}

public partial class LoadGame : Node
{
	public static void SaveGame()
	{
		return;
	}
}
