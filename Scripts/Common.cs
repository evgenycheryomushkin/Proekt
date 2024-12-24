using Godot;
using System;

public partial class Common : Node
{
    private ulong StartTime;
    private int Shag = 100; //milliseconds
    public static int Tick = 0;
    private static bool Stop = false;

    public static Vector2 vrag;
    public static Vector2 geroi;

    public override void _Ready()
    {
        StartTime = Time.GetTicksMsec();
    }

    public static void StopGame()
    {
        if (!Stop)
        {
            Stop = true;
            Console.WriteLine("Time: " + Tick);
        }
    }

    public static bool Stopped()
    {
        return Stop;
    }

    public override void _PhysicsProcess(double delta)
    {
        ulong time = Time.GetTicksMsec();
        Tick = ((int)(time - StartTime)) / Shag;
    }
}
