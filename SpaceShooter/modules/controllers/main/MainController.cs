using Godot;
using System;

public partial class MainController : Node
{
	[Export] public int Lives { get; set; } = 3;
	[Export] public int Score { get; set; }

	private UiController uiController;
	private Player player;
	private Vector2 playerSpawnPosition;

	public override void _Ready()
	{
		uiController = GetNode<UiController>("UiController");
		
		player = GetNode<Player>("Player");
		playerSpawnPosition = player.Position;

		UpdateHud();
	}

	private void UpdateHud()
	{
		uiController.SetLives(Lives);
		uiController.SetScore(Score);
	}

	private void StartGame()
	{
		GetNode<EnemySpawner>("EnemySpawner").Start();

		player.Position = playerSpawnPosition;
		player.SetProcess(true);
		player.Show();

		Lives = 3;
		Score = 0;
		
		UpdateHud();
	}

	private void OnPlayerHit()
	{
		Lives--;
		UpdateHud();
		
		if (Lives <= 0)
		{
			GameOver();
		}
	}

	private void GameOver()
	{
		player.Hide();
		player.SetProcess(false);

		GetNode<EnemySpawner>("EnemySpawner").Stop();
		GetTree().CallGroup("enemies", "queue_free");

		uiController.GameOver();

		GetNode<AudioStreamPlayer>("LoseSound").Play();
	}

	private void OnEnemySpawnerEnemyKilled()
	{
		Score++;
		UpdateHud();
	}
}
