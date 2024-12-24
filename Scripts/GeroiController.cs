using Godot;
using System;

public partial class GeroiController : Controller
{
    private static float A = 1.0f / 500;
    private static float B = 500.0f;
    private static float C = 10.0f;

    private float cx;
    private float cy;

    public GeroiController(float x0, float y0, float xMax, float yMax) : base(x0, y0, xMax, yMax)
    {
        cx = x0 + (xMax - x0) / 2;
        cy = y0 + (yMax - y0) / 2;
    }

    public override Tuple<float, int> Decision(float x, float y, float angle, float otherX, float otherY, int health)
    {
        float dx1 = cx - x;
        float dy1 = cy - y;
        float dx = otherX - x;
        float dy = otherY - y;
        float dxf = dx1 * A + B / dy;
        float dyf = dy1 * A + B / dx;
        float v = C * Mathf.Sqrt(dxf * dxf + dyf * dyf);
        if (v > 20) v = 20;
        if (health < v) v = 0;
        float alpha = Mathf.Atan2(dxf, dyf);
        Console.WriteLine("v = " + v);
        return new Tuple<float, int>(alpha, health);
    }
}
