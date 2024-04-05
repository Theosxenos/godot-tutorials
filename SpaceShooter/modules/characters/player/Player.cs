using System;
using Godot;

public partial class Player : Area2D
{
	[Signal]
	public delegate void HitEventHandler();
	
	[Export] public int Speed { get; set; } = 400;
	
	public override void _Process(double delta)
	{
		var direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		Position += direction * Speed * (float)delta;
	}

	private void OnAreaEntered(Area2D area)
	{
		GD.Print("Area entered");
	}
}
