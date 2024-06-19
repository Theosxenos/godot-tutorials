using Godot;
using System.Linq;

public partial class EnemyChaseState : EnemyState
{
    [Export] private Timer updateDestinationTimer;
    
    private CharacterBody3D target;
    
    protected override void EnterState()
    {
        CharacterNode.AnimationPlayer.Play(GameConstants.ANIM_MOVE);

        target = CharacterNode.ChaseAreaNode.GetOverlappingBodies()
            .FirstOrDefault(body => body is CharacterBody3D) as CharacterBody3D;
        
        updateDestinationTimer.Timeout += UpdateDestinationTimerOnTimeout;
        CharacterNode.AttackAreaNode.BodyEntered += HandleAttackAreaEntered;
        CharacterNode.ChaseAreaNode.BodyExited += HandleChaseAreaBodyExited;
    }

    protected override void ExitState()
    {
        updateDestinationTimer.Timeout -= UpdateDestinationTimerOnTimeout;
        CharacterNode.AttackAreaNode.BodyEntered -= HandleAttackAreaEntered;
        CharacterNode.ChaseAreaNode.BodyExited -= HandleChaseAreaBodyExited;
    }

    public override void _PhysicsProcess(double delta)
    {
        Move();
    }

    private void UpdateDestinationTimerOnTimeout()
    {
        Destination = target.GlobalPosition;
        CharacterNode.AgentNode.TargetPosition = Destination;
    }
    
    private void HandleAttackAreaEntered(Node3D body)
    {
        CharacterNode.StateMachine.SwitchState<EnemyAttackState>();
    }
    
    private void HandleChaseAreaBodyExited(Node3D body)
    {
        CharacterNode.StateMachine.SwitchState<EnemyReturnState>();
    }
}
