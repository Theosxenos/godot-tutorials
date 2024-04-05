using Godot;

namespace SpaceShooter.modules.characters.shared;

public partial class Enemy : Area2D
{
	[Signal]
	public delegate void KilledEventHandler();
	[Export] public int Speed { get; set; } = 100;
	[Export] public int Lives { get; set; } = 1;
	
	public override void _Process(double delta)
	{
		Position += Vector2.Left * Speed * (float)delta;
	}
	
	public void OnAreaEntered(Area2D area)
	{
		if (--Lives > 0) return;
		
		EmitSignal(SignalName.Killed);
		QueueFree();
	}
}
