using Godot;
using System;

public partial class Player : CharacterBody3D
{
    [Export] public int Speed { get; set; }

    public override void _PhysicsProcess(double delta)
    {
        var direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

        Velocity += direction * Speed * (float)delta;
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
    }
}
