using Godot;

public partial class Main : Node3D
{
    [Export] private PackedScene enemyScene;
    [Export] private Path3D path;
    [Export] private int enemiesToSpawn = 3;

    private bool canSpawn = true;
    private Timer spawnTimer;

    public override void _Ready()
    {
        spawnTimer = GetNode<Timer>("SpawnTimer");
        spawnTimer.Timeout += SpawnTimerOnTimeout;
    }

    public override void _Process(double delta)
    {
        GameManager();
    }

    private void GameManager()
    {
        if (enemiesToSpawn <= 0 || !canSpawn) return;
        
        spawnTimer.Start();

        var enemy = enemyScene.Instantiate();
        path.AddChild(enemy);

        enemiesToSpawn--;
        canSpawn = false;
    }

    private void SpawnTimerOnTimeout()
    {
        canSpawn = true;
    }
}
