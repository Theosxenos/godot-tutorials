using Godot;

namespace DungeonRPG.Scenes.Characters.Shared;

public abstract partial class Character : CharacterBody3D
{
    [ExportGroup("RequiredNodes")]
    [Export] public Sprite3D Sprite { get; private set; }
    [Export] public AnimationPlayer AnimationPlayer { get; private set; }
    [Export] public StateMachine StateMachine { get; private set; }
    
    [ExportGroup("AI Nodes")]
    [Export] public Path3D PathNode { get; private set; }
    [Export] public NavigationAgent3D AgentNode { get; private set; }
    [Export] public Area3D ChaseAreaNode { get; private set; }
    
    public Vector2 Direction { get; set; } = Vector2.Zero;
    
    public void FlipSprite()
    {
        // Not moving horizontally
        if (Velocity.X == 0) return;

        Sprite.FlipH = Direction.X < 0;
    }
}