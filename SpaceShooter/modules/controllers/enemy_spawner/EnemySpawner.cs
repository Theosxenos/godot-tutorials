using Godot;
using SpaceShooter.modules.characters.shared;

public partial class EnemySpawner : Node2D
{
    [Signal]
    public delegate void EnemyEscapedEventHandler();

    [Signal]
    public delegate void EnemyKilledEventHandler();

    [Export] public PackedScene[] Enemies { get; set; }

    public void Start()
    {
        GetNode<Timer>("SpawnTimer").Start();
    }

    private void OnSpawnTimerTimeout()
    {
        var enemyScene = Enemies[GD.RandRange(0, Enemies.Length - 1)];
        var enemy = enemyScene.Instantiate<Enemy>();

        enemy.Connect(Enemy.SignalName.Escaped, Callable.From(OnEnemyEscaped));
        enemy.Connect(Enemy.SignalName.Killed, Callable.From(OnEnemyKilled));

        var topPosition = GetNode<Marker2D>("TopPosition").Position;
        var bottomPosition = GetNode<Marker2D>("BottomPosition").Position;

        enemy.Position = new Vector2(topPosition.X, GD.RandRange((int)topPosition.Y, (int)bottomPosition.Y));
        AddChild(enemy);
    }

    private void OnEnemyEscaped()
    {
        EmitSignal(SignalName.EnemyEscaped);
    }

    private void OnEnemyKilled()
    {
        EmitSignal(SignalName.EnemyKilled);
    }

    public void Stop()
    {
        GetNode<Timer>("SpawnTimer").Stop();
    }
}