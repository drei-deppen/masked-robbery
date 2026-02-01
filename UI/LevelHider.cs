using System.Threading.Tasks;
using Godot;

public partial class LevelHider : CanvasLayer
{
	public async Task FadeOut()
	{
		var rect = GetNode<ColorRect>("ColorRect");
		rect.Modulate = new Color(0, 0, 0);
		var tween = CreateTween();
		tween.SetProcessMode(Tween.TweenProcessMode.Physics);
		tween.TweenProperty(rect, "modulate:a", 0f, 2.0);
		await ToSignal(tween, "finished");
		Visible = false;
	}
	
	public async Task FadeIn()
	{
		var rect = GetNode<ColorRect>("ColorRect");
		rect.Modulate = new Color(0, 0, 0, 0);
		Visible = true;
		var tween = CreateTween();
		tween.SetProcessMode(Tween.TweenProcessMode.Physics);
		tween.TweenProperty(rect, "modulate:a", 1f, 2.0);
		await ToSignal(tween, "finished");
	}
}
