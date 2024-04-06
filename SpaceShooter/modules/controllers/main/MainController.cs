using Godot;
using System;

public partial class MainController : Node
{
	[Export] public int Lives { get; set; } = 3;
	[Export] public int Score { get; set; }

	private UiController uiController;
	
	public override void _Ready()
	{
		GetNode<EnemySpawner>("EnemySpawner").Start();
		uiController = GetNode<UiController>("UiController");
		uiController.SetLives(Lives);
	}

	private void OnPlayerHit()
	{
		uiController.SetLives(--Lives);
		
		if (Lives <= 0)
		{
			GameOver();
		}
	}

	private void GameOver()
	{
		GetNode<Player>("Player").QueueFree();
		GetNode<EnemySpawner>("EnemySpawner").Stop();
		GetTree().CallGroup("enemies", "queue_free");
	}

	private void OnEnemySpawnerEnemyKilled()
	{
		uiController.SetScore(++Score);
	}
}