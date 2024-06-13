using Godot;

public partial class EnemyReturnState : EnemyState
{
    private Vector3 destination;

    public override void _Ready()
    {
        base._Ready();

        var localPosition = CharacterNode.PathNode.Curve.GetPointPosition(0);
        var globalPosition = CharacterNode.PathNode.GlobalPosition;
        destination = localPosition + globalPosition;
    }

    protected override void EnterState()
    {
        CharacterNode.AnimationPlayer.Play(GameConstants.ANIM_MOVE);

        CharacterNode.GlobalPosition = destination;
    }
}
