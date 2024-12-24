using Godot;
using System;

public abstract partial class Object : CharacterBody2D
{
    public int Power = 100;
    public int V = 0;
    protected int VMAX = 15;

    protected int Tick = 0;

    protected abstract Tuple<float, int> Decide();

    public override void _PhysicsProcess(double delta)
    {
        if (Timer.Ticknew > Tick)
        {
            Tick = Timer.Ticknew;
            Tuple<float, int> angleSpeed = Decide();
            float alpha = angleSpeed.Item1;
            Velocity = Transform.X * 50 * angleSpeed.Item2;
            Rotate(alpha - Transform.Rotation);
        }
        bool moved = MoveAndSlide();
    }

    protected int ChangeSpeed(int V)
    {
        if (Power <= 10)
        {
            V = 0;
        }
        return V;
    }

    protected abstract int Rashod(int V);
    protected void IncreasePower(int V)
    {
        int R = Rashod(V);
        Power = Power + R;
        if (Power > 100)
        {
            Power = 100;
        }
    }
}
