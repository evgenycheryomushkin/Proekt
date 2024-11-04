using Godot;
using System;

public abstract partial class Controller
{
    int x0, y0, xMax, yMax;
    public Controller(int x0, int y0, int xMax, int yMax)
    {
        this.x0 = x0;
        this.y0 = y0;
        this.xMax = xMax;
        this.yMax = yMax;
    }

    public bool collapse(int x1, int y1, int x2, int y2)
    {
        int dx = x1 - x2;
        int dy = y1 - y2;
        int d2 = dx * dx + dy * dy;
        if (d2 < 16384) 
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public abstract Tuple<float, int> Decision(int x, int y, float angle, int otherX, int otherY, int health);
}
