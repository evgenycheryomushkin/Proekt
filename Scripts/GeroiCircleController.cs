using Godot;
using System;

public partial class GeroiCircleController : Controller
{
	private static float A = 1500;
	private static float B = 0.005f;

	private float cx;
	private float cy;

	public GeroiCircleController(float x0, float y0, float xMax, float yMax) : base(x0, y0, xMax, yMax)
	{
		cx = x0 + (xMax - x0) / 2;
		cy = y0 + (yMax - y0) / 2;
	}

	public override Tuple<float, int> ChooseAngleAndSpeed(float x, float y, float angle, 
		float otherX, float otherY, int health, int tick)
	{
		float dx = cx - x;
		float dy = cy - y;
		float dxv = otherX - x;
		float dyv = otherY - y;
		
		float dvrag = Mathf.Sqrt(dxv * dxv + dyv * dyv);
		if (dvrag < 20) dvrag = 20;
		float v = A / dvrag;
		if (v > 20) v = 20;
		if (health < v) v = 0;
		float alpha = Mathf.Atan2(dy, dx) - Mathf.Pi / 2.0f + B * v;
		return new Tuple<float, int>(alpha, (int)v);
	}
}
