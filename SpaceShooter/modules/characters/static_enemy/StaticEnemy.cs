using Godot;
using SpaceShooter.modules.characters.shared;

public partial class StaticEnemy : Enemy
{
    public override void _Process(double delta)
    {
        Position += Vector2.Left * Speed * (float)delta;
    }
}