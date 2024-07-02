using Godot;
using System;

public partial class PlayerAttackState : PlayerState
{    
    [Export] private Timer comboResetTimer;
    
    private int comboCounter = 1;
    private int maxComboCount = 2;

    public override void _Ready()
    {
        base._Ready();
        
        comboResetTimer.Timeout += () => comboCounter = 1;
    }

    protected override void EnterState()
    {
        var animationToPlay = comboCounter == 1 ? GameConstants.ANIM_ATTACK_SLASH : GameConstants.ANIM_ATTACK_KICK;
        CharacterNode.AnimationPlayer.Play(animationToPlay, customSpeed: 1.5f);
        CharacterNode.AnimationPlayer.AnimationFinished += AnimationPlayerOnAnimationFinished;
    }

    protected override void ExitState()
    {
        CharacterNode.AnimationPlayer.AnimationFinished -= AnimationPlayerOnAnimationFinished;
        
        comboResetTimer.Start();
    }

    private void AnimationPlayerOnAnimationFinished(StringName animName)
    {
        comboCounter = Mathf.Wrap(comboCounter + 1, 1, maxComboCount + 1);
        
        CharacterNode.StateMachine.SwitchState<PlayerIdleState>();
    }

    private void PerformHit()
    {
        GD.Print("Hit");
    }
}
