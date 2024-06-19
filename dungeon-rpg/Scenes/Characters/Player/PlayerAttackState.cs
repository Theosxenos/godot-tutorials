using Godot;
using System;

public partial class PlayerAttackState : PlayerState
{
    private int comboCounter = 1;
    private int maxComboCount = 2;

    protected override void EnterState()
    {
        var animationToPlay = comboCounter == 1 ? GameConstants.ANIM_ATTACK_SLASH : GameConstants.ANIM_ATTACK_KICK;
        CharacterNode.AnimationPlayer.Play(animationToPlay);
        CharacterNode.AnimationPlayer.AnimationFinished += AnimationPlayerOnAnimationFinished;
    }

    protected override void ExitState()
    {
        CharacterNode.AnimationPlayer.AnimationFinished -= AnimationPlayerOnAnimationFinished;
    }

    private void AnimationPlayerOnAnimationFinished(StringName animName)
    {
        comboCounter = Mathf.Wrap(comboCounter + 1, 1, maxComboCount + 1);
        
        CharacterNode.StateMachine.SwitchState<PlayerIdleState>();
    }
}
