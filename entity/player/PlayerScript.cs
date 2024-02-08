using System.Collections.Generic;
using System.Numerics;
using Godot;
using Vector2 = Godot.Vector2;

namespace goofy_cave_game_godot.entity.player;

public partial class PlayerScript : CharacterBody2D
{
	[Export] public float Speed = 200.0f;
	[Export] public float LerpSpeed = 10.0f;
	
	public List<Node2D> EquippedList = new();

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
				EquippedList.Add(limb.GetChild<Node2D>(0));
				GD.Print(limb.Name);
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
			var mousePos = GetGlobalMousePosition();
			
			var weaponList = new List<Node2D>();
			
			foreach (var item in EquippedList)
			{
				if (item.HasMethod("LightAttack"))
				{
					weaponList.Add(item);
				}
			}
			foreach (var weapon in weaponList)
			{
				var attack = (AttackData) weapon.Call("LightAttack", this, mousePos, _combo);
				weapon.GetParent<Node2D>().LookAt(mousePos);
				_animPlayer.Play(attack.AttackAnim);
				Velocity = weapon.GetParent<Node2D>().GetGlobalTransform().BasisXform(attack.Displacement);
				_combo += attack.Combo;
			}
		}
	}
}