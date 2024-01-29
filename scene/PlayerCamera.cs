using Godot;

namespace goofy_cave_game_godot.scene;

public partial class PlayerCamera : Camera2D
{
	[Export] public float FollowSpeed = 8.0f;
	
	private Sprite2D _playerSprite;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_playerSprite = GetNode<Sprite2D>("../Player/Sprite2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GlobalPosition = GlobalPosition.Lerp(_playerSprite.GlobalPosition, (float) delta * FollowSpeed);
	}
}