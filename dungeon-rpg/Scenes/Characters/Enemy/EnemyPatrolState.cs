using Godot;
using System;

public partial class EnemyPatrolState : EnemyState
{
    private int pointIndex;
    
    protected override void EnterState()
    {
        CharacterNode.AnimationPlayer.Play(GameConstants.ANIM_MOVE);

        pointIndex = 1;
        
        Destination = GetPointGlobalPosition(pointIndex);
        CharacterNode.AgentNode.TargetPosition = Destination;
        
        CharacterNode.AgentNode.NavigationFinished += AgentNodeOnNavigationFinished;
    }

    public override void _PhysicsProcess(double delta)
    {
        Move();
    }

    private void AgentNodeOnNavigationFinished()
    {
        pointIndex = Mathf.Wrap(pointIndex + 1, 0, CharacterNode.PathNode.Curve.PointCount);

        Destination = GetPointGlobalPosition(pointIndex);
        CharacterNode.AgentNode.TargetPosition = Destination;
    }
}
