using Godot;
using System;

public partial class EnemyPatrolState : EnemyState
{
    protected override void EnterState()
    {
        CharacterNode.AnimationPlayer.Play(GameConstants.ANIM_MOVE);

        Destination = GetPointGlobalPosition(1);
        CharacterNode.AgentNode.TargetPosition = Destination;
    }

    public override void _PhysicsProcess(double delta)
    {
        Move();
    }
}
