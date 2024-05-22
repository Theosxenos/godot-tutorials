using Godot;
using System;

public partial class StateMachine : Node
{
    [Export] private Node currentState;
    [Export] private Node[] states;

    public override void _Ready()
    {
        currentState._Notification(5001);
    }

    public void SwitchState<T>()
    {
        Node newState = null;

        foreach (var state in states)
        {
            if (state is T)
            {
                newState = state;
            }
        }

        newState?._Notification(5_001);
    }
}
