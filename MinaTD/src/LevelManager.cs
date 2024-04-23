using Godot;
using System;

public partial class LevelManager : Node2D
{
	[Export] private PackedScene shipScene;
	[Export] private Path2D path;
	
	private Timer spawnTimer;
	public override void _Ready()
	{
		spawnTimer = GetNode<Timer>("EnemySpawnTimer");
		spawnTimer.Timeout += OnEnemySpawn;
	}

	private void OnEnemySpawn()
	{
		var ship = shipScene.Instantiate();
		path.AddChild(ship);
	}
}
