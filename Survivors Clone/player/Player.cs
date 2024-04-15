using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public int Health { get; set; } = 80;
	[Export] public int Speed { get; set; } = 300;

	private string lastAnimation = "walk_left";
	private AnimationPlayer animationPlayer;
	private Sprite2D sprite;
	
	public override void _Ready()
	{
		sprite = GetNode<Sprite2D>("Sprite");
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
	}

	public override void _PhysicsProcess(double delta)
	{
		Movement();
	}

	void Movement()
	{
		var direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");

		if (direction != Vector2.Zero && direction.X == 0)
		{
			animationPlayer.Play(lastAnimation);
		}
		else if (direction != Vector2.Zero)
		{
			lastAnimation = direction.X > 0 ? "walk_right" : "walk_left";
			animationPlayer.Play(lastAnimation);
		}
		else
		{
			animationPlayer.Pause();
		}
		
		Velocity = direction * Speed;// * (float)delta;

		MoveAndSlide();
	}

	void OnHurt(int damage)
	{
		Health -= damage;
	}
}
