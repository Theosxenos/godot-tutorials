using Godot;
using System;

public partial class Hud : CanvasLayer
{
	[Signal]
	public delegate void StartGameEventHandler();
	
	[Export] public Label ScoreLabel { get; set; }
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ScoreLabel.Text = "0";
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void ShowMessage(string message)
	{
		var label = GetNode<Label>("Message");
		label.Text = message;
		label.Show();
	}

	public void ShowToast(string message)
	{
		ShowMessage(message);
		
		GetNode<Timer>("MessageTimer").Start();
	}

	async public void GameOver()
	{
		ShowToast("Game Over!");

		var messageTimer = GetNode<Timer>("MessageTimer");
		await ToSignal(messageTimer, Timer.SignalName.Timeout);
		
		ShowMessage("Dodge the Creeps!");

		await ToSignal(GetTree().CreateTimer(1f), Timer.SignalName.Timeout);
		GetNode<Button>("StartButton").Show();
	}

	public void UpdateScore(int score)
	{
		ScoreLabel.Text = score.ToString();
	}

	public void OnMessageTimerTimeOut()
	{
		GetNode<Label>("Message").Hide();
	}
	
	private void OnStartButtonPressed()
	{
		GetNode<Button>("StartButton").Hide();
		EmitSignal(SignalName.StartGame);
	}
}
