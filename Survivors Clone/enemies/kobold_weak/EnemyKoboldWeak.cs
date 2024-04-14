using Godot;
using System;

public partial class EnemyKoboldWeak : CharacterBody2D
{
	[Export] public float MovementSpeed { get; set; } = 20f;

	[Export] public Player player;

	private string lastAnimation = "walk_right";
	private AnimationPlayer animationPlayer;
	public override void _Ready()
	{
		player = GetTree().GetFirstNodeInGroup("wizzard") as Player;
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
	}

	public override void _PhysicsProcess(double delta)
	{
		var direction = GlobalPosition.DirectionTo(player.GlobalPosition);
		
		if (direction != Vector2.Zero && direction.X == 0)
		{
			animationPlayer.Play(lastAnimation);
		}
		else if (direction != Vector2.Zero)
		{
			lastAnimation = direction.X > 0 ? "walk_right" : "walk_left";
			animationPlayer.Play(lastAnimation);
		}
		else
		{
			animationPlayer.Pause();
		}
		
		Velocity = direction * MovementSpeed;
		MoveAndSlide();
	}
}
