using Godot;
using System;

public partial class Main : Node2D
{
	[Export]
	private Sprite2D _startTextureNode;
	[Export]
	private CpuParticles2D _cpuParticles2DNode;
	[Export]
	private AudioStreamPlayer _musicNode;
	[Export]
	private AudioStreamPlayer _soundsNode;
	[Export]
	private BoxContainer _boxContainer;

	[Export]
	private Label _titleNode;
	[Export]
	private Label _subTitleNode;

	public override void _Ready()
	{
		SetJson();
		Tools.SetTexture(_startTextureNode,"start_texture");
		_cpuParticles2DNode.Texture = Tools.LoadImage("./image/particle.png");
		_boxContainer.MouseExited += OnBoxContainerMouseExited;
	}

	private void SetJson()
	{
		string titleText = ToolsInit.FindInitValue<string>("start", "title", "text", _titleNode.Text);
		Color titleColor = ToolsInit.FindInitColor("start", "title", "font_color", _titleNode.GetThemeColor("font_color", "Label"));
		Color titleOutlineColor = ToolsInit.FindInitColor("start", "title", "font_outline_color", _titleNode.GetThemeColor("font_outline_color", "Label"));
		int titleSize = ToolsInit.FindInitValue<int>("start", "title", "font_size", _titleNode.GetThemeFontSize("font_size", "Label"));
		_titleNode.Text = titleText;
		_titleNode.AddThemeColorOverride("font_color", titleColor);
		_titleNode.AddThemeColorOverride("font_outline_color", titleOutlineColor);
		_titleNode.AddThemeFontSizeOverride("font_size", titleSize);
		string subTitleText = ToolsInit.FindInitValue<string>("start", "subtitle", "text", _subTitleNode.Text);
		Color subTitleColor = ToolsInit.FindInitColor("start", "subtitle", "font_color", _subTitleNode.GetThemeColor("font_color", "Label"));
		Color subTitleOutlineColor = ToolsInit.FindInitColor("start", "subtitle", "font_outline_color", _subTitleNode.GetThemeColor("font_outline_color", "Label"));
		int subTitleSize = ToolsInit.FindInitValue<int>("start", "subtitle", "font_size", _subTitleNode.GetThemeFontSize("font_size", "Label"));
		_subTitleNode.Text = subTitleText;
		_subTitleNode.AddThemeColorOverride("font_color", subTitleColor);
		_subTitleNode.AddThemeColorOverride("font_outline_color", subTitleOutlineColor);
		_subTitleNode.AddThemeFontSizeOverride("font_size", subTitleSize);
		string musicPath = ToolsInit.FindInitValue<string>("start", "music", "stream", "思念,交织于世界彼端.mp3");
		float musicVolumeDb = ToolsInit.FindInitValue<float>("start", "music", "volume_db", _musicNode.VolumeDb);
		_musicNode.Stream = Tools.LoadAudio($"./sounds/{musicPath}");
		_musicNode.VolumeDb = musicVolumeDb;
		_musicNode.Play();
		Color themeColor = ToolsInit.FindInitColor("main", "theme", "color", _cpuParticles2DNode.Modulate);
		_cpuParticles2DNode.Modulate = themeColor;
	}
	
	void OnBoxContainerMouseExited()
	{
		_soundsNode.Play();
	}
}
