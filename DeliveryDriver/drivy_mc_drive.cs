using Godot;
using System;

public partial class drivy_mc_drive : CharacterBody2D
{
	[Export]
	public float SteerSpeed { get; set; } = 100;
	[Export]
	public float MoveSpeed { get; set; } = 300;

	public override void _PhysicsProcess(double delta)
	{	
		var steerAmount = Input.GetAxis("ui_left","ui_right");
		var moveDirection = Input.GetAxis("ui_up", "ui_down");

		// if (steerAmount != 0 && moveDirection == 0)
		// {
		// 	moveDirection = -1;
		// }

		RotationDegrees += steerAmount * SteerSpeed * (float)delta;

		var direction = new Vector2(0f, moveDirection * MoveSpeed * (float)delta).Rotated(Rotation);
		// Velocity = Transform.X * moveDirection * MoveSpeed * (float)delta;
		// Position += direction;
		Velocity = Transform.Y * MoveSpeed * moveDirection;
		MoveAndSlide();
	}
}
