using Godot;
using System;
using System.Threading.Tasks;

public partial class UiController : Node
{
	private Label scoreLabel;
	private Control gameMenu;

	[Signal]
	public delegate void StartGameEventHandler();
	
	[Export] public PackedScene LifeScene { get; set; }
	public override void _Ready()
	{
		gameMenu = GetNode<Control>("CanvasLayer/GameMenu");
		scoreLabel = GetNode<Label>("CanvasLayer/HUD/Score");
	}

	public void SetScore(int score)
	{
		scoreLabel.Text = $"Score: {score}";
	}

	public void SetLives(int lives)
	{
		var lifePosition = GetNode<Marker2D>("CanvasLayer/HUD/LifePosition");
		
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
	
	private void OnStartButtonPressed()
	{
		gameMenu.Hide();
		EmitSignal(SignalName.StartGame);
	}

	public async Task GameOver()
	{
		gameMenu.Show();
		var label = GetNode<Label>("CanvasLayer/GameMenu/Panel/Message");
		label.Text = "Game Over";
		await ToSignal(GetTree().CreateTimer(1.5f), Timer.SignalName.Timeout);
		label.Text = "Space Shooter";
	}
}


