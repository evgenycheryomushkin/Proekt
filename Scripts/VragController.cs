using Godot;
using System;

public partial class VragController : Controller
{
    public VragController(float x0, float y0, float xMax, float yMax) : base(x0, y0, xMax, yMax)
    {
    }

    public override Tuple<float, int> Decision(float x, float y, float angle, float otherX, float otherY, int health)
    {
        float dx = otherX - x;
        float dy = otherY - y;

        float alpha = Mathf.Atan2(dy, dx);
      
        Console.WriteLine("vx = " + x + "; vy = " + y + "; gx = " + otherX + "; gy = " + otherY + "; dy = " + dy + "; dx = " + dx + "; alpha = " + alpha);
        return new Tuple<float, int>(alpha, 2);
    }
}
