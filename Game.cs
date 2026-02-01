using Godot;
using System;
using System.Threading.Tasks;
using MaskedRobbery;

public partial class Game : Node
{
	public override void _Ready()
	{
		base._Ready();
		_start();
	}

	private async void _start()
	{
		ScoreService.Reset();
		GetNode<CanvasLayer>("GameOverUI").Hide();
		await GetNode<LevelHider>("LevelHider").FadeOut();
		GetNode<Level1>("Level1").Start();
	}

	public async void OnGameOver()
	{
		var level1 = GetNode<Level1>("Level1");
		level1.Stop();
		await Task.Delay(1000);
		await GetNode<LevelHider>("LevelHider").FadeIn();
		RemoveChild(level1);
		level1.QueueFree();
		var scene = ResourceLoader.Load<PackedScene>("res://Levels/Level1.tscn").Instantiate();
		scene.Name = "Level1";
		AddChild(scene);
		GetNode<Ferret>("Level1/Ferret").GameOver += OnGameOver;
		GetNode<CanvasLayer>("GameOverUI").Show();
	}

	public void OnRetry()
	{
		_start();
	}

	public void OnHuzzahButtonPressed()
	{
		GetNode<CanvasLayer>("HelpBox").Hide();
	}

	public async void OnWinHere(Node2D body)
	{
		var level1 = GetNode<Level1>("Level1");
		level1.Stop();
		await Task.Delay(1000);
		await GetNode<LevelHider>("LevelHider").FadeIn();
		RemoveChild(level1);
		level1.QueueFree();
		var scene = ResourceLoader.Load<PackedScene>("res://Levels/Level1.tscn").Instantiate();
		scene.Name = "Level1";
		AddChild(scene);
		GetNode<Ferret>("Level1/Ferret").GameOver += OnGameOver;
		GetNode<CanvasLayer>("GameWonUI").Show();
	}
}
