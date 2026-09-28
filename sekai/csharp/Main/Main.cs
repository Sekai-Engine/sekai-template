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
	[Export(PropertyHint.FilePath)]
	public string DefaultFontPath { get; set; }

	public override void _Ready()
	{
		SetJson();
		_boxContainer.MouseExited += OnBoxContainerMouseExited;
	}

	private void SetJson()
	{
		Color themeColor = ToolsInit.FindInitColor("main", "theme", "color", _cpuParticles2DNode.Modulate);
		_cpuParticles2DNode.Modulate = themeColor;

		string titleText = ToolsInit.FindInitValue<string>("start", "title", "text", _titleNode.Text);
		Color titleColor = ToolsInit.FindInitColor("start", "title", "font_color", _titleNode.GetThemeColor("font_color", "Label"));
		Color titleOutlineColor = ToolsInit.FindInitColor("start", "title", "font_outline_color", _titleNode.GetThemeColor("font_outline_color", "Label"));
		int titleSize = ToolsInit.FindInitValue<int>("start", "title", "font_size", _titleNode.GetThemeFontSize("font_size", "Label"));
		string titleFont = ToolsInit.FindInitValue<string>("start", "title", "font", DefaultFontPath);
		_titleNode.Text = titleText;
		_titleNode.AddThemeColorOverride("font_color", titleColor);
		_titleNode.AddThemeColorOverride("font_outline_color", titleOutlineColor);
		_titleNode.AddThemeFontSizeOverride("font_size", titleSize);
		FontFile titleFontFile = GD.Load<FontFile>(titleFont);
		_titleNode.AddThemeFontOverride("font", titleFontFile);
		
		string subTitleText = ToolsInit.FindInitValue<string>("start", "subtitle", "text", _subTitleNode.Text);
		Color subTitleColor = ToolsInit.FindInitColor("start", "subtitle", "font_color", _subTitleNode.GetThemeColor("font_color", "Label"));
		Color subTitleOutlineColor = ToolsInit.FindInitColor("start", "subtitle", "font_outline_color", _subTitleNode.GetThemeColor("font_outline_color", "Label"));
		int subTitleSize = ToolsInit.FindInitValue<int>("start", "subtitle", "font_size", _subTitleNode.GetThemeFontSize("font_size", "Label"));
		string subTitleFont = ToolsInit.FindInitValue<string>("start", "subtitle", "font", DefaultFontPath);
		_subTitleNode.Text = subTitleText;
		_subTitleNode.AddThemeColorOverride("font_color", subTitleColor);
		_subTitleNode.AddThemeColorOverride("font_outline_color", subTitleOutlineColor);
		_subTitleNode.AddThemeFontSizeOverride("font_size", subTitleSize);
		FontFile subTitleFontFile = GD.Load<FontFile>(subTitleFont);
		_subTitleNode.AddThemeFontOverride("font", subTitleFontFile);
		
		string musicPath = ToolsInit.FindInitValue<string>("start", "music", "stream", "思念,交织于世界彼端.mp3");
		float musicVolumeDb = ToolsInit.FindInitValue<float>("start", "music", "volume_db", _musicNode.VolumeDb);
		_musicNode.Stream = Tools.LoadAudio($"./sounds/{musicPath}");
		_musicNode.VolumeDb = musicVolumeDb;
		_musicNode.Play();
		
		string imagePath = ToolsInit.FindInitValue<string>("start", "image", "texture", "start_texture.png");
		string imageParticles = ToolsInit.FindInitValue<string>("start", "image", "particles", "./image/particle.png");
		Tools.SetTexture(_startTextureNode, imagePath);
		_cpuParticles2DNode.Texture = Tools.LoadImage(imageParticles);
	}
	
	void OnBoxContainerMouseExited()
	{
		_soundsNode.Play();
	}
}
