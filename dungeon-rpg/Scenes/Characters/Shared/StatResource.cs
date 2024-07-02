using Godot;

namespace DungeonRPG.Scenes.Shared;

[GlobalClass]
public partial class StatResource : Resource
{
    private float statValue;
    [Export] public Stat StatType { get; private set; }

    [Export]
    public float StatValue
    {
        get => statValue;
        set => statValue = Mathf.Clamp(value, 0, Mathf.Inf);
    }
}