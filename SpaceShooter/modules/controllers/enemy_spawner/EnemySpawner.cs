using Godot;
using System;

public partial class EnemySpawner : Node2D
{
	[Export] public PackedScene[] Enemies { get; set; }
	
	public void Start()
	{
		GetNode<Timer>("SpawnTimer").Start();
	}

	private void OnSpawnTimerTimeout()
	{
		var enemyScene = Enemies[GD.RandRange(0, Enemies.Length - 1)];
		var enemy = enemyScene.Instantiate<Area2D>();

		var topPosition = GetNode<Marker2D>("TopPosition").Position;
		var bottomPosition = GetNode<Marker2D>("BottomPosition").Position;

		enemy.Position = new Vector2(topPosition.X, GD.RandRange((int)topPosition.Y, (int)bottomPosition.Y));
		AddChild(enemy);
	}
}


