using Godot;

public partial class PlayerProjectile : Area2D
{
    [Export] public int Speed { get; set; } = 800;

    public override void _Process(double delta)
    {
        Position += Vector2.Right * Speed * (float)delta;
    }

    public void OnAreaEntered(Area2D area)
    {
        QueueFree();
    }

    private void OnVisibleOnScreenNotifier2dScreenExited()
    {
        QueueFree();
    }
}