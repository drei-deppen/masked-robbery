using System.Threading.Tasks;
using Godot;

public partial class Ferret : CharacterBody2D
{
	[Export] private Flash _Flash;
	
	[Signal]
	public delegate void GameOverEventHandler();
	
	private Vector2 _direction = Vector2.Zero;
	private float _speed = 5f;
	private RandomNumberGenerator _rng = new();
	private bool _caught = false;

	private Node2D _masks;
	private AnimatedSprite2D _sprite;
	private AudioStreamPlayer2D _stepSounds;
	private AudioStreamPlayer2D _grabSound;

	public override void _Ready()
	{
		base._Ready();
		_masks = GetNode<Node2D>("Masks");
		_sprite = GetNode<AnimatedSprite2D>("Sprite");
		_stepSounds = GetNode<AudioStreamPlayer2D>("StepSounds");
		
		_grabSound = new AudioStreamPlayer2D();
		AddChild(_grabSound);
	}

	public void Caught()
	{
		_caught = true;
		SetMask("Ferret");
		_sprite.FlipV = true;
		EmitSignal(SignalName.GameOver);
	}

	public async Task Grab(AudioStream sound = null)
	{
		_sprite.Play("Grabbing");
		await ToSignal(_sprite, "animation_finished");
		
		_grabSound.Stream = sound;
		_grabSound.Playing = true;

	}

	public void Idle()
	{
		_sprite.Play("Idle");
	}
	
	public void OnFlashed() => _Flash.Flashing();

	public void SetMask(string maskName)
	{
		if (_caught) return;
		GD.Print(Name + " tries to mask as " + maskName);
		var masks = _masks.FindChildren("*");
		foreach (var mask in masks)
			if (mask is Node2D mask2d)
			{
				mask2d.Visible = mask2d.Name == maskName;
				if (mask2d.Visible) GD.Print(Name + " now masks as " + maskName);
			}
	}

	public bool IsMasked(string maskName = null)
	{
		var masks = _masks.FindChildren("*");
		foreach (var mask in masks)
		{
			if (mask is not Node2D mask2d || !mask2d.IsVisible()) continue;
			if (null == maskName) return true;
			if (mask2d.Name == maskName) return true;
		}

		return false;
	}

	/// <summary>
	/// This method is used for Moving the Fettet.
	/// </summary>
	/// <param name="event"></param>
	/// <returns></returns>
	public override void _Input(InputEvent @event)
	{
		// If cuaght there will be a different set of controls.
		if (_caught) return;

		// Default Controls
		if (@event.IsActionPressed("Left"))
			_walk(Vector2.Left);
		else if (@event.IsActionPressed("Right"))
			_walk(Vector2.Right);
		else if (@event.IsActionReleased("Right") || @event.IsActionReleased("Left"))
		{
			_masks.Position = GetNode<Node2D>("MaskIdle" + (_sprite.FlipH ? "Right" : "Left")).Position;
			_direction = Vector2.Zero;
			_stepSounds.Playing = false;
			_sprite.Play("Idle");
		}
	}

	private void _walk(Vector2 direction)
	{
		_direction = direction;
		_sprite.FlipH = Vector2.Right == direction;
		foreach (var mask in _masks.FindChildren("*"))
			if (mask is Sprite2D mask2d)
				mask2d.FlipH = _sprite.FlipH;
		_masks.Position = GetNode<Node2D>("MaskWalking" + (_sprite.FlipH ? "Right" : "Left")).Position;
		_stepSounds.Playing = true;
		_sprite.Play("Walking");
	}

	public override void _PhysicsProcess(double delta)
	{
		if (!_caught)
			MoveAndCollide(_direction * _speed);
		base._PhysicsProcess(delta);
	}

	public void OnBlinkTimer()
	{
		if (_caught) return;
		if (_direction.Length() > 0.1f) return;
		_sprite.Play("Yapping");
		GetNode<Timer>("BlinkTimer").WaitTime = _rng.Randf() + 2.5f;
	}
}
