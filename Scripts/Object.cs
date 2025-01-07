using Godot;
using System;

public abstract partial class Object : CharacterBody2D
{
	public static int SPEED_MULTIPLIER = 50;
	public static int MAX_HEALTH = 900;
	// Чтобы посмотреть или поменять размеры окна нужно зайти в 
	// Проект/Настройки проекта/Окно
	protected static float XMAX = 1000;
	protected static float YMAX = 1000;

	public int Power = MAX_HEALTH;


	public override void _PhysicsProcess(double delta)
	{
		MoveAndSlide();
	}

	internal void ChangeSpeedAndDirection(int Tick)
	{
		Tuple<float, int> angleSpeed = ChooseNewSpeedAndDir(Tick);
		float alpha = angleSpeed.Item1;
		int speed = angleSpeed.Item2;
		IncreasePower(speed);

		Rotate(alpha - Transform.Rotation);
		Velocity = Transform.X * SPEED_MULTIPLIER * speed;
	}

	public int GetSpeed()
	{
		return (int)(Velocity.Length() / Transform.X.Length() / SPEED_MULTIPLIER);
	}
	internal void Stop()
	{
		Velocity = Vector2.Zero;
	}

	private Tuple<float, int> ChooseNewSpeedAndDir(int Tick)
	{
		float x = Position.X;
		float y = Position.Y;
		float opponentX = GetOpponentCoordinates().X;
		float opponentY = GetOpponentCoordinates().Y;

		return GetController().ChooseAngleAndSpeed(x, y, Rotation, opponentX, opponentY, 
			Power, Tick);
	}
	
	protected abstract Vector2 GetOpponentCoordinates();
	protected abstract Controller GetController();
	protected abstract int Rashod(int V);

	private void IncreasePower(int V)
	{
		int R = Rashod(V);
		Power = Power + R;
		if (Power > MAX_HEALTH)
		{
			Power = MAX_HEALTH;
		}
	}
}
