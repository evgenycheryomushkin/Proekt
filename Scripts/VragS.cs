using Godot;

public partial class VragS : Object
{
    private Controller controller;

    public override void _Ready()
    {
        controller = new VragController(0, 0, 1150, 650);
    }

    protected override void SavePosition()
    {
        Common.vrag = Position;
    }

    protected override Vector2 GetOpponentCoordinates()
    {
        return Common.geroi;
    }

    protected override Controller GetController()
    {
        return controller;
    }

    protected override int Rashod(int V)
    {
        return 5 - V;
    }
}
