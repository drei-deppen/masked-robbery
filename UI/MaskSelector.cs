using Godot;
using System;

public partial class MaskSelector : Panel
{
	[Signal]
	public delegate void MaskSelectedEventHandler(string maskName);

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
