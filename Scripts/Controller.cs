using Godot;
using System;

public abstract partial class Controller
{
    float x0, y0, xMax, yMax;
    public Controller(float x0, float y0, float xMax, float yMax)
    {
        this.x0 = x0;
        this.y0 = y0;
        this.xMax = xMax;
        this.yMax = yMax;
    }

    public bool collapse(float x1, float y1, float x2, float y2)
    {
        float dx = x1 - x2;
        float dy = y1 - y2;
        float d2 = dx * dx + dy * dy;
        if (d2 < 20000) 
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public abstract Tuple<float, int> Decision(float x, float y, float angle, float otherX, float otherY, int health);
}
