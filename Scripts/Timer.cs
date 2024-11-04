using Godot;
using System;

public partial class Timer : Node
{
    private ulong StartTime;
    private int Shag = 100; //milliseconds
    public static int Ticknew = 0;
    public static Vector2 vrag;
    public static Vector2 geroi;

    public override void _Ready()
    {
        StartTime = Time.GetTicksMsec();
    }

    public override void _PhysicsProcess(double delta)
    {
        ulong time = Time.GetTicksMsec();
        Ticknew = ((int)(time - StartTime)) / Shag;
    }
}
