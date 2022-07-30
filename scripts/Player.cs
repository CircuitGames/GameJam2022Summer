using Godot;

public class Player : KinematicBody2D
{
    [Export]
    float m_movementSpeed = 400;
    [Export]
    float m_acceleration = 100;
    [Export]
    float m_frictionCoeficient = 8;
    Vector2 m_velocity = Vector2.Zero;

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

    public override void _Process ( float delta )
    {
        PhysHelp.constrainDelta( ref delta );
        Vector2 directionalInput = Input.GetVector( "ui_left", "ui_right", "ui_up", "ui_down" );
        UpdateVelocity( directionalInput, delta );

        MoveAndCollide( m_velocity );
    }
}