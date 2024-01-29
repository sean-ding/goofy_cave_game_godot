using Godot;

namespace goofy_cave_game_godot.entity.player;

public partial class PlayerSprite : Sprite2D
{
	private CharacterBody2D _player;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_player = GetNode<CharacterBody2D>("..");
		TopLevel = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var roundedPos = (_player.Position * 10).Round() * 0.1f;
		GlobalPosition = roundedPos;
	}
}