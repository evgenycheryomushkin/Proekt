using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class VragFrontRunning : Controller
{
	private float LotherX = 0.0f;
	private float LotherY = 0.0f;
	private float lastotherX = 0.0f;
	private float lastotherY = 0.0f;

	public VragFrontRunning(float x0, float y0, float xMax, float yMax) : base(x0, y0, xMax, yMax)
	{
	}

	public override Tuple<float, int> ChooseAngleAndSpeed(float x, float y, float angle, float otherX, float otherY, int health, int Tick)
	{
		float dx = otherX - x;
		float dy = otherY - y;
		float Mdx = otherX - x + (otherX - lastotherX);
		float Mdy = otherY - y + (otherY - lastotherY);
		float Ldx = otherX - x + (2*(otherX - lastotherX));
		float Ldy = otherY - y + (2*(otherY - lastotherY));
		float distanceX = Mathf.Abs(otherX - x);
		float distanceY = Mathf.Abs(otherY - y);

		float Shortalpha = Mathf.Atan2(dy, dx);
		float Longalpha = Mathf.Atan2(Ldy, Ldx);
		float Midlalpha = Mathf.Atan2(Mdy, Mdx);

		lastotherX = LotherX;
		lastotherY = LotherY;
		LotherX = otherX;
		LotherY = otherY;

		//        Console.WriteLine("Tick = " + Game.Tick + "; lastotherX = " + lastotherX + "; otherX = " + otherX + "; Shortalpha = " + Shortalpha + "; Longalpha = " + Longalpha + "; Midlalpha = " + Midlalpha);


		Tuple<float, int> res;
		String direction;
		if (distanceX > 220 | distanceY > 220)
		{
			res = new Tuple<float, int>(Longalpha, 2);
			direction = "L";  
		}
		else if (distanceX > 145| distanceY > 145)
		{
			res = new Tuple<float, int>(Midlalpha, 2);
			direction = "M";        
		}
		else
		{
			res = new Tuple<float, int>(Shortalpha, 2);
			direction = "S";
		}

		GD.Print(String.Format("{0,6:000.0}|{1,6:#000.0}|{2,6:#000.0}|{3,6:#000.0}|{4,6:#000.0}|{5,6:#000.0}|{6,6:000.0}|{7,6:000.0}|{8,6:000.0}|{9,6:000.0}|{10,6:000.0}|{11,6:000}",
			x, y,
			otherX, otherY,
			x + Mdx, y + Mdy,
			x + Ldx, y + Ldy,
			direction,
			lastotherX, lastotherY,
			Tick
		));
		return res;
	}
}
