using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public int Speed { get; set; } = 300;

	public override void _PhysicsProcess(double delta)
	{
		Movement();
	}

	void Movement()
	{
		var direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");

		Velocity = direction * Speed;// * (float)delta;
		MoveAndSlide();
	}
}
