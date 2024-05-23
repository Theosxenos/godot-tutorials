using Godot;
using System;

public partial class PlayerDashState : PlayerState
{

    [Export] private int dashSpeed = 10;
    [Export] private Timer dashTimer;

    public override void _PhysicsProcess(double delta)
    {
        CharacterNode.MoveAndSlide();
    }

    protected override void EnterState()
    {
        CharacterNode.AnimationPlayer.Play(GameConstants.ANIM_DASH);
            
        CharacterNode.Velocity = new Vector3(CharacterNode.Direction.X, 0, CharacterNode.Direction.Y);

        if (CharacterNode.Velocity == Vector3.Zero)
        {
            CharacterNode.Velocity = CharacterNode.Sprite.FlipH
                ? CharacterNode.Velocity = Vector3.Left
                : CharacterNode.Velocity = Vector3.Right;
        }
            
        CharacterNode.Velocity *= dashSpeed;
            
        dashTimer.Start();
    }

    private void OnDashTimerTimeout()
    {
        CharacterNode.Velocity = Vector3.Zero;
        
        CharacterNode.StateMachine.SwitchState<PlayerIdleState>();
    }
}
