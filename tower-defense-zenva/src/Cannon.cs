using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class Cannon : StaticBody3D
{
    [Export] private PackedScene bulletScene;
    private int bulletDamage = 5;
    private List<Enemy> targets = new();
    private Enemy currentTarget;
    private bool canShoot = true;
    private Timer shootingCooldownTimer;

    public override void _Ready()
    {
        shootingCooldownTimer = GetNode<Timer>("ShootingCooldown");
    }

    public override void _Process(double delta)
    {
        if (currentTarget == null || !IsInstanceValid(currentTarget) || !canShoot)
        {
            return;
        }

        Shoot();
    }

    private void OnMobDetectorBodyEntered (Node3D body)
    {
        if (body is not Enemy enemy) return;
    
        targets.Add(enemy);
        ChooseTarget();
    }
    
    private void OnMobDetectorBodyExited(Node3D body)
    {
        if (body is not Enemy enemy) return;

        if (currentTarget == enemy)
        {
            currentTarget = null;
        }
        
        targets.Remove(enemy);
        ChooseTarget();
    }

    private void ChooseTarget()
    {
        currentTarget = null;
        targets = targets.Where(IsInstanceValid).ToList();

        if (targets == null) return;
        
        foreach (var target in targets)
        {
            if (currentTarget == null || target.Progress > currentTarget.Progress)
            {
                currentTarget = target;
            }
        }
    }

    private void OnShootingCooldownTimeout()
    {
        canShoot = true;
    }

    private void Shoot()
    {
        canShoot = false;

        LookAt(currentTarget.GlobalPosition);
        
        var bullet = bulletScene.Instantiate() as Bullet;
        bullet!.Target = currentTarget;
        bullet.BulletDamage = bulletDamage;
        var bulletContainer =  GetNode("BulletContainer");
        bulletContainer.AddChild(bullet);
        bullet.GlobalPosition = GetNode<Marker3D>("Aim").GlobalPosition;
    }
}
