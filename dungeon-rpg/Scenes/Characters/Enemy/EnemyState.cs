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
        CharacterNode.AgentNode.GetNextPathPosition();
        CharacterNode.Velocity = CharacterNode.GlobalPosition.DirectionTo(Destination);

        CharacterNode.MoveAndSlide();
        CharacterNode.FlipSprite();
    }

    protected void HandleChaseAreaBodyEntered(Node3D body)
    {
        CharacterNode.StateMachine.SwitchState<EnemyChaseState>();
    }
}
