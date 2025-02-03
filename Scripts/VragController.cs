using Godot;
using System;

public partial class VragController : Controller
{
	public VragController(float x0, float y0, float xMax, float yMax) : base(x0, y0, xMax, yMax)
	{
	}

	public override Tuple<float, int> ChooseAngleAndSpeed(float x, float y, float angle, 
		float otherX, float otherY, int health, int tick)
	{
		float dx = otherX - x;
		float dy = otherY - y;

		float alpha = Mathf.Atan2(dy, dx);
		
		int speed = 4;
		//if (tick > 400) {
			//if (health > 20) speed = 20;
		//}
	
		return new Tuple<float, int>(alpha, speed);
	}
}
