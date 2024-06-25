using Godot;

public partial class Enemy : CharacterBody3D
{
    public int Speed { get; set; } = 2;
    public int Health { get; set; } = 15;

    private PathFollow3D pathFollow3D;

    public override void _Ready()
    {
        pathFollow3D = GetParent<PathFollow3D>();
    }

    public override void _PhysicsProcess(double delta)
    {
        pathFollow3D.Progress = pathFollow3D.Progress * Speed * (float)delta;

        if (pathFollow3D.ProgressRatio >= 0.99f)
        {
            pathFollow3D.QueueFree();
        }
}
}
