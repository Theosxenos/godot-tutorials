using Godot;
using System;

public partial class MainController : Node
{
	public override void _Ready()
	{
		GetNode<EnemySpawner>("EnemySpawner").Start();
	}

	private void OnPlayerHit()
	{
		// Replace with function body.
	}
}


