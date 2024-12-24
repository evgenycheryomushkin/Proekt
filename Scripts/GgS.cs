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
        int x = (int)Position.X;
        int y = (int)Position.Y;
        int vragX = (int)Timer.vrag.X;
        int vragY = (int)Timer.vrag.Y;
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
