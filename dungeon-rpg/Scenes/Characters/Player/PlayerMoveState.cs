using Godot;

public partial class PlayerMoveState : PlayerState
{
    public override void _PhysicsProcess(double delta)
    {
        if (CharacterNode.Direction == Vector2.Zero)
        {
            CharacterNode.StateMachine.SwitchState<PlayerIdleState>();
            return;
        }

        CharacterNode.Velocity = new Vector3(CharacterNode.Direction.X, 0, CharacterNode.Direction.Y);
        CharacterNode.Velocity *= CharacterNode.Speed;

        CharacterNode.MoveAndSlide();

        CharacterNode.FlipSprite();
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH))
            CharacterNode.StateMachine.SwitchState<PlayerDashState>();
    }

    protected override void EnterState()
    {
        CharacterNode.AnimationPlayer.Play(GameConstants.ANIM_MOVE);
    }
}