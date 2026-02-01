using System.Threading.Tasks;
using Godot;

public partial class Flash : Sprite2D
{
	[Export] private AudioStream _sound;
	
	private AudioStreamPlayer2D flashSound;
		
	public override void _Ready()
	{
		Modulate.A = 0;
		base._Ready();
		flashSound = new AudioStreamPlayer2D();
		AddChild(flashSound);
		flashSound.Stream = _sound;
		
		Flashing();
	}
	
	public async Task Flashing()
	{
		flashSound.Playing = true;
		Scale.x = 0;
		Scale.y = 0;
		Modulate.a = 1;
		tween.SetProcessMode(Tween.TweenProcessMode.Physics);
		tween.TweenProperty(this, "modulate:a", 1f, 0);
		tween.TweenProperty(this, "scale", 0, 4);
		await ToSignal(tween, "finished");
	}
}
