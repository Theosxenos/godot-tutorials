using Godot;
using System;

public partial class Meteor : Area2D
{
	[Export] public int Speed { get; set; } = 100;
	[Export] public int Lives { get; set; } = 5;

	public override void _Process(double delta)
	{
		Position += Vector2.Left * Speed * (float)delta;
	}
}
