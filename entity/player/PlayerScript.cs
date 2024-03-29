using System;
using System.Collections.Generic;
using Godot;
using Vector2 = Godot.Vector2;

namespace goofy_cave_game_godot.entity.player;

public partial class PlayerScript : CharacterBody2D
{
	[Export] public float Speed = 200.0f;
	[Export] public float LerpSpeed = 0.1f;
	[Export] public float Acceleration = 2000f;
	[Export] public float Deceleration = 2000f;
	
	public List<Node2D> EquippedList = new();
	
	private bool _attacking;
	private int _combo;
	private bool _comboWindow;
	private SceneTreeTimer _comboTimer;

	private Label _debugLabel;
	private Sprite2D _sprite;
	
	public override void _Ready()
	{
		_debugLabel = GetNode<Label>("../PlayerCamera/Label");
		_sprite = GetNode<Sprite2D>("SpriteContainer/Sprite2D");
		
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
		var direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		if (direction != Vector2.Zero)
		{
			Velocity = Velocity.MoveToward(direction.Normalized() * Speed, Acceleration * (float) delta);
		}
		else
		{
			Velocity = Velocity.MoveToward(Vector2.Zero, Deceleration * (float) delta);
		}
		
		MoveAndSlide();
	}

	public override void _Process(double delta)
	{
		_sprite.GlobalPosition = _sprite.GlobalPosition.Lerp(GlobalPosition, (float) delta * 100);
        
		
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("click"))
		{
			if (!_attacking || _comboWindow)
			{
				Attack();
			}
		}
	}

	private async void Attack()
	{
		_attacking = true;
		_comboWindow = false;
		_comboTimer?.Dispose();

		var weaponList = new List<Node2D>();

		var longestAttack = 0f;
		var totalTime = 0f;
		var shortestPreTime = -1f;
		var shortestPostTime = -1f;
				
		foreach (var item in EquippedList)
		{
			if (item.HasMethod("LightAttack"))
			{
				var attack = (AttackData) item.Call("LightAttack", _combo);
				if (attack.AttackTime > longestAttack)
				{
					longestAttack = attack.AttackTime;
				}

				totalTime += attack.AttackTime;
				weaponList.Add(item);
			}
		}

		var timeScale = (float) (totalTime / longestAttack * Math.Pow(Math.Log10(weaponList.Count) * -0.35f + 1, 2));
		_debugLabel.Text = "Weapon Count: " + weaponList.Count + "\nTime Scale: " + timeScale + "\nTotal Time: " + totalTime + "\nLongest Attack: " + longestAttack + "\nMultiplier: " + Math.Pow(Math.Log10(weaponList.Count) * -0.35f + 1, 2);
		foreach (var weapon in weaponList)
		{
			weapon.GetParent<Node2D>().LookAt(GetGlobalMousePosition());
			var attack = (AttackData) weapon.Call("LightAttack", _combo);
			weapon.Call("SetAttacking", true);
			if ((attack.ComboPreTime < shortestPreTime && attack.ComboPreTime != 0) || shortestPreTime < 0)
			{
				shortestPreTime = attack.ComboPreTime;
			}
			if ((attack.ComboPostTime < shortestPostTime && attack.ComboPostTime != 0) || shortestPostTime < 0)
			{
				shortestPostTime = attack.ComboPostTime;
			}
			var weaponAnimPlayer = (AnimationPlayer) weapon.Call("GetAnimPlayer");
			weaponAnimPlayer.Play(attack.AttackAnim, -1, timeScale);
			Velocity = weapon.GetParent<Node2D>().GetGlobalTransform().BasisXform(attack.Displacement);
			await ToSignal(weaponAnimPlayer, "animation_finished");
			weapon.Call("SetAttacking", false);
			_combo += attack.Combo;
		}

		_comboWindow = true;
		_comboTimer = GetTree().CreateTimer(shortestPostTime);
		_comboTimer.Timeout += () =>
		{
			if (_attacking) return;
			_comboWindow = false;
			_combo = 0;
		};
		
		_attacking = false;
	}
}