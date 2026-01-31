using Godot;
using System;
using MaskedRobbery.Characters;

public partial class MaskSelector : Panel
{
	[Signal]
	public delegate void MaskSelectedEventHandler(string maskName);
	
	private bool _inCameraMode = false;

	public void OnCharacterPhotographed(Character character)
	{
		if (!_inCameraMode) return;
		GD.Print("*Click*");
		_inCameraMode = false;
		EnableMask(character.GetMaskName());
	}

	public void EnableMask(string maskName)
	{
		foreach (var maskButton in GetNode<Node>("Layout/Masks").FindChildren("*"))
			if (maskButton is CanvasItem maskButton2d && maskButton2d.Name == maskName)
				maskButton2d.Visible = true;
	}

	public void OnCameraPressed()
	{
		_inCameraMode = true;
		GD.Print("Camera mode on");
	}

	public void OnFerretPressed()
	{
		EmitSignal(SignalName.MaskSelected, "Ferret");
	}

	public void OnFrogPressed()
	{
		EmitSignal(SignalName.MaskSelected, "Toad");
	}

	public void OnHarePressed()
	{
		EmitSignal(SignalName.MaskSelected, "Hare");
	}
}
