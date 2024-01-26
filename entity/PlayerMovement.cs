using Godot;
using System;

public partial class PlayerMovement : CharacterBody2D
{
	[Export]
	public NodePath SpritePath; 
	
	public const float Speed = 30.0f;
	
	private Transform2D _lastTransform;
	private Transform2D _currentTransform;

	public override void _Ready()
	{
		
	}

	public override void _PhysicsProcess(double delta)
	{
		var velocity = Velocity;

		var direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		if (direction != Vector2.Zero)
		{
			velocity = direction;
		}
		else
		{
			velocity = Vector2.Zero;
		}

		Velocity = velocity;
		MoveAndCollide(velocity);
	}
}
