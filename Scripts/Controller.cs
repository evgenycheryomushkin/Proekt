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

    public abstract Tuple<float, int> ChooseAngleAndSpeed(float x, float y, 
        float angle, float otherX, float otherY, int health);
}
