using Godot;
using System;

public partial class MainController : Node
{
		
	[Export] public int Lives { get; set; } = 3;

	public override void _Ready()
	{
		GetNode<EnemySpawner>("EnemySpawner").Start();
	}

	private void OnPlayerHit()
	{
		if (--Lives <= 0)
		{
			GameOver();
		}
	}

	private void GameOver()
	{
		GetNode<Player>("Player").QueueFree();
	}

	private void OnEnemySpawnerEnemyKilled()
	{
		// Replace with function body.
	}
}