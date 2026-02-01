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
		_sprite.Play("Yapping");
		await Task.Delay(Rng.RandiRange(500, 5000));
		_sprite.Play("Idle");
		Busy = false;
	}

	public override async void Walk(int where)
	{
		Busy = true;
		_sprite = GetNode<AnimatedSprite2D>("Sprite");
		_sprite.FlipH = where < 0;
		_sprite.Play("Walking");
		_stepSounds.Playing = true;
		await Move(Position + Vector2.Right * where);
		_sprite.Play("Idle");
		_stepSounds.Playing = false;
		Busy = false;
	}

	public override void Look(string direction)
	{
		if (Busy) return;
		_sprite.FlipH = direction == "right";
	}

	public override bool Catches(Ferret ferret)
	{
		return false;
	}
}
