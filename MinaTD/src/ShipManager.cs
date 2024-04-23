using Godot;
using System;

public partial class ShipManager : PathFollow2D
{
	// private PathFollow2D _pathFollow2D;

	private float speed = 1f;
	
	public override void _Ready()
	{
		// _pathFollow2D = GetNode<PathFollow2D>(GetPath());
		
		if (Progress > 0)
		{
			GD.PushWarning("Progress is not zero. It most likely should be.");
		}
	}

	public override void _Process(double delta)
	{
		ProgressRatio += (float)delta * speed * 0.03f;
	}
}
