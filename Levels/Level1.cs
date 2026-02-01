using Godot;
using MaskedRobbery.Characters;

public partial class Level1 : Node
{
	private RandomNumberGenerator _rng = new();

	public void Start()
	{
		GetNode<CanvasLayer>("UI").Visible = true;
		GetNode<Timer>("BoogieTimer").Start();
		GD.Print("Level started");
	}
	
	public void OnMaskSelected(string maskName)
	{
		GetNode<Ferret>("Ferret").SetMask(maskName);
	}

	public void OnCaught()
	{
		GD.Print("Caught");
		GetNode<Ferret>("Ferret").Caught();
	}

	public void OnBoogieTimer()
	{
		var boogies = GetNode<Node>("Boogies").FindChildren("*");
		var boogie = boogies[_rng.RandiRange(0, boogies.Count-1)];
		if (boogie is Character character)
			character.DoSomething();
	}
}
