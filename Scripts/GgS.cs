using Godot;

public partial class GgS : Object
{
    private Controller controller;

    public override void _Ready()
    {
        controller = new GeroiController(0, 0, 1150, 650);
    }

    protected override void SavePosition()
    {
        Common.geroi = Position;
    }

    protected override Vector2 GetOpponentCoordinates()
    {
        return Common.vrag;
    }

    protected override Controller GetController()
    {
        return controller;
    }

    protected override int Rashod(int V)
    {
        return 5 - V + 1;
    }
}
