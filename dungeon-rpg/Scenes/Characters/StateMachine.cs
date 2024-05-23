using Godot;

public partial class StateMachine : Node
{
    [Export] private PlayerState currentState;
    [Export] private PlayerState[] states;

    public override void _Ready()
    {
        currentState._Notification(5001);
    }

    public void SwitchState<T>()
    {
        PlayerState newState = null;

        foreach (var state in states)
            if (state is T)
                newState = state;

        if (newState == null) return;

        currentState._Notification(5_002);
        currentState = newState;
        newState._Notification(5_001);
    }
}