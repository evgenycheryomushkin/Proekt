using Godot;
using System;

public abstract partial class Object : CharacterBody2D
{
    private static int SPEED_MULTIPLIER = 50;

    public int Power = 100;

    private int Tick = 10;

    public override void _PhysicsProcess(double delta)
    {
        MoveAndSlide();
    }

    internal void ChangeSpeedAndDirection()
    {
        Tuple<float, int> angleSpeed = ChooseNewSpeedAndDir();
        float alpha = angleSpeed.Item1;
        int speed = angleSpeed.Item2;
        IncreasePower(speed);

        Rotate(alpha - Transform.Rotation);
        Velocity = Transform.X * SPEED_MULTIPLIER * speed;
    }
    internal void Stop()
    {
        Velocity = Vector2.Zero;
    }

    private Tuple<float, int> ChooseNewSpeedAndDir()
    {
        float x = Position.X;
        float y = Position.Y;
        float opponentX = GetOpponentCoordinates().X;
        float opponentY = GetOpponentCoordinates().Y;

        return GetController().ChooseAngleAndSpeed(x, y, Rotation, opponentX, opponentY, Power);
    }
    protected abstract Vector2 GetOpponentCoordinates();
    protected abstract Controller GetController();
    protected abstract int Rashod(int V);

    private void IncreasePower(int V)
    {
        int R = Rashod(V);
        Power = Power + R;
        if (Power > 100)
        {
            Power = 100;
        }
    }
}
