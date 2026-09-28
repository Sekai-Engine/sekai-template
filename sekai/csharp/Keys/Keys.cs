using Godot;
using System;

public partial class Keys : Control
{
	[Export]
	public Control TechnicalScene;
	[Export]
	private Game _gameScene;
	[Export]
	public Button BackNode;
	[Export]
	private Button _historyNode;
	[Export]
	private Button _settingsNode;
	[Export]
	private Control _settingsScene;
	[Export]
	public BoxContainer BoxContainerNode;
	
	private static ColorRect _backgroundNode;
	public static History _historyScene;
	
	public override void _Ready()
	{
		_backgroundNode = GetNode<ColorRect>("Background");
		_historyScene = GetNode<History>("Background/History");

		BoxContainerNode.Position = new Vector2(
					Global.window_width - 30 - BoxContainerNode.Size.X,
					30
				);
		BackNode.Pressed += OnBackPressed;
		_historyNode.Pressed += OnHistoryPressed;
		_settingsNode.Pressed += OnSettingsPressed;
	}

	public void OnBackPressed()
	{
		BackgroundPressed(Global.KeysState);
	}

	public void OnHistoryPressed()
	{
		BackgroundPressed("History");
	}

	public void OnSettingsPressed()
	{
		BackgroundPressed("Settings");
	}

	public void BackgroundPressed(string scene)
	{	
		foreach (Control child in _backgroundNode.GetChildren())
		{
			child.Hide();
		}
		if ( Global.KeysState == scene )
		{
			_backgroundNode.Hide();
			Global.KeysState = null;
		}
		else
		{
			_backgroundNode.Show();
			switch (scene)
			{
				case "History":
					BackNode.Show();
					_historyScene.Show();
					_historyScene.Text = _historyScene.loadFlowText(_gameScene.Datas);
					break;
				case "Technical":
					BackNode.Show();
					TechnicalScene.Show();
					break;
				case "Settings":
					BackNode.Show();
					_settingsScene.Show();
					break;
				default:
					GD.PrintErr($"unexpected scene's type: {scene}");
					break;
			}
			Global.KeysState = scene;
		}
	}

}
