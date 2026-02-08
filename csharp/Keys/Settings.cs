using Godot;
using System;

public partial class Setting : Control
{
	private HSlider _volumeSlider;
	private HSlider _speedSlider;
	private CheckButton _wholesomeModeCheck;
	private CheckButton _autoSaveCheck;

	public override void _Ready()
	{
		// Get nodes
		_volumeSlider = GetNode<HSlider>("ScrollContainer/VBoxContainer/VolumeContainer/VolumeSlider");
		_speedSlider = GetNode<HSlider>("ScrollContainer/VBoxContainer/SpeedContainer/SpeedSlider");
		_wholesomeModeCheck = GetNode<CheckButton>("ScrollContainer/VBoxContainer/WholesomeModeCheck");
		_autoSaveCheck = GetNode<CheckButton>("ScrollContainer/VBoxContainer/AutoSaveCheck");

		// Connect signals
		_volumeSlider.ValueChanged += OnVolumeChanged;
		_speedSlider.ValueChanged += OnSpeedChanged;
		_wholesomeModeCheck.Toggled += OnWholesomeModeToggled;
		_autoSaveCheck.Toggled += OnAutoSaveToggled;
	}

	private void OnVolumeChanged(double value)
	{
		GD.Print($"Volume changed to: {value}");
		// Update Master bus volume
		int busIndex = AudioServer.GetBusIndex("Master");
		// LinearToDb converts linear volume (0-1) to decibels
		// Use Mathf.LinearToDb if available or simple conversion
		AudioServer.SetBusVolumeDb(busIndex, (float)Mathf.LinearToDb(value));
	}

	private void OnSpeedChanged(double value)
	{
		GD.Print($"Speed changed to: {value}");
		// Store speed setting
	}

	private void OnWholesomeModeToggled(bool toggled)
	{
		GD.Print($"Wholesome Mode: {toggled}");
		// Store wholesome mode setting
	}

	private void OnAutoSaveToggled(bool toggled)
	{
		GD.Print($"Auto Save: {toggled}");
		// Store auto save setting
	}

	public override void _Process(double delta)
	{
	}
}
