using Godot;
using System;

public partial class Mob : RigidBody2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		var animations = animatedSprite.SpriteFrames.GetAnimationNames();
		animatedSprite.Animation = animations[GD.Randi() % animations.Length];
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
