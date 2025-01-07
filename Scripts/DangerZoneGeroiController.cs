using Godot;
using System;

public partial class DangerZoneGeroiController : Controller
{
	static float R = 250f;
	static float D = 10f;
	static float D2 = 1f;
	static float ANGLE_MULTIPLIER = 1f;
	
	public DangerZoneGeroiController(float x0, float y0, float xMax, float yMax) : base(x0, y0, xMax, yMax)
	{
	}

	public override Tuple<float, int> ChooseAngleAndSpeed(float x, float y, float angle, 
		float otherX, float otherY, int health, int tick)
	{
		float a = angle;
		int s = 0;
		bool inDangerZone = closeToWall(x, y) || closeToCorner(x, y) || closeToEnemy(x, y, otherX, otherY);
		if (inDangerZone) {
			Tuple<float, float> destination = findClosestPoint(otherX, otherY, x, y, angle);
			a = Mathf.Atan2(destination.Item1 - x, destination.Item2 - y);
			if (health > 25) s = 25;
		}
		GD.Print(String.Format(
			"{0,6:#000.0}|{1,6:#000.0}|" +
			"{2,6:#000.0}|{3,6:#000.0}|" +
			"{4,6:#000.0}|{5,6:#000}|{6,6:#000}", 
			x, y, otherX, otherY, a, s, inDangerZone ? "D" : " "
		));
		
		return new Tuple<float, int>(a, s);
	}
	
	Tuple<float, float> findClosestPoint(float otherX, float otherY, float x, float y, float origAngle) {
		bool wminSet = false;
		float wmin = 0;
		float xmin = 0;
		float ymin = 0;
		// enumerate 12 points around enemy
		for(float angle = -Mathf.Pi; angle < Mathf.Pi; angle += 2 * Mathf.Pi / 60) {
			float px = otherX + Mathf.Cos(angle) * (R + D2);
			float py = otherY + Mathf.Sin(angle) * (R + D2);
			if (closeToWall(px, py) || closeToCorner(px, py)) continue;
			float dx = x - px;
			float dy = y - py;
			float d = Mathf.Sqrt(dx*dx+dy*dy);
			if (!wminSet || d + Mathf.Abs(diff(angle, origAngle)) * ANGLE_MULTIPLIER < wmin) {
				wminSet = true;
				wmin = d + diff(angle, origAngle) * ANGLE_MULTIPLIER;
				xmin = px;
				ymin = py;
			}
		}
		return new Tuple<float, float>(xmin, ymin);
	}
	
	float diff(float a1, float a2) {
		float d = a1 - a2;
		while (d >= Mathf.Pi)  d -= 2 * Mathf.Pi;
		while (d <= -Mathf.Pi) d += 2 * Mathf.Pi;
		return d;
	}
	
	// return true if point is on less than D distance to the wall
	bool closeToWall(float x, float y) {
		if (x < x0   + D) return true;
		if (x > xMax - D) return true;
		if (y < y0   + D) return true;
		if (y > yMax - D) return true;
		return false;
	}
	
	bool closeToCorner(float x, float y) {
		// test left up corner - point (R+x0,R+y0)
		if (x < R+x0 && y < R+y0) {
			float dx = R+x0 - x;
			float dy = R+y0 - y;
			if (Mathf.Sqrt(dx*dx+dy*dy) > R) return true;
		}
		// test left bottom corner - point (R+x0,yMax-R)
		if (x < R+x0 && y > yMax-R) {
			float dx = R+x0   - x;
			float dy = yMax-R - y;
			if (Mathf.Sqrt(dx*dx+dy*dy) > R) return true;
		}
		// test right bottom corner - point (xMax-R,yMax-R)
		if (x > xMax-R && y > yMax-R) {
			float dx = xMax-R - x;
			float dy = yMax-R - y;
			if (Mathf.Sqrt(dx*dx+dy*dy) > R) return true;
		}
		// test right up corner - point (xMax-R,R+y0)
		if (x > xMax-R && y < R) {
			float dx = xMax-R - x;
			float dy = R+y0 - y;
			if (Mathf.Sqrt(dx*dx+dy*dy) > R) return true;
		}
		return false;
	}
	
	bool closeToEnemy(float x, float y, float otherX, float otherY) {
		float dx = otherX - x;
		float dy = otherY - y;
		return Mathf.Sqrt(dx*dx+dy*dy) < R;
	}
}
