using Godot;

[GlobalClass]
public abstract partial class PlayerState : CharacterState
{
    protected void CheckForAttackInput()
    {
        if(Input.IsActionJustPressed(GameConstants.INPUT_ATTACK))
        {
            CharacterNode.StateMachine.SwitchState<PlayerAttackState>();
        }
    }
}