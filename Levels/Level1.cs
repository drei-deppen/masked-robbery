using Godot;
using System;
using System.ComponentModel.DataAnnotations;

public partial class Level1 : Node
{
	public void OnMaskSelected(string maskName)
	{
		GetNode<Ferret>("Ferret").SetMask(maskName);
	}
}
