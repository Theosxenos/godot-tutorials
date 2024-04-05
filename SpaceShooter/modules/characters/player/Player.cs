using System;
using Godot;

public partial class Player : Area2D
{
	[Signal]
	public delegate void HitEventHandler();
	
	[Export] public int Lives { get; set; } = 3;
	[Export] public int Speed { get; set; } = 400;
	[Export] public PackedScene ProjectileScene { get; set; }
	
	public override void _Process(double delta)
	{
		var direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		Position += direction * Speed * (float)delta;

		if (Input.IsActionJustPressed("fire_weapon"))
		{
			var projectile = ProjectileScene.Instantiate<PlayerProjectile>();
			projectile.Position = GetNode<Marker2D>("Weapon").Position;
			
			AddChild(projectile);
		}
	}

	private void OnAreaEntered(Area2D area)
	{
		if (--Lives <= 0)
		{
			QueueFree();
		}

		EmitSignal("Hit");
	}
}
