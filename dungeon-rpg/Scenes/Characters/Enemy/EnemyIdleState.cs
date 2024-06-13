using Godot;
using System;

[GlobalClass]
public partial class EnemyIdleState : EnemyState
{
    protected override void EnterState()
    {
        CharacterNode.AnimationPlayer.Play(GameConstants.ANIM_IDLE);
    }

    public override void _PhysicsProcess(double delta)
    {
        CharacterNode.StateMachine.SwitchState<EnemyReturnState>();
    }
}
