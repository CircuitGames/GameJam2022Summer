using System;
using Godot;

public class Player : KinematicBody2D
{

    [Export]
    string m_playerID = "";

    [Export]
    float m_bulletCooldownTime = 0.125f;
    float m_currentBulletCooldown = 0;

    [Export]
    float m_movementSpeed = 30;
    [Export]
    float m_acceleration = 35;
    [Export]
    float m_frictionCoeficient = 20;
    Vector2 m_velocity = Vector2.Zero;
    Pause pauseScreen = null;
    Node2D parent = null;

    private AnimatedSprite sprite;


    PackedScene bulletScene;


    void UpdateVelocity( Vector2 directionalInput, float delta )
    {
        float friction = m_velocity.Length() * m_frictionCoeficient; // friction increases with speed
        m_velocity = m_velocity.MoveToward( Vector2.Zero, friction * delta );

        if( directionalInput != Vector2.Zero )
        {
            m_velocity = m_velocity.MoveToward( directionalInput * m_movementSpeed, m_acceleration * delta );
        }
        return;
    }

    public override void _Ready()
    {
        initiateAnimations();
        parent = (Node2D)GetParent();
        pauseScreen = (Pause)parent.GetNode<Control>("Pause Screen");
        bulletScene = GD.Load<PackedScene>( "res://Assets/Prefabs/Bullet.tscn" );
    }

    public void initiateAnimations()
    {
        sprite = GetNode<AnimatedSprite>( "AnimatedSprite" );
        sprite.Playing = true;
    }

    public override void _PhysicsProcess( float delta )
    {
        PhysHelp.constrainDelta( ref delta );
        Vector2 directionalInput = Input.GetVector( m_playerID + "ui_left", m_playerID + "ui_right", m_playerID + "ui_up", m_playerID + "ui_down" );
        if( directionalInput == Vector2.Zero )
        {
            directionalInput = Input.GetVector( m_playerID + "ui_left_cr", m_playerID + "ui_right_cr", m_playerID + "ui_up_cr", m_playerID + "ui_down_cr" );
        }
        UpdateVelocity( directionalInput, delta );

        MoveAndCollide( m_velocity );

        if( m_currentBulletCooldown <= 0 )
        {
            if( Input.IsActionPressed( m_playerID + "ui_shoot" ) || Input.IsActionPressed( m_playerID + "ui_shoot_cr" ) )
            {
                m_currentBulletCooldown = m_bulletCooldownTime;
                shootBullet();
            }
        }
        else
        {
            m_currentBulletCooldown -= delta;
        }
        if (Input.IsActionPressed("ui_pause"))
        {
            pauseScreen.onPauseButtonPressed();
        }
    }

    private void shootBullet()
    {
        bulletLogic bullet = bulletScene.Instance<bulletLogic>();
        if( bullet.reloaded == true )
        {
            bullet.Position = Position + ( 25 * Vector2.Up );
            bullet.Rotation = Rotation;
            GetParent().AddChild( bullet );
        }

    }
}
