using Godot;
using System;
using System.Threading.Tasks;
using MaskedRobbery.Characters;

public partial class Baby : Character
{
    bool _caught = false;
    
    public override string GetMaskName()
    {
        return "Baby";
    }

    public async override void Yap()
    {
        Busy = true;
        var sprite = GetNode<AnimatedSprite2D>("Sprite");
        sprite.FlipH = Rng.Randf() < 0.5f;
        sprite.Play("Yapping");
        await Task.Delay(Rng.RandiRange(500, 5000));
        if (_caught) return;
        sprite.Play("Idle");
        Busy = false;
    }

    public override void Walk(int where)
    {
        Yap();
    }

    public override bool Catches(Ferret ferret)
    {
        if (ferret.IsMasked("Governess")) return false;
        _caught = true;
        Busy = true;
        GD.Print("WAAHH!!!!");
        GetNode<AnimatedSprite2D>("Sprite").Play("Caught");
        return true;
    }

    public override void Look(string direction)
    {
        GetNode<AnimatedSprite2D>("Sprite").FlipH = direction == "right";
    }
}
