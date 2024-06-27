using Godot;
using System;

public partial class Bullet : CharacterBody3D
{
    [Export] private int Speed { get; set; } = 20;
    public CharacterBody3D Target { get; set; }
    public int BulletDamage { get; set; }


    public override void _PhysicsProcess(double delta)
    {
        if (IsInstanceValid(Target))
        {
            Velocity = GlobalPosition.DirectionTo(Target.GlobalPosition) * Speed;
            LookAt(Target.GlobalPosition);

            if (MoveAndSlide())
            {
                HandleCollision();
            }
        }
        else
        {
            QueueFree();
        }
    }

    private void HandleCollision()
    {
        var collision = GetLastSlideCollision();
        var collider = collision.GetCollider();

        if (collider is not Enemy enemy)
        {
            return;
        }
        
        enemy.TakeDamage(BulletDamage);
        QueueFree();
    }
}
