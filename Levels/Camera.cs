using Godot;
using System;

public partial class Camera : Camera2D
{
	[Export] private Node2D _focus;
	
	public override void _Process(double delta)
	{
		if (null != _focus)
		{
			GlobalPosition = _focus.GetGlobalPosition();
		}
	}
}
