using Godot;
using System;

public partial class Mob : CharacterBody2D
{
	private Player player;

	public override void _Ready()
	{
		player = GetParent().GetNode<Player>("Player");
	}

	public override void _PhysicsProcess(double delta)
	{
		var direction = GlobalPosition.DirectionTo(player.GlobalPosition);
		Velocity = direction * 300;
		MoveAndSlide();
	}
}
