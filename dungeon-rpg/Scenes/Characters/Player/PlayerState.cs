using Godot;

public partial class PlayerState : Node
{
    protected Player CharacterNode;

    public override void _Ready()
    {
        CharacterNode = GetOwner<Player>();

        SetPhysicsProcess(false);
        SetProcessInput(false);
    }
    
    public override void _Notification(int what)
    {
        base._Notification(what);

        if (what == 5_001)
        {
            EnterState();
            SetPhysicsProcess(true);
            SetProcessInput(true);
        }
        else if (what == 5_002)
        {
            SetPhysicsProcess(false);
            SetProcessInput(false);
        }
    }

    protected virtual void EnterState() { }
}