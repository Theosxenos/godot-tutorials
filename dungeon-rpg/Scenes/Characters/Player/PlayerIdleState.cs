using Godot;


public partial class PlayerIdleState : PlayerState
{
    public override void _PhysicsProcess(double delta)
    {
        if (CharacterNode.Direction != Vector2.Zero) CharacterNode.StateMachine.SwitchState<PlayerMoveState>();
    }

    public override void _Input(InputEvent @event)
    {
        CheckForAttackInput();

        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH))
        {
            CharacterNode.StateMachine.SwitchState<PlayerDashState>();
        }
    }

    protected override void EnterState()
    {
        CharacterNode.AnimationPlayer.Play(GameConstants.ANIM_IDLE);
    }
}