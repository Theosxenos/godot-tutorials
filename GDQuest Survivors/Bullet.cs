using Godot;
using System;

public partial class Bullet : Area2D
{
	[Export] public int Speed { get; set; } = 1000;
	[Export] public int Range { get; set; } = 1200;

	private float travelledDistance = 0;

	public override void _PhysicsProcess(double delta)
	{
		var direction = Vector2.Right.Rotated(Rotation);
		Position += direction * Speed * (float)delta;

		travelledDistance += Speed * (float)delta;
		if(travelledDistance >= Range)
			QueueFree();
	}

	void OnBodyEntered(Node2D body)
	{
		QueueFree();
		if (body is Mob mob)
		{
			mob.TakeDamage();
		}
	}
}
