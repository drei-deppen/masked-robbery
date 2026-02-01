using System.Threading.Tasks;
using Godot;

public partial class Flash : Sprite2D
{
	[Export] private AudioStream _sound;
	
	private AudioStreamPlayer2D flashSound;
		
	public override void _Ready()
	{
		var mod = Modulate;
		mod.A = 0;
		Modulate = mod;
		base._Ready();
		flashSound = new AudioStreamPlayer2D();
		AddChild(flashSound);
		flashSound.Stream = _sound;
		
		Flashing();
	}
	
	public async Task Flashing()
	{
		flashSound.Playing = true;
		Scale = Vector2.Zero;
		var mod = Modulate;
		mod.A = 1;
		Modulate = mod;
		var tween = CreateTween();
		tween.SetProcessMode(Tween.TweenProcessMode.Physics);
		tween.TweenProperty(this, "modulate:a", 1f, 0);
		tween.TweenProperty(this, "scale", 0, 4);
		await ToSignal(tween, "finished");
	}
}
