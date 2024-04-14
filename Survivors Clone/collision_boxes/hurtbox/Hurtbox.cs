using Godot;
using System;

public partial class Hurtbox : Area2D
{
	[Signal]
	public delegate void HurtEventHandler(int damage);
	[Export] public HurtBoxType HurtBoxType { get; set; }
	[Export] private CollisionShape2D collisionShape2D;

	private Timer disableTimer;

	public override void _Ready()
	{
		disableTimer = GetNode<Timer>("Timer");
	}
 
	public void OnAreaEntered(Area2D area)
	{
		if (area is not Hitbox hitBox) return;

		switch (HurtBoxType)
		{
			case HurtBoxType.Cooldown:
				collisionShape2D.SetDeferred("disabled", true);
				disableTimer.Start();
				break;
			case HurtBoxType.HitOnce:
				break;
			case HurtBoxType.DisableHitBox:
				hitBox.TempDisable();
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}

		EmitSignal(SignalName.Hurt, hitBox.Damage);
	}

	void OnTimerTimeout()
	{
		collisionShape2D.SetDeferred("disabled", false);
	}
}

public enum HurtBoxType
{
	Cooldown,
	HitOnce,
	DisableHitBox
}
