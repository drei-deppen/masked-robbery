using Godot;
using System;

public partial class Door : Area2D
{
    [Export] private Door _target;
    private Ferret _ferret;

    public void OnBodyEnters(Node2D body)
    {
        if (body is Ferret ferret)
        {
            _ferret = ferret;
            GetNode<Polygon2D>("Polygon2D").Color = Color.Color8(255, 0, 0);
        }
    }

    public void OnBodyLeaves(Node2D body)
    {
        if (body is Ferret ferret)
        {
            _ferret = null;
            GetNode<Polygon2D>("Polygon2D").Color = Color.Color8(255, 255, 255);
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (null != _target && null != _ferret && @event.IsActionPressed("Up"))
        {
            _ferret.GlobalPosition = _target.GetGlobalPosition();
        }
        base._Input(@event);
    }
}
