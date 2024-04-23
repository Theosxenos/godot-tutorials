using Godot;
using System;

public partial class GameManager : Node
{
	[Export] private Label livesLabel;
	[Export] private Label coinsLabel;

	public static GameManager Instance { get; set; }
	
	private int coins = 40;
	private int lives = 50;

	public override void _Ready()
	{
		Instance = this;

		UpdateUi();
	}

	private void UpdateUi()
	{
		coinsLabel.Text = $"{coins}";
		livesLabel.Text = $"{lives}";
	}
}
