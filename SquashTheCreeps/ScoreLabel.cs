using Godot;
using System;

public partial class ScoreLabel : Label
{

	[Export] public string ScoreTextTemplate { get; set; } = "Score: {0}";
	
	private int score;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Text = string.Format(ScoreTextTemplate, score);
	}

	public void OnMobSquashed()
	{
		Text = string.Format(ScoreTextTemplate, ++score);
	}
}
