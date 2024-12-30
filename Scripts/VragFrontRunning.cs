using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class VragController : Controller
{
    private float LotherX = 1;
    private float LotherY = 1;
    private float lastotherX = 1;
    private float lastotherY = 1;

    public VragController(float x0, float y0, float xMax, float yMax) : base(x0, y0, xMax, yMax)
    {
    }

    public override Tuple<float, int> ChooseAngleAndSpeed(float x, float y, float angle, float otherX, float otherY, int health)
    {
        float dx = otherX - x;
        float dy = otherY - y;
        float Mdx = otherX - x + (otherX - lastotherX);
        float Mdy = otherY - y + (otherY - lastotherY);
        float Ldx = otherX - x + (2*(otherX - lastotherX));
        float Ldy = otherY - y + (2*(otherY - lastotherY));
        float distanceX = Mathf.Abs(otherX - x);
        float distanceY = Mathf.Abs(otherY - y);
        int LastTick = 0;

        float Shortalpha = Mathf.Atan2(dy, dx);
        float Longalpha = Mathf.Atan2(Ldy, Ldx);
        float Midlalpha = Mathf.Atan2(Mdy, Mdx);

        if (LastTick < Game.Tick - 1)
        {
            lastotherX = LotherX;
            lastotherY = LotherY;
            LastTick = Game.Tick - 1;
            LotherX = otherX;
            LotherX = otherY;
        }
      
        Console.WriteLine("vx = " + x + "; vy = " + y + "; gx = " + otherX + "; gy = " + otherY + "; dy = " + dy + "; dx = " + dx);
        if (distanceX > 220 | distanceY > 220)
        {
            return new Tuple<float, int>(Longalpha, 2);
        }
        else if (distanceX > 145| distanceY > 145)
        {
            return new Tuple<float, int>(Midlalpha, 2);
        }
        else
        {
            return new Tuple<float, int>(Shortalpha, 2);
        }
    }
}
