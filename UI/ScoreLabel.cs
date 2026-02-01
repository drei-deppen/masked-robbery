using Godot;
using System;
using MaskedRobbery;

public partial class ScoreLabel : Label
{
    [Export] private string _format = "{0} €";

    public override void _PhysicsProcess(double delta)
    {
        Text = string.Format(_format, ScoreService.Score);
        base._PhysicsProcess(delta);
    }
}
