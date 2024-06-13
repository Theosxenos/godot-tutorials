using Godot;

public partial class EnemyReturnState : EnemyState
{
    private Vector3 destination;

    public override void _Ready()
    {
        base._Ready();

        var localPosition = CharacterNode.PathNode.Curve.GetPointPosition(0);
        var globalPosition = CharacterNode.PathNode.GlobalPosition;
        destination = localPosition + globalPosition;
    }

    protected override void EnterState()
    {
        CharacterNode.AnimationPlayer.Play(GameConstants.ANIM_MOVE);

        CharacterNode.AgentNode.TargetPosition = destination;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (CharacterNode.AgentNode.IsNavigationFinished())
        {
            // CharacterNode.StateMachine.SwitchState<EnemyIdleState>();
            GD.Print("Destination reached");
            return;
        }

        CharacterNode.Velocity = CharacterNode.GlobalPosition.DirectionTo(destination);

        CharacterNode.MoveAndSlide();
    }
}

