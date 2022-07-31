using Godot;
using System;

public class Player : KinematicBody2D
{

	[Export]
	string m_playerID = "";

	[Export]
    float m_movementSpeed = 30;
    [Export]
    float m_acceleration = 35;
    [Export]
    float m_frictionCoeficient = 20;
    Vector2 m_velocity = Vector2.Zero;

	private AnimatedSprite sprite;


    PackedScene bulletScene;


    void UpdateVelocity ( Vector2 directionalInput, float delta )
    {
        float friction = m_velocity.Length() * m_frictionCoeficient; // friction increases with speed
        m_velocity = m_velocity.MoveToward( Vector2.Zero, friction * delta );

        if ( directionalInput != Vector2.Zero )
        {
            m_velocity = m_velocity.MoveToward( directionalInput * m_movementSpeed, m_acceleration * delta );
        }
        return;
    }

	public override void _Ready ()
	{
		initiateAnimations();

        bulletScene = GD.Load<PackedScene>("res://Bullet.tscn");
	}

	public void initiateAnimations()
	{
		sprite = ((AnimatedSprite)GetNode("AnimatedSprite"));
		sprite.Play("Idle");
	}

    public override void _PhysicsProcess ( float delta )
    {
        PhysHelp.constrainDelta( ref delta );
        Vector2 directionalInput = Input.GetVector( m_playerID + "ui_left", m_playerID + "ui_right", m_playerID + "ui_up", m_playerID + "ui_down" );
		if (directionalInput == Vector2.Zero)
		{
			directionalInput = Input.GetVector( m_playerID + "ui_left_cr", m_playerID + "ui_right_cr", m_playerID + "ui_up_cr", m_playerID + "ui_down_cr" );
		}
        UpdateVelocity( directionalInput, delta );

        MoveAndCollide( m_velocity );

		float shoot = 0;
		shoot = Input.GetActionStrength(m_playerID + "ui_shoot");
		if (shoot == 0)
		{
			shoot = Input.GetActionStrength(m_playerID + "ui_shoot_cr");
		}
		else
		{
			shootBullet();
		}
    }

	private void shootBullet()
	{
		bulletLogic bullet = (bulletLogic)bulletScene.Instance();
		if (bullet.reloaded == true)
		{
		bullet.Position = Position;
		bullet.Rotation = Rotation;
		GetParent().AddChild(bullet);
		}

	}
}
