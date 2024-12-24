using Godot;
using Godot.NativeInterop;
using System;

public partial class GgS : Object
{
    private Controller controller;

    public override void _Ready()
    {
        controller = new GeroiController(0, 0, 1150, 650);
    }

    protected override Tuple<float, int> Decide()
    {
        Timer.geroi = Position;
        float x = Position.X;
        float y = Position.Y;
        float vragX = Timer.vrag.X;
        float vragY = Timer.vrag.Y;
        if (!controller.collapse(x, y, vragX, vragY))
        {
            float angle = Rotation;
            return controller.Decision(x, y, angle, vragX, vragY, Power);
        } else
        {
            return new Tuple<float, int>(0, 0);
        }
    }

    protected override int Rashod(int V)
    {
        return 5 - V + 1;
    }
}
