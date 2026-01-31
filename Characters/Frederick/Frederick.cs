using Godot;
using System;
using System.Numerics;
using System.Threading.Tasks;
using MaskedRobbery.Characters;
using Vector2 = Godot.Vector2;

public partial class Frederick : Character
{
    public override string GetMaskName()
    {
        return "Toad";
    }

    public override async void Yap()
    {
        Busy = true;
        var sprite = GetNode<AnimatedSprite2D>("Sprite");
        sprite.Play("Yapping");
        await Task.Delay(Rng.RandiRange(500, 5000));
        sprite.Play("Idle");
        Busy = false;
    }

    public override async void Walk(int where)
    {
        Busy = true;
        var sprite = GetNode<AnimatedSprite2D>("Sprite");
        sprite.FlipH = where < 0;
        sprite.Play("Walking");
        await Move(Position + Vector2.Right * where);
        sprite.Play("Idle");
        Busy = false;
    }

    public override void Look(string direction)
    {
        if (Busy) return;
        GetNode<AnimatedSprite2D>("Sprite").FlipH = direction == "right";
    }

    public override bool Catches(Ferret ferret)
    {
        return false;
    }
}
