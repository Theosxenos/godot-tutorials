using Godot;

public partial class EnemyReturnState : EnemyState
{
    public override void _Ready()
    {
        base._Ready();

        Destination = GetPointGlobalPosition(0);
    }

    protected override void EnterState()
    {
        CharacterNode.AnimationPlayer.Play(GameConstants.ANIM_MOVE);

        CharacterNode.AgentNode.TargetPosition = Destination;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (CharacterNode.AgentNode.IsNavigationFinished())
        {
            CharacterNode.StateMachine.SwitchState<EnemyPatrolState>();
            return;
        }

        Move();
    }
}

