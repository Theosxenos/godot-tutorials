using Godot;

public partial class Player : CharacterBody3D
{
    [ExportGroup("RequiredNodes")]
    [Export] public Sprite3D Sprite { get; private set; }
    [Export] public AnimationPlayer AnimationPlayer { get; private set; }
    [Export] public StateMachine StateMachine { get; private set; }

    public Vector2 Direction { get; set; } = Vector2.Zero;

    public override void _Input(InputEvent @event)
    {
        Direction = Input.GetVector(GameConstants.INPUT_MOVE_LEFT, GameConstants.INPUT_MOVE_RIGHT,
            GameConstants.INPUT_MOVE_FORWARD, GameConstants.INPUT_MOVE_BACKWARD);
    }

    public void FlipSprite()
    {
        // Not moving horizontally
        if (Velocity.X == 0) return;

        Sprite.FlipH = Direction.X < 0;
    }
}