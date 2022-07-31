using Godot;
using System;

public class PlayerOld : KinematicBody2D
{
	[Export]
	float m_speed = 250;

	public Vector2 GetInput()
	{
		// Detect up/down/left/right keystate and only move when pressed
		Vector2 velocity = Vector2.Zero;

		if (Input.IsActionPressed("ui_right"))
			velocity.x += 1;

		if (Input.IsActionPressed("ui_left"))
			velocity.x -= 1;

		if (Input.IsActionPressed("ui_down"))
			velocity.y += 1;

		if (Input.IsActionPressed("ui_up"))
			velocity.y -= 1;
		velocity = velocity.Normalized() * m_speed;
		return velocity;
	}

	public override void _PhysicsProcess(float delta)
	{
		Vector2 velocity = GetInput();
		MoveAndCollide(velocity * delta);
	}
}
