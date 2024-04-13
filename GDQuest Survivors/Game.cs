using Godot;
using System;

public partial class Game : Node
{
	[Export] private PackedScene MobScene { get; set; }

	public override void _Ready()
	{
		GD.Randomize();
	}

	void SpawnMob()
	{
		var spawnPath = GetNode<PathFollow2D>("%SpawnPoints");
		spawnPath.ProgressRatio = GD.Randf();
		
		var mob = MobScene.Instantiate<Mob>();
		mob.GlobalPosition = spawnPath.GlobalPosition;
		
		GetNode("Level").AddChild(mob);
	}

	void OnMobSpawnTimerTimeout()
	{
		SpawnMob();
	}

	void OnPlayerPlayerHealthDepleted()
	{
		GetNode<CanvasLayer>("%GameOver").Show();
		GetTree().Paused = true;
	}
}
