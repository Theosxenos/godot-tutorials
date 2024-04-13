using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public override void _PhysicsProcess(double delta)
	{
		var direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");

		Velocity = direction * 600;
		MoveAndSlide();

		var happyBoo = GetNode("%HappyBoo");
		if(direction != Vector2.Zero)
			happyBoo.Call("play_walk_animation");
		else
		{
			happyBoo.Call("play_idle_animation");
		}
	}
}
