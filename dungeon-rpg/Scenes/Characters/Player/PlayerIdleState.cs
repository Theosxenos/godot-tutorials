using Godot;
using System;

public partial class PlayerIdleState : Node
{
    private Player characterNode;

    public override void _Ready()
    {
        characterNode = GetOwner<Player>();
    }

    public override void _PhysicsProcess(double delta)
    {
        if (characterNode.Direction != Vector2.Zero)
        {
            characterNode.StateMachine.SwitchState<PlayerMoveState>();
        }
    }

    public override void _Notification(int what)
    {
        base._Notification(what);

        if (what != 5_001) return;
        
        characterNode.AnimationPlayer.Play(GameConstants.ANIM_IDLE);
    }
}
