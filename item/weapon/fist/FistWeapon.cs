using Godot;

namespace goofy_cave_game_godot.item.weapon.fist;

public partial class FistWeapon : Area2D
{
	private bool _attacking;
	private Node2D _limb;
	private AttackData _currentAttack;
	
	private AttackData _lightAttack1;
	private AttackData _lightAttack2;
	private AttackData _lightAttack3;
	private AttackData _heavyAttack1;
	
	public override void _Ready()
	{
		_limb = GetNode<Node2D>("..");
		Visible = false;

		_lightAttack1 = new AttackData
		(
			3, 
			AttackData.DamageTypes.Bludgeon, 
			0.2f, 
			new Vector2(0.5f, 0), 
			"weapon_fist_lightattack1"
		);
		
		_lightAttack2 = new AttackData
		(
			3, 
			AttackData.DamageTypes.Bludgeon, 
			0.2f, 
			new Vector2(0.5f, 0), 
			"weapon_fist_lightattack2"
		);
		
		_lightAttack3 = new AttackData
		(
			5, 
			AttackData.DamageTypes.Bludgeon, 
			0.3f, 
			new Vector2(0.8f, 0), 
			"weapon_fist_lightattack3"
		);
		
		_heavyAttack1 = new AttackData
		(
			10, 
			AttackData.DamageTypes.Bludgeon, 
			0.8f, 
			new Vector2(0.8f, 0), 
			"weapon_fist_heavyattack1"
		);
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _PhysicsProcess(double delta)
	{
	}

	public AttackData LightAttack(Node2D attacker, Vector2 targetPos, int combo)
	{
		_limb.LookAt(targetPos);

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
		_limb.LookAt(targetPos);

		return _heavyAttack1;
	}
}