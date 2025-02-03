using Godot;
using System;

public partial class DangerZoneGeroiController : Controller
{
	static float R1 = 200f;
	static float R2 = 250f;
	static float D = 100f;
	static float D2 = 10f;
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
		float[] destination = new float[]{0f, -1f,-1f};
		if (inDangerZone) {
			destination = findClosestPoint(otherX, otherY, x, y, angle);
			a = Mathf.Atan2(destination[2] - y, destination[1] - x);
			s = (int)(destination[0] / 5);
			int smax;
			if (health > 20) {
				smax = 20;
			} else {
				smax = health; 
			}
			if (s > smax) {
				s = smax;
			}
		}
		//GD.Print(String.Format(
			//"{0,6:#000.0}|{1,6:#000.0}|" +
			//"{2,6:#000.0}|{3,6:#000.0}|" +
			//"{4,6:#000.0}|{5,6:#000}|{6,6:#000}|" +
			//"{7,8:#000.0}|{8,8:#000.0}|{9,8:#000}", 
			//x, y, otherX, otherY, a, s, inDangerZone ? "D" : " ", 
			//destination[1], destination[2], health
		//));
		
		return new Tuple<float, int>(a, s);
	}
	
	float[] findClosestPoint(float otherX, float otherY, float x, float y, float origAngle) {
		bool wminSet = false;
		float wmin = 0;
		float xmin = 0;
		float ymin = 0;
		// enumerate 12 points around enemy
		for(float angle = -Mathf.Pi; angle < Mathf.Pi; angle += 2 * Mathf.Pi / 600) {
			float px = otherX + Mathf.Cos(angle) * (R1 + D2);
			float py = otherY + Mathf.Sin(angle) * (R1 + D2);
			if (closeToWall(px, py) || closeToCorner(px, py)) continue;
			float dx = x - px;
			float dy = y - py;
			float d = Mathf.Sqrt(dx*dx+dy*dy);
			if (!wminSet || d < wmin) {
				wminSet = true;
				wmin = d;
				xmin = px;
				ymin = py;
			}
		}
		return new float[]{wmin, xmin, ymin};
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
		float DC = R2+D;
		// test left up corner - point (R+x0,R+y0)
		if (x < DC+x0 && y < DC+y0) {
			float dx = DC+x0 - x;
			float dy = DC+y0 - y;
			if (Mathf.Sqrt(dx*dx+dy*dy) > R2) return true;
		}
		// test left bottom corner - point (R+x0,yMax-R)
		if (x < DC+x0 && y > yMax-DC) {
			float dx = DC+x0   - x;
			float dy = yMax-DC - y;
			if (Mathf.Sqrt(dx*dx+dy*dy) > R2) return true;
		}
		// test right bottom corner - point (xMax-R,yMax-R)
		if (x > xMax-DC && y > yMax-DC) {
			float dx = xMax-DC - x;
			float dy = yMax-DC - y;
			if (Mathf.Sqrt(dx*dx+dy*dy) > R2) return true;
		}
		// test right up corner - point (xMax-R,R+y0)
		if (x > xMax-DC && y < DC) {
			float dx = xMax-DC - x;
			float dy = DC+y0 - y;
			if (Mathf.Sqrt(dx*dx+dy*dy) > R2) return true;
		}
		return false;
	}
	
	bool closeToEnemy(float x, float y, float otherX, float otherY) {
		float dx = otherX - x;
		float dy = otherY - y;
		return Mathf.Sqrt(dx*dx+dy*dy) < R1;
	}
}
