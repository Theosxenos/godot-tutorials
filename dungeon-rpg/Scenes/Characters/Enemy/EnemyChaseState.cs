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
    }

    protected override void ExitState()
    {
        updateDestinationTimer.Timeout -= UpdateDestinationTimerOnTimeout;
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
}
