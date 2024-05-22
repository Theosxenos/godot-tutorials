using Godot;
using System;

public partial class Player : CharacterBody3D
{
    [Export] public int Speed { get; set; } = 5;
    
    [ExportGroup("RequiredNodes")]
    [Export] public Sprite3D Sprite { get; set; }
    [Export] public AnimationPlayer AnimationPlayer { get; set; }

    public Vector2 Direction { get; set; } = Vector2.Zero;
    public StateMachine StateMachine { get; set; }

    public override void _Ready()
    {
        StateMachine = GetNode<StateMachine>("StateMachine");
    }

    public override void _PhysicsProcess(double delta)
    {
        Velocity = new(Direction.X, 0, Direction.Y);
        Velocity *= Speed;

        MoveAndSlide();

        FlipSprite();
    }

    private void FlipSprite()
    {
        // Not moving horizontally
        if(Velocity.X == 0) return;

        Sprite.FlipH = Direction.X < 0;
    }

    public override void _Input(InputEvent @event)
    {
        Direction = Input.GetVector(GameConstants.INPUT_MOVE_LEFT, GameConstants.INPUT_MOVE_RIGHT, GameConstants.INPUT_MOVE_FORWARD, GameConstants.INPUT_MOVE_BACKWARD);
        // AnimationPlayer.Play(direction != Vector2.Zero ? GameConstants.ANIM_MOVE : GameConstants.ANIM_IDLE);
    }
}
