using System.Threading.Tasks;
using Godot;

namespace MaskedRobbery.Characters;

public abstract partial class Character: CharacterBody2D
{
    [Signal]
    public delegate void ClickedEventHandler(Character character);

    public Room CurrentRoom { get; set; }
    protected bool Busy = false;
    protected RandomNumberGenerator Rng = new();

    private Vector2 _destination;
    private TaskCompletionSource _walkingTask = null;
    protected float Speed = 50f;

    public override void _PhysicsProcess(double delta)
    {
        if (_walkingTask is { Task.IsCompleted: false })
        {
            var movement = (Position - _destination).Normalized() * (float)delta * Speed;
            if (MoveAndCollide(movement) != null || (Position - _destination).Length() < 0.1f)
            {
                _destination = Position;
                _walkingTask.TrySetResult();
            }
        }
        base._PhysicsProcess(delta);
    }

    public void OnInput(Node viewport, InputEvent @event, int shapeIdx)
    {
        if (@event is InputEventMouseButton { ButtonIndex: MouseButton.Left, Pressed: true })
            EmitSignal(SignalName.Clicked, this);
    }

    public void DoSomething()
    {
        if (Busy) return;
        GD.Print(Name + " does something");
        
        var dice =  Rng.RandiRange(1, 6);
        var canYap = null != CurrentRoom && !CurrentRoom.JustMe(this);
        
        if (canYap && dice < 3)
        {
            GD.Print(Name + " yaps");
            Yap();
        } else if (dice < 5)
        {
            GD.Print(Name + " walks away");
            Walk(Rng.RandiRange(-500, 500));
        }
        else
        {
            GD.Print(Name + " hangs around");
        }
    }

    protected async Task Move(Vector2 destination)
    {
        _destination = destination;
        _walkingTask = new TaskCompletionSource();
        await _walkingTask.Task;
    }

    public virtual bool Catches(Ferret ferret)
    {
        return !ferret.IsMasked() || ferret.IsMasked(GetMaskName());
    }
    
    public abstract string GetMaskName();
    public abstract void Yap();
    public abstract void Walk(int where);
    public abstract void Look(string direction);
}