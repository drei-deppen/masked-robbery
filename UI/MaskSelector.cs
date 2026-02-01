using Godot;
using System;
using MaskedRobbery.Characters;

public partial class MaskSelector : Panel
{
	[Signal]
	public delegate void MaskSelectedEventHandler(string maskName);
	
	[Signal]
	public delegate void FlashedEventHandler();
	
	private bool _inCameraMode = false;

	public void OnCharacterPhotographed(Character character)
	{
		if (!_inCameraMode) return;
		GD.Print("*Click*");
		EmitSignal(SignalName.Flashed);
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

	public void OnSirePressed()
	{
		EmitSignal(SignalName.MaskSelected, "Sire");
	}

	public void OnDamePressed()
	{
		EmitSignal(SignalName.MaskSelected, "Dame");
	}

	public void OnGovernessPressed()
	{
		EmitSignal(SignalName.MaskSelected, "Governess");
	}
}
