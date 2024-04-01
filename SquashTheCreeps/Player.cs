using Godot;
using System;

public partial class Player : CharacterBody3D
{
	// Don't forget to rebuild the project so the editor knows about the new export variable.

	// How fast the player moves in meters per second.
	[Export]
	public int Speed { get; set; } = 14;
	// The downward acceleration when in the air, in meters per second squared.
	[Export]
	public int FallAcceleration { get; set; } = 75;

	private Vector3 targetVelocity = Vector3.Zero;

	public override void _PhysicsProcess(double delta)
	{
		var direction = new Vector3(
			(Input.IsActionPressed("move_right") ? 1.0f : 0) - (Input.IsActionPressed("move_left") ? 1.0f : 0),
			0, // Assuming you're not modifying the Y axis for up/down movement in this snippet
			(Input.IsActionPressed("move_back") ? 1.0f : 0) - (Input.IsActionPressed("move_forward") ? 1.0f : 0)
		);

		if (direction != Vector3.Zero)
		{
			direction = direction.Normalized();
			// Setting the basis property will affect the rotation of the node.
			GetNode<Node3D>("Pivot").Basis = Basis.LookingAt(direction);
		}
		
		// Ground velocity
		targetVelocity.X = direction.X * Speed;
		targetVelocity.Z = direction.Z * Speed;
		
		// Vertical velocity
		if (!IsOnFloor()) // If in the air, fall towards the floor. Literally gravity
		{
			targetVelocity.Y -= FallAcceleration * (float)delta;
		}
		
		// Moving the character
		Velocity = targetVelocity;
		MoveAndSlide();
	}
}
