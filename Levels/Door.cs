using Godot;
using System;
using System.Threading.Tasks;
using Timer = System.Timers.Timer;

public partial class Door : Area2D
{
    [Export] private Door _target;
    private Ferret _ferret;
    private int _transferTime = 250;

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
        if (body is Ferret)
        {
            _ferret = null;
            GetNode<Polygon2D>("Polygon2D").Color = Color.Color8(255, 255, 255);
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("Up"))
        {
            _moveFerret();
        }
        base._Input(@event);
    }

    private async void _moveFerret()
    {
        if (null == _target || null == _ferret)
        {
            return;
        }
        
        var camera = GetViewport().GetCamera2D();
        var ferret = _ferret;
        
        // TODO Play Animation

        ferret.Visible = false;
        ferret.GlobalPosition = _target.GetGlobalPosition();
        
        var tween = CreateTween();
        tween.SetEase(Tween.EaseType.InOut);
        tween.SetProcessMode(Tween.TweenProcessMode.Physics);
        tween.TweenProperty(camera, "global_position", _target.GetGlobalPosition(), _transferTime / 1000.0);
        await ToSignal(tween, "finished");
        
        // TODO Play Target animation
        
        ferret.Visible = true;
        
    }
}
