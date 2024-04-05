using Godot;

public partial class StaticEnemy : Area2D
{
    [Signal]
    public delegate void HitEventHandler();
}
