using Godot;

public partial class Ferret : Node2D
{
    public void SetMask(string maskName)
    {
        var masks = GetNode<Node2D>("Masks").FindChildren("*");
        foreach (var mask in masks)
        {
            if (mask is Node2D mask2d)
            {
                mask2d.Visible = mask2d.Name == maskName;
            }
        }
    }
}
