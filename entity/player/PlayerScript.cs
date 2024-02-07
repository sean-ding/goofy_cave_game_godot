using System.Collections.Generic;
using Godot;

namespace goofy_cave_game_godot.entity.player;

public partial class PlayerScript : CharacterBody2D
{
	[Export] public float Speed = 200.0f;
	[Export] public float LerpSpeed = 10.0f;
	
	public List<Node> EquippedList = new();

	private AnimationPlayer _animPlayer;
	private bool _attacking;
	private int _combo;
	
	public override void _Ready()
	{
		_animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		
		foreach (var limb in GetNode<Node2D>("Limbs").GetChildren())
		{
			if (limb.GetChildCount() != 0)
			{
				EquippedList.Add(limb.GetChild(0));
			}
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		var velocity = Vector2.Zero;

		var direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		if (direction != Vector2.Zero)
		{
			velocity = direction.Normalized() * Speed;
		}

		Velocity = Velocity.Lerp(velocity, (float)delta * LerpSpeed);
		MoveAndSlide();
	}
	
	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("click"))
		{
			AttackData attack;
			var mousePos = GetGlobalMousePosition();
			
			var weaponList = new List<Node>();
			
			foreach (var item in EquippedList)
			{
				if (item.HasMethod("Attack"))
				{
					weaponList.Add(item);
				}
			}
			foreach (var weapon in weaponList)
			{
				attack = (AttackData) weapon.Call("LightAttack", this, mousePos, _combo);
				_animPlayer.Play(attack.AttackAnim);
				Velocity += attack.Displacement;
				GD.Print("erm!");
			}
		}
	}
}