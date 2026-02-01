using Godot;
using System;

public partial class Splash : CanvasLayer
{
    [Signal]  public delegate void AnyKeyPressedEventHandler();
    
    public override void _PhysicsProcess(double delta)
    {
        var panel = GetNode<Panel>("TitlePanel");
        panel.CustomMinimumSize = new Vector2(
            GetViewport().GetWindow().Size.X,
            panel.CustomMinimumSize.Y
        );
        base._PhysicsProcess(delta);
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventKey { Pressed: true } || @event is InputEventMouseButton { Pressed: true })
        {
            EmitSignal(SignalName.AnyKeyPressed);
        }
    }
}