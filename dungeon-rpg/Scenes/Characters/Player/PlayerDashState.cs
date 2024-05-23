using Godot;
using System;

public partial class PlayerDashState : Node
{

    [Export] private int dashSpeed = 10;

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
        characterNode.MoveAndSlide();
        // characterNode.FlipSprite();
    }

    public override void _Notification(int what)
    {
        base._Notification(what);

        if (what == 5_001)
        {
            characterNode.AnimationPlayer.Play(GameConstants.ANIM_DASH);
            
            characterNode.Velocity = new Vector3(characterNode.Direction.X, 0, characterNode.Direction.Y);

            if (characterNode.Velocity == Vector3.Zero)
            {
                characterNode.Velocity = characterNode.Sprite.FlipH
                    ? characterNode.Velocity = Vector3.Left
                    : characterNode.Velocity = Vector3.Right;
            }
            
            characterNode.Velocity *= dashSpeed;
            
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
        characterNode.Velocity = Vector3.Zero;
        
        characterNode.StateMachine.SwitchState<PlayerIdleState>();
    }
}
