using System;
using Godot;

public class Enemy : KinematicBody2D
{

    [Export]
    float m_bulletCooldownTime = 0.125f;
    float m_currentBulletCooldown = 0;

    [Export]
    float m_movementSpeed = 30;
    Vector2 m_velocity = Vector2.Up;

    private AnimatedSprite sprite;


    PackedScene bulletScene;



    public override void _Ready()
    {
        initiateAnimations();

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

        MoveAndCollide( m_velocity * delta );
    }

}
