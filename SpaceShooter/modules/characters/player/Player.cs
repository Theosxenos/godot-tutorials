using System;
using Godot;
using SpaceShooter.modules.shared.interfaces;

public partial class Player : Area2D, IHitable
{
	[Signal]
	public delegate void HitEventHandler();
	
	[Export] public int Speed { get; set; } = 400;
	[Export] public PackedScene Projectile { get; set; }
	
	public override void _Process(double delta)
	{
		var direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		Position += direction * Speed * (float)delta;
	}

	private void OnAreaEntered(Area2D area)
	{
		GD.Print("Area entered");
	}

	void IHitable.Hit()
	{
		throw new NotImplementedException();
	}
}
