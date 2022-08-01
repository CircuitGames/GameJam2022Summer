using System;
using Godot;

public class bulletLogic : Node2D
{
    [Export]
    float speed = 1000;
    private AnimatedSprite sprite;

    Area2D area2DNode;

    public bool reloaded = true;

    Timer cooldown;


    public override void _Ready()
    {
        initiateAnimations();
        reloaded = false;

        cooldown = GetNode<Timer>( "Timer" );
        cooldown.Connect( "timeout", this, "onTimerTimeout" );

        area2DNode = GetNode<Area2D>( "Area2D" );
        area2DNode.Connect( "area_entered", this, "OnBulletBodyEntered" );
        area2DNode.Connect( "body_entered", this, "OnBulletBodyEntered" );


    }

    public override void _PhysicsProcess( float delta )
    {
        PhysHelp.constrainDelta( ref delta );
        float moveAmount = speed * delta;

        //move the object straight up
        Position -= Vector2.Down * moveAmount;
    }

    //delete the bullet upon collision with a bounding box or player
    public void OnBulletBodyEntered( PhysicsBody2D body )
    {
        QueueFree();
    }

    public void initiateAnimations()
    {
        //finds the child sprite of this node
        sprite = GetNode<AnimatedSprite>( "AnimatedSprite" );
        //instantiates idle animation loop
        sprite.Play( "default" );
    }

    public void onTimerTimeout()
    {
        reloaded = true;
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //
    //  }
}
