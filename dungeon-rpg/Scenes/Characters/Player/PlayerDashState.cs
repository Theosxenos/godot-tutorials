using Godot;
using System;

public partial class PlayerDashState : Node
{
    private Player characterNode;
    private Timer dashTimer;

    public override void _Ready()
    {
        characterNode = GetOwner<Player>();
        dashTimer = GetNode<Timer>("Timer");
        
        dashTimer.Timeout += DashTimerOnTimeout;

        SetPhysicsProcess(false);
        SetProcessInput(false);
    }

    public override void _PhysicsProcess(double delta)
    {
        // if (characterNode.Direction != Vector2.Zero)
        // {
        //     characterNode.StateMachine.SwitchState<PlayerMoveState>();
        // }
    }

    public override void _Notification(int what)
    {
        base._Notification(what);

        if (what == 5_001)
        {
            characterNode.AnimationPlayer.Play(GameConstants.ANIM_DASH);
            dashTimer.Start();
            
            SetPhysicsProcess(true);
            SetProcessInput(true);
        }
        else if (what == 5_002)
        {
            SetPhysicsProcess(false);
            SetProcessInput(false);
        }
    }
    
    private void DashTimerOnTimeout()
    {
        characterNode.StateMachine.SwitchState<PlayerIdleState>();
    }
}
