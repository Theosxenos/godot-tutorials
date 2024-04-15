using Godot;
using System;

[GlobalClass]
public partial class SpawnInfo : Resource
{
	[Export] public int TimeStart { get; set; }
	[Export] public int TimeStop { get; set; }
	[Export] public PackedScene Enemy { get; set; }
	[Export] public int EnemyNumbers { get; set; }
	[Export] public int SpawnDelay { get; set; }

	public int SpawnDelayCounter { get; set; }
}
