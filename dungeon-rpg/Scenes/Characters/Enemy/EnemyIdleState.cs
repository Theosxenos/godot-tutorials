using Godot;
using System;

[GlobalClass]
public partial class EnemyIdleState : EnemyState
{
    protected override void EnterState()
    {
        CharacterNode.AnimationPlayer.Play(GameConstants.ANIM_IDLE);
    }
}
