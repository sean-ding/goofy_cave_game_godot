using Godot;

namespace goofy_cave_game_godot.entity.swarmer;

public partial class SwarmerScript : CharacterBody2D
{
	[Export] public float Speed = 100.0f;
	[Export] public float Acceleration = 2000f;
	[Export] public float Deceleration = 2000f;
	
	private Sprite2D _sprite;
	private CharacterBody2D _player;
	private NavigationAgent2D _navigationAgent;

	public Vector2 MovementTarget
	{
		get { return _navigationAgent.TargetPosition; }
		set { _navigationAgent.TargetPosition = value; }
	}

	public override void _Ready()
	{
		base._Ready();
		
		_sprite = GetNode<Sprite2D>("SpriteContainer/Sprite2D");
		_player = GetNode<CharacterBody2D>("../Player");
		_navigationAgent = GetNode<NavigationAgent2D>("NavigationAgent2D");

		// These values need to be adjusted for the actor's speed
		// and the navigation layout.
		_navigationAgent.PathDesiredDistance = 4.0f;
		_navigationAgent.TargetDesiredDistance = 4.0f;
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		MovementTarget = _player.GlobalPosition;

		var currentAgentPosition = GlobalTransform.Origin;
		var nextPathPosition = _navigationAgent.GetNextPathPosition();

		Velocity = currentAgentPosition.DirectionTo(nextPathPosition) * Speed;
		MoveAndSlide();
	}
	
	public override void _Process(double delta)
	{
		_sprite.GlobalPosition = _sprite.GlobalPosition.Lerp(GlobalPosition, (float) delta * 80);
	}
}