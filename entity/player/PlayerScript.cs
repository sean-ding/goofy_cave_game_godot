using Godot;

namespace goofy_cave_game_godot.entity.player;

public partial class PlayerScript : CharacterBody2D
{
	[Export] public float Speed = 200.0f;
	[Export] public float LerpSpeed = 10.0f;

	public override void _Ready()
	{
		
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity;

		var direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		if (direction != Vector2.Zero)
		{
			velocity = direction.Normalized() * Speed;
		}
		else
		{
			velocity = Vector2.Zero;
		}

		Velocity = Velocity.Lerp(velocity, (float) delta * LerpSpeed);
		MoveAndSlide();
	}
}