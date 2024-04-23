using Godot;
using System;

public partial class EnemyPathDisplay : Node2D
{
    [Export] private Node2D level;
    
    [Export] private Color pathColor = new Color(1, 1, 1, 0.5f);
    [Export] private float pathWidth = 15f;
    
    private Curve2D pathCurve;

    public override void _Ready()
    {
        var path2D = level.GetNode<Path2D>("ShipPath");
        pathCurve = path2D.Curve;
    }

    public override void _Draw()
    {
        DrawPolyline(pathCurve.GetBakedPoints(), pathColor, pathWidth, true);
    }
}
