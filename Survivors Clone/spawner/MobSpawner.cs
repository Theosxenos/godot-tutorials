using Godot;
using System;
using Godot.Collections;

public partial class MobSpawner : Node2D
{
	[Export] public float SpawnRadius { get; set; } = 400;
	[Export] public SpawnInfo[] Spawns { get; set; }
	[Export] private bool enabled = false;
	
	private Timer spawnTimer;
	private int time;

	public override void _Ready()
	{
		spawnTimer = GetNode<Timer>("SpawnTimer");
		spawnTimer.Timeout += SpawnTimerOnTimeout;
	}

	private void SpawnTimerOnTimeout()
	{
		if(!enabled) return;

		time++;
		
		var spawns = Spawns;

		foreach (var spawn in spawns)
		{
			if (time < spawn.TimeStart || time > spawn.TimeStop) continue;
			
			if (spawn.SpawnDelayCounter < spawn.SpawnDelay)
			{
				spawn.SpawnDelayCounter++;
			}
			else
			{
				spawn.SpawnDelayCounter = 0;
				for (var i = 0; i < spawn.EnemyNumbers; i++)
				{
					var enemy = spawn.Enemy.Instantiate() as Node2D;
					enemy!.Position = Position + GetRandomPosition();
					GetParent().AddChild(enemy);
				}
			}
		}
	}

	private Vector2 GetRandomPosition()
	{
		var randomAngle = GD.RandRange(0, Mathf.Tau);
		return new Vector2(SpawnRadius, 0).Rotated((float)randomAngle);
	}
}
