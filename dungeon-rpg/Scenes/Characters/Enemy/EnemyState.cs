using Godot;

[GlobalClass]
public abstract partial class EnemyState : CharacterState
{
    protected Vector3 Destination { get; set; }

    protected Vector3 GetPointGlobalPosition(int index)
    {
        var localPosition = CharacterNode.PathNode.Curve.GetPointPosition(index);
        var globalPosition = CharacterNode.PathNode.GlobalPosition;
        return localPosition + globalPosition;
    }

    protected void Move()
    {
        CharacterNode.Velocity = CharacterNode.GlobalPosition.DirectionTo(Destination);

        CharacterNode.MoveAndSlide();
    }
}
