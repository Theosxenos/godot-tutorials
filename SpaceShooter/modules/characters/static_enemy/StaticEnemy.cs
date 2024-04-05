using Godot;
using System;
using SpaceShooter.modules.shared.interfaces;

public partial class StaticEnemy : Area2D, IHitable
{
    [Signal]
    public delegate void EnemyHitEventHandler();

    public void Hit()
    {
        QueueFree();
        EmitSignal(nameof(EnemyHitEventHandler));
    }
}
