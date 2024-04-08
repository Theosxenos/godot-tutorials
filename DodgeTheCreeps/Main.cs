using Godot;
using System;

public partial class Main : Node
{
	[Export] public PackedScene MobScene { get; set; }

	private int score;

	public void GameOver()
	{
		GetNode<Timer>("MobTimer").Stop();
		GetNode<Timer>("ScoreTimer").Stop();
		
		GetNode<Hud>("HUD").GameOver();
		
		GetNode<AudioStreamPlayer>("Music").Stop();
		GetNode<AudioStreamPlayer>("DeathSound").Play();
	}

	public void NewGame()
	{
		score = 0;
		GetTree().CallGroup("mobs", Node.MethodName.QueueFree);

		var player = GetNode<Player>("Player");
		var startPosition = GetNode<Marker2D>("StartPosition");
		player.Start(startPosition.Position);

		GetNode<Timer>("StartTimer").Start();

		var hud = GetNode<Hud>("HUD");
		hud.ShowToast("Get Ready!");
		hud.UpdateScore(score);
		
		GetNode<AudioStreamPlayer>("Music").Play();
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
		GetNode<Hud>("HUD").UpdateScore(++score);
	}

	public void OnStartTimerTimeout()
	{
		GetNode<Timer>("MobTimer").Start();
		GetNode<Timer>("ScoreTimer").Start();
	}
}
