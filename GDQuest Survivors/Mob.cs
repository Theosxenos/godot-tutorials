using Godot;
using System;

public partial class Mob : CharacterBody2D
{
	[Export] public int Health { get; set; } = 3;
	
	private Player player;
	private Node2D slime;
	
	public override void _Ready()
	{
		player = GetParent().GetNode<Player>("Player");
		slime = GetNode<Node2D>("Slime");
		slime.Call("play_walk");
	}

	public override void _PhysicsProcess(double delta)
	{
		var direction = GlobalPosition.DirectionTo(player.GlobalPosition);
		Velocity = direction * 300;
		MoveAndSlide();
	}

	public void TakeDamage()
	{
		slime.Call("play_hurt");

		Health--;
		if (Health > 0) return;

		var smokeEffect = ResourceLoader.Load<PackedScene>("res://smoke_explosion/smoke_explosion.tscn").Instantiate<Node2D>();
		GetParent().AddChild(smokeEffect);

		smokeEffect.GlobalPosition = GlobalPosition;
		
		QueueFree();
	}
}
