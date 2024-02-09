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
	private bool _comboWindow;
	private SceneTreeTimer _comboTimer;

	private Label _debugLabel;
	
	public override void _Ready()
	{
		_debugLabel = GetNode<Label>("../PlayerCamera/Label");
		
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

	public override void _Process(double delta)
	{
		_debugLabel.Text = "Combo Number: " + _combo + "\nCan Combo: " + _comboWindow;
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
				
		var mousePos = GetGlobalMousePosition();
		var weaponList = new List<Node2D>();

		var shortestPreTime = -1f;
		var shortestPostTime = -1f;
				
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
			if (attack.ComboPreTime < shortestPreTime || shortestPreTime < 0)
			{
				shortestPreTime = attack.ComboPreTime;
			}
			if (attack.ComboPostTime < shortestPostTime || shortestPostTime < 0)
			{
				shortestPostTime = attack.ComboPostTime;
			}
			weapon.GetParent<Node2D>().LookAt(mousePos);
			_animPlayer.Play(attack.AttackAnim, -1, weaponList.Count);
			Velocity = weapon.GetParent<Node2D>().GetGlobalTransform().BasisXform(attack.Displacement);
			await ToSignal(_animPlayer, "animation_finished");
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