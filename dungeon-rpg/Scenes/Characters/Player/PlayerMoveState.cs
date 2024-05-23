using Godot;
using System;

public partial class PlayerMoveState : Node
{
    private Player characterNode;

    public override void _Ready()
    {
        characterNode = GetOwner<Player>();

        SetPhysicsProcess(false);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (characterNode.Direction == Vector2.Zero)
        {
            characterNode.StateMachine.SwitchState<PlayerIdleState>();
            return;
        }
        
        characterNode.Velocity = new(characterNode.Direction.X, 0, characterNode.Direction.Y);
        characterNode.Velocity *= characterNode.Speed;

        characterNode.MoveAndSlide();

        characterNode.FlipSprite();
    }

    public override void _Notification(int what)
    {
        base._Notification(what);

        if (what == 5_001)
        {
            characterNode.AnimationPlayer.Play(GameConstants.ANIM_MOVE);
            SetPhysicsProcess(true);
        }
        else if (what == 5_002)
        {
            SetPhysicsProcess(false);
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH))
        {
            characterNode.StateMachine.SwitchState<PlayerDashState>();
        }
    }
}
