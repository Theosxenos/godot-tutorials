using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Signal]
	public delegate void PlayerHealthDepletedEventHandler();
	
	[Export] public float Health { get; set; } = 100f;

	private Area2D hurtBox;

	public override void _Ready()
	{
		hurtBox = GetNode<Area2D>("%Hurtbox");
	}

	public override void _PhysicsProcess(double delta)
	{
		var direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");

		Velocity = direction * 600;
		MoveAndSlide();

		var happyBoo = GetNode("%HappyBoo");
		if(direction != Vector2.Zero)
			happyBoo.Call("play_walk_animation");
		else
		{
			happyBoo.Call("play_idle_animation");
		}

		const float damageRate = 5.0f;
		
		var overlappingMobs = hurtBox.GetOverlappingBodies();
		if (overlappingMobs.Count <= 0) return;
		
		Health -= damageRate * overlappingMobs.Count * (float)delta;
		GetNode<ProgressBar>("%ProgressBar").Value = Health;
		
		if (Health <= 0)
		{
			EmitSignal(SignalName.PlayerHealthDepleted);
		}
	}
}
