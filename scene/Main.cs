using Godot;

namespace goofy_cave_game_godot.scene;

public partial class Main : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetWindow().Size = new Vector2I(1920, 1080);
		GetWindow().Mode = Window.ModeEnum.Maximized;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}