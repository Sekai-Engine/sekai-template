using Godot;
using System;

public partial class End : Control
{
	[Export]
	private AudioStreamPlayer _musicNode;
	[Export]
	private Sprite2D _endTextureNode;

	public override void _Ready()
	{
		Tools.SetTexture(_endTextureNode, "end_texture");
		  	SetJson();
	}

	private void SetJson()
	{
		string musicPath = ToolsInit.FindInitValue<string>("end", "music", "stream", "思念,交织于世界彼端.mp3");
		float musicVolumeDb = ToolsInit.FindInitValue<float>("end", "music", "volume_db", _musicNode.VolumeDb);
		_musicNode.Stream = Tools.LoadAudio($"./sounds/{musicPath}");
		_musicNode.VolumeDb = musicVolumeDb;
		_musicNode.Play();
	}

	public override void _Process(double delta)
	{
	}
}
