using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Signal]
	public delegate void DiggingEventHandler(Vector2 mousePos);

	[Export] public bool IsDigging { get; set; }
	[Export] public float DiggingRange { get; set; } = 300f;
	
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("LMB"))
		{
			IsDigging = true;
		}

		if (Input.IsActionJustReleased("LMB"))
		{
			IsDigging = false;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		if (IsDigging)
		{
			if (Position.DistanceTo(GetGlobalMousePosition()) < DiggingRange)
			{
				// GD.Print(GetGlobalMousePosition());
				EmitSignal(SignalName.Digging, GetGlobalMousePosition());
			}
		}
		
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		var body = GetNode<Sprite2D>("BodySprite");
		body.FlipH = velocity.X > 0;
		foreach (var node in body.GetChildren())
		{
			var part = (Sprite2D)node;
			part.FlipH = velocity.X > 0;
		}
		
		Velocity = velocity;
		MoveAndSlide();
	}
}
