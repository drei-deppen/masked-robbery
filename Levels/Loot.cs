using Godot;
using System;
using System.Threading.Tasks;
using MaskedRobbery;

public partial class Loot : Area2D
{
    [Signal]
    public delegate void TakenEventHandler();
    
    [Export] private int _value = 100;
    private Ferret _ferret;

    public void OnBodyEnters(Node2D body)
    {
        GD.Print(body.Name + " approaches " + Name);
        if (body is Ferret ferret)
            _ferret = ferret;
    }

    public void OnBodyLeaves(Node2D body)
    {
        GD.Print(body.Name + " walks away from " + Name);
        if (body is Ferret)
            _ferret = null;
    }

    private async void FreeLater()
    {
        await Task.Delay(250);
        QueueFree();
        ;
    }

    private async void _take()
    {
        await _ferret.Grab();
        ScoreService.Add(_value);
        EmitSignal(SignalName.Taken);
        FreeLater();
        _ferret.Idle();
    }

    public override void _Input(InputEvent @event)
    {
        if (null != _ferret && @event.IsActionPressed("Up"))
            _take();
        else
            base._Input(@event);
    }
}
