using Godot;
using System;

public partial class Player : CharacterBody3D
{
    [Export] public int Speed { get; set; } = 5;
    
    [ExportGroup("RequiredNodes")]
    [Export] public Sprite3D Sprite { get; set; }
    [Export] public AnimationPlayer AnimationPlayer { get; set; }

    private Vector2 direction = Vector2.Zero;

    public override void _PhysicsProcess(double delta)
    {
        AnimationPlayer.Play(direction != Vector2.Zero ? GameConstants.ANIM_MOVE : GameConstants.ANIM_IDLE);

        Sprite.FlipH = direction.X < 0;
        
        Velocity = new(direction.X, 0, direction.Y);
        Velocity *= Speed;

        MoveAndSlide();
    }

    public override void _Input(InputEvent @event)
    {
        direction = Input.GetVector(GameConstants.INPUT_MOVE_LEFT, GameConstants.INPUT_MOVE_RIGHT, GameConstants.INPUT_MOVE_FORWARD, GameConstants.INPUT_MOVE_BACKWARD);

    }
}
