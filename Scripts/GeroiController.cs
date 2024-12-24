using Godot;
using System;

public partial class GeroiController : Controller
{
    public GeroiController(float x0, float y0, float xMax, float yMax) : base(x0, y0, xMax, yMax)
    {
    }

    public override Tuple<float, int> Decision(float x, float y, float angle, float otherX, float otherY, int health)
    {
        return new Tuple<float, int>(0.1f, 0);
    }
}
