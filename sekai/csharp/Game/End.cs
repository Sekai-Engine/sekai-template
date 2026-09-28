using Godot;
using System;

public partial class End : Control
{
	Tween tween;
	[Export]
	private AudioStreamPlayer _musicNode;
	[Export]
	private Sprite2D _endTextureNode;
	[Export]
	private RichTextLabel _richTextLabelNode;
	[Export(PropertyHint.FilePath)]
	public string MainScenePath { get; set; }

	public override void _Ready()
	{
		SetJson();

		_musicNode.Play();
	}

	private void SetJson()
	{
		string musicPath = ToolsInit.FindInitValue<string>("end", "music", "stream", "思念,交织于世界彼端.mp3");
		float musicVolumeDb = ToolsInit.FindInitValue<float>("end", "music", "volume_db", _musicNode.VolumeDb);
		_musicNode.Stream = Tools.LoadAudio($"./sounds/{musicPath}");
		_musicNode.VolumeDb = musicVolumeDb;
		
		string labelText = ToolsInit.FindInitValue<string>("end", "label", "text", "Sekai引擎\n让世界成为你的舞台");
		float labelSpeed = ToolsInit.FindInitValue<float>("end", "label", "speed", 0.1f);
		_richTextLabelNode.Text = labelText;
		_richTextLabelNode.Position = new Vector2(0, (float)Global.window_height);
		
		string imagePath = ToolsInit.FindInitValue<string>("end", "image", "texture", "start_texture.png");
		Tools.SetTexture(_endTextureNode, imagePath);

		float labelY = _richTextLabelNode.Size[1];
		tween = GetTree().CreateTween();
		tween.TweenProperty(_richTextLabelNode, "position", new Vector2(0, -labelY), labelSpeed*labelY);
	}

		public override void _Input(InputEvent @event)
		{
				if (@event is InputEventKey keyEvent)
				{
						if (keyEvent.Pressed)
						{
				GetTree().ChangeSceneToFile(MainScenePath);

						}
				}
				else if (@event is InputEventMouseButton mouseButtonEvent)
				{
						if (mouseButtonEvent.Pressed)
						{
				GetTree().ChangeSceneToFile(MainScenePath);
						}
				}
		}
}
