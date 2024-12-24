using Godot;
using System;

public partial class GeroiCircleController : Controller
{
    private static float A = 1500;

    private float cx;
    private float cy;

    public GeroiCircleController(float x0, float y0, float xMax, float yMax) : base(x0, y0, xMax, yMax)
    {
        cx = x0 + (xMax - x0) / 2;
        cy = y0 + (yMax - y0) / 2;
    }

    public override Tuple<float, int> Decision(float x, float y, float angle, float otherX, float otherY, int health)
    {
        float dx = cx - x;
        float dy = cy - y;
        float dxv = otherX - x;
        float dyv = otherY - y;
        float db = Mathf.Sqrt(dxv * dxv + dyv * dyv);
        if (db < 20) db = 20;
        float v = A/db;
        if (v > 20) v = 20;
        if (health < v) v = 0;
        float alpha = Mathf.Atan2(-dx, dy);
        return new Tuple<float, int>(alpha, (int)v);
    }
}
