using Godot;
using System;

public partial class EnemyAttackState : EnemyState
{
    protected override void EnterState()
    {
        CharacterNode.AnimationPlayer.Play(GameConstants.ANIM_ATTACK);
    }
}
