using Godot;
using System;

public partial class LevelHider : ColorRect
{
	float alpha = 1.2f;
	bool reset = false;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Process(double delta)
	{
		this.SetColor(new Color(1, 1, 1, alpha));
		if (!reset)
		{
			if(alpha>0f)
			alpha -= (float)delta;
		}
		else
			{
				alpha += (float)delta;
				if(alpha>1.2f)
					GetTree().ReloadCurrentScene();
			}
	}

	public void Reset()
	{
		reset = true;
	}
}
