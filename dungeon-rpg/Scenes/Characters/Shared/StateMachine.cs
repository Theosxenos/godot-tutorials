using System.Linq;
using Godot;

public partial class StateMachine : Node
{
    [Export] private CharacterState currentState;
    [Export] private CharacterState[] states;

    public override void _Ready()
    {
        currentState._Notification(GameConstants.NOTIFICATION_ENTER_STATE);
    }

    public void SwitchState<T>()
    {
        CharacterState newState = null;

        foreach (var state in states)
        {
            if (state is T)
                newState = state;
        }

        if (newState == null) return;

        currentState._Notification(GameConstants.NOTIFICATION_EXIT_STATE);
        currentState = newState;
        newState._Notification(GameConstants.NOTIFICATION_ENTER_STATE);
    }
}