using Godot;
using System;

public partial class SwarmerScript : CharacterBody2D
{
	[Export] public float Speed = 200.0f;
	[Export] public float LerpSpeed = 0.1f;
	[Export] public float Acceleration = 2000f;
	[Export] public float Deceleration = 2000f;
	
	private Sprite2D _sprite;
	public override void _Ready()
	{
		_sprite = GetNode<Sprite2D>("SpriteContainer/Sprite2D");
	}

	public override void _PhysicsProcess(double delta)
	{
		
		
		Velocity = Velocity.MoveToward(direction.Normalized() * Speed, Acceleration * (float) delta);
		
		MoveAndSlide();
	}

	public override void _Process(double delta)
	{
		_sprite.GlobalPosition = _sprite.GlobalPosition.Lerp(GlobalPosition, (float) delta * 100);
	}
}
