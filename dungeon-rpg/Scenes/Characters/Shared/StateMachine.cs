using System.Linq;
using Godot;

public partial class StateMachine : Node
{
    [Export] private CharacterState currentState;
    [Export] private CharacterState[] states;

    public override void _Ready()
    {
        states = new CharacterState[GetChildCount()];

        var i = 0;
        foreach (var child in GetChildren())
        {
            states[i] = child as CharacterState;
            i++;
        }
        
        currentState._Notification(GameConstants.NOTIFICATION_ENTER_STATE);
    }

    public void SwitchState<T>()
    {
        var newState = states.FirstOrDefault(state => state is T);

        if (newState == null) return;

        currentState._Notification(GameConstants.NOTIFICATION_EXIT_STATE);
        currentState = newState;
        newState._Notification(GameConstants.NOTIFICATION_ENTER_STATE);
    }
}