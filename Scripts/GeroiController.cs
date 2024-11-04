using Godot;
using System;

public partial class GeroiController : Controller
{
    public GeroiController(int x0, int y0, int xMax, int yMax) : base(x0, y0, xMax, yMax)
    {
    }

    public override Tuple<float, int> Decision(int x, int y, float angle, int otherX, int otherY, int health)
    {
        return new Tuple<float, int>(0, 1);



    }
}
