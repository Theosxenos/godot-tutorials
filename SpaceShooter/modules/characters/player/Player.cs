using Godot;
using SpaceShooter.modules.characters.shared;

public partial class Player : Area2D
{
    [Signal]
    public delegate void HitEventHandler();

    private AudioStreamPlayer shootSound;

    [Export] public int Speed { get; set; } = 400;
    [Export] public PackedScene ProjectileScene { get; set; }

    public override void _Ready()
    {
        SetProcess(false);
        shootSound = GetNode<AudioStreamPlayer>("ShootSound");
    }

    public override void _Process(double delta)
    {
        var direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        Position += direction * Speed * (float)delta;
        Position = GetClampedPosition(Position);

        if (Input.IsActionJustPressed("fire_weapon"))
        {
            var projectile = ProjectileScene.Instantiate<PlayerProjectile>();
            projectile.GlobalPosition = GetNode<Marker2D>("Weapon").GlobalPosition;

            GetTree().CurrentScene.AddChild(projectile);

            shootSound.Play();
        }
    }

    private Vector2 GetClampedPosition(Vector2 direction)
    {
        return new Vector2(Mathf.Clamp(direction.X, 0, 960), Mathf.Clamp(direction.Y, 0, 512));
    }

    private void OnAreaEntered(Area2D area)
    {
        if (area is Enemy enemy)
            enemy.Hit();

        GetNode<AudioStreamPlayer>("HitSound").Play();

        EmitSignal("Hit");
    }
}