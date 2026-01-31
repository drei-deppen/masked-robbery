using Godot;

public partial class Ferret : CharacterBody2D
{
    private Vector2 _direction = Vector2.Zero;
    private float _speed = 5f;

    public void SetMask(string maskName)
    {
        GD.Print(Name + " tries to mask as " + maskName);
        var masks = GetNode<Node2D>("Masks").FindChildren("*");
        foreach (var mask in masks)
            if (mask is Node2D mask2d)
            {
                mask2d.Visible = mask2d.Name == maskName;
                if (mask2d.Visible) GD.Print(Name + " now masks as " + maskName);
            }
    }

    public bool IsMasked(string maskName = null)
    {
        var masks = GetNode<Node2D>("Masks").FindChildren("*");
        foreach (var mask in masks)
        {
            if (mask is not Node2D mask2d || !mask2d.IsVisible()) continue;
            if (null == maskName) return true;
            if (mask2d.Name == maskName) return true;
        }

        return false;
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("Left"))
            _direction = Vector2.Left;
        else if (@event.IsActionPressed("Right"))
            _direction = Vector2.Right;
        else if (@event.IsActionReleased("Right") || @event.IsActionReleased("Left"))
            _direction = Vector2.Zero;
    }

    public override void _PhysicsProcess(double delta)
    {
        MoveAndCollide(_direction * _speed);
        base._PhysicsProcess(delta);
    }
}