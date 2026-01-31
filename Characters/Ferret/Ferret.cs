using Godot;

public partial class Ferret : CharacterBody2D
{
    private Vector2 _direction = Vector2.Zero;
    private float _speed = 5f;

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

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("Left"))
        {
            _direction = Vector2.Left;
        }
        else if (@event.IsActionPressed("Right"))
        {
            _direction = Vector2.Right;
        }
        else if (@event.IsActionReleased("Right") || @event.IsActionReleased("Left"))
        {
            _direction = Vector2.Zero;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        GlobalPosition += _direction * _speed;
        base._PhysicsProcess(delta);
    }
}