using Godot;

namespace goofy_cave_game_godot.item.weapon.fist;

public partial class FistWeapon : Area2D
{
	private bool _attacking;
	private AttackData _currentAttack;
	
	private AttackData _lightAttack1;
	private AttackData _lightAttack2;
	private AttackData _lightAttack3;
	private AttackData _heavyAttack1;
	
	public override void _Ready()
	{
		Visible = false;

		_lightAttack1 = new AttackData
		(
			3,
			AttackData.DamageTypes.Bludgeon,
			0.2f,
			new Vector2(300, 0),
			new Vector2(-300, 0),
			"weapon_fist_lightattack1",
			1,
			0.07f
		);
		
		_lightAttack2 = new AttackData
		(
			3,
			AttackData.DamageTypes.Bludgeon,
			0.2f,
			new Vector2(300, 0),
			new Vector2(-300, 0),
			"weapon_fist_lightattack2",
			1,
			0.07f
		);
		
		_lightAttack3 = new AttackData
		(
			5,
			AttackData.DamageTypes.Bludgeon,
			0.3f,
			new Vector2(500, 0),
			new Vector2(-500, 0),
			"weapon_fist_lightattack3",
			-2,
			0.07f
		);
		
		_heavyAttack1 = new AttackData
		(
			10,
			AttackData.DamageTypes.Bludgeon,
			0.8f,
			new Vector2(800, 0),
			new Vector2(-1000, 0),
			"weapon_fist_heavyattack1",
			0,
			0
		);
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _PhysicsProcess(double delta)
	{
	}
	
	private void _OnBodyEntered(PhysicsBody2D body)
	{
		if (_attacking && !body.IsInGroup("player"))
		{
			GD.Print(body.Name + " hit");
		}
	}

	public AttackData LightAttack(Node2D attacker, Vector2 targetPos, int combo)
	{
		_attacking = true;
		
		return combo switch
		{
			0 => _lightAttack1,
			1 => _lightAttack2,
			2 => _lightAttack3,
			_ => _lightAttack1
		};
	}
	
	public AttackData HeavyAttack(Node2D attacker, Vector2 targetPos)
	{
		return _heavyAttack1;
	}
}