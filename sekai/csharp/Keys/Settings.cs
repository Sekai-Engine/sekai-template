using Godot;
using System;

public partial class Settings : Control
{
	[Export]
	private HSlider _soundsVolumeSlider;
	[Export]
	private HSlider _musicVolumeSlider;
	[Export]
	private HSlider _gameVolumeSlider;
	[Export]
	private HSlider _speedVolumeSlider;
	[Export]
	private CheckButton _friendlyModeCheck;
	[Export]
	private CheckButton _autoSaveCheck;
	[Export]
	private Button _backMainButton;
	[Export(PropertyHint.FilePath)]
	public string MainScenePath { get; set; }
	
	public override void _Ready()
	{
		// Connect signals
		_soundsVolumeSlider.ValueChanged += OnSoundsVolumeChanged;
		_musicVolumeSlider.ValueChanged += OnMusicVolumeChanged;
		_gameVolumeSlider.ValueChanged += OnGameVolumeChanged;
		_speedVolumeSlider.ValueChanged += OnSpeedVolumeChanged;
		_friendlyModeCheck.Toggled += OnFriendlyModeToggled;
		_autoSaveCheck.Toggled += OnAutoSaveToggled;
		_backMainButton.Pressed += OnBackMainButtonPressed;
	}

	private void OnSoundsVolumeChanged(double value)
	{
		int busIndex = AudioServer.GetBusIndex("Sounds");
		AudioServer.SetBusVolumeDb(busIndex, (float)Mathf.LinearToDb(value));
	}

	private void OnMusicVolumeChanged(double value)
	{
		int busIndex = AudioServer.GetBusIndex("Music");
		AudioServer.SetBusVolumeDb(busIndex, (float)Mathf.LinearToDb(value));
	}


	private void OnGameVolumeChanged(double value)
	{
		int busIndex = AudioServer.GetBusIndex("Game");
		AudioServer.SetBusVolumeDb(busIndex, (float)Mathf.LinearToDb(value));
	}

	private void OnSpeedVolumeChanged(double value)
	{
		Global.text_speed = (float)value;
	}

	private void OnFriendlyModeToggled(bool toggled)
	{
		GD.Print($"Wholesome Mode: {toggled}");
	}

	private void OnAutoSaveToggled(bool toggled)
	{
		GD.Print($"Auto Save: {toggled}");
	}

	private void OnBackMainButtonPressed()
	{
		Global.KeysState = null;
		GetTree().ChangeSceneToFile(MainScenePath);	
	}
}
