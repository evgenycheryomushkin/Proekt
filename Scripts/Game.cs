using Godot;
using System;

public partial class Game : Node
{
	private static int SHAG = 100; //milliseconds

	private ulong GameStartTimeMs;
	private static bool Stopped = false;
	/**
	 * Текущий шаг в игре. За одну секунду проходит 10 шагов
	 */
	public static int Tick = 0;
	public static int PreviousTick = -1;

	public static Object vrag;
	public static Object geroi;

	public override void _Ready()
	{
		GameStartTimeMs = Time.GetTicksMsec();
	}

	public override void _Process(double delta)
	{
		ulong time = Time.GetTicksMsec();
		Tick = ((int)(time - GameStartTimeMs)) / SHAG;
		if (PreviousTick < Tick)
		{
			PreviousTick = Tick;
			Step();
		}
	}

	private void Step()
	{
		if (Stopped) { return; }

		if (Common.Collides(geroi.Position.X, geroi.Position.Y, vrag.Position.X, vrag.Position.Y) 
			|| Tick >= 600)
		{
			StopGame();
		} else
		{
			vrag.ChangeSpeedAndDirection(Tick);
			geroi.ChangeSpeedAndDirection(Tick);
			GD.Print(String.Format(
				"{0,6:#000.0}|{1,6:#000.0}|{2,6:#00}|" +
				"{3,6:#000.0}|{4,6:#000.0}|" +
				"{5,6:#000.0}|{6,6:#000.0}|{7,6:#00}|" +
				"{8,6:000}|{9,6:000}|{10,6:000}",
				geroi.Position.X, geroi.Position.Y, geroi.GetSpeed(), 
				vrag.Position.X - geroi.Position.X, vrag.Position.Y - geroi.Position.Y,
				vrag.Position.X, vrag.Position.Y, vrag.GetSpeed(),
				geroi.Power, vrag.Power, Tick
			));

		}
	}

	private void StopGame()
	{
		if (!Stopped)
		{
			Stopped = true;
			geroi.Stop();
			vrag.Stop();
			Console.WriteLine("Game ended. Time: " + Tick/10.0f);
		}
	}
}
