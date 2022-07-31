using System;
using Godot;



public class SwiftPlayer : KinematicBody2D
{
	[Export]
	string up = "ui_up";
	[Export]
	string down = "ui_down";
	[Export]
	string left = "ui_left";
	[Export]
	string right = "ui_right";
	
	[Export]
	float m_movement_speed = 750;
	[Export]
	float m_acceleration = 3000;
	[Export]
	float m_friction = 5000;

	public override void _PhysicsProcess ( float delta )
	{
		var directional_input = Vector2.Zero;
		directional_input.x = Input.GetActionStrength( up ) - Input.GetActionStrength( left );
		directional_input.y = Input.GetActionStrength( down ) - Input.GetActionStrength( right );

		Vector2 velocity = Vector2.Zero;
		if ( directional_input != Vector2.Zero )
		{
			velocity = velocity.MoveToward( directional_input * m_movement_speed, m_acceleration * delta );
		}
		else
		{
			velocity = velocity.MoveToward( Vector2.Zero, m_friction * delta );
		}

		MoveAndSlide( velocity );
	}
}
