using Godot;
using System;

public partial class EnemyPatrolState : EnemyState
{
    [Export] private Timer idleTimer;
    [Export(PropertyHint.Range, "0,20,0.1")] private float maxIdleTime = 4;
    
    private int pointIndex;
    
    protected override void EnterState()
    {
        CharacterNode.AnimationPlayer.Play(GameConstants.ANIM_MOVE);

        pointIndex = 1;
        
        Destination = GetPointGlobalPosition(pointIndex);
        CharacterNode.AgentNode.TargetPosition = Destination;
        
        CharacterNode.AgentNode.NavigationFinished += AgentNodeOnNavigationFinished;
        idleTimer.Timeout += IdleTimerOnTimeout;
    }

    protected override void ExitState()
    {
        idleTimer.Timeout -= IdleTimerOnTimeout;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!idleTimer.IsStopped())
        {
            return;
        }
        
        Move();
    }

    private void AgentNodeOnNavigationFinished()
    {
        CharacterNode.AnimationPlayer.Play(GameConstants.ANIM_IDLE);
        
        idleTimer.Start(GD.RandRange(0f, maxIdleTime));
        // idleTimer.Start(10);
    }

    private void IdleTimerOnTimeout()
    {
        CharacterNode.AnimationPlayer.Play(GameConstants.ANIM_MOVE);

        pointIndex = Mathf.Wrap(pointIndex + 1, 0, CharacterNode.PathNode.Curve.PointCount);

        Destination = GetPointGlobalPosition(pointIndex);
        CharacterNode.AgentNode.TargetPosition = Destination;
    }

}
