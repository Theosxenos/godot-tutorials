using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class Cannon : StaticBody3D
{
    private PackedScene bulletScene;
    private int bulletDamage = 5;
    private List<Enemy> targets = new();
    private Enemy currentTarget;
    
    private void OnMobDetectorBodyEntered (Node3D body)
    {
        if (body is not Enemy enemy) return;
    
        targets.Add(enemy);
        ChooseTarget();
    }
    

    private void OnMobDetectorBodyExited(Node3D body)
    {
        if (body is not Enemy enemy) return;

        if (currentTarget == enemy)
        {
            currentTarget = null;
        }
        
        targets.Remove(enemy);
        ChooseTarget();
    }

    private void ChooseTarget()
    {
        currentTarget = null;
        targets = targets.Where(IsInstanceValid).ToList();

        if (targets == null) return;
        
        foreach (var target in targets)
        {
            if (currentTarget == null || target.Progress > currentTarget.Progress)
            {
                currentTarget = target;
            }
        }
    }

}
