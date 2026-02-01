using System.Threading.Tasks;
using Godot;

public partial class LevelHider : CanvasLayer
{
	public async Task FadeOut()
	{
		var rect = GetNode<ColorRect>("ColorRect");
		var tween = CreateTween();
		tween.SetProcessMode(Tween.TweenProcessMode.Physics);
		tween.TweenProperty(rect, "modulate:a", 0f, 2.0);
		await ToSignal(tween, "finished");
		Visible = false;
	}
	
	public async Task FadeIn()
	{
		Visible = false;
		var rect = GetNode<ColorRect>("ColorRect");
		var tween = CreateTween();
		tween.SetProcessMode(Tween.TweenProcessMode.Physics);
		tween.TweenProperty(rect, "modulate:a", 1f, 2.0);
		await ToSignal(tween, "finished");
	}
}
