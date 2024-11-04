using Godot;
using System;

public partial class VragController : Controller
{
    public VragController(int x0, int y0, int xMax, int yMax) : base(x0, y0, xMax, yMax)
    {
    }

    public override Tuple<float, int> Decision(int x, int y, float angle, int otherX, int otherY, int health)
    {
        return new Tuple<float, int>(0.1f, 0);
    }
}
