using Godot;
using System;
using System.Threading.Tasks;

public partial class Game : Node
{
	public override void _Ready()
	{
		base._Ready();
		_start();
	}

	private async void _start()
	{
		await GetNode<LevelHider>("LevelHider").FadeOut();
		GetNode<Level1>("Level1").Start();
	}

	public async void OnGameOver()
	{
		await Task.Delay(1000);
		await GetNode<LevelHider>("LevelHider").FadeIn();
		GetNode<Level1>("Level1").QueueFree();
		var scene = ResourceLoader.Load<PackedScene>("res://Levels/Level1.tscn").Instantiate();
		AddChild(scene);
	}
}
