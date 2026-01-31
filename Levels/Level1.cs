using Godot;

public partial class Level1 : Node
{
	public void OnMaskSelected(string maskName)
	{
		GetNode<Ferret>("Ferret").SetMask(maskName);
	}

	public void OnCaught()
	{
		GD.Print("Caught");
	}
}
