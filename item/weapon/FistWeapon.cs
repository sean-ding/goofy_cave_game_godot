using Godot;

namespace goofy_cave_game_godot.item.weapon;

public partial class FistWeapon : Area2D
{
	[Export] public float Speed = 1000.0f;
	
	private bool _attacking;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Visible = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _PhysicsProcess(double delta)
	{
	}

	public void Attack(Node2D attacker, Vector2 targetPos, int speed)
	{
	}
}