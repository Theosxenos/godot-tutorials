using Godot;

public partial class StaticEnemy : Area2D
{
	[Signal]
	public delegate void HitEventHandler();

	[Export] public int Lives { get; set; } = 1;
	[Export] public int Speed { get; set; } = 200;
	
	public override void _Process(double delta)
	{
		Position += Vector2.Left * Speed * (float)delta;
	}
}
