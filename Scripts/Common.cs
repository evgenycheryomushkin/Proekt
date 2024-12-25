using Godot;
using System;

public partial class Common     
{
    private static int MIN_DISTANCE = 80;
    public static bool Collides(float x1, float y1, float x2, float y2)
    {
        float dx = x1 - x2;
        float dy = y1 - y2;
        float d2 = dx * dx + dy * dy;
        return d2 < MIN_DISTANCE * MIN_DISTANCE;
    }
}
