using Godot;
using System;

public partial class VragController : Controller
{
    static int DMIN = 1;
    public VragController(int x0, int y0, int xMax, int yMax) : base(x0, y0, xMax, yMax)
    {
    }

    public override Tuple<float, int> Decision(int x, int y, float angle, int otherX, int otherY, int health)
    {
        float alpha = 0f;
        int dx = x - otherX;
        int dy = y - otherY;
        if (Mathf.Abs(dx) < DMIN)
        {
            if (dy > 0)
            {
                alpha = Mathf.Pi / 2;
            }
            else
            {
                alpha = - Mathf.Pi / 2;
            }
        }
        else
        {
            alpha = Mathf.Atan(1.0f * dy / dx);
        }
        Console.WriteLine("dy = " + dy + "; dx = " + dx + "; alpha = " + alpha);
        return new Tuple<float, int>(alpha, 2);
    }
}
