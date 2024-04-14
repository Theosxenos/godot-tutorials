using Godot;
using System;

public partial class Hitbox : Area2D
{
	[Export] public int Damage { get; set; } = 1;

	[Export] private CollisionShape2D collisionShape2D;
	private Timer timer;

	public override void _Ready()
	{
		timer = GetNode<Timer>("DisableHitBoxTimer");
		timer.Timeout += TimerOnTimeout;
	}

	public override void _ExitTree()
	{
		 base._ExitTree();
		timer.Timeout -= TimerOnTimeout;
	}

	private void TimerOnTimeout()
	{
		collisionShape2D.SetDeferred("disabled", false);
	}

	public void TempDisable()
	{
		collisionShape2D.SetDeferred("disabled", true);
		timer.Start();
	}
}
