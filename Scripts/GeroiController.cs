using Godot;
using System;

public partial class GeroiController : Controller
{
	private static float R = 150;
	private static float MULT = 4/3f;
	private static float DESCAPE = 80;

	private float destX;
	private float destY;

	private bool escape = false;

	public GeroiController(float x0, float y0, float xMax, float yMax) : base(x0, y0, xMax, yMax)
	{
	}

	public override Tuple<float, int> ChooseAngleAndSpeed(float x, float y, float angle, 
		float otherX, float otherY, int health, int tick)
	{
		int speed;
		float alpha;
		
		float dx = x - otherX;
		float dy = y - otherY;
		float d = Mathf.Sqrt(dx*dx+dy);
		if (escapeEnd(x,y,otherX,otherY,d)) {
			escape = false;
			speed = 0;
		}
		if (!escape) {
			testEscape(x, y, otherX, otherY);
		}
		if (escape) {
			speed = 20;
			alpha = Mathf.Atan2(destY - y, destX - x);
		} else {
			alpha = Mathf.Atan2(y - otherY, x - otherX);
			if (d < R) {
				speed = 15;
			} else {
				speed = 0;
			}
		}
		if (health < 1) {
			speed = 0;
		}
		GD.Print(String.Format(
				"{0,6:#000.0}|{1,6:#000.0}|" +
				"{2,6:#000.0}|{3,6:#000.0}|" +
				"{4,6:#000.0}|{5,6:#000.0}|" +
				"{6,6:#000.0}|{7,6}|{8,6:#000.0}|{9,6:#00}|{10,6:0000}",
				x, y, otherX, otherY,
				destX, destY, d,
				escape ? "T" : "F", alpha, speed, tick
			));
		return new Tuple<float, int>(alpha, speed);
	}
	
	bool escapeEnd(float x, float y, float otherX, float otherY, float d) {
		if (d > R * MULT) return true;
		float dEscapeX = x - destX;
		float dEscapeY = y - destY;
		float dEscape = Mathf.Sqrt(dEscapeX*dEscapeX + dEscapeY*dEscapeY);
		if (dEscape < DESCAPE) return true;
		return false;
	}
	
	void testEscape(float x, float y, float otherX, float otherY) {
		if (x < 0) {
			// герой прижался к оси Y
			escape = true;
			destX = 2 * R;
			if (y < otherY) {
				// герой прижался к оси Y и враг находится выше
				destY = 0;
			} else {
				// герой прижался к оси Y и враг находится ниже
				destY = yMax;
			}
		}
		if (x > xMax) {
			// герой прижался к оси Y
			escape = true;
			destX = xMax - 2 * R;
			if (y < otherY) {
				// герой прижался к оси Y и враг находится выше
				destY = 0;
			} else {
				// герой прижался к оси Y и враг находится ниже
				destY = yMax;
			}
		}
		if (y < 0) {
			// герой прижался к оси X
			escape = true;
			destY = 2 * R;
			if (x < otherX) {
				// враг находится правее
				destX = 0;
			} else {
				// враг находится левее
				destX = xMax;
			}
		}
		if (y > yMax) {
			// герой прижался к оси X
			escape = true;
			destY = yMax - 2 * R;
			if (x < otherX) {
				// враг находится правее
				destX = 0;
			} else {
				// враг находится левее
				destX = xMax;
			}
		}
	}
}
