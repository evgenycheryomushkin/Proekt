using Godot;

public partial class VragS : Object
{
	private Controller controller;

	public override void _Ready()
	{
		Game.vrag = this;
		controller = new VragFrontRunning(0, 0, XMAX, YMAX);
	}

	protected override Vector2 GetOpponentCoordinates()
	{
		return Game.geroi.Position;
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
