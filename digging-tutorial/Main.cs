using Godot;
using System;

public partial class Main : Node
{
	public override void _Ready()
	{
		var player = GetNode<Player>("Player");
		player.Digging += PlayerOnDigging; 
	}

	private void PlayerOnDigging(Vector2 mousePos)
	{
		var tileMap = GetNode<TileMap>("TileMap");
		// var localVector = tileMap.ToLocal(mousePos);
		var localVector = tileMap.GetLocalMousePosition();
		var cellMapCoords = tileMap.LocalToMap(localVector);
		var tileData = tileMap.GetCellTileData(0, cellMapCoords);
		if ((bool?)tileData?.GetCustomData("Diggable") is true)
		{
			tileMap.EraseCell(0, cellMapCoords);
		}
	}
}
