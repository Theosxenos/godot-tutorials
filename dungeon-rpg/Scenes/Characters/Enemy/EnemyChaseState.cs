using Godot;
using System;

public partial class EnemyChaseState : EnemyState
{
    protected override void EnterState()
    {
        CharacterNode.AnimationPlayer.Play(GameConstants.ANIM_MOVE);
        GD.Print("Chase state");
    }
}
