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

    public override void _PhysicsProcess(double delta)
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

        if (Common.Collides(geroi.Position.X, geroi.Position.Y, vrag.Position.X, vrag.Position.Y))
        {
            StopGame();
        } else
        {
            vrag.ChangeSpeedAndDirection();
            geroi.ChangeSpeedAndDirection();
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
