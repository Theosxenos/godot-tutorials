using Godot;
using System;

public partial class UiController : Node
{
	private Label scoreLabel;

	[Export] public PackedScene LifeScene { get; set; }
	public override void _Ready()
	{
		scoreLabel = GetNode<Label>("CanvasLayer/Score");
	}

	public void SetScore(int score)
	{
		scoreLabel.Text = $"Score: {score}";
	}

	public void SetLives(int lives)
	{
		var lifePosition = GetNode<Marker2D>("CanvasLayer/LifePosition");
		
		if(lifePosition.GetChildren().Count > 0)
			GetTree().CallGroup("lives", "queue_free");

		var margin = 10;
		for (int i = 0; i < lives; i++)
		{
			var life = LifeScene.Instantiate<Sprite2D>();
			lifePosition.AddChild(life);
			life.Position = new Vector2(i * life.Texture.GetWidth() + margin, 0);
		}
	}
}
