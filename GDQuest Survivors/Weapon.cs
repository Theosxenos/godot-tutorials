using Godot;
using System;

public partial class Weapon : Area2D
{
	[Export] public PackedScene  ProjectileScene { get; set; }
	
	public override void _PhysicsProcess(double delta)
	{
		var enemiesInRange = GetOverlappingBodies();
		if (enemiesInRange.Count > 0)
		{
			var targetEnemy = enemiesInRange[0];
			LookAt(targetEnemy.GlobalPosition);
		}
	}

	public void FireWeapon()
	{
		var projectile = ProjectileScene.Instantiate() as Area2D;
		projectile!.GlobalPosition = GetNode<Marker2D>("WeaponPivot/Pistol/BarrelEnd").GlobalPosition;
		projectile!.GlobalRotation = GlobalRotation;
		// GetTree().Root.AddChild(projectile); // Set top position in the inspector instead
		AddChild(projectile);
	}
}
