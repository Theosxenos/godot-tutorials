using Godot;
using System;

public partial class Main : Node
{
	[Export] public PackedScene MobScene { get; set; }

	private int score;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		NewGame();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void GameOver()
	{
		GetNode<Timer>("MobTimer").Stop();
		GetNode<Timer>("ScoreTimer").Stop();
	}

	public void NewGame()
	{
		score = 0;

		var player = GetNode<Player>("Player");
		var startPosition = GetNode<Marker2D>("StartPosition");
		player.Start(startPosition.Position);

		GetNode<Timer>("StartTimer").Start();
	}
	
	public void OnMobTimerTimeout()
	{
		// Load the GDScript
		// var mobScript = (GDScript)GD.Load("res://mob.gd");

		// Instantiate the Mob
		var mobInstance = MobScene.Instantiate<Mob>();

		// Choose a random location on Path2D
		var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
		mobSpawnLocation.ProgressRatio = GD.Randf();

		// Set the mob's direction perpendicular to the path direction.
		var direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;

		// Set the mob's position to a random location.
		mobInstance.Position = mobSpawnLocation.Position;

		var rad = Mathf.DegToRad(45d);
		var pistuff = Mathf.Pi / 4;
		
		// Add some randomness to the direction.
		direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
		mobInstance.Rotation = direction;

		// Choose the velocity.
		var velocity = new Vector2((float)GD.RandRange(150.0, 250.0), 0);
		mobInstance.LinearVelocity = velocity.Rotated(direction);

		// Spawn the mob by adding it to the Main scene.
		AddChild(mobInstance);
	}
	
	public void OnScoreTimerTimeout()
	{
		score++;
	}
	
	public void OnStartTimerTimeout()
	{
		GetNode<Timer>("MobTimer").Start();
		GetNode<Timer>("ScoreTimer").Start();
	}

}
