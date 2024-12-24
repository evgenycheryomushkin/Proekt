using Godot;
using System;
using System.Diagnostics;
using System.Threading;

public partial class VragS : Object
{
    private Controller controller;

    public override void _Ready()
    {
        controller = new VragController(0,0,1150,650);
    }

    protected override Tuple<float, int> Decide()
    {
        Timer.vrag = Position;
        int x = (int)Position.X;
        int y = (int)Position.Y;
        int geroiX = (int)Timer.geroi.X;
        int geroiY = (int)Timer.geroi.Y;
        if (!controller.collapse(x, y, geroiX, geroiY))
        {
            float angle = Rotation;
            return controller.Decision(x, y, angle, geroiX, geroiY, Power);
        }
        else
        {
            Console.WriteLine("catch " + Timer.Ticknew);
            return new Tuple<float, int>(0, 0);
        }
    }

    protected override int Rashod(int V)
    {
        return 5 - V;
    }
}
