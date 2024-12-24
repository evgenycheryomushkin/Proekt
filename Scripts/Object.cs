using Godot;
using System;

public abstract partial class Object : CharacterBody2D
{
    private static int SPEED_MULTIPLIER = 50;

    public int Power = 100;

    private int Tick = 10;

    public override void _Ready()
    {
        SavePosition();
    }

    public override void _PhysicsProcess(double delta)
    {
        if (Common.Tick > Tick)
        {
            Tick = Common.Tick;
            ChangeSpeedAndDirection();
        }
        bool moved = MoveAndSlide();
    }

    private void ChangeSpeedAndDirection()
    {
        Tuple<float, int> angleSpeed = ChooseNewSpeedAndDir();
        float alpha = angleSpeed.Item1;
        int speed = angleSpeed.Item2;

        Rotate(alpha - Transform.Rotation);
        Velocity = Transform.X * SPEED_MULTIPLIER * speed;

        IncreasePower(speed);
    }

    private Tuple<float, int> ChooseNewSpeedAndDir()
    {
        if (Common.Stopped())
        {
            return new Tuple<float, int>(0, 0);
        }

        SavePosition();
        float x = Position.X;
        float y = Position.Y;
        float opponentX = GetOpponentCoordinates().X;
        float opponentY = GetOpponentCoordinates().Y;

        if (GetController().Collides(x, y, opponentX, opponentY))
        {
            Common.StopGame();
            return new Tuple<float, int>(0, 0);
        }
        else
        {
            return GetController().Decision(x, y, Rotation, opponentX, opponentY, Power);
        }
    }
    protected abstract Vector2 GetOpponentCoordinates();
    protected abstract Controller GetController();
    protected abstract void SavePosition();
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
