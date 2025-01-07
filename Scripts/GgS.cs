using Godot;

public partial class GgS : Object
{
	private Controller controller;

	public override void _Ready()
	{
		Game.geroi = this;
		controller = new DangerZoneGeroiController(0, 0, XMAX, YMAX);
	}

	protected override Vector2 GetOpponentCoordinates()
	{
		return Game.vrag.Position;
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
