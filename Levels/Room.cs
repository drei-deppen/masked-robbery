using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using MaskedRobbery.Characters;

public partial class Room : Area2D
{
    [Signal]
    public delegate void CaughtEventHandler();
    private Ferret _ferret;
    private readonly List<Character> _characters = new();

    public void OnBodyEnters(Node2D body)
    {
        GD.Print(body.Name + " enters " + Name);
        switch (body)
        {
            case Ferret ferret:
                _ferret = ferret;
                break;
            case Character character:
                _characters.Add(character);
                character.CurrentRoom = this;
                break;
        }

        if (_ferret == null || _characters.Count <= 0) return;
        if (
            !_ferret.IsMasked()
            || _characters.Any(character => _ferret.IsMasked(character.GetMaskName()))
        ) EmitSignal(SignalName.Caught);
    }

    public bool JustMe(Node2D me)
    {
        return null == _ferret && _characters.All(character => character == me);
    }

    public void OnBodyLeaves(Node2D body)
    {
        GD.Print(body.Name + " leaves " + Name);
        switch (body)
        {
            case Ferret:
                _ferret = null;
                break;
            case Character character:
                _characters.Remove(character);
                character.CurrentRoom = null;
                break;
        }
    }
}